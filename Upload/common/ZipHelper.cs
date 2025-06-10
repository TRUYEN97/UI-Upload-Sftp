using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoDownload.Gui;
using ICSharpCode.SharpZipLib.Zip;

namespace Upload.Common
{
    internal class ZipHelper
    {
        public static async Task ExtractSingleFileFromStream(Stream zipStream, string localPath, string zipPassword = null)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(localPath));
            await Task.Run(() =>
            {
                try
                {
                    using (var zipInput = new ZipInputStream(zipStream))
                    {
                        if (!string.IsNullOrWhiteSpace(zipPassword))
                        {
                            zipInput.Password = zipPassword;
                        }
                        ZipEntry entry = zipInput.GetNextEntry() ?? throw new InvalidOperationException("File ZIP Empty!");
                        if (entry.IsDirectory)
                            throw new InvalidOperationException("First file is Directory");
                        using (var outputStream = File.Create(localPath))
                        {
                            zipInput.CopyTo(outputStream);
                        }
                    }
                }
                catch (ZipException ex)
                {
                    LoggerBox.Addlog($"Extract: {ex.Message}");
                }
            });
        }

        public static async Task ZipSingleFiletoStream(string entryName, Stream zipStream, string localPath, string zipPassword = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var zipOutputStream = new ZipOutputStream(zipStream))
                    {
                        zipOutputStream.SetLevel(9); // Mức nén tối đa
                        if (!string.IsNullOrWhiteSpace(zipPassword))
                        {
                            zipOutputStream.Password = zipPassword;
                        }
                        var entry = new ZipEntry(entryName)
                        {
                            DateTime = File.GetLastWriteTime(localPath)
                        };
                        zipOutputStream.PutNextEntry(entry);
                        using (var fileStream = File.OpenRead(localPath))
                        {
                            fileStream.CopyTo(zipOutputStream);
                        }
                        zipOutputStream.CloseEntry();
                        zipOutputStream.IsStreamOwner = false;
                        zipOutputStream.Close();
                        zipStream.Position = 0;
                    }
                }
                catch (ZipException ex)
                {
                    LoggerBox.Addlog($"Zip: {ex.Message}");
                }
            });
        }

        public static void ExtractZipWithPassword(string zipFilePath, string extractDirectory, string password)
        {
            if (!File.Exists(zipFilePath))
                throw new FileNotFoundException("ZIP file not found.", zipFilePath);
            try
            {
                Directory.CreateDirectory(extractDirectory);

                using (var fs = File.OpenRead(zipFilePath))
                using (var zipStream = new ZipInputStream(fs))
                {
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        zipStream.Password = password;
                    }
                    ZipEntry entry;
                    while ((entry = zipStream.GetNextEntry()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(entry.Name))
                            continue;

                        string fullPath = Path.Combine(extractDirectory, entry.Name);

                        // Chống path traversal
                        if (!fullPath.StartsWith(Path.GetFullPath(extractDirectory), StringComparison.OrdinalIgnoreCase))
                            throw new InvalidOperationException("Path invalid ZIP.");

                        if (entry.IsDirectory)
                        {
                            Directory.CreateDirectory(fullPath);
                            continue;
                        }
                        string dir = Path.GetDirectoryName(fullPath);
                        if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                        {
                            if (File.Exists(dir))
                            {
                                File.Delete(dir);
                            }
                            Directory.CreateDirectory(dir);
                        }
                        using (var outputStream = File.Create(fullPath))
                        {
                            zipStream.CopyTo(outputStream);
                        }
                    }
                }
            }
            catch (ZipException ex)
            {
                LoggerBox.Addlog($"Extract: {ex.Message}");
            }
        }

        public static async Task ExtractSingleFileWithPassword(string zipFilePath, string targetPath, string password = null)
        {
            await Task.Run(() =>
            {
                if (!File.Exists(zipFilePath))
                    throw new FileNotFoundException(zipFilePath);
                try
                {
                    using (var fs = File.OpenRead(zipFilePath))
                    using (var zipStream = new ZipInputStream(fs))
                    {
                        if (!string.IsNullOrWhiteSpace(password))
                        {
                            zipStream.Password = password;
                        }
                        ZipEntry entry;
                        if ((entry = zipStream.GetNextEntry()) != null)
                        {
                            if (entry.IsDirectory)
                            {
                                throw new Exception($"Zip file invailed: {targetPath}");
                            }
                            string dir = Path.GetDirectoryName(targetPath);
                            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                            {
                                if (File.Exists(dir))
                                {
                                    File.Delete(dir);
                                }
                                Directory.CreateDirectory(dir);
                            }
                            using (var outputStream = File.Create(targetPath))
                            {
                                zipStream.CopyTo(outputStream);
                            }
                        }
                    }
                }
                catch (ZipException ex)
                {
                    LoggerBox.Addlog($"Extract: {ex.Message}");
                }
            });
        }

        public static async Task<Stream> JsonAsZipToStream(Stream memStream, string json, string jsonName, string password)
        {
            if (memStream == null)
            {
                return null;
            }
            return await Task.Run(() =>
            {
                using (var zipStream = new ZipOutputStream(memStream))
                {
                    zipStream.SetLevel(9);
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        zipStream.Password = password;
                    }
                    var entry = new ZipEntry(jsonName ?? "data.json");
                    zipStream.PutNextEntry(entry);

                    byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                    zipStream.Write(jsonBytes, 0, jsonBytes.Length);
                    zipStream.CloseEntry();
                    zipStream.IsStreamOwner = false;
                    zipStream.Close();
                    memStream.Position = 0;
                }
                return memStream;
            });
        }

        public static async Task<string> ExtractToJsonString(Stream memStream, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (memStream != null)
                    {
                        memStream.Position = 0;
                        using (var zipStream = new ZipInputStream(memStream))
                        {
                            if (!string.IsNullOrWhiteSpace(password))
                            {
                                zipStream.Password = password;
                            }
                            ZipEntry entry;
                            while ((entry = zipStream.GetNextEntry()) != null)
                            {
                                if (!entry.IsDirectory)
                                {
                                    using (var reader = new StreamReader(zipStream, Encoding.UTF8))
                                    {
                                        return reader.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }

                }
                catch (ZipException ex)
                {
                    LoggerBox.Addlog($"Extract: {ex.Message}");
                }
                return null;
            });
        }

    }
}
