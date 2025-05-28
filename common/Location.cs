using System;

namespace AutoDownload.Common
{
    public class Location
    {
        public string PcName 
        {
            get
            {
                return PcInfo.PcName;
            }
        }
        private string _product;
        public string Product { get { return _product; } set { _product = value == null ? "" : value.ToUpper(); } }
        private string _station;
        public string Station { get { return _station; } set { _station = value == null ? "" : value.ToUpper(); } }
        private string _appName;
        public string AppName { get { return _appName; } set { _appName = value == null ? "" : value.ToUpper(); } }

        public Location(string product, string station, string version)
        {
            this.Product = product;
            this.Station = station;
            this.AppName = version;
        }
        public Location(Location location)
        {
            this.Product = location.Product;
            this.Station = location.Station;
            this.AppName = location.AppName;
        }
        public Location()
        {
        }
        

    }
}
