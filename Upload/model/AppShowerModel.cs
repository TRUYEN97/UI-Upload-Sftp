using System.Collections.Generic;

namespace Upload.Model
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
