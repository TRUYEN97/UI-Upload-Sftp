using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Upload.Common;
using Upload.Model;
using System.IO;
using Upload.gui;
using System.Drawing;
using System.Runtime.InteropServices;
using AutoDownload.Gui;
using System.Threading;
using static Upload.Service.LockManager;
using Upload.Config;

namespace Upload.Service
{
    internal class MyTreeActional
    {
        private static readonly Color FILE_UPDATE_COLOR = Color.Yellow;
        private static readonly Color FILE_RENAME_COLOR = Color.Orange;
        private static readonly Color FILE_CREATE_COLOR = Color.Green;
        private static readonly Color FOLDER_UPDATE_COLOR = Color.YellowGreen;
        private static readonly Color FOLDER_RENAME_COLOR = Color.OrangeRed;
        private static readonly Color FOLDER_CREATE_COLOR = Color.DarkBlue;
        private static readonly Color FILE_COLOR = Color.AntiqueWhite;
        private static readonly Color FOLDER_COLOR = Color.Black;
        private static readonly Color TREE_BACK_COLOR = Color.Gray;
        private static readonly Color TREE_LINE_COLOR = Color.Yellow;
        private static readonly Font FILE_FONT = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        private static readonly Font FOLDER_FONT = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        private static readonly string FOLDER_KEY = "folder";

        private readonly TreeView _treeView;
        private readonly ImageList _imageList;

        public List<FileModel> RemoveFileModel { get; }

        public string RemoteDir { get; set; }
        public MyTreeActional(TreeView treeView)
        {
            this._treeView = treeView;
            this._treeView.BackColor = TREE_BACK_COLOR;
            this._treeView.LineColor = TREE_LINE_COLOR;
            _imageList = new ImageList();
            RemoveFileModel = new List<FileModel>();
            _treeView.ImageList = _imageList;
            if (!_imageList.Images.ContainsKey(FOLDER_KEY))
            {
                _imageList.Images.Add(FOLDER_KEY, SystemIcons.Information);
            }
        }


        internal async Task AddFolderToNodeIterative(TreeNodeCollection rootNodes, string rootFolderPath)
        {
            try
            {
                using (ProgressDialogForm form = new ProgressDialogForm($"Add folder: {rootFolderPath}"))
                {
                    await form.DoworkAsync(async (report, token) =>
                    {
                        await Task.Run(() =>
                        {
                            int count = 0;
                            CursorUtil.SetCursorIs(Cursors.WaitCursor);
                            ConfirmOverrideForm confirmOverrideFile = new ConfirmOverrideForm();
                            var stack = new Stack<(TreeNodeCollection nodes, string path)>();
                            stack.Push((rootNodes, rootFolderPath));
                            bool checkUnique = FindFolder(rootNodes, Path.GetFileName(rootFolderPath)) != null;
                            while (stack.Count > 0)
                            {
                                if (token.IsCancellationRequested)
                                    return;
                                var (currentNodes, currentPath) = stack.Pop();
                                string folderName = Path.GetFileName(currentPath);
                                TreeNode folderNode = new TreeNode(folderName);

                                folderNode = AddNewFolderNode(currentNodes, folderNode, checkUnique);
                                TreeNodeCollection childNodes = folderNode.Nodes;
                                foreach (string file in Directory.GetFiles(currentPath))
                                {
                                    report?.Invoke(++count, file);
                                    AddNewMode(childNodes, file, confirmOverrideFile, checkUnique);
                                    if (token.IsCancellationRequested)
                                        return;
                                }
                                foreach (string subfolder in Directory.GetDirectories(currentPath))
                                {
                                    stack.Push((childNodes, subfolder));
                                    if (token.IsCancellationRequested)
                                        return;
                                }
                            }
                        });
                    });
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

        internal async Task Download(string folderPath, List<FileModel> fileModels, string zipPassword)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                Util.ShowMessager($"Download to folder: {folderPath}");
                using (ProgressDialogForm form = new ProgressDialogForm($"Download to folder: {folderPath}"))
                {
                    await form.DoworkAsync(async (report, token) =>
                    {
                        if (fileModels == null || fileModels.Count == 0)
                        {
                            return;
                        }
                        form.Maximum = fileModels.Count;
                        int count = 0;
                        foreach (FileModel fileModel in fileModels)
                        {
                            if (token.IsCancellationRequested)
                            {
                                return;
                            }
                            report(++count, fileModel.ProgramPath);
                            await DownloadFileModel(folderPath, fileModel, zipPassword);
                        }
                        Util.ShowMessager($"Download to folder: {folderPath} done.");
                    });
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

        internal async Task<List<FileModel>> GetFileModels(TreeNode selectedNode)
        {
            if (selectedNode?.Nodes?.Count > 0)
            {
                return await GetAllLeafNodes(selectedNode.Nodes);
            }
            else
            {
                var list = new List<FileModel>();
                if (selectedNode?.Tag is FileModel fileModel)
                {
                    list.Add(fileModel);
                }
                return list;
            }
        }

        private async Task DownloadFileModel(string folderPath, FileModel fileModel, string zipPassword)
        {
            await Task.Run(async () =>
            {
                string fullPath = Path.GetFullPath(folderPath);
                if (fileModel is StoreFileModel storeFileModel)
                {
                    string targetPath = Path.Combine(fullPath, storeFileModel.ProgramPath);
                    if (!File.Exists(targetPath) || !Util.GetMD5HashFromFile(storeFileModel.StorePath).Equals(Util.GetMD5HashFromFile(targetPath)))
                    {
                        Util.ShowMessager($"Copy: {fileModel.ProgramPath} -> {targetPath}");
                        Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                        File.Copy(storeFileModel.StorePath, targetPath);
                    }
                }
                else
                {
                    Util.ShowMessager($"Download: {fileModel.ProgramPath}");
                    await TranferUtil.Download(fileModel, fullPath, zipPassword);
                }
            });
        }


        internal async Task AddFilesToNodeIterative(TreeNodeCollection nodes, string[] fileNames)
        {
            try
            {
                using (ProgressDialogForm form = new ProgressDialogForm("Add Files"))
                {
                    await form.DoworkAsync(async (report, token) =>
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                LockManager.Instance.SetLock(true, Reasons.LOCK_UPDATE);
                                ConfirmOverrideForm confirmOverrideFile = new ConfirmOverrideForm();
                                foreach (string filePath in fileNames)
                                {
                                    AddNewMode(nodes, filePath, confirmOverrideFile);
                                }
                            }
                            finally
                            {
                                LockManager.Instance.SetLock(false, Reasons.LOCK_UPDATE);
                            }
                        });
                    });
                }
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }


        internal TreeNode AddFolder(TreeNodeCollection nodes, TreeNode folderNode)
        {
            folderNode.ForeColor = FOLDER_COLOR;
            return AddFolderNode(nodes, folderNode, false);
        }

        private TreeNode AddFolderNode(TreeNodeCollection nodes, TreeNode folderNode, bool checkUnique = true)
        {
            folderNode.ImageKey = FOLDER_KEY;
            folderNode.SelectedImageKey = FOLDER_KEY;
            TreeNode oldNode = checkUnique ? FindFolder(nodes, folderNode.Text) : null;
            Util.SafeInvoke(_treeView, () =>
            {
                folderNode.NodeFont = FOLDER_FONT;
                if (oldNode == null)
                {
                    nodes.Add(folderNode);
                }
                else
                {
                    folderNode = oldNode;
                    folderNode.ForeColor = FOLDER_UPDATE_COLOR;
                }
            });
            return folderNode;
        }

        private void AddNewMode(TreeNodeCollection nodes, string filePath, ConfirmOverrideForm confirmOverrideFile = null, bool checkUnique = true)
        {
            string fileName = Path.GetFileName(filePath);
            TreeNode node = new TreeNode(fileName);
            StoreFileModel fileModel = new StoreFileModel();
            AddFileNode(nodes, node, fileModel, confirmOverrideFile, checkUnique);
            fileModel.StorePath = filePath;
            fileModel.RemoteDir = RemoteDir;
            string fullPath = node.Parent?.FullPath;
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                fileModel.ProgramPath = fileName;
            }
            else
            {
                fileModel.ProgramPath = Path.Combine(fullPath, fileName);
            }
        }

        internal TreeNode AddNewFolderNode(TreeNodeCollection nodes, TreeNode newNode, bool checkUnique)
        {
            newNode.ForeColor = FOLDER_CREATE_COLOR;
            return AddFolderNode(nodes, newNode, checkUnique);
        }

        internal async Task OpenFile(TreeNode node)
        {
            if (node?.Tag is FileModel fileModel)
            {
                if (fileModel is StoreFileModel storeFileModel && File.Exists(storeFileModel.StorePath))
                {
                    Util.OpenFile(storeFileModel.StorePath);
                }
                else
                {
                    await SftpFileAction.Open(fileModel, AutoDLConfig.ConfigModel.TempDir);
                }
            }
        }

        internal async Task Delete(TreeNode selectedNode)
        {
            using (ProgressDialogForm form = new ProgressDialogForm("Delete Files"))
            {
                List<FileModel> nodes = await GetFileModels(selectedNode);
                if (nodes.Count > 0)
                {
                    form.Maximum = nodes.Count;
                    List<FileModel> toRemove = await form.DoworkAsync(async (report, token) =>
                    {
                        return await GetRemoveFileModels(report, nodes, token);
                    });
                    if (toRemove?.Count > 0)
                    {
                        RemoveFileModel.AddRange(toRemove);
                    }
                    Util.SafeInvoke(_treeView, () =>
                    {
                        selectedNode.Remove();
                    });
                    Util.ShowMessager($"Đã bỏ [{selectedNode.Text}] ra khỏi danh sách");
                }
            }
        }

        private static async Task<List<FileModel>> GetRemoveFileModels(Action<int, string> report, List<FileModel> nodes, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                int count = 0;
                List<FileModel> rm = new List<FileModel>();
                foreach (FileModel filemodel in nodes)
                {
                    if (!(filemodel is StoreFileModel))
                    {
                        report(++count, filemodel.ProgramPath);
                        rm.Add(filemodel);
                        Util.ShowMessager($"Đã bỏ [{filemodel.ProgramPath}] ra khỏi danh sách");
                    }
                    if (token.IsCancellationRequested)
                    {
                        Util.ShowMessager("Cancel!");
                        return null;
                    }
                }
                return rm;
            });
        }

        internal void AddFileNode(TreeNodeCollection nodes, TreeNode fileNode, FileModel fileModel, ConfirmOverrideForm confirmOverrideFile = null, bool checkUnique = true)
        {
            string text = fileNode.Text;
            string ext = Path.GetExtension(text).ToLower();
            if (string.IsNullOrEmpty(ext))
            {
                ext = "__no_ext__";
            }
            if (!_imageList.Images.ContainsKey(ext))
            {
                Util.SafeInvoke(_treeView, () =>
                {
                    try
                    {
                        Icon icon = GetIconForExtension(text);
                        _imageList.Images.Add(ext, icon);
                    }
                    catch
                    {
                        _imageList.Images.Add(ext, SystemIcons.Application.ToBitmap());
                    }
                });
            }
            fileNode.ImageKey = ext;
            fileNode.SelectedImageKey = ext;
            fileNode.Tag = fileModel;
            TreeNode oldNode = checkUnique ? FindFile(nodes, fileNode.Text) : null;
            Util.SafeInvoke(_treeView, () =>
            {
                fileNode.NodeFont = FILE_FONT;
                if (oldNode == null)
                {
                    if (fileNode.Tag is StoreFileModel)
                    {
                        fileNode.ForeColor = FILE_CREATE_COLOR;
                    }
                    else
                    {
                        fileNode.ForeColor = FILE_COLOR;
                    }
                    nodes.Add(fileNode);
                }
                else if (confirmOverrideFile == null || confirmOverrideFile.IsAccept($"\"{text}\" đã tồn tại!\r\nBạn muốn update file này không?"))
                {
                    fileNode.ForeColor = FILE_UPDATE_COLOR;

                    if (oldNode.TreeView != null && oldNode.Parent != null)
                    {
                        RemoveFileModel.Add((FileModel)oldNode.Tag);
                        oldNode.Remove();
                        oldNode = null;
                    }
                    else if (oldNode.TreeView != null && oldNode.Parent == null)
                    {
                        RemoveFileModel.Add((FileModel)oldNode.Tag);
                        _treeView.Nodes.Remove(oldNode);
                        oldNode = null;
                    }

                    nodes.Add(fileNode);
                }
            });

        }

        internal void RenameNode(TreeNode rootNode, string newName)
        {
            Task.Run(() =>
            {
                try
                {
                    CursorUtil.SetCursorIs(Cursors.WaitCursor);

                    var parent = rootNode.Parent;
                    if (parent != null)
                    {
                        var oldNode = FindFolder(parent.Nodes, newName) ?? FindFile(parent.Nodes, newName);
                        if (oldNode != null)
                        {
                            LoggerBox.Addlog($"Tên:{newName} trùng với tên đường dẫn là {oldNode.FullPath}");
                        }
                    }

                    Util.ShowMessager($"Rename: {rootNode.Text} -> {newName}");
                    Util.SafeInvoke(_treeView, () =>
                    {
                        rootNode.Text = newName;
                        if (rootNode.Tag is FileModel)
                        {
                            rootNode.ForeColor = FILE_RENAME_COLOR;
                        }
                        else
                        {
                            rootNode.ForeColor = FOLDER_RENAME_COLOR;
                        }
                    });
                    var stack = new Stack<TreeNode>();
                    stack.Push(rootNode);

                    while (stack.Count > 0)
                    {
                        TreeNode current = stack.Pop();
                        if (current.Tag is FileModel model)
                        {
                            model.ProgramPath = current.FullPath;
                            current.ForeColor = FILE_RENAME_COLOR;
                        }
                        else
                        {
                            current.ForeColor = FOLDER_RENAME_COLOR;
                            for (int i = current.Nodes.Count - 1; i >= 0; i--)
                            {
                                stack.Push(current.Nodes[i]);
                            }
                        }
                    }
                    Util.ShowMessager($"Rename: {rootNode.Text} -> {newName} done");
                }
                finally
                {
                    CursorUtil.SetCursorIs(Cursors.Default);
                }
            });
        }

        internal async Task<List<FileModel>> GetAllLeafNodes(TreeNodeCollection nodes)
        {
            try
            {
                CursorUtil.SetCursorIs(Cursors.WaitCursor);
                List<FileModel> leafNodes = new List<FileModel>();
                if (nodes == null) return leafNodes;
                Stack<TreeNode> stack = new Stack<TreeNode>();
                return await Task.Run(() =>
                {
                    // Đẩy tất cả node gốc vào stack
                    foreach (TreeNode node in nodes)
                    {
                        stack.Push(node);
                    }

                    // Duyệt theo chiều sâu
                    while (stack.Count > 0)
                    {
                        TreeNode currentNode = stack.Pop();

                        if (currentNode.Nodes.Count == 0 && currentNode.Tag is FileModel fileModel)
                        {
                            leafNodes.Add(fileModel); // Là node lá và có Tag là FileModel
                        }
                        else
                        {
                            foreach (TreeNode child in currentNode.Nodes)
                            {
                                stack.Push(child); // Thêm các node con vào stack
                            }
                        }
                    }
                    return leafNodes;
                });
            }
            finally
            {
                CursorUtil.SetCursorIs(Cursors.Default);
            }
        }

        internal TreeNode FindFolder(TreeNodeCollection nodes, string text)
        {
            TreeNode oldNode = null;
            foreach (TreeNode node in nodes)
            {
                if (node.Text.Equals(text, StringComparison.OrdinalIgnoreCase) && !(node.Tag is FileModel) && node.ImageKey == FOLDER_KEY)
                {
                    oldNode = node;
                    break;
                }
            }
            return oldNode;
        }
        internal TreeNode FindFile(TreeNodeCollection nodes, string text)
        {
            TreeNode oldNode = null;
            foreach (TreeNode node in nodes)
            {
                if (node.Text.Equals(text, StringComparison.OrdinalIgnoreCase) && node.Tag is FileModel)
                {
                    oldNode = node;
                    break;
                }
            }
            return oldNode;
        }

        internal TreeNodeCollection GetNodeCollection()
        {
            TreeNodeCollection nodes;
            if (_treeView.SelectedNode != null)
            {
                if (_treeView.SelectedNode.Tag is FileModel)
                {
                    return null;
                }
                nodes = _treeView.SelectedNode.Nodes;
            }
            else
            {
                nodes = _treeView.Nodes;
            }
            return nodes;
        }

        internal string[] GetParts(FileModel fileModel)
        {
            if (fileModel == null || fileModel.ProgramPath == null)
            {
                return new string[] { };
            }
            string normalizedPath = fileModel.ProgramPath.Replace('/', Path.DirectorySeparatorChar)
                                    .Replace('\\', Path.DirectorySeparatorChar);
            return normalizedPath.Split(Path.DirectorySeparatorChar);
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi,
        uint cbFileInfo, uint uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        const uint SHGFI_ICON = 0x000000100;
        const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        internal Icon GetIconForExtension(string fileNameOrExt)
        {
            SHFILEINFO shinfo = new SHFILEINFO();

            SHGetFileInfo(fileNameOrExt, FILE_ATTRIBUTE_NORMAL, ref shinfo,
                (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_USEFILEATTRIBUTES);

            return Icon.FromHandle(shinfo.hIcon);
        }
    }

}
