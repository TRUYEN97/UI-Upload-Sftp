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
                        Util.ShowConnectFailedMessager();
                        return false;
                    }
                    string prodcutPath = PathUtil.GetProductPath(location);
                    if (await sftp.Exists(prodcutPath))
                    {
                        Util.ShowMessager($"Product {location.Product} has exists!");
                        return true;
                    }
                    if (await sftp.CreateDirectory(prodcutPath))
                    {
                        Util.ShowCreatedMessager("Product", location.Product);
                        return true;
                    }
                }
                Util.ShowCreateFailedMessager("Product", location.Product);
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
                        Util.ShowConnectFailedMessager();
                        return false;
                    }
                    string prodcutPath = PathUtil.GetProductPath(location);
                    if (!await sftp.Exists(prodcutPath))
                    {
                        Util.ShowNotFoundMessager("Product", location.Product);
                        return true;
                    }
                    if (await sftp.DeleteFolder(prodcutPath, true))
                    {
                        Util.ShowCreatedMessager("Product", location.Product);
                        return true;
                    }
                }
                Util.ShowCreateFailedMessager("Product", location.Product);
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
                            Util.ShowConnectFailedMessager();
                            return false;
                        }
                        string path = PathUtil.GetStationPath(location);
                        if (await sftp.Exists(path))
                        {
                            Util.ShowNotFoundMessager("Station", location.Station);
                            return true;
                        }
                        string commonFilePath = PathUtil.GetCommonPath(location);
                        if (!await sftp.Exists(commonFilePath) && !await sftp.CreateDirectory(commonFilePath))
                        {
                            Util.ShowCreateFailedMessager($"{location.Station}/Common", "");
                            return false;
                        }
                        if (!await sftp.CreateDirectory(path))
                        {
                            Util.ShowCreateFailedMessager("station" ,location.Station);
                            return false;
                        }
                        await TranferUtil.UploadModel(new AccessUserListModel(), PathUtil.GetStationAccessUserPath(location), zipPassword);
                        if (!await CreateAppModel(location))
                        {
                            Util.ShowMessager($"Station {location.Station} create app list failed!");
                            return false;
                        }
                    }
                    Util.ShowCreatedMessager("Station", location.Station);
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
                        Util.ShowConnectFailedMessager();
                        return false;
                    }
                    string path = PathUtil.GetStationPath(loca);
                    if (!await sftp.Exists(path))
                    {
                        Util.ShowDeletedMessager("Station", loca.Station);
                        return true;
                    }
                    var responceRs = await TranferUtil.GetAppListModel(loca, zipPassword);
                    AppList appList = responceRs.Item1;
                    if (appList?.ProgramPaths == null || appList?.ProgramPaths.Count == 0)
                    {
                        if (await sftp.DeleteFolder(path))
                        {
                            Util.ShowDeletedMessager("Station", loca.Station);
                            return true;
                        }
                    }
                }
                Util.ShowDeleteFailedMessager("Station", loca.Station);
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
                        Util.ShowConnectFailedMessager();
                        return false;
                    }
                    if (await CreateAppModel(location))
                    {
                        Util.ShowCreatedMessager("Program", location.AppName);
                        return true;
                    }
                }
                Util.ShowCreateFailedMessager("Program", location.AppName);
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
                            Util.ShowMessager($"Station [{loca.Station}] create [{loca.AppName}] app data faild!");
                            return false;
                        }
                        if (!await TranferUtil.UploadModel(new AccessUserListModel(), programAccessUserPath, zipPassword))
                        {
                            Util.ShowMessager($"Station [{loca.Station}] create access user for [{loca.AppName}] failed!");
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
                        Util.ShowConnectFailedMessager();
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
                                Util.ShowDeleteFailedMessager(location.AppName,"");
                                return false;
                            }
                        }
                    }
                }
                Util.ShowDeletedMessager(location.AppName, "");
                return true;
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }
    }
}
