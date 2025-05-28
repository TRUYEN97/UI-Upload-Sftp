using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDownload.Config;

namespace AutoDownload.Common
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
            return Path.Combine(GetProgramFolderPath(location), $"{location.AppName}.json");
        }

        internal static string GetAppConfigRemotePath(Location location)
        {
            return Path.Combine(GetStationPath(location), "Apps.json");
        }

        internal static string GetCommonPath(Location location)
        {
            return Path.Combine(GetStationPath(location),"Common");
        }
    }
}
