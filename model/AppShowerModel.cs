using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoDownload.Model;

namespace Upload.model
{
    internal class AppShowerModel
    {
        public AppModel AppModel { get; }
        public List<FileModel> RemoveFileModel { get; }

        public AppShowerModel(AppModel appModel) 
        {
            AppModel = appModel;
            RemoveFileModel = new List<FileModel>();
        }
    }
}
