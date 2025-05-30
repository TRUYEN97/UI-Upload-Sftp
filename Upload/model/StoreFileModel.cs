using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upload.Model
{
    internal class StoreFileModel : FileModel
    {
        public StoreFileModel()
        {   
        }

        public StoreFileModel(FileModel fileModel) 
        {
            ProgramPath = fileModel?.ProgramPath;
            RemotePath = fileModel?.RemotePath;
            Md5 = fileModel?.Md5;
        }
        public string StorePath { get; set; }
        public string RemoteDir { get; set; }

        internal FileModel FileModel()
        {
            return new FileModel()
            {
                ProgramPath = ProgramPath,
                RemotePath = RemotePath,
                Md5 = Md5
            };
        }
    }
}
