using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload.Common;
using AutoDownload.Model;
using Newtonsoft.Json;
using Renci.SshNet;
using Upload.gui;
using Upload.model;

namespace Upload.common
{
    internal class TranforUtil
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
                        Util.ShowMessager(Util.Status.CONNECT_ERR, "");
                        return null;
                    }
                    string remotePath = fileModel.RemotePath;
                    if (!await sftp.Exists(remotePath))
                    {
                        Util.ShowMessager($"Sftp File: {remotePath}, không tồn tại!");
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

        internal static async Task<(bool, string)> UpLoadAppListModel(AppList appList, Location location)
        {
            string appConfigRemotePath = PathUtil.GetAppConfigRemotePath(location);
            return (await UploadModel(appList, appConfigRemotePath), appConfigRemotePath);
        }

        public static async Task<bool> UploadModel(object model, string path)
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
                        Util.ShowMessager(Util.Status.CONNECT_ERR, "");
                        return false;
                    }
                    if (await sftp.WriteAllText(path, JsonConvert.SerializeObject(model, Formatting.Indented)))
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

        public static async Task RemoveRemoteFile(List<FileModel> removeFileModel)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager(Util.Status.CONNECT_ERR, "");
                        return;
                    }
                    foreach (var fileModel in removeFileModel)
                    {

                        if (await sftp.Exists(fileModel.RemotePath) && !await sftp.DeleteFile(fileModel.RemotePath))
                        {
                            Util.ShowMessager($"{fileModel.RemotePath}, xóa thất bại!");
                        }
                        else
                        {
                            Util.ShowMessager($"{fileModel.RemotePath}, xóa Ok!");
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
                                Util.ShowMessager(Util.Status.CONNECT_ERR, "");
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
                                        Util.ShowMessager($"{storeFileModel.StorePath} -> {remotePath}, upload thất bại!");
                                        MessageBox.Show($"{storeFileModel.StorePath} -> {remotePath}, upload thất bại!");
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

        internal static async Task<(AppList, string)> GetAppListModel(Location location)
        {
            string appConfigRemotePath = PathUtil.GetAppConfigRemotePath(location);
            return (await GetModelConfig<AppList>(appConfigRemotePath), appConfigRemotePath);
        }

        internal static async Task<T> GetModelConfig<T>(string path)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                using (var sftp = Util.GetSftpInstance())
                {
                    if (!await sftp.Connect())
                    {
                        Util.ShowMessager(Util.Status.CONNECT_ERR, path);
                        return default;
                    }

                    if (!await sftp.Exists(path))
                    {
                        Util.ShowMessager(Util.Status.LOCATION_ERR, path);
                        return default;
                    }

                    try
                    {
                        string appConfig = await sftp.ReadAllText(path);
                        var result = JsonConvert.DeserializeObject<T>(appConfig);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        string errorStr = $"File appConfig.json tại: {path} có thể bị lỗi format.\r\nHãy kiểm tra thủ công để tránh mất dữ liệu!";
                        throw new Exception(errorStr, ex);
                    }
                }

            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

    }
}
