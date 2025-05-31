using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDownload.Gui;
using static Upload.Service.LockManager;
using Upload.Config;
using Upload.gui;

namespace Upload.Common
{
    internal class PasswordLocker
    {
        private bool _isLockPasswork = true;
        public bool CheckLock()
        {

            if (!_isLockPasswork)
            {
                _isLockPasswork = true;
            }
            else
            {
                _isLockPasswork = !CheckPassword();
            }
            return _isLockPasswork;
        }

        public static bool CheckPassword()
        {
            string inputPw = InputForm.GetInputPassword("Mật khẩu");
            string password = AutoDLConfig.ConfigModel.Password;
            if (!string.IsNullOrWhiteSpace(inputPw) && Util.GetMD5HashFromString(inputPw).Equals(password))
            {
                LoggerBox.Addlog("Mật khẩu đúng");
                return true;
            }
            else
            {
                LoggerBox.Addlog("Mật khẩu không đúng!");
                return false;
            }
        }
    }
}
