using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload.Gui;
using Upload.Common;
using Upload.Model;
using Upload.ModelView;
using static Upload.Service.LockManager;

namespace Upload.gui
{
    public partial class AccessUserControl : UserControl
    {
        private readonly UserListViewModelView _userListViewModelView;
        private readonly string zipPassword;
        private string remotePath;
        public AccessUserControl()
        {
            InitializeComponent();
            _userListViewModelView = new UserListViewModelView(listUser, btAdd, btDelete);
            this.zipPassword = ConstKey.ZIP_PASSWORD; ;
        }

        internal void Clear()
        {
            _userListViewModelView.Clear();
        }

        internal void Lock()
        {
            Util.SafeInvoke(btAdd, () => { btAdd.Enabled = false; });
            Util.SafeInvoke(btDelete, () => { btDelete.Enabled = false; });
            Util.SafeInvoke(btUpdate, () => { btUpdate.Enabled = false; });
        }

        internal void UnLock()
        {
            Util.SafeInvoke(btAdd, () => { btAdd.Enabled = true; });
            Util.SafeInvoke(btDelete, () => { btDelete.Enabled = true; });
            Util.SafeInvoke(btUpdate, () => { btUpdate.Enabled = true; });
        }

        internal void LoadFormPath(string remotePath)
        {
            if (string.IsNullOrEmpty(remotePath))
            {
                Clear();
                return;
            }
            this.remotePath = remotePath;
            Task.Run(async () => {
                try
                {
                    ForceLockAll(Reasons.LOCK_ACCESS_USER_UPDATE);
                    AccessUserListModel userModels = await TranforUtil.GetModelConfig<AccessUserListModel>(remotePath, zipPassword);
                    if (userModels == null)
                    {
                        Clear();
                    }
                    else
                    {
                        _userListViewModelView.SetUsers(userModels);
                    }
                }
                finally
                {
                    ForceUnlockAll(Reasons.LOCK_ACCESS_USER_UPDATE);
                }
            });
            
        }

        private void btUserUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(remotePath))
            {
                return;
            }
            Task.Run(async () => {
                try
                {
                    ForceLockAll(Reasons.LOCK_ACCESS_USER_UPDATE);
                    AccessUserListModel userModels = _userListViewModelView.AccessUserListModel ?? new AccessUserListModel();
                    if (!await TranforUtil.UploadModel(userModels, remotePath, zipPassword))
                    {
                        LoggerBox.Addlog("Upload Access user list failed!");
                    }
                    else
                    {
                        LoggerBox.Addlog("Upload Access user list");
                    }
                }
                finally
                {
                    ForceUnlockAll(Reasons.LOCK_ACCESS_USER_UPDATE);
                }
            });
        }
    }
}
