using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using AutoDownload.Common;
using AutoDownload.Gui;
using Ookii.Dialogs.Wpf;
using Upload.common;
using Upload.gui;
using Upload.model;
using static Upload.Service.LockManager;
using Cursors = System.Windows.Forms.Cursors;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Upload.Service
{
    internal class MyTreeFolder
    {
        private readonly string zipPassword;
        private readonly TreeView treeView;
        private readonly ContextMenuStrip _treeFolderContextMenu;
        private readonly ContextMenuStrip _treeFileContextMenu;
        private readonly ContextMenuStrip _treeContextMenu;
        private readonly MyTreeActional _myTreeActional;
        public event Action<FileModel> OnChosseMainFile;
        public event Action<FileModel> OnChosseRunFile;
        public event Action<FileModel> OnChosseCloseFile;
        private List<TreeNode> selectedNodes = new List<TreeNode>();
        private bool _isEmpty;

        public List<FileModel> RemoveFileModel { get => _myTreeActional.RemoveFileModel; }

        public string RemoteDir { get { return _myTreeActional.RemoteDir; } set { _myTreeActional.RemoteDir = value; } }


        internal MyTreeFolder(TreeView treeView, string zipPw)
        {
            this.zipPassword = zipPw;
            this.treeView = treeView;
            _myTreeActional = new MyTreeActional(treeView);
            _treeFolderContextMenu = new ContextMenuStrip();
            _treeFileContextMenu = new ContextMenuStrip();
            _treeContextMenu = new ContextMenuStrip();
            _isEmpty = true;
            InitTreeFolderContextMenu();
            InitTreeFileContextMenu();
            InitTreeContextMenu();
            this.treeView.NodeMouseDoubleClick += TreeView_NodeMouseDoubleClick;
            //this.treeView.NodeMouseClick += TreeView_NodeMouseClick;
            this.treeView.MouseDown += TreeView_MouseDown;
            this.treeView.ContextMenuStrip = _treeContextMenu;
            /////////////////////////////////////
        }

        private CancellationTokenSource _cts;

        private async Task PopulateTreeView(ICollection<FileModel> fileModels, string remoteDir, CancellationToken token)
        {
            using (ProgressDialogForm form = new ProgressDialogForm("Show")
            {
                Maximum = fileModels.Count
            })
            {
                await form.DoworkAsync(async (report, tk) =>
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            LockManager.Instance.ForceLockAll(Reasons.LOCK_LOAD_FILES);
                            CursorUtil.SetCursorIs(Cursors.WaitCursor);
                            Util.SafeInvoke(treeView, () =>
                            {
                                treeView.Nodes.Clear();
                            });
                            RemoveFileModel.Clear();
                            int count = 0;
                            foreach (FileModel fileModel in fileModels)
                            {
                                if (token.IsCancellationRequested || tk.IsCancellationRequested)
                                    return;

                                string[] parts = _myTreeActional.GetParts(fileModel);
                                TreeNodeCollection nodes = treeView.Nodes;

                                for (int i = 0; i < parts.Length; i++)
                                {
                                    if (token.IsCancellationRequested || tk.IsCancellationRequested)
                                        return;

                                    string part = parts[i];
                                    TreeNode foundNode = nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == part);
                                    if (foundNode == null)
                                    {
                                        foundNode = new TreeNode(part);
                                        bool isFile = (i == parts.Length - 1);
                                        if (isFile)
                                        {
                                            report.Invoke(++count, fileModel.ProgramPath);
                                            _myTreeActional.AddFileNode(nodes, foundNode, fileModel, checkUnique: false);
                                        }
                                        else
                                        {
                                            foundNode = _myTreeActional.AddFolder(nodes, foundNode);
                                        }
                                    }
                                    nodes = foundNode.Nodes;
                                }
                            }
                            Util.SafeInvoke(treeView, () =>
                            {
                                treeView.ExpandAll();
                            });
                            _isEmpty = false;
                            RemoteDir = remoteDir;
                            LockManager.Instance.ForceUnlockAll(Reasons.LOCK_LOAD_FILES);
                        }
                        finally
                        {
                            CursorUtil.SetCursorIs(Cursors.Default);
                        }
                    }, token);
                });
            }
        }

        public void StartPopulate(ICollection<FileModel> fileModels, string remoteDir, CancellationTokenSource cts)
        {
            if (cts == null)
            {
                return;
            }
            _cts = cts;
            _ = PopulateTreeView(fileModels, remoteDir, _cts.Token);
        }

        public void CancelPopulate()
        {
            _cts?.Cancel();
        }

        internal async Task<List<FileModel>> GetAllLeafNodes()
        {
            return await _myTreeActional.GetAllLeafNodes(treeView.Nodes);
        }

        private void InitTreeFileContextMenu()
        {
            _treeFileContextMenu.Items.Add("Mở", null, (s, e) =>
            {
                Open();
            });
            _treeFileContextMenu.Items.Add("Làm file khởi động", null, (s, e) =>
            {
                TreeNode node = treeView.SelectedNode;
                if (node?.Tag is FileModel fileModel)
                {
                    OnChosseRunFile?.Invoke(fileModel);
                }
            });
            _treeFileContextMenu.Items.Add("Làm file đóng", null, (s, e) =>
            {
                TreeNode node = treeView.SelectedNode;
                if (node?.Tag is FileModel fileModel)
                {
                    OnChosseCloseFile?.Invoke(fileModel);
                }
            });
            _treeFileContextMenu.Items.Add("lấy icon", null, (s, e) =>
            {
                TreeNode node = treeView.SelectedNode;
                if (node?.Tag is FileModel fileModel)
                {
                    OnChosseMainFile?.Invoke(fileModel);
                }
            });
            _treeFileContextMenu.Items.Add("Download", null, async (s, e) =>
            {
                await Download();
            });
            _treeFileContextMenu.Items.Add("Rename", null, (s, e) =>
            {
                Rename();
            });
            _treeFileContextMenu.Items.Add("Xóa", null, async (s, e) =>
            {
                await Delete();
            });
        }

        private void InitTreeFolderContextMenu()
        {
            _treeFolderContextMenu.Items.Add("Download", null, async (s, e) =>
            {
                await Download();
            });

            _treeFolderContextMenu.Items.Add("Tạo thư mục", null, (s, e) =>
            {
                CreateFolder();

            });

            _treeFolderContextMenu.Items.Add("Rename", null, (s, e) =>
            {
                Rename();
            });

            _treeFolderContextMenu.Items.Add("Thêm file", null, async (s, e) =>
            {
                await AddFiles();
            });

            _treeFolderContextMenu.Items.Add("Thêm folder", null, async (s, e) =>
            {
                await AddFolder();
            });

            _treeFolderContextMenu.Items.Add("Xóa", null, async (s, e) =>
            {
                await Delete();
            });
        }

        private void InitTreeContextMenu()
        {

            _treeContextMenu.Items.Add("Download", null, (s, e) =>
            {
                DownloadAll();
            });

            _treeContextMenu.Items.Add("Tạo thư mục", null, (s, e) =>
            {
                CreateFolder();
            });

            _treeContextMenu.Items.Add("Thêm file", null, (s, e) =>
            {
                AddFiles();
            });

            _treeContextMenu.Items.Add("Thêm folder", null, (s, e) =>
            {
                AddFolder();
            });

        }

        internal void Clear()
        {
            CancelPopulate();
            _ = Task.Delay(1000);
            Util.SafeInvoke(treeView, () =>
            {
                treeView.Nodes.Clear();
            });
            _isEmpty = true;
        }

        private void Open()
        {
            TreeNode treeNode = treeView.SelectedNode;
            if (treeNode != null)
            {
                Task.Run(async () =>
                {
                    await _myTreeActional.OpenFile(treeNode);
                });
            }
        }

        private async Task DownloadAll()
        {
            TreeNodeCollection nodes = _myTreeActional.GetNodeCollection();
            if (nodes == null) return;
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = fbd.SelectedPath;
                    List<FileModel> fileModels = await GetAllLeafNodes();
                    if (fileModels == null || fileModels.Count == 0) return;
                    await _myTreeActional.Download(folderPath, fileModels, zipPassword);
                }
            }
        }

        private async Task Download()
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode == null) return;
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = fbd.SelectedPath;
                    List<FileModel> fileModels = await _myTreeActional.GetFileModels(selectedNode);
                    if (fileModels == null || fileModels.Count == 0) return;
                    await _myTreeActional.Download(folderPath, fileModels, zipPassword);
                }

            }
        }

        private void Rename()
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode == null) return;
            string newName = InputForm.GetInputString("Tên mới", selectedNode.Text);
            if (newName == null || newName.Equals(selectedNode.Text)) return;
            _myTreeActional.RenameNode(selectedNode, newName);
        }

        private async Task Delete()
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode == null) return;
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa '{selectedNode.Text}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                await _myTreeActional.Delete(selectedNode);
            }
        }

        private async Task AddFolder()
        {
            TreeNodeCollection nodes = _myTreeActional.GetNodeCollection();
            if (nodes == null) return;
            var dialog = new VistaFolderBrowserDialog()
            {
                Description = "Chọn thư mục",
                UseDescriptionForTitle = true,
                Multiselect = true
            };
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    LockManager.Instance.SetLock(true, Reasons.LOCK_UPDATE);
                    foreach (string dir in dialog.SelectedPaths)
                    {
                        await _myTreeActional.AddFolderToNodeIterative(nodes, dir);
                    }
                }
                finally
                {
                    LockManager.Instance.SetLock(false, Reasons.LOCK_UPDATE);
                }
            }
        }

        private async Task AddFiles()
        {
            TreeNodeCollection nodes = _myTreeActional.GetNodeCollection();
            if (nodes == null) return;
            CursorUtil.SetCursorIs(Cursors.WaitCursor);
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string[] fileNames = ofd.FileNames;
                    await _myTreeActional.AddFilesToNodeIterative(nodes, fileNames);
                }
            }
        }

        private void CreateFolder()
        {
            TreeNodeCollection nodes = _myTreeActional.GetNodeCollection();
            if (nodes == null) return;
            string folderName = InputForm.GetInputString("Tên folder");
            if (folderName == null) return;
            var oldNode = _myTreeActional.FindFolder(nodes, folderName) ?? _myTreeActional.FindFile(nodes, folderName);
            if (oldNode != null)
            {
                LoggerBox.Addlog($"Tên:{folderName} trùng với đường dẫn là {oldNode.FullPath}");
            }
            else
            {
                _myTreeActional.AddNewFolderNode(nodes, new TreeNode(folderName), true);
            }
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isEmpty)
            {
                treeView.ContextMenuStrip = null;
                return;
            }
            TreeNode clickedNode = treeView.GetNodeAt(e.X, e.Y);
            treeView.SelectedNode = clickedNode;
            if (clickedNode != null)
            {
                if (clickedNode.Tag is FileModel)
                {
                    treeView.ContextMenuStrip = _treeFileContextMenu;
                }
                else
                {
                    treeView.ContextMenuStrip = _treeFolderContextMenu;
                }
            }
            else
            {
                treeView.ContextMenuStrip = _treeContextMenu;
            }
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Task.Run(async () =>
            {
                await _myTreeActional.OpenFile(e.Node);
            });
        }

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView.SelectedNode = null; // disable default selection

            AddSelectedNode(e.Node);
        }

        private void AddSelectedNode(TreeNode node)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                ToggleSelection(node);
            }
            else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && selectedNodes.Count > 0)
            {
                SelectRange(selectedNodes[selectedNodes.Count - 1], node);
            }
            else
            {
                ClearSelection();
                AddSelection(node);
            }
        }

        private void ToggleSelection(TreeNode node)
        {
            if (selectedNodes.Contains(node))
            {
                selectedNodes.Remove(node);
                node.BackColor = treeView.BackColor;
                node.ForeColor = treeView.ForeColor;
            }
            else
            {
                AddSelection(node);
            }
        }

        private void AddSelection(TreeNode node)
        {
            selectedNodes.Add(node);
            node.BackColor = Color.SlateBlue;
            node.ForeColor = Color.White;
        }

        private void ClearSelection()
        {
            foreach (var node in selectedNodes)
            {
                node.BackColor = treeView.BackColor;
                node.ForeColor = treeView.ForeColor;
            }
            selectedNodes.Clear();
        }

        private void SelectRange(TreeNode start, TreeNode end)
        {
            List<TreeNode> allNodes = GetAllNodes(treeView.Nodes);
            int startIndex = allNodes.IndexOf(start);
            int endIndex = allNodes.IndexOf(end);

            if (startIndex == -1 || endIndex == -1) return;

            ClearSelection();

            int low = Math.Min(startIndex, endIndex);
            int high = Math.Max(startIndex, endIndex);

            for (int i = low; i <= high; i++)
            {
                AddSelection(allNodes[i]);
            }
        }

        private List<TreeNode> GetAllNodes(TreeNodeCollection nodes)
        {
            var result = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                result.Add(node);
                result.AddRange(GetAllNodes(node.Nodes));
            }
            return result;
        }

    }
}
