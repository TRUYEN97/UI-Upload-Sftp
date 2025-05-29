using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload.Config;
using AutoDownload.Gui;
using AutoDownload.Model;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;
using Upload.gui;

namespace AutoDownload.Common
{
    public class Util
    {
        public static void SafeInvoke(Control control, Action updateAction)
        {
            if (control == null)
            {
                return;
            }
            if (control.InvokeRequired)
            {
                control.Invoke(updateAction);
            }
            else
            {
                updateAction();
            }
        }

        public static string RunCmdWithConsole(string command, bool isWaitForExit = true)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false,
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                if (isWaitForExit)
                {
                    process.WaitForExit();
                }
                return string.IsNullOrEmpty(error) ? output : error;
            }
        }
        public static string RunCmd(string command, bool isWaitForExit = true)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                if (isWaitForExit)
                {
                    process.WaitForExit();
                }
                return string.IsNullOrEmpty(error) ? output : error;
            }
        }

        internal static string GetMD5HashFromFile(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hash)
                        sb.Append(b.ToString("x2"));
                    return sb.ToString();
                }
            }
        }

        internal static MySftp GetSftpInstance()
        {
            var _configModel = AutoDLConfig.ConfigModel;
            return new MySftp(
                _configModel.SftpConfig.Host,
                _configModel.SftpConfig.Port,
                _configModel.SftpConfig.User,
                _configModel.SftpConfig.Password);
        }
        public enum Status
        {
            VERSION_ERR, LOCATION_ERR, CONNECT_ERR, CONFIG_FAIL,
            SUCCESS, DOWNLOAD_FAIL, WAITING,
            DOWNDLOAD_SUCCESS, ERROR
        }
        internal static void ShowMessager(string mess)
        {
            LoggerBox.Addlog(mess);
        }

        internal static void ShowMessager(Status st, string mess)
        {
            switch (st)
            {
                case Status.VERSION_ERR:
                    LoggerBox.Addlog($"{mess}: Version không hợp lệ!");
                    break;
                case Status.LOCATION_ERR:
                    LoggerBox.Addlog($"{mess}: Đường dẫn không tồn tại!");
                    break;
                case Status.CONNECT_ERR:
                    LoggerBox.Addlog("Không thể kết nối đến server!");
                    break;
                case Status.DOWNDLOAD_SUCCESS:
                    LoggerBox.Addlog($"Download {mess} OK!");
                    break;
                case Status.DOWNLOAD_FAIL:
                    LoggerBox.Addlog($"Download {mess} Failed!");
                    break;
                case Status.WAITING:
                    break;
                case Status.CONFIG_FAIL:
                    LoggerBox.Addlog($"Down load app config failed! {mess}");
                    break;
                case Status.ERROR:
                    LoggerBox.Addlog($"Lỗi! {mess}");
                    break;
                default:
                    break;
            }
        }

        internal static void OpenFile(string storePath)
        {
            RunCmd(storePath, false);
        }

        internal static string GetMD5HashFromString(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); 
                }
                return sb.ToString();
            }
        }

        internal static void SetCursor(Form form, Cursor cursor)
        {
            Util.SafeInvoke(form, () => { form.Cursor = cursor; });
        }
    }
}
