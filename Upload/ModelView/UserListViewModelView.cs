using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Upload.Common;
using Upload.gui;
using Upload.Model;
using Upload.Service;

namespace Upload.ModelView
{
    internal class UserListViewModelView
    {
        private readonly ListBox _listView;
        public AccessUserListModel AccessUserListModel { get; private set; }
        public UserListViewModelView(ListBox listView, Button btAdd, Button btDelete)
        {
            _listView = listView;
            _listView.Items.Clear();
            _listView.MouseDoubleClick += (s, e) =>
            {
                var item = _listView.SelectedItem;
                if (item is UserModel user)
                {
                    UserForm.EditUserModel(user);
                }
            };
            btAdd.Click += (s, e) =>
            {
                var user = UserForm.CreateUserModel();
                if (user != null)
                {
                    AddUser(user);
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
                    RemoveUser(user);
                }
            };
        }
        private void AddUser(UserModel user)
        {
            if(user == null) return;
            if (AccessUserListModel?.UserModels == null) return;
            var userModels = AccessUserListModel.UserModels;
            if (!userModels.Contains(user) || new ConfirmOverrideForm().IsAccept($"[{user.Id}] đã tồn tại!\r\nBạn có muốn thay thế không?"))
            {
                userModels.Add(user);
                Reload();
            }
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

        private void RemoveUser(UserModel user)
        {
            if (user == null) return;
            if (AccessUserListModel?.UserModels == null || _listView == null)
            {
                return;
            }
            AccessUserListModel.UserModels.Remove(user);
            Reload();
        }

        public void SetUsers(AccessUserListModel accessUserList)
        {
            if (accessUserList?.UserModels == null || _listView == null)
            {
                return;
            }
            AccessUserListModel = accessUserList;
            Reload();
        }

        public void Clear()
        {
            _listView?.Items.Clear();
            AccessUserListModel?.UserModels?.Clear();
        }

    }
}
