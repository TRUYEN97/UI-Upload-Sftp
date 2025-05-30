
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Upload.Common;
using Upload.Model;

namespace Upload.Service
{
    internal class SftpFileAction
    {
        internal static async Task Open(FileModel fileModel, string localDir)
        {
            try
            {
                string fullPath = Path.GetFullPath(localDir);
                 var storeF = await TranforUtil.Download(fileModel, fullPath, ConstKey.ZIP_PASSWORD);
                if (storeF != null)
                {
                    Util.OpenFile(storeF.StorePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở file: " + ex.Message);
            }
        }
    }
}
