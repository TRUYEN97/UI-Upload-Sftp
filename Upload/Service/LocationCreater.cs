using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Upload.Common;
using Upload.Model;

namespace Upload
{
    internal class LocationCreater
    {

        private readonly string zipPassword;
        internal LocationCreater(string zipPassword)
        {
            this.zipPassword = zipPassword;
        }

        internal async Task<bool> CreateProduct(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.Product))
            {
                return false;
            }
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager("Không thể kết nối!");
                        return false;
                    }
                    string prodcutPath = PathUtil.GetProductPath(location);
                    if (await sftp.Exists(prodcutPath))
                    {
                        Util.ShowMessager($"Hàng {location.Product} đã tồn tại!");
                        return true;
                    }
                    if (await sftp.CreateDirectory(prodcutPath))
                    {
                        Util.ShowMessager($"Hàng {location.Product} đã tạo thành công!");
                        return true;
                    }
                }
                Util.ShowMessager($"Hàng {location.Product} đã tạo thất bại!");
                return false;
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }
        internal async Task<bool> DeleteProduct(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.Product))
            {
                return false;
            }
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);


                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager("Không thể kết nối!");
                        return false;
                    }
                    string prodcutPath = PathUtil.GetProductPath(location);
                    if (!await sftp.Exists(prodcutPath))
                    {
                        Util.ShowMessager($"Hàng {location.Product} không tồn tại!");
                        return true;
                    }
                    if (await sftp.DeleteFolder(prodcutPath, true))
                    {
                        Util.ShowMessager($"Hàng {location.Product} đã xóa thành công!");
                        return true;
                    }
                }
                Util.ShowMessager($"Hàng {location.Product} đã xóa thất bại!");
                return false;


            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }
        internal async Task<bool> CreateStation(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.Product) || string.IsNullOrWhiteSpace(location.Station))
            {
                return false;
            }
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                return await Task.Run(async () =>
                {
                    using (var sftp = Util.GetSftpInstance())
                    {
                        if (!await sftp.Connect())
                        {
                            Util.ShowMessager("Không thể kết nối!");
                            return false;
                        }
                        string path = PathUtil.GetStationPath(location);
                        if (await sftp.Exists(path))
                        {
                            Util.ShowMessager($"Trạm {location.Station} đã tồn tại!");
                            return true;
                        }
                        string commonFilePath = PathUtil.GetCommonPath(location);
                        if (!await sftp.Exists(commonFilePath) && !await sftp.CreateDirectory(commonFilePath))
                        {
                            Util.ShowMessager($"Khởi tạo {location.Station}/Common đã thất bại!");
                            return false;
                        }
                        if (!await sftp.CreateDirectory(path))
                        {
                            Util.ShowMessager($"Trạm {location.Station} đã tạo thất bại!");
                            return false;
                        }
                        await TranferUtil.UploadModel(new AccessUserListModel(), PathUtil.GetStationAccessUserPath(location), zipPassword);
                        if (!await CreateAppModel(location))
                        {
                            Util.ShowMessager($"Trạm {location.Station} khởi tạo app list thất bại!");
                            return false;
                        }
                    }
                    Util.ShowMessager($"Trạm {location.Station} đã tạo thành công!");
                    return true;
                });

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }

        }

        internal async Task<bool> DeleteStation(Location location)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(location.Product) || string.IsNullOrWhiteSpace(location.Station))
                {
                    return false;
                }
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                Location loca = new Location(location);
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager("Không thể kết nối!");
                        return false;
                    }
                    string path = PathUtil.GetStationPath(loca);
                    if (!await sftp.Exists(path))
                    {
                        Util.ShowMessager($"Trạm {loca.Station} không tồn tại!");
                        return true;
                    }
                    var responceRs = await TranferUtil.GetAppListModel(loca, zipPassword);
                    AppList appList = responceRs.Item1;
                    if (appList?.ProgramPaths == null || appList?.ProgramPaths.Count == 0)
                    {
                        if (await sftp.DeleteFolder(path))
                        {
                            Util.ShowMessager($"Trạm {loca.Station} đã xóa thành công!");
                            return true;
                        }
                    }
                }
                Util.ShowMessager($"Trạm {loca.Station} đã xóa thất bại!");
                return false;

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }

        }

        internal async Task<bool> CreateProgram(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.AppName))
            {
                return false;
            }
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager("Không thể kết nối!");
                        return false;
                    }
                    if (await CreateAppModel(location))
                    {
                        Util.ShowMessager($"Chương trình [{location.AppName}] đã tạo thành công!");
                        return true;
                    }
                }
                Util.ShowMessager($"Chương trình [{location.AppName}] đã tạo thất bại!");
                return false;

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }

        }

        private async Task<bool> CreateAppModel(Location location)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                Location loca = new Location(location);
                if (string.IsNullOrWhiteSpace(loca.Product)
                    || string.IsNullOrWhiteSpace(loca.Station))
                {
                    return false;
                }
                var responceRs = await TranferUtil.GetAppListModel(loca, zipPassword);
                AppList model = responceRs.Item1;
                if (model == null)
                {
                    model = new AppList();
                }
                if (!string.IsNullOrWhiteSpace(loca.AppName))
                {
                    if (!model.ProgramPaths.ContainsKey(loca.AppName))
                    {
                        string programDataPath = PathUtil.GetAppModelPath(loca);
                        string programAccessUserPath = PathUtil.GetAppAccessUserPath(loca);
                        if (!await TranferUtil.UploadModel(new AppModel()
                        {
                            RemoteStoreDir = PathUtil.GetCommonPath(loca),
                            RemoteAppListPath = responceRs.Item2,
                            Path = programDataPath
                        }, programDataPath, zipPassword))
                        {
                            Util.ShowMessager($"Trạm [{loca.Station}] khởi tạo [{loca.AppName}] app data thất bại!");
                            return false;
                        }
                        if (!await TranferUtil.UploadModel(new AccessUserListModel(), programAccessUserPath, zipPassword))
                        {
                            Util.ShowMessager($"Trạm [{loca.Station}] tạo dữ liệu access user cho [{loca.AppName}] thất bại!");
                            return false;
                        }
                        model.ProgramPaths.Add(loca.AppName, new ProgramPathModel()
                        {
                            AccectUserPath = programAccessUserPath,
                            AppPath = programDataPath
                        });
                    }
                }
                if ((await TranferUtil.UpLoadAppListModel(model, loca, zipPassword)).Item1)
                {
                    return true;
                }
                return false;

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

        internal async Task<bool> DeleteProgram(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.Product) || string.IsNullOrWhiteSpace(location.Station) || string.IsNullOrWhiteSpace(location.AppName))
            {
                return false;
            }
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                Location loca = new Location(location);
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager("Không thể kết nối!");
                        return false;
                    }
                    var responceRs = await TranferUtil.GetAppListModel(loca, zipPassword);
                    AppList appList = responceRs.Item1;
                    if (appList?.ProgramPaths != null)
                    {
                        if (appList.ProgramPaths.TryGetValue(loca.AppName, out var modelPath))
                        {
                            AppModel appModel = await TranferUtil.GetModelConfig<AppModel>(modelPath.AppPath, zipPassword);
                            if (appModel?.FileModels != null)
                            {
                                List<FileModel> canDeletes = await TranferUtil.GetCanDeleteFileModelsAsync(modelPath.AppPath, appModel.FileModels.ToList(), appList, zipPassword);
                                await TranferUtil.RemoveRemoteFile(canDeletes);
                                await sftp.DeleteFile(modelPath.AppPath);
                            }
                            await sftp.DeleteFile(modelPath.AccectUserPath);
                            appList.ProgramPaths.Remove(loca.AppName);
                            if (!await TranferUtil.UploadModel(appList, responceRs.Item2, zipPassword))
                            {
                                Util.ShowMessager($"{location.AppName} xóa thất bại!");
                                return false;
                            }
                        }
                    }
                }
                Util.ShowMessager($"{location.AppName} đã xóa!");
                return true;
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }
    }
}
