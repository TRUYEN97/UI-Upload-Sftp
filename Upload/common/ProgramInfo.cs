using System.Windows.Forms;

namespace Upload.Common
{
    internal class ProgramInfo
    {
        internal static string ProductVersion => Application.ProductVersion;

        internal static string CompanyName => Application.CompanyName;

        internal static string ProductName => Application.ProductName;

        internal static string CurrentCultureName => Application.CurrentCulture.Name;
    }
}
