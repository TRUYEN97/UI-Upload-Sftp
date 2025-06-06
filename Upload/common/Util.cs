﻿using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using AutoDownload.Gui;
using Upload.Config;

namespace Upload.Common
{
    public class Util
    {
        public static void SafeInvoke(Control control, Action updateAction)
        {
            try
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
            catch (Exception)
            {
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

        internal static void ShowMessager(string mess)
        {
            LoggerBox.Addlog(mess);
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
