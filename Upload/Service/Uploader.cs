using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Upload.Model;
using AutoDownload.Gui;
using Upload.Common;
using AutoDownload.Model;
using System.Linq;
using static Upload.Service.LockManager;
using System.Threading;
using Upload.ModelView;
using Upload.gui;

namespace Upload.Service
{
    internal partial class Uploader
    {
        private readonly MyTreeFolder _treeVersion;
        private readonly FormMain _formMain;
        private readonly AccessUserControl _accessControl;
        private AppShowerModel showerModel;
        private readonly string zipPassword;
        private readonly CheckConditon checkConditon;
        internal Uploader(FormMain formMain, LocationManagement locationManagement, AccessUserControl accessControl)
        {
            this._formMain = formMain;
            this.zipPassword = ConstKey.ZIP_PASSWORD;
            this._treeVersion = new MyTreeFolder(formMain.TreeVersion, zipPassword);
            this.checkConditon = new CheckConditon(formMain);
            this._accessControl = accessControl;
            locationManagement.ShowVerionAction += (v) =>
            {
                ResetData();
                if (v == null)
                {
                    return;
                }
                ShowAppModel(v);
            };
            this._treeVersion.OnChosseMainFile += this._formMain.OnChosseMainFile;
            this._treeVersion.OnChosseRunFile += this._formMain.OnChosseRunFile;
            this._treeVersion.OnChosseCloseFile += this._formMain.OnChosseCloseFile;
            this._formMain.BtUpdate.Click += (s,e) =>
            {
                _= UpLoad(locationManagement);
            };
            InitCheckCondition();
        }

        private void InitCheckCondition()
        {
            this.checkConditon.AddElems("BOM version", _formMain.TxtBOMVersion);
            this.checkConditon.AddElems("FCD version", _formMain.TxtFCDVersion);
            this.checkConditon.AddElems("FTU version", _formMain.TxtFTUVersion);
            this.checkConditon.AddElems("FW version", _formMain.TxtFWVersion);
            this.checkConditon.AddElems("Open command", _formMain.TxtOpenCmd);
            this.checkConditon.AddElems("Main file", _formMain.TxtMainFile);
            this.checkConditon.AddElems("Version", _formMain.TxtVersion);
        }

        private async Task UpLoad(LocationManagement locationManagement)
        {
            try
            {
                SetLockFor(true, Reasons.LOCK_UPDATE);
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                if (this._accessControl.HasChanged)
                {
                    this._accessControl.UpdateData();
                }
                if (await UpdateAppModel())
                {
                    try
                    {
                        ////////////////////////////////
                        var appModel = showerModel.AppModel;
                        if(await TranferUtil.UploadFile(appModel.FileModels, zipPassword))
                        {
                            appModel.FileModels = FilterFileModelClass(appModel.FileModels);
                            //////////////////////////////
                            if (await TranferUtil.UploadModel(appModel, appModel.Path, zipPassword))
                            {
                                AppList appList = await TranferUtil.GetModelConfig<AppList>(appModel.RemoteAppListPath, zipPassword);
                                List<FileModel> canDeletes = await TranferUtil.GetCanDeleteFileModelsAsync(appModel.Path, showerModel.RemoveFileModel, appList, zipPassword);
                                await TranferUtil.RemoveRemoteFile(canDeletes);
                                await locationManagement.UpdateProgramListItems();
                                LoggerBox.Addlog("Hoàn thành cập nhập");
                                return;
                            }
                        }
                        LoggerBox.Addlog($"Cập nhập thất bại");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi:{ex.Message}");
                        LoggerBox.Addlog($"Cập nhập thất bại:{ex.Message}");
                    }
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
                SetLockFor(false, Reasons.LOCK_UPDATE);
            }
        }
        private CancellationTokenSource _cts;
        private void ShowAppModel(ProgramPathModel programDataPath)
        {
            if (programDataPath == null)
            {
                ResetData();
                return;
            }
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            this._accessControl.LoadFormPath(programDataPath.AccectUserPath);
            _ = Show(programDataPath.AppPath, _cts);
        }

        private async Task Show(string programDataPath, CancellationTokenSource cts)
        {
            AppModel appModel = await TranferUtil.GetModelConfig<AppModel>(programDataPath, zipPassword);
            if (appModel == null)
            {
                ResetProgramData();
                SetLockFor(true, Reasons.LOCK_INPUT);
            }
            else
            {
                if (cts.Token.IsCancellationRequested)
                {
                    return;
                }
                LockManager.Instance.SetLock(false, Reasons.LOCK_INPUT);
                this.showerModel = new AppShowerModel(appModel);
                this._formMain.SetData(appModel);
                if (appModel?.FileModels != null)
                {
                    this._treeVersion.StartPopulate(appModel.FileModels, appModel.RemoteStoreDir, _cts);
                }
            }
        }

        private void ResetProgramData()
        {
            this._formMain.ClearData();
            this._treeVersion.Clear();
        }
        private void ResetData()
        {
            ResetProgramData();
            _accessControl.Clear();
        }

        private HashSet<FileModel> FilterFileModelClass(ICollection<FileModel> fileModels)
        {
            HashSet<FileModel> rs = new HashSet<FileModel>();
            if (fileModels != null)
            {
                foreach (var fileModel in fileModels)
                {
                    if (fileModel is StoreFileModel storeFileModel)
                    {
                        rs.Add(storeFileModel.FileModel());
                    }
                    else
                    {
                        rs.Add(fileModel);
                    }
                }
            }
            return rs;
        }

        private async Task<bool> UpdateAppModel()
        {
            if (showerModel == null )
            {
                return false;
            }
            var appModel = showerModel.AppModel;
            var path = appModel.RemoteStoreDir;
            if (appModel == null || path == null || !checkConditon.IsOk())
            {
                return false;
            }
            appModel.OpenCmd = _formMain.TxtOpenCmd.Text;
            appModel.CloseCmd = _formMain.TxtCloseCmd.Text;
            appModel.MainPath = _formMain.TxtMainFile.Text;
            appModel.Version = _formMain.TxtVersion.Text;
            appModel.BOMVersion = _formMain.TxtBOMVersion.Text;
            appModel.FCDVersion = _formMain.TxtFCDVersion.Text;
            appModel.FTUVersion = _formMain.TxtFTUVersion.Text;
            appModel.FWSersion = _formMain.TxtFWVersion.Text;
            appModel.Enable = _formMain.CbEnabled.Checked;
            appModel.AutoOpen = _formMain.CbAutoOpen.Checked;
            appModel.AutoRemove = _formMain.CbAutoRemove.Checked;
            appModel.AutoUpdate = _formMain.CbAutoUpdate.Checked;
            appModel.CloseAndClear = _formMain.CbCloseAndClear.Checked;
            showerModel.RemoveFileModel.Clear();
            //////////////
            List<FileModel> appFileModel = await this._treeVersion.GetAllLeafNodes();
            appModel.FileModels = new HashSet<FileModel>(appFileModel);
            showerModel.RemoveFileModel.AddRange(this._treeVersion.RemoveFileModel);
            return true;
        }

        private class CheckConditon
        {
            private readonly Dictionary<string, TextBox> textBoxs = new Dictionary<string, TextBox>();
            private readonly Form _own;
            public CheckConditon(Form own)
            {
                _own = own;
            }
            public void AddElems(string name, TextBox textBox)
            {
                if(!textBoxs.ContainsKey(name)){
                    textBoxs.Add(name, textBox);
                }
            }

            public void RemoveElems(string name)
            {
                textBoxs.Remove(name);
            }

            public bool IsOk()
            {
                bool ok = true;
                foreach(var textBoxElem in textBoxs)
                {
                    if (string.IsNullOrEmpty(textBoxElem.Value.Text))
                    {
                        Util.SafeInvoke(_own, () =>
                        {
                            MessageBox.Show(_own,$"{textBoxElem.Key} không được để rỗng!");
                        });
                        ok = false;
                    }
                }
                return ok;
            }
        }
    }
}
