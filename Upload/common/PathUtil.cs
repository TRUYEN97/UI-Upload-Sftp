using System.IO;
using Upload.Config;

namespace Upload.Common
{
    internal class PathUtil
    {

        internal static string GetRemotePath()
        {
            return AutoDLConfig.ConfigModel.RemotePath;
        }

        internal static string GetProductPath(Location location)
        {
            return Path.Combine(GetRemotePath(),location.Product);
        }

        internal static string GetStationPath(Location location)
        {
            return Path.Combine(GetProductPath(location),location.Station);
        }

        internal static string GetProgramFolderPath(Location location)
        {
            return Path.Combine(GetStationPath(location), "Program");
        }

        internal static string GetAppModelPath(Location location)
        {
            return Path.Combine(GetProgramFolderPath(location), $"{location.AppName}_AppModel.zip");
        }

        internal static string GetAppAccessUserPath(Location location)
        {
            return Path.Combine(GetProgramFolderPath(location), $"{location.AppName}_AccessUserList.zip");
        }
        
        internal static string GetStationAccessUserPath(Location location)
        {
            return Path.Combine(GetStationPath(location), "AccessUserList.zip");
        }

        internal static string GetAppConfigRemotePath(Location location)
        {
            return Path.Combine(GetStationPath(location), "Apps.zip");
        }

        internal static string GetCommonPath(Location location)
        {
            return Path.Combine(GetStationPath(location),"Common");
        }
    }
}
