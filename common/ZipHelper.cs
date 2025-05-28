using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace Upload.common
{
    internal class ZipHelper
    {
        public static async Task ExtractSingleFileFromStream(Stream zipStream, string localPath, string zipPassword = null)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(localPath));
            await Task.Run(() =>
            {
                using (var zipInput = new ZipInputStream(zipStream))
                {
                    if (!string.IsNullOrWhiteSpace(zipPassword))
                    {
                        zipInput.Password = zipPassword;
                    }
                    ZipEntry entry = zipInput.GetNextEntry() ?? throw new InvalidOperationException("File ZIP rỗng.");
                    if (entry.IsDirectory)
                        throw new InvalidOperationException("File đầu tiên trong ZIP là thư mục.");
                    using (var outputStream = File.Create(localPath))
                    {
                        zipInput.CopyTo(outputStream);
                    }
                }
            });
        }

        public static async Task ZipSingleFiletoStream(string entryName, Stream zipStream, string localPath, string zipPassword = null)
        {
            await Task.Run(() =>
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
                }
            });
        }

        public static void ExtractZipWithPassword(string zipFilePath, string extractDirectory, string password)
        {
            if (!File.Exists(zipFilePath))
                throw new FileNotFoundException("Không tìm thấy file ZIP.", zipFilePath);

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
                        throw new InvalidOperationException("Đường dẫn không hợp lệ trong ZIP.");

                    if (entry.IsDirectory)
                    {
                        Directory.CreateDirectory(fullPath);
                        continue;
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                    using (var outputStream = File.Create(fullPath))
                    {
                        zipStream.CopyTo(outputStream);
                    }
                }
            }
        }

        public static async Task<bool> ExtractSingleFileWithPassword(string zipFilePath, string targetPath, string password = null)
        {
            return await Task.Run(() =>
            {
                if (!File.Exists(zipFilePath))
                    return false;
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
                            return false;
                        using (var outputStream = File.Create(targetPath))
                        {
                            zipStream.CopyTo(outputStream);
                            return true;
                        }
                    }
                }
                return false;
            });
        }
    }
}
