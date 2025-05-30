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
        public event Action UpdatedAction;
        private readonly FormMain _formMain;
        private readonly AccessUserControl _accessControl;
        private AppShowerModel showerModel;
        private readonly string zipPassword;
        internal Uploader(FormMain formMain, LocationManagement locationManagement, AccessUserControl accessControl)
        {
            this._formMain = formMain;
            this.zipPassword = ConstKey.ZIP_PASSWORD;
            this._treeVersion = new MyTreeFolder(formMain.TreeVersion, zipPassword);
            this._accessControl = accessControl;
            locationManagement.ShowVerionAction += (v) =>
            {
                ResetTree();
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
        }

        private async Task UpLoad(LocationManagement locationManagement)
        {
            try
            {
                SetLockFor(true, Reasons.LOCK_UPDATE);
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                if (await UpdateAppModel())
                {
                    try
                    {
                        ////////////////////////////////
                        var appModel = showerModel.AppModel;
                        if(await TranforUtil.UploadFile(appModel.FileModels, zipPassword))
                        {
                            appModel.FileModels = FilterFileModelClass(appModel.FileModels);
                            //////////////////////////////
                            if (await TranforUtil.UploadModel(appModel, appModel.Path, zipPassword))
                            {
                                List<FileModel> canDeletes = await GetCanDeleteFileModelsAsync(showerModel, appModel.RemoteAppListPath);
                                await TranforUtil.RemoveRemoteFile(canDeletes);
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
                ResetTree();
                this._accessControl.Clear();
                return;
            }
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            this._accessControl.LoadFormPath(programDataPath.AccectUserPath);
            _ = Show(programDataPath.AppPath, _cts);
        }

        private async Task Show(string programDataPath, CancellationTokenSource cts)
        {
            AppModel appModel = await TranforUtil.GetModelConfig<AppModel>(programDataPath, zipPassword);
            if (appModel == null)
            {
                ResetTree();
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

        private void ResetTree()
        {
            this._formMain.ClearData();
            this._treeVersion.Clear();
        }

        private async Task<List<FileModel>> GetCanDeleteFileModelsAsync(AppShowerModel showerModel, string appConfigPath)
        {
            AppList appList = await TranforUtil.GetModelConfig<AppList>(appConfigPath, zipPassword);
            List<FileModel> canDeleteFiles = new List<FileModel>( showerModel.RemoveFileModel);
            var fileModelUsed = new Dictionary<string, FileModel>();
            AppModel appModel = null;
            foreach (var appInfo in appList.ProgramPaths)
            {
                appModel = await TranforUtil.GetModelConfig<AppModel>(appInfo.Value.AppPath, zipPassword);
                var files = appModel?.FileModels?.GroupBy( f => f.Md5).ToDictionary(g => g.Key, g => new HashSet<string>( g.Select( f => f.ProgramPath)));
                canDeleteFiles = canDeleteFiles.Where(f =>  !files.ContainsKey(f.Md5) ).ToList();
                if (canDeleteFiles.Count == 0) break;
            }
            return canDeleteFiles;
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
            if (showerModel == null)
            {
                return false;
            }
            var appModel = showerModel.AppModel;
            var path = appModel.RemoteStoreDir;
            if (appModel == null || path == null)
            {
                return false;
            }
            appModel.OpenCmd = _formMain.TxtOpenCmd.Text;
            appModel.CloseCmd = _formMain.TxtCloseCmd.Text;
            appModel.MainPath = _formMain.TxtMainFile.Text;
            appModel.WindowTitle = _formMain.TxtWindowName.Text;
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
    }
}
