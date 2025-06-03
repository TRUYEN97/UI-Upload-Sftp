using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Upload.Common;
using Upload.gui;
using Upload.Model;

namespace Upload.ModelView
{
    internal class UserListViewModelView
    {
        private readonly ListBox _listView;
        public AccessUserListModel AccessUserListModel { get; private set; }
        public bool HasChanged { get; set; }
        public UserListViewModelView(ListBox listView, Button btAdd, Button btDelete)
        {
            _listView = listView;
            _listView.Items.Clear();
            _listView.MouseDoubleClick += (s, e) =>
            {
                var item = _listView.SelectedItem;
                if (item is UserModel user)
                {
                    if (UserForm.EditUserModel(user))
                    {
                        HasChanged = true;
                    }
                }
            };
            btAdd.Click += (s, e) =>
            {
                var user = UserForm.CreateUserModel();
                if (user != null)
                {
                    if (AddUser(user))
                    {
                        HasChanged = true;
                    }
                }
            };
            btDelete.Click += (s, e) =>
            {
                var items = _listView.SelectedItems;
                List< UserModel > toRemoves = new List< UserModel >();
                foreach (var item in items)
                {
                    if (item == null) { continue; }
                    if (item is UserModel user)
                    {
                        if (MessageBox.Show($"Xác nhận xóa user: [{user.Id}]", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            toRemoves.Add(user);
                        }
                    }
                }
                foreach (var user in toRemoves)
                {
                    if (RemoveUser(user))
                    {
                        HasChanged = true;
                    }
                }
            };
        }
        private bool AddUser(UserModel user)
        {
            if(user == null) return false;
            if (AccessUserListModel?.UserModels == null) return false;
            var userModels = AccessUserListModel.UserModels;
            if (!userModels.Contains(user) || new ConfirmOverrideForm().IsAccept($"[{user.Id}] đã tồn tại!\r\nBạn có muốn thay thế không?"))
            {
                userModels.Add(user);
                Reload();
                return true;
            }
            return false;
        }

        private void Reload()
        {
            Util.SafeInvoke(_listView, () =>
            {
                _listView.Items.Clear();
                if(AccessUserListModel?.UserModels == null) return;
                foreach (var user in AccessUserListModel.UserModels)
                {
                    if (string.IsNullOrEmpty(user?.Id)) continue;
                    _listView.Items.Add(user);
                }
            });
        }

        private bool RemoveUser(UserModel user)
        {
            if (user == null) return false;
            if (AccessUserListModel?.UserModels == null || _listView == null)
            {
                return false;
            }
            AccessUserListModel.UserModels.Remove(user);
            Reload();
            return true;
        }

        public void SetUsers(AccessUserListModel accessUserList)
        {
            if (_listView == null)
            {
                return;
            }
            if(accessUserList?.UserModels == null)
            {
                accessUserList = new AccessUserListModel();
            }
            AccessUserListModel = accessUserList;
            Reload();
            HasChanged = false;
        }

        public void Clear()
        {
            Util.SafeInvoke(_listView, () =>
            {
                _listView.Items.Clear();
            });
            AccessUserListModel?.UserModels?.Clear();
            HasChanged = false;
        }
    }
}
