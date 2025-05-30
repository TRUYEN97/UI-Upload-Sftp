using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upload.Model;
using Upload.Service;

namespace AutoDownload.Model
{
    internal class AppList
    {
        public Dictionary <string, ProgramPathModel> ProgramPaths {  get; set; } = new Dictionary<string, ProgramPathModel>();
    }
}
