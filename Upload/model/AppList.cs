using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upload.model;

namespace AutoDownload.Model
{
    internal class AppList
    {
        public AppList() { 

        }
        public Dictionary <string, string> ProgramPaths {  get; set; } = new Dictionary<string, string>();

    }
}
