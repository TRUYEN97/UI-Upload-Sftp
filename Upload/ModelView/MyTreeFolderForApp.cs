using System;
using System.Windows.Forms;
using Upload.Model;

namespace Upload.ModelView
{
    internal class MyTreeFolderForApp: MyTreeFolder
    {
        public event Action<FileModel> OnChosseMainFile;
        public event Action<FileModel> OnChosseRunFile;
        public event Action<FileModel> OnChosseCloseFile;
        internal MyTreeFolderForApp(TreeView treeView, string zipPw) : base(treeView, zipPw)
        {
            InitTreeFileContextMenu();
            InitTreeFolderContextMenu();
            InitTreeContextMenu();
        }

        private void InitTreeFileContextMenu()
        {
            _treeFileContextMenu.Items.Add("Set open file", Properties.Resources.LaunchApp, (s, e) =>
            {
                TreeNode node = _myTreeActional.SelectedNode;
                if (node?.Tag is FileModel fileModel)
                {
                    OnChosseRunFile?.Invoke(fileModel);
                }
            });
            _treeFileContextMenu.Items.Add("Set close file", Properties.Resources.Close, (s, e) =>
            {
                TreeNode node = _myTreeActional.SelectedNode;
                if (node?.Tag is FileModel fileModel)
                {
                    OnChosseCloseFile?.Invoke(fileModel);
                }
            });
            _treeFileContextMenu.Items.Add("Get icon of file", Properties.Resources.AddAsIcon, (s, e) =>
            {
                TreeNode node = _myTreeActional.SelectedNode;
                if (node?.Tag is FileModel fileModel)
                {
                    OnChosseMainFile?.Invoke(fileModel);
                }
            });
            _treeFileContextMenu.Items.Add("Open", Properties.Resources.OpenFile, (s, e) =>
            {
                Open();
            });
            _treeFileContextMenu.Items.Add("Download", Properties.Resources.DownloadIcon, async (s, e) =>
            {
                await Download();
            });
            _treeFileContextMenu.Items.Add("Update", Properties.Resources.Update, async (s, e) =>
            {
                Update();
            });
            _treeFileContextMenu.Items.Add("Rename", Properties.Resources.Rename, (s, e) =>
            {
                Rename();
            });
            _treeFileContextMenu.Items.Add("Delete", Properties.Resources.Delete, async (s, e) =>
            {
                await Delete();
            });
        }

        private void InitTreeFolderContextMenu()
        {
            _treeFolderContextMenu.Items.Add("Download", Properties.Resources.DownloadIcon, async (s, e) =>
            {
                await Download();
            });

            _treeFolderContextMenu.Items.Add("Create new folder", Properties.Resources.NewFolder, (s, e) =>
            {
                CreateFolder();

            });

            _treeFolderContextMenu.Items.Add("Rename", Properties.Resources.Rename, (s, e) =>
            {
                Rename();
            });

            _treeFolderContextMenu.Items.Add("Add files", Properties.Resources.AddFile, async (s, e) =>
            {
                await AddFiles();
            });

            _treeFolderContextMenu.Items.Add("Add folder", Properties.Resources.AddFromFolder, async (s, e) =>
            {
                await AddFolder();
            });

            _treeFolderContextMenu.Items.Add("Delete", Properties.Resources.Delete, async (s, e) =>
            {
                await Delete();
            });
        }

        private void InitTreeContextMenu()
        {

            _treeContextMenu.Items.Add("Download", Properties.Resources.DownloadIcon, async (s, e) =>
            {
                await DownloadAll();
            });

            _treeContextMenu.Items.Add("Create new folder", Properties.Resources.NewFolder, (s, e) =>
            {
                CreateFolder();
            });

            _treeContextMenu.Items.Add("Add files", Properties.Resources.AddFile, async (s, e) =>
            {
                await AddFiles();
            });

            _treeContextMenu.Items.Add("Add folder", Properties.Resources.AddFromFolder, async (s, e) =>
            {
                await AddFolder();
            });

        }

    }
}
