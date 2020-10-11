// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using Marathon.IO.Formats;
using Marathon.IO.Helpers;
using Marathon.Toolkit.Helpers;
using Marathon.Toolkit.Dialogs;
using Marathon.Toolkit.Components;

namespace Marathon.Toolkit.Forms
{
    public partial class ArchiveExplorer : DockContent
    {
        private Archive _LoadedArchive;
        private TreeNode _ActiveNode;

        /// <summary>
        /// The current archive serialised in the TreeView and ListView controls.
        /// </summary>
        public Archive LoadedArchive
        {
            get => _LoadedArchive;

            set
            {
                // Set the initialiser.
                _LoadedArchive = value;

                // Set the title to contain the archive path.
                Text = $"Archive Explorer ({_LoadedArchive.Location})";

                // Load the archive nodes.
                InitialiseDirectoryTree();
            }
        }

        /// <summary>
        /// Populate the specified TreeNode with all sub-directories.
        /// </summary>
        private void InitialiseDirectoryTree()
        {
            // Clear the TreeView.
            TreeView_Explorer.Nodes.Clear();

            // Create the root node.
            TreeNode rootNode = new TreeNode
            {
                Text = Path.GetFileName(LoadedArchive.Location),
                ImageKey = "Folder"
            };

            // If the archive is now empty, create a new root node - otherwise, use the pre-existing one.
            rootNode.Tag = LoadedArchive.Data.Count == 0 ? new ArchiveDirectory(rootNode.Text) : LoadedArchive.Data[0];

            // Add the root node by the name of the archive.
            TreeView_Explorer.Nodes.Add(rootNode);

            // Set active node to root.
            _ActiveNode = rootNode;

            // Recurse through the directory nodes.
            RecurseNodes(rootNode);

            void RecurseNodes(TreeNode child)
            {
                foreach (ArchiveData entry in ((ArchiveDirectory)child.Tag).Data)
                {
                    if (TypeHelper.IsObjectOfType(entry, typeof(ArchiveDirectory)))
                    {
                        TreeNode node = new TreeNode
                        {
                            Text = entry.Name,
                            Tag = entry,
                            ImageKey = "Folder"
                        };

                        child.Nodes.Add(node);

                        // Add child nodes for sub-directories.
                        if (((ArchiveDirectory)entry).Data.OfType<ArchiveDirectory>().Count() != 0)
                            RecurseNodes(node);
                    }
                }
            }

            // Expand root node, because we all like better UX.
            rootNode.Expand();

            // Load the files into the ListView.
            InitialiseFileItems(rootNode.Tag);
        }

        /// <summary>
        /// Gets the data from the specified child node and adds it to the ListView control.
        /// </summary>
        private void InitialiseFileItems(object child)
        {
            if (TypeHelper.IsObjectOfType(child, typeof(ArchiveDirectory)))
            {
                // Clear current directory.
                ListViewDark_Explorer.Items.Clear();

                // Add directory nodes to the ListView control.
                foreach (ArchiveFile entry in ((ArchiveDirectory)child).Data.OfType<ArchiveFile>())
                {
                    ListViewItem node = new ListViewItem(new[]
                    {
                        // Name
                        entry.Name,

                        // Type
                        $"{Path.GetExtension(entry.Name).ToUpper()} File".Substring(1),

                        // Size
                        StringHelper.ByteLengthToDecimalString(entry.UncompressedSize)
                    })
                    {
                        Tag = entry,
                        ImageKey = "File"
                    };

                    ListViewDark_Explorer.Items.Add(node);
                }

                // Set empty directory text visibility.
                Label_DirectoryEmpty.Visible = ListViewDark_Explorer.Items.Count == 0;
            }
        }

        public ArchiveExplorer(Archive file)
        {
            InitializeComponent();

            LoadedArchive = file;
        }

        /// <summary>
        /// Returns whether the file exists or not, with added warning.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private bool ReplaceFileNode(string fileName)
        {
            ArchiveDirectory dir = (ArchiveDirectory)_ActiveNode.Tag;

            if (dir.Data.Any(x => x.Name == fileName))
            {
                DialogResult overwrite =
                    MarathonMessageBox.Show($"The destination already has a file named '{fileName}' - would you like to replace it?",
                                            "Replace Files", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (overwrite == DialogResult.Yes)
                {
                    // Remove any duplicates that somehow managed to get in.
                    dir.Data.RemoveAll(x => x.Name == fileName);

                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Adds a file to the currently active node from a path.
        /// </summary>
        private void AddFile(string file)
        {
            string fileName = Path.GetFileName(file);

            // Ensures the file doesn't exist already.
            if (!ReplaceFileNode(fileName))
                return;

            // Create a new data entry using the location from the input file.
            ((ArchiveDirectory)_ActiveNode.Tag).Data.Add(new ArchiveFile()
            {
                Name = fileName,
                Location = file,
                UncompressedSize = (uint)new FileInfo(file).Length
            });

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);
        }

        /// <summary>
        /// Adds a file to the currently active node from a ListViewItem.
        /// </summary>
        private void AddFile(ListViewItem item)
        {
            ArchiveFile file = (ArchiveFile)item.Tag;

            string fileName = file.Name;

            // Ensures the file doesn't exist already.
            if (!ReplaceFileNode(fileName))
                return;

            byte[] data = file.Data;

            // Create a new data entry using the bytes from the input file.
            ((ArchiveDirectory)_ActiveNode.Tag).Data.Add(new ArchiveFile(fileName, data)
            {
                UncompressedSize = (uint)data.Length
            });

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);
        }

        /// <summary>
        /// Deletes a file from the currently active node.
        /// </summary>
        private void DeleteFile(ListViewItem file)
        {
            // Remove the file entry from the directory entry.
            ((ArchiveDirectory)_ActiveNode.Tag).Data.Remove((ArchiveFile)file.Tag);

            // Remove the item from the active node.
            file.Remove();

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);
        }

        /// <summary>
        /// Renames a file in the currently active node.
        /// </summary>
        private void RenameFile(ListViewItem file, string newName)
        {
            // Change ListViewItem and ArchiveData text.
            file.Text = ((ArchiveData)file.Tag).Name = newName;

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);
        }

        /// <summary>
        /// Renames a directory in the TreeView.
        /// </summary>
        private void RenameFolder(TreeNode dir, string newName)
        {
            // Change TreeNode and ArchiveData text.
            dir.Text = ((ArchiveData)dir.Tag).Name = newName;

            // Refresh the TreeView.
            InitialiseDirectoryTree();
        }

        /// <summary>
        /// Sets the new name of the file after editing the label.
        /// </summary>
        private void ListViewDark_Explorer_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
                RenameFile(ListViewDark_Explorer.Items[e.Item], e.Label);
        }

        /// <summary>
        /// Ensures the node isn't root before editing the label.
        /// </summary>
        private void TreeView_Explorer_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
            => e.CancelEdit = e.Node.Parent == null;

        /// <summary>
        /// Sets the new name of the directory after editing the label.
        /// </summary>
        private void TreeView_Explorer_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
                RenameFolder(e.Node, e.Label);
        }

        /// <summary>
        /// Perform tasks upon clicking a ListViewItem.
        /// </summary>
        private void ListViewDark_Explorer_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    // Add context menu item.
                    menu.Items.Add(new ToolStripMenuItem("Add", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_AddFile)), delegate
                    {
                        OpenFileDialog openDialog = new OpenFileDialog
                        {
                            Title = "Please select a file...",
                            Filter = "All files (*.*)|*.*",
                            Multiselect = true
                        };

                        if (openDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Iterate through selection.
                            foreach (string file in openDialog.FileNames)
                                AddFile(file);
                        }
                    }));

                    // Import context menu item.
                    menu.Items.Add(new ToolStripMenuItem("Import", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_OpenFolder)), delegate
                    {
                        OpenFolderDialog folderDialog = new OpenFolderDialog
                        {
                            Title = "Please select a folder..."
                        };

                        // Import the selected directory.
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Add the selected path to the new node.
                            LoadedArchive.AddDirectory(folderDialog.SelectedPath, (ArchiveDirectory)_ActiveNode.Tag, false);

                            // Refresh current node.
                            InitialiseFileItems(_ActiveNode.Tag);
                        }
                    }));

                    // Get the selected item at the current X/Y position.
                    ListViewItem selected = ListViewDark_Explorer.GetItemAt(e.X, e.Y);

                    /* WinForms is dumb and updates the ListViewItem selection state lazily when right-clicking.
                       This is a workaround for that to ensure at least one item is selected.
                       Just checking the count doesn't work and requires two right-clicks, which is painful. */
                    if (selected != null)
                    {
                        // Gotta make things look pretty.
                        menu.Items.Add(new ToolStripSeparator());

                        // Extract context menu item.
                        menu.Items.Add(new ToolStripMenuItem("Extract", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Export)), delegate
                        {
                            /* These switches will be used frequently in cases where items can be operated on,
                               either as a single element or multiple. If we want to change the behaviour of either
                               operation, we can do so this way. */
                            switch (ListViewDark_Explorer.SelectedItems.Count)
                            {
                                // Just a single item was selected.
                                case 1:
                                {
                                    // Store file for later.
                                    var file = (ArchiveFile)ListViewDark_Explorer.SelectedItems[0].Tag;

                                    SaveFileDialog saveDialog = new SaveFileDialog
                                    {
                                        Title = "Extract",
                                        Filter = "All files (*.*)|*.*",
                                        FileName = file.Name
                                    };

                                    // Extract single selected item.
                                    if (saveDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        File.WriteAllBytes(saveDialog.FileName, file.Decompress(LoadedArchive.Location, file));
                                    }

                                    break;
                                }

                                // More than one item was selected.
                                default:
                                {
                                    OpenFolderDialog folderDialog = new OpenFolderDialog
                                    {
                                        Title = "Extract"
                                    };

                                    // Extract all selected items.
                                    if (folderDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        foreach (ListViewItem selected in ListViewDark_Explorer.SelectedItems)
                                        {
                                            // Store file for later.
                                            var file = (ArchiveFile)selected.Tag;

                                            File.WriteAllBytes(Path.Combine(folderDialog.SelectedPath, ((ArchiveFile)selected.Tag).Name),
                                                               file.Decompress(LoadedArchive.Location, (ArchiveFile)selected.Tag));
                                        }
                                    }

                                    break;
                                }
                            }
                        }));

                        // Delete context menu item.
                        menu.Items.Add(new ToolStripMenuItem("Delete", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_RemoveFile)), delegate
                        {
                            switch (ListViewDark_Explorer.SelectedItems.Count)
                            {
                                // Just a single item was selected.
                                case 1:
                                {
                                    DialogResult confirmSingleDelete =
                                        MarathonMessageBox.Show($"Are you sure that you want to permanently delete '{selected.Text}?'",
                                                                "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    // Delete single selected item.
                                    if (confirmSingleDelete == DialogResult.Yes)
                                    {
                                        DeleteFile(selected);
                                    }

                                    break;
                                }

                                // More than one item was selected.
                                default:
                                {
                                    DeleteFiles();

                                    break;
                                }
                            }
                        }));

                        // Yet another confusing looking workaround for another dumb WinForms bug.
                        if (ListViewDark_Explorer.SelectedItems.Count < 2)
                        {
                            // Rename context menu item.
                            menu.Items.Add(new ToolStripMenuItem("Rename",
                                           Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Rename)),
                                           delegate { selected.BeginEdit(); }));
                        }
                    }

                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }

        /// <summary>
        /// Deletes all selected files in the ListView.
        /// </summary>
        private void DeleteFiles()
        {
            DialogResult confirmMultiDelete =
                MarathonMessageBox.Show($"Are you sure that you want to permanently delete these {ListViewDark_Explorer.SelectedItems.Count} items?",
                                        "Delete Multiple Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Delete all selected items.
            if (confirmMultiDelete == DialogResult.Yes)
            {
                foreach (ListViewItem selected in ListViewDark_Explorer.SelectedItems)
                {
                    DeleteFile(selected);
                }
            }
        }

        /// <summary>
        /// Deletes the selected folder in the TreeView.
        /// </summary>
        private void DeleteFolder()
        {
            DialogResult confirmFolderDelete =
                MarathonMessageBox.Show($"Are you sure that you want to permanently delete this folder?",
                                        "Delete Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Delete selected item.
            if (confirmFolderDelete == DialogResult.Yes)
            {
                // Store the directory and its parent.
                var dir = (ArchiveDirectory)TreeView_Explorer.SelectedNode.Tag;
                var parent = (ArchiveDirectory)dir.Parent;

                // Remove the item from the parent.
                parent.Data.Remove(dir);

                // Reload the TreeView.
                InitialiseDirectoryTree();
            }
        }

        /// <summary>
        /// Perform tasks upon clicking a TreeNode.
        /// </summary>
        private void TreeView_Explorer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Fucking WinForms...
            TreeView_Explorer.SelectedNode = e.Node;

            // Store the directory for later.
            ArchiveDirectory dir = (ArchiveDirectory)e.Node.Tag;

            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    // Add context menu item.
                    menu.Items.Add(new ToolStripMenuItem("Add", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_AddFile)), delegate
                    {
                        // Create new directory node.
                        TreeNode dirNode = new TreeNode()
                        {
                            Text = "New folder"
                        };

                        // Set the tag to the new directory.
                        dirNode.Tag = new ArchiveDirectory(dirNode.Text);

                        // Add node to the TreeView.
                        e.Node.Nodes.Add(dirNode);

                        // Add node tag to the archive as a directory.
                        dir.Data.Add((ArchiveDirectory)dirNode.Tag);

                        // Select the new node.
                        TreeView_Explorer.SelectedNode = dirNode;

                        // Enter edit mode.
                        dirNode.BeginEdit();

                        // Navigates to the selected node if valid.
                        InitialiseFileItems(dirNode.Tag);
                    }));

                    // Import context menu item.
                    menu.Items.Add(new ToolStripMenuItem("Import", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_OpenFolder)), delegate
                    {
                        OpenFolderDialog folderDialog = new OpenFolderDialog
                        {
                            Title = "Please select a folder..."
                        };

                        // Import the selected directory.
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Confirm the inclusion of subdirectories.
                            bool includeSubDirectories =
                                MarathonMessageBox.Show("Would you like to import the subdirectories?", "Archive Explorer",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                            // Create new directory node.
                            TreeNode dirNode = new TreeNode()
                            {
                                Text = Path.GetFileName(folderDialog.SelectedPath)
                            };

                            // Set the tag to the new directory.
                            dirNode.Tag = new ArchiveDirectory(dirNode.Text);

                            // Add the selected path to the new node.
                            LoadedArchive.AddDirectory(folderDialog.SelectedPath, (ArchiveDirectory)dirNode.Tag, includeSubDirectories);

                            // Add node to the TreeView.
                            e.Node.Nodes.Add(dirNode);

                            // Add node tag to the archive as a directory.
                            dir.Data.Add((ArchiveDirectory)dirNode.Tag);

                            // Select the new node.
                            TreeView_Explorer.SelectedNode = dirNode;

                            // Enter edit mode.
                            dirNode.BeginEdit();

                            // Refresh views.
                            InitialiseDirectoryTree();
                            InitialiseFileItems(dirNode.Tag);
                        }
                    }));

                    // Prettify the context menu. :)
                    menu.Items.Add(new ToolStripSeparator());

                    // Extract context menu item.
                    menu.Items.Add(new ToolStripMenuItem("Extract",
                                   Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Export)),
                                   delegate { ExtractDialog(dir); }));

                    // Delete context menu item.
                    menu.Items.Add(new ToolStripMenuItem("Delete",
                                   Resources.LoadBitmapResource(nameof(Properties.Resources.Task_RemoveFile)),
                                   delegate { DeleteFolder(); }));

                    // If the parent is null, we're at root and shouldn't allow renaming.
                    if (e.Node.Parent != null)
                    {
                        // Rename context menu item.
                        menu.Items.Add(new ToolStripMenuItem("Rename",
                                       Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Rename)),
                                       delegate { e.Node.BeginEdit(); }));
                    }

                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }

        /// <summary>
        /// Perform tasks upon double clicking a TreeNode.
        /// </summary>
        private void TreeView_Explorer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    // Update active node.
                    _ActiveNode = e.Node;

                    // Navigates to the selected node if valid.
                    InitialiseFileItems(e.Node.Tag);

                    // Set expanded state.
                    e.Node.Expand();

                    break;
                }
            }
        }

        /// <summary>
        /// Extracts the archive to the selected location.
        /// </summary>
        private void ExtractDialog(ArchiveDirectory directory)
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog
            {
                Title = "Extract"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
                directory.Extract(folderDialog.SelectedPath, true, LoadedArchive.Location);
        }

        /// <summary>
        /// Group event for MenuStripDark_Main items.
        /// </summary>
        private void MenuStripDark_Main_File_Click_Group(object sender, EventArgs e)
        {
            // Displays the extract dialog.
            if (sender == MenuStripDark_Main_File_Extract)
            {
                ExtractDialog((ArchiveDirectory)LoadedArchive.Data[0]);
            }

            // Saves the loaded archive.
            else if (sender == MenuStripDark_Main_File_Save)
            {
                SaveArchive(LoadedArchive.Location);
            }

            // Saves the loaded archive to the desired location.
            else if (sender == MenuStripDark_Main_File_SaveAs)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Title = "Save As",
                    Filter = Program.FileTypes[".arc"]
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveArchive(saveDialog.FileName);
                }
            }

            // Closes the form.
            else if (sender == MenuStripDark_Main_File_Close)
            {
                Close();
            }

            void SaveArchive(string location)
            {
                // Store the current expanded nodes before reloading...
                List<string> storedExpansionState = TreeView_Explorer.Nodes.GetExpansionState();

                // Decompress everything before repacking so we have valid data.
                LoadedArchive.Decompress(ref LoadedArchive.Data);

                // Save the modified archive.
                LoadedArchive.Save(location);

                // Reload the archive.
                LoadedArchive = LoadedArchive.Reload();

                // Restore expanded nodes.
                TreeView_Explorer.Nodes.SetExpansionState(storedExpansionState);
            }
        }

        /// <summary>
        /// Disposes the loaded archive upon exit.
        /// </summary>
        private void ArchiveExplorer_FormClosing(object sender, FormClosingEventArgs e) => LoadedArchive.Dispose();

        /// <summary>
        /// Gets the data dropped onto the window.
        /// </summary>
        private void ListViewDark_Explorer_DragDrop(object sender, DragEventArgs e)
        {
            // Dragged content is a file or collection of files.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Add all files to the current node.
                foreach (string file in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    AddFile(file);
                }
            }

            // Dragged content is a ListViewItem or collection of ListViewItems.
            else if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
            {
                // Add all ListViewItems to the current node.
                foreach (ListViewItem item in (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)))
                {
                    AddFile(item);
                }
            }
        }

        /// <summary>
        /// Changes the cursor if data is present.
        /// </summary>
        private void ListViewDark_Explorer_DragEnter(object sender, DragEventArgs e) => e.Effect = e.AllowedEffect;

        /// <summary>
        /// Events for key presses on ListViewDark_Explorer.
        /// </summary>
        private void ListViewDark_Explorer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Delete all selected files.
                DeleteFiles();
            }
        }

        /// <summary>
        /// Events for key presses on TreeView_Explorer.
        /// </summary>
        private void TreeView_Explorer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Delete selected folder.
                DeleteFolder();
            }
        }

        /// <summary>
        /// Initialises the drag and drop events for ListViewItems.
        /// </summary>
        private void ListViewDark_Explorer_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Decompress all files.
            foreach (ListViewItem item in ListViewDark_Explorer.SelectedItems)
            {
                // Store node for easier reference.
                ArchiveFile file = (ArchiveFile)item.Tag;

                // Decompress current file.
                file.Data = file.Decompress(_LoadedArchive.Location, file);
            }

            // Start file dragging.
            ListViewDark_Explorer.DoDragDrop(ListViewDark_Explorer.SelectedItems, DragDropEffects.All);
        }

        /// <summary>
        /// Displays a tool tip for the hovered item.
        /// </summary>
        private void ListViewDark_Explorer_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            ArchiveFile file = (ArchiveFile)e.Item.Tag;

            // This is rather self-explanatory.
            e.Item.ToolTipText = $"Type: {Path.GetExtension(file.Name).ToUpper().Substring(1)} File\n" +
                                 $"Size: {StringHelper.ByteLengthToDecimalString(file.UncompressedSize)}";
        }

        /// <summary>
        /// Displays a tool tip for the hovered node.
        /// </summary>
        private void TreeView_Explorer_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            ArchiveDirectory dir = (ArchiveDirectory)e.Node.Tag;

            // Lists of directories and files in the root of the current node.
            string folders = string.Join(", ", dir.Data.OfType<ArchiveDirectory>().Select(x => x.Name)),
                   files   = string.Join(", ", dir.Data.OfType<ArchiveFile>().Select(x => x.Name));

            // Common length to truncate the above strings.
            int truncateLength = 45;

            // This is rather self-explanatory.
            e.Node.ToolTipText = $"Size: {StringHelper.ByteLengthToDecimalString(dir.TotalContentsSize)}\n" +
                                 (folders.Length == 0 ? string.Empty : $"Folders: {StringHelper.Truncate(folders, truncateLength)}\n") +
                                 (files.Length == 0 ? string.Empty : $"Files: {StringHelper.Truncate(files, truncateLength)}");
        }
    }
}
