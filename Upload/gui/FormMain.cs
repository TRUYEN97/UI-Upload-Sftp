using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload.Common;
using AutoDownload.Config;
using AutoDownload.Gui;
using Upload.Service;
using Upload.gui;
using Upload.common;
using Upload.model;
using AutoDownload.Model;
using System.Windows.Shapes;
using System.IO;
using static Upload.Service.LockManager;
using Path = System.IO.Path;

namespace Upload
{
    public partial class FormMain : Form
    {
        private readonly LocationManagement _locationManagement;
        private readonly LockManager _lockManager;
        private bool _isLockPasswork;
        public FormMain()
        {
            InitializeComponent();
            Text = $"{ProgramInfo.ProductName} - V{ProgramInfo.ProductVersion}";
            LoggerBox.TxtMessager = txtMassage;
            _locationManagement = new LocationManagement(this, ConstKey.ZIP_PASSWORD);
            _lockManager = Instance;
            _isLockPasswork = true;
            new Uploader(this, _locationManagement, ConstKey.ZIP_PASSWORD);
            CursorUtil.Instance.FormMain=this;
            InitLockLocation();
            InitLockInput(); 
            InitLockCreateBt();
            InitLockUpdate();
            InitLockPassword();
            InitLockBtUpdate();
            _lockManager.SetLock(true, Reasons.LOCK_PASSWORD);
        }

        internal void ClearData()
        {
            UpdateTxt(txtOpenCmd, "");
            UpdateTxt(txtCloseCmd, "");
            UpdateTxt(txtMainFile, "");
            UpdateTxt(txtWindowName, "");
            UpdateCheckBox(cbEnabled, false);
            UpdateCheckBox(cbAutoOpen, false);
        }

        internal void SetData(AppModel appModel)
        {
            UpdateTxt(txtOpenCmd, appModel.OpenCmd);
            UpdateTxt(txtCloseCmd, appModel.CloseCmd);
            UpdateTxt(txtMainFile, appModel.MainPath);
            UpdateTxt(txtWindowName, appModel.WindowTitle);
            UpdateCheckBox(cbEnabled, appModel.Enable);
            UpdateCheckBox(cbAutoOpen, appModel.AutoOpen);
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
                _lockManager.SetLock(true, Reasons.LOCK_PASSWORD);
                _isLockPasswork = true;
                return;
            }
            string inputPw = InputForm.GetInputPassword("Mật khẩu");
            string password = AutoDLConfig.ConfigModel.Password;
            if (!string.IsNullOrWhiteSpace(inputPw) && Util.GetMD5HashFromString(inputPw).Equals(password))
            {
                _lockManager.SetLock(false, Reasons.LOCK_PASSWORD);
                _isLockPasswork = false;
                LoggerBox.Addlog("Mật khẩu đúng");
            }
            else
            {
                _lockManager.SetLock(true, Reasons.LOCK_PASSWORD);
                LoggerBox.Addlog("Mật khẩu không đúng!");
                _isLockPasswork = true;
            }
        }
        private void InitLockLocation()
        {
            _lockManager.AddToGroup(Reasons.LOCK_LOCATION, cbbProduct);
            _lockManager.AddToGroup(Reasons.LOCK_LOCATION, CbbStation);
            _lockManager.AddToGroup(Reasons.LOCK_LOCATION, CbbProgram);
        }
        private void InitLockInput()
        {
            _lockManager.AddToGroup(Reasons.LOCK_INPUT, txtCloseCmd);
            _lockManager.AddToGroup(Reasons.LOCK_INPUT, txtOpenCmd);
            _lockManager.AddToGroup(Reasons.LOCK_INPUT, txtMainFile);
            _lockManager.AddToGroup(Reasons.LOCK_INPUT, txtWindowName);
            _lockManager.AddToGroup(Reasons.LOCK_INPUT, cbEnabled);
            _lockManager.AddToGroup(Reasons.LOCK_INPUT, cbAutoOpen);
            _lockManager.AddToGroup(Reasons.LOCK_INPUT, treeFolder);
            _lockManager.AddToGroup(Reasons.LOCK_INPUT, BtUpdate);
        }
        private void InitLockCreateBt()
        {
            _lockManager.AddToGroup(Reasons.LOCK_CREATE_BT, btCreateProduct);
            _lockManager.AddToGroup(Reasons.LOCK_CREATE_BT, btDeleteProduct);
            _lockManager.AddToGroup(Reasons.LOCK_CREATE_BT, btCreateStation);
            _lockManager.AddToGroup(Reasons.LOCK_CREATE_BT, btDeleteStation);
            _lockManager.AddToGroup(Reasons.LOCK_CREATE_BT, btCreateProgram);
            _lockManager.AddToGroup(Reasons.LOCK_CREATE_BT, btDeleteProgram);
        }
        private void InitLockUpdate()
        {
            _lockManager.AddToGroup(Reasons.LOCK_UPDATE, _lockManager.GroupControls(Reasons.LOCK_CREATE_BT));
            _lockManager.AddToGroup(Reasons.LOCK_UPDATE, _lockManager.GroupControls(Reasons.LOCK_INPUT));
            _lockManager.AddToGroup(Reasons.LOCK_UPDATE, _lockManager.GroupControls(Reasons.LOCK_LOCATION));
        }
        private void InitLockPassword()
        {
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, btCreateProduct);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, btDeleteProduct);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, btCreateStation);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, btDeleteStation);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, btCreateProgram);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, btDeleteProgram);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, BtUpdate);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, txtCloseCmd);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, txtOpenCmd);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, txtMainFile);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, txtWindowName);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, cbEnabled);
            _lockManager.AddToGroup(Reasons.LOCK_PASSWORD, cbAutoOpen);
        }

        private void InitLockBtUpdate()
        {
            _lockManager.AddToGroup(Reasons.LOCK_LOAD_FILES, btDeleteProduct);
            _lockManager.AddToGroup(Reasons.LOCK_LOAD_FILES, btDeleteStation);
            _lockManager.AddToGroup(Reasons.LOCK_LOAD_FILES, btDeleteProgram);
            _lockManager.AddToGroup(Reasons.LOCK_LOAD_FILES, _lockManager.GroupControls(Reasons.LOCK_INPUT));
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
    }
}
