using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload;
using AutoDownload.Common;
using AutoDownload.Model;
using Newtonsoft.Json;
using Upload.common;
using Upload.model;

namespace Upload
{
    internal class LocationCreater
    {

        internal LocationCreater()
        {
        }

        internal async Task<bool> CreateProduct(Location location)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager(Util.Status.CONNECT_ERR, "");
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
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);


                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager(Util.Status.CONNECT_ERR, "");
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
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                return await Task.Run(async () =>
                {
                    using (var sftp = Util.GetSftpInstance())
                    {
                        if (!await sftp.Connect())
                        {
                            Util.ShowMessager(Util.Status.CONNECT_ERR, "");
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
                        if (await sftp.CreateDirectory(path) && await CreateAppModel(location))
                        {
                            Util.ShowMessager($"Trạm {location.Station} đã tạo thành công!");
                            return true;
                        }
                    }
                    Util.ShowMessager($"Trạm {location.Station} đã tạo thất bại!");
                    return false;
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
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager(Util.Status.CONNECT_ERR, "");
                        return false;
                    }
                    string path = PathUtil.GetStationPath(location);
                    if (!await sftp.Exists(path))
                    {
                        Util.ShowMessager($"Trạm {location.Station} không tồn tại!");
                        return true;
                    }
                    if (!await sftp.DeleteFolder(path, true))
                    {
                        Util.ShowMessager($"Trạm {location.Station} đã xóa thành công!");
                        return true;
                    }
                }
                Util.ShowMessager($"Trạm {location.Station} đã xóa thất bại!");
                return false;

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
            
        }

        internal async Task<bool> CreateApp(Location location)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager(Util.Status.CONNECT_ERR, "");
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
                var responceRs = await TranforUtil.GetAppListModel(loca);
                AppList model = responceRs.Item1;
                if (model == null)
                {
                    model = new AppList();
                }
                if (!string.IsNullOrWhiteSpace(loca.AppName))
                {
                    if (!model.ProgramPaths.ContainsKey(loca.AppName))
                    {
                        string versionPath = PathUtil.GetAppModelPath(location);
                        if (!await TranforUtil.UploadModel(new AppModel()
                        {
                            RemoteStoreDir = PathUtil.GetCommonPath(location),
                            RemoteAppListPath = responceRs.Item2,
                            Path = versionPath
                        }, versionPath))
                        {
                            return false;
                        }
                        model.ProgramPaths.Add(loca.AppName, versionPath);
                    }
                }
                if ((await TranforUtil.UpLoadAppListModel(model, loca)).Item1)
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
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                return false;
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }
    }
}
