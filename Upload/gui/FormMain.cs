using System;
using System.Windows.Forms;
using AutoDownload.Gui;
using Upload.Service;
using Upload.gui;
using Upload.Common;
using Upload.Model;
using AutoDownload.Model;
using Path = System.IO.Path;
using Upload.Config;
using static Upload.Service.LockManager;

namespace Upload
{
    public partial class FormMain : Form
    {
        private readonly LocationManagement _locationManagement;
        private readonly LockActionCallBack _lockAccessUserViewCallBack = new LockActionCallBack();
        private readonly AccessUserControl _accessControl = new AccessUserControl();
        private readonly AccessUserListForm _stationAccessUserForm = new AccessUserListForm();
        private bool _isLockPasswork = true;
        public FormMain()
        {
            InitializeComponent();
            Text = $"{ProgramInfo.ProductName} - V{ProgramInfo.ProductVersion}";
            _locationManagement = new LocationManagement(this);
            new Uploader(this, _locationManagement, _accessControl);
            _lockAccessUserViewCallBack.lockCallBack = () => { _accessControl.Lock(); };
            _lockAccessUserViewCallBack.UnlockCallBack = () => { _accessControl.UnLock(); };
            CursorUtil.Instance.FormMain=this;
            LoggerBox.TxtMessager = txtMassage;
            InitLockLocation();
            InitLockInput(); 
            InitLockCreateBt();
            InitLockUpdate();
            InitLockPassword();
            InitLockBtUpdate();
            InitLockAccessUserUpdate();
            SetLockFor(true, Reasons.LOCK_PASSWORD);
            pnAccessUser.Controls.Add(_accessControl);
        }

        internal void ClearData()
        {
            UpdateTxt(txtOpenCmd, "");
            UpdateTxt(txtCloseCmd, "");
            UpdateTxt(txtMainFile, "");
            UpdateTxt(txtVersion, "");
            UpdateTxt(txtFWVersion, "");
            UpdateTxt(txtFCDVersion, "");
            UpdateTxt(txtBOMVersion, "");
            UpdateTxt(txtFTUVersion, "");
            UpdateCheckBox(cbEnabled, false);
            UpdateCheckBox(cbAutoOpen, false);
            UpdateCheckBox(cbAutoRemove, false);
            UpdateCheckBox(cbAutoUpdate, false);
            UpdateCheckBox(cbCloseAndClear, false);
        }

        internal void SetData(AppModel appModel)
        {
            UpdateTxt(txtOpenCmd, appModel.OpenCmd);
            UpdateTxt(txtCloseCmd, appModel.CloseCmd);
            UpdateTxt(txtMainFile, appModel.MainPath);
            UpdateTxt(txtVersion, appModel.WindowTitle);
            UpdateTxt(txtBOMVersion, appModel.BOMVersion);
            UpdateTxt(txtFCDVersion, appModel.FCDVersion);
            UpdateTxt(txtFTUVersion, appModel.FTUVersion);
            UpdateTxt(txtFWVersion, appModel.FWSersion);
            UpdateCheckBox(cbEnabled, appModel.Enable);
            UpdateCheckBox(cbAutoOpen, appModel.AutoOpen);
            UpdateCheckBox(cbAutoRemove, appModel.AutoRemove);
            UpdateCheckBox(cbAutoUpdate, appModel.AutoUpdate);
            UpdateCheckBox(cbCloseAndClear, appModel.CloseAndClear);
        }
        private void UpdateTxt(TextBox textBox, string value)
        {
            Util.SafeInvoke(textBox, () =>
            {
                textBox.Text = value;
            });
        }
        private void UpdateCheckBox(CheckBox cb, bool value)
        {
            Util.SafeInvoke(cb, () =>
            {
                cb.Checked = value;
            });
        }

        private void btSetting_Click(object sender, EventArgs e)
        {
            if (!_isLockPasswork)
            {
                LockManager.SetLockFor(true, Reasons.LOCK_PASSWORD);
                _isLockPasswork = true;
                return;
            }
            string inputPw = InputForm.GetInputPassword("Mật khẩu");
            string password = AutoDLConfig.ConfigModel.Password;
            if (!string.IsNullOrWhiteSpace(inputPw) && Util.GetMD5HashFromString(inputPw).Equals(password))
            {
                LockManager.SetLockFor(false, Reasons.LOCK_PASSWORD);
                _isLockPasswork = false;
                LoggerBox.Addlog("Mật khẩu đúng");
            }
            else
            {
                LockManager.SetLockFor(true, Reasons.LOCK_PASSWORD);
                LoggerBox.Addlog("Mật khẩu không đúng!");
                _isLockPasswork = true;
            }
        }
        private void InitLockLocation()
        {
            AddToGroup(Reasons.LOCK_LOCATION, cbbProduct);
            AddToGroup(Reasons.LOCK_LOCATION, CbbStation);
            AddToGroup(Reasons.LOCK_LOCATION, CbbProgram);
        }
        private void InitLockInput()
        {
            AddToGroup(Reasons.LOCK_INPUT, txtCloseCmd);
            AddToGroup(Reasons.LOCK_INPUT, txtOpenCmd);
            AddToGroup(Reasons.LOCK_INPUT, txtMainFile);
            AddToGroup(Reasons.LOCK_INPUT, txtVersion);
            AddToGroup(Reasons.LOCK_INPUT, txtBOMVersion);
            AddToGroup(Reasons.LOCK_INPUT, txtFCDVersion);
            AddToGroup(Reasons.LOCK_INPUT, txtFWVersion);
            AddToGroup(Reasons.LOCK_INPUT, txtFTUVersion);
            AddToGroup(Reasons.LOCK_INPUT, cbEnabled);
            AddToGroup(Reasons.LOCK_INPUT, cbAutoOpen);
            AddToGroup(Reasons.LOCK_INPUT, cbAutoRemove);
            AddToGroup(Reasons.LOCK_INPUT, cbAutoUpdate);
            AddToGroup(Reasons.LOCK_INPUT, cbCloseAndClear);
            AddToGroup(Reasons.LOCK_INPUT, treeFolder);
            AddToGroup(Reasons.LOCK_INPUT, btUpdate);
        }
        private void InitLockCreateBt()
        {
            AddToGroup(Reasons.LOCK_CREATE_BT, btCreateProduct);
            AddToGroup(Reasons.LOCK_CREATE_BT, btDeleteProduct);
            AddToGroup(Reasons.LOCK_CREATE_BT, btCreateStation);
            AddToGroup(Reasons.LOCK_CREATE_BT, btDeleteStation);
            AddToGroup(Reasons.LOCK_CREATE_BT, btCreateProgram);
            AddToGroup(Reasons.LOCK_CREATE_BT, btDeleteProgram);
        }
        private void InitLockUpdate()
        {
            AddToGroupFor(Reasons.LOCK_UPDATE, GetGroupControls(Reasons.LOCK_CREATE_BT));
            AddToGroupFor(Reasons.LOCK_UPDATE, GetGroupControls(Reasons.LOCK_INPUT));
            AddToGroupFor(Reasons.LOCK_UPDATE, GetGroupControls(Reasons.LOCK_LOCATION));
        }
        private void InitLockPassword()
        {
            AddToGroup(Reasons.LOCK_PASSWORD, btCreateProduct);
            AddToGroup(Reasons.LOCK_PASSWORD, btDeleteProduct);
            AddToGroup(Reasons.LOCK_PASSWORD, btCreateStation);
            AddToGroup(Reasons.LOCK_PASSWORD, btDeleteStation);
            AddToGroup(Reasons.LOCK_PASSWORD, btCreateProgram);
            AddToGroup(Reasons.LOCK_PASSWORD, btDeleteProgram);
            AddToGroup(Reasons.LOCK_PASSWORD, btAccessUser);
            AddToGroup(Reasons.LOCK_PASSWORD, btUpdate);
            AddToGroup(Reasons.LOCK_PASSWORD, txtCloseCmd);
            AddToGroup(Reasons.LOCK_PASSWORD, txtOpenCmd);
            AddToGroup(Reasons.LOCK_PASSWORD, txtMainFile);
            AddToGroup(Reasons.LOCK_PASSWORD, txtVersion);
            AddToGroup(Reasons.LOCK_PASSWORD, txtBOMVersion);
            AddToGroup(Reasons.LOCK_PASSWORD, txtFCDVersion);
            AddToGroup(Reasons.LOCK_PASSWORD, txtFWVersion);
            AddToGroup(Reasons.LOCK_PASSWORD, txtFTUVersion);
            AddToGroup(Reasons.LOCK_PASSWORD, cbEnabled);
            AddToGroup(Reasons.LOCK_PASSWORD, cbAutoOpen);
            AddToGroup(Reasons.LOCK_PASSWORD, cbAutoRemove);
            AddToGroup(Reasons.LOCK_PASSWORD, cbAutoUpdate);
            AddToGroup(Reasons.LOCK_PASSWORD, cbCloseAndClear);
            AddToGroup(Reasons.LOCK_PASSWORD, _lockAccessUserViewCallBack);
        }

        private void InitLockBtUpdate()
        {
            AddToGroup(Reasons.LOCK_LOAD_FILES, btDeleteProduct);
            AddToGroup(Reasons.LOCK_LOAD_FILES, btDeleteStation);
            AddToGroup(Reasons.LOCK_LOAD_FILES, btDeleteProgram);
            AddToGroupFor(Reasons.LOCK_LOAD_FILES, GetGroupControls(Reasons.LOCK_INPUT));
        }

        private void InitLockAccessUserUpdate()
        {
            AddToGroup(Reasons.LOCK_ACCESS_USER_UPDATE, _lockAccessUserViewCallBack);
        }

        internal void OnChosseMainFile(FileModel model)
        {
            if (!string.IsNullOrEmpty(model?.ProgramPath))
            {
                Util.SafeInvoke(txtMainFile, () => { txtMainFile.Text = model.ProgramPath; });
            }
        }

        internal void OnChosseRunFile(FileModel model)
        {
            if (!string.IsNullOrEmpty(model?.ProgramPath))
            {
                string path = model.ProgramPath;
                string dir = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(dir))
                {
                    Util.SafeInvoke(TxtOpenCmd, () => { TxtOpenCmd.Text = Path.GetFileName(path); });
                }
                else
                {
                    string cmd = $"cd \"{dir}\" && {Path.GetFileName(path)}";
                    Util.SafeInvoke(TxtOpenCmd, () => { TxtOpenCmd.Text = cmd; });
                }

            }
        }
        
        internal void OnChosseCloseFile(FileModel model)
        {
            if (!string.IsNullOrEmpty(model?.ProgramPath))
            {
                string path = model.ProgramPath;
                string dir = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(dir))
                {
                    Util.SafeInvoke(txtCloseCmd, () => { txtCloseCmd.Text = Path.GetFileName(path); });
                }
                else
                {
                    string cmd = $"cd \"{dir}\" && {Path.GetFileName(path)}";
                    Util.SafeInvoke(txtCloseCmd, () => { txtCloseCmd.Text = cmd; });
                }
                    
            }
        }

        private void btAccessUser_Click(object sender, EventArgs e)
        {
            _stationAccessUserForm.ShowDialog();
        }
    }
}
