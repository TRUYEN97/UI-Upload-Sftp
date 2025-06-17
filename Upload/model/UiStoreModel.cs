using System;
using System.Collections.Generic;

namespace Upload.Model
{
    internal class UiStoreModel
    {
        public string MainPath { get; set; }
        public string RemoteStoreDir { get; set; }
        public HashSet<FileModel> FileModels { get; set; } = new HashSet<FileModel>();
        public string Path { get; set; }
    }
}
