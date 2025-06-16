using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Upload.Common;
using Upload.gui;
using Upload.Model;

namespace Upload.Service
{
    internal class LocationManagement
    {
        public Location Location { get; }
        private AppList _appList;
        private readonly LocationCreater _locatonCreater;
        private readonly FormMain formMain;
        private readonly ComboBox cbbProduct;
        private readonly ComboBox cbbStation;
        private readonly ComboBox cbbProgram;
        private readonly string zipPassword;
        public event Action<ProgramPathModel> ShowVerionAction;

        public LocationManagement(FormMain formMain)
        {
            this.zipPassword = ConstKey.ZIP_PASSWORD;
            this.Location = new Location();
            this._locatonCreater = new LocationCreater(zipPassword);
            this.formMain = formMain;
            this.cbbProduct = formMain.CbbProduct;
            this.cbbStation = formMain.CbbStation;
            this.cbbProgram = formMain.CbbProgram;
            InitButtonEnvent();
            InitComboboxEvent();
        }
        private void UpdateLocation()
        {
            if (cbbProduct.SelectedItem != null)
            {
                Location.Product = cbbProduct.SelectedItem.ToString();
            }
            if (cbbStation.SelectedItem != null)
            {
                Location.Station = cbbStation.SelectedItem.ToString();
            }
            if (cbbProgram.SelectedItem != null)
            {
                Location.AppName = cbbProgram.SelectedItem.ToString();
            }
        }

        private async Task UpdateProductItem(string name = null)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                string path = PathUtil.GetRemotePath();
                if (!await UpdateItems(path, cbbProduct, name))
                {
                    Util.ShowMessager("Station invaild");
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }

        }

        private async Task UpdateStationItems(string name = null)
        {
            string remotePath = PathUtil.GetProductPath(Location);
            if (!await UpdateItems(remotePath, cbbStation, name))
            {
                UpdateCombobox(cbbProgram, new List<string>());
                ShowProgram(null);
            }
        }

        private void InitButtonEnvent()
        {
            this.formMain.BtCreateProduct.Click += (s, e) =>
            {
                string name = InputForm.GetInputString("Product name");
                Task.Run(async () =>
                {
                    if (name == null)
                    {
                        return;
                    }
                    if (await _locatonCreater.CreateProduct(new Location() { Product = name }))
                    {
                        await UpdateProductItem(name);
                    }
                });
            };

            this.formMain.BtDeleteProduct.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(Location.Product)
                    || MessageBox.Show($"Do you want to delete [{Location.Product}]?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes
                    || !PasswordLocker.CheckPassword())
                {
                    return;
                }
                Task.Run(async () =>
                {
                    if (await _locatonCreater.DeleteProduct(Location))
                    {
                        await UpdateProductItem();
                    }
                });
            };

            this.formMain.BtCreateStation.Click += (s, e) =>
            {
                string name = InputForm.GetInputString("Station name");
                if (name == null || string.IsNullOrWhiteSpace(Location.Product))
                {
                    return;
                }
                Task.Run(async () =>
                {
                    if (await _locatonCreater.CreateStation(new Location() { Product = Location.Product, Station = name }))
                    {
                        await UpdateStationItems(name);
                    }
                });
            };

            this.formMain.BtDeleteStation.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(Location.Product) || string.IsNullOrWhiteSpace(Location.Station)
                    || MessageBox.Show($"Do you want to delete [{Location.Product}/{Location.Station}]?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes
                    || !PasswordLocker.CheckPassword())
                {
                    return;
                }
                Task.Run(async () =>
                {
                    if (await _locatonCreater.DeleteStation(Location))
                    {
                        await UpdateStationItems();
                    }
                });
            };

            this.formMain.BtCreateProgram.Click += (s, e) =>
            {
                string name = InputForm.GetInputString("Program name");
                Task.Run(async () =>
                {
                    if (name == null || string.IsNullOrWhiteSpace(Location.Product) || string.IsNullOrWhiteSpace(Location.Station))
                    {
                        return;
                    }
                    if (await _locatonCreater.CreateProgram(new Location() { Product = Location.Product, Station = Location.Station, AppName = name }))
                    {
                        await UpdateProgramListItems(name);
                    }
                });
            };

            this.formMain.BtDeleteProgram.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(Location.Product) || string.IsNullOrWhiteSpace(Location.Station) || string.IsNullOrWhiteSpace(Location.AppName)
                    || MessageBox.Show($"Do you want to delete [{Location.Product}/{Location.Station}/{Location.AppName}]?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes
                    || !PasswordLocker.CheckPassword())
                {
                    return;
                }
                Task.Run(async () =>
                {
                    if (await _locatonCreater.DeleteProgram(Location))
                    {
                        await UpdateProgramListItems();
                    }
                });
            };
        }

        private void InitComboboxEvent()
        {

            this.cbbProduct.SelectedIndexChanged += async (s, e) =>
            {
                UpdateLocation();
                await UpdateStationItems();
            };

            this.cbbStation.SelectedIndexChanged += async (s, e) =>
            {
                UpdateLocation();
                await UpdateProgramListItems();
            };

            this.cbbProgram.SelectedIndexChanged += (s, e) =>
            {
                UpdateLocation();
                ShowProgram(_appList);
            };
            Task.Run(async () =>
            {
                await UpdateProductItem();
            });
        }

        public async Task UpdateProgramListItems(string selectName = null)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                _appList = (await TranferUtil.GetAppListModel(Location, zipPassword)).Item1;
                List<string> list = new List<string>();
                if (_appList != null)
                {
                    list.AddRange(_appList.ProgramPaths.Keys);
                }
                UpdateCombobox(cbbProgram, list, selectName);
                if (list.Count <= 0)
                {
                    ShowProgram(null);
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

        private void ShowProgram(AppList versionList)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                string appName = Location.AppName;
                if (versionList != null && !string.IsNullOrWhiteSpace(appName) && true.Equals(_appList.ProgramPaths?.ContainsKey(appName)))
                {
                    ShowVerionAction?.Invoke(_appList.ProgramPaths[appName]);
                }
                else
                {
                    ShowVerionAction?.Invoke(null);
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

        private async Task<bool> UpdateItems(string remotePath, ComboBox target, string selectName)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowConnectFailedMessager();
                        return false;
                    }
                    if (!await sftp.Exists(remotePath))
                    {
                        return false;
                    }
                    List<string> list = await sftp.ListDirectoryName(remotePath);
                    UpdateCombobox(target, list, selectName);
                    return list.Count > 0;
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }

        }

        private void UpdateCombobox(ComboBox target, List<string> list, string selectName = null)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                Util.SafeInvoke(target, () =>
                {
                    target.Items.Clear();
                    target.Items.AddRange(list.ToArray());
                    if (list.Count > 0)
                    {
                        int selectedItem = 0;
                        if (!string.IsNullOrEmpty(selectName))
                        {
                            selectedItem = target.Items.IndexOf(selectName.ToUpper().Trim());
                        }
                        target.SelectedIndex = selectedItem == -1 ? 0: selectedItem;
                    }
                    else
                    {
                        target.Text = "";
                    }
                });
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }

        }

    }
}
