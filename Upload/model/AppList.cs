using System.Collections.Generic;

namespace Upload.Model
{
    internal class AppList
    {
        public Dictionary <string, ProgramPathModel> ProgramPaths {  get; set; } = new Dictionary<string, ProgramPathModel>();
    }
}
