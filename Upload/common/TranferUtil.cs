using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload.Gui;
using Newtonsoft.Json;
using Upload.gui;
using Upload.Model;

namespace Upload.Common
{
    internal class TranferUtil
    {

        public static async Task<StoreFileModel> Download(FileModel fileModel, string localDir, string zipPassword)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                if (fileModel == null)
                {
                    return null;
                }
                string storePath = Path.Combine(localDir, fileModel.ProgramPath);
                StoreFileModel storeFileModel = new StoreFileModel(fileModel)
                {
                    StorePath = storePath
                };
                if (File.Exists(storePath) && Util.GetMD5HashFromFile(storePath).Equals(fileModel.Md5))
                {
                    return storeFileModel;
                }
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowConnectFailedMessager();
                        return null;
                    }
                    string remotePath = fileModel.RemotePath;
                    if (!await sftp.Exists(remotePath))
                    {
                        Util.ShowMessager($"Sftp File: [{remotePath}], not found!");
                        return null;
                    }
                    if (await sftp.DownloadFileAndUnzip(remotePath, storePath, zipPassword))
                    {
                        return storeFileModel;
                    }
                }
                return null;

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

        internal static async Task<(bool, string)> UpLoadAppListModel(AppList appList, Location location, string zipPassword)
        {
            string appConfigRemotePath = PathUtil.GetAppConfigRemotePath(location);
            return (await UploadModel(appList, appConfigRemotePath, zipPassword), appConfigRemotePath);
        }

        public static async Task<bool> UploadModel(object model, string path, string zipPassword)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);

                if (model == null || path == null)
                {
                    return false;
                }
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowConnectFailedMessager();
                        return false;
                    }
                    if (await sftp.UploadZipFileFormModel(model, path, zipPassword))
                    {
                        return true;
                    }
                    return false;
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

        public static async Task RemoveRemoteFile(ICollection<FileModel> removeFileModel)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowConnectFailedMessager();
                        return;
                    }
                    foreach (var fileModel in removeFileModel)
                    {

                        if (await sftp.Exists(fileModel.RemotePath) && !await sftp.DeleteFile(fileModel.RemotePath))
                        {
                            Util.ShowMessager($"{fileModel.RemotePath}, delete failed!");
                        }
                        else
                        {
                            Util.ShowMessager($"{fileModel.RemotePath}, delete Ok!");
                        }
                    }

                }

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }

        }

        public static async Task<bool> UploadFile(ICollection<FileModel> fileModels, string zipPassword)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                using (ProgressDialogForm form = new ProgressDialogForm("Upload files")
                {
                    Maximum = fileModels.Count
                })
                {
                    return await form.DoworkAsync<bool>(async (report, tk) =>
                    {
                        using (var sftp = Util.GetSftpInstance())
                        {
                            if (!await sftp.Connect())
                            {
                                Util.ShowConnectFailedMessager();
                                return false;
                            }
                            int count = 0;
                            foreach (var fileModel in fileModels)
                            {
                                if (tk.IsCancellationRequested) return false;
                                if (fileModel is StoreFileModel storeFileModel)
                                {
                                    string md5 = Util.GetMD5HashFromFile(storeFileModel.StorePath);
                                    string remoteName = $"{md5}.zip";
                                    string remotePath = Path.Combine(storeFileModel.RemoteDir, remoteName);
                                    storeFileModel.RemotePath = remotePath;
                                    storeFileModel.Md5 = md5;
                                    report.Invoke(++count, storeFileModel.StorePath);
                                    if (await sftp.Exists(remotePath))
                                    {
                                        Util.ShowMessager($"{storeFileModel.StorePath} -> {remotePath}, has exists!");
                                    }
                                    else if (await sftp.UploadZipFile(remotePath, storeFileModel.StorePath, zipPassword))
                                    {
                                        Util.ShowMessager($"{storeFileModel.StorePath} -> {remotePath}, upload ok!");
                                    }
                                    else
                                    {
                                        Util.ShowMessager($"{storeFileModel.StorePath} -> {remotePath}, upload failed!");
                                        MessageBox.Show($"{storeFileModel.StorePath} -> {remotePath}, upload failed!");
                                    }
                                }
                            }
                            return true;
                        }
                    });
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }

        }

        internal static async Task<(AppList, string)> GetAppListModel(Location location, string zipPassword)
        {
            string appConfigRemotePath = PathUtil.GetAppConfigRemotePath(location);
            return (await GetModelConfig<AppList>(appConfigRemotePath, zipPassword), appConfigRemotePath);
        }

        internal static async Task<(UiStoreModel, string)> GetUiStoreModel(string zipPassword)
        {
            string uiStorePath = PathUtil.GetUiStoreRemotePath();
            return (await GetModelConfig<UiStoreModel>(uiStorePath, zipPassword), uiStorePath);
        }

        internal static async Task<(bool, string)> UpdateUiStoreModel(UiStoreModel storeModel, string zipPassword)
        {
            string uiStorePath = PathUtil.GetUiStoreRemotePath();
            return (await UploadModel(storeModel, uiStorePath, zipPassword), uiStorePath);
        }

        internal static async Task<T> GetModelConfig<T>(string path, string zipPassword)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowConnectFailedMessager();
                        return default;
                    }

                    if (!await sftp.Exists(path))
                    {
                        return default;
                    }

                    try
                    {
                        string appConfig = await sftp.DownloadZipFileFormModel(path, zipPassword);
                        var result = JsonConvert.DeserializeObject<T>(appConfig);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        LoggerBox.Addlog(ex.Message);
                        return default;
                    }
                }

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }
        internal static async Task<HashSet<FileModel>> GetCanDeleteFileModelsAsync(List<FileModel> fileModels,
            AppList appList, string zipPassword, string thisAppPath = null)
        {
            Dictionary<string, HashSet<FileModel>> canDeleteFileGroups = fileModels.GroupBy(f => f.Md5).ToDictionary(g => g.Key, g => new HashSet<FileModel>(g.Select(f => f)));
            var fileModelUsed = new Dictionary<string, FileModel>();
            AppModel appModel = null;
            foreach (var appInfo in appList.ProgramPaths)
            {
                if (!string.IsNullOrEmpty(thisAppPath) && thisAppPath == appInfo.Value.AppPath)
                {
                    continue;
                }
                appModel = await GetModelConfig<AppModel>(appInfo.Value.AppPath, zipPassword);
                Dictionary<string, HashSet<FileModel>> md5FileGroups = appModel?.FileModels?.GroupBy(f => f.Md5).ToDictionary(g => g.Key, g => new HashSet<FileModel>(g.Select(f => f)));
                canDeleteFileGroups = canDeleteFileGroups.Where(f => !md5FileGroups.ContainsKey(f.Key)).ToDictionary(f => f.Key, f => f.Value);
                if (canDeleteFileGroups.Count == 0) break;
            }
            return canDeleteFileGroups.Values.SelectMany(set => set).Distinct().ToHashSet();
        }
    }
}
