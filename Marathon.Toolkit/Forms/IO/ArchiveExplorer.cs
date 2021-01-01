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
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Marathon.Helpers;
using Marathon.Components;
using Marathon.Toolkit.Helpers;
using Marathon.IO.Formats.Archives;
using ComponentFactory.Krypton.Ribbon;

namespace Marathon.Toolkit.Forms
{
    public partial class ArchiveExplorer : MarathonDockContent
    {
        private List<ListViewItem> _Clipboard = new List<ListViewItem>();

        private Archive _LoadedArchive;
        private TreeNode _ActiveNode;
        private int _EditCount;

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
                Text = $"{Text} ({_LoadedArchive.Location})";

                // Load the archive nodes.
                InitialiseDirectoryTree();
            }
        }

        /// <summary>
        /// Populate the specified TreeNode with all sub-directories.
        /// </summary>
        private void InitialiseDirectoryTree()
        {
            // Store the current expanded nodes before reloading...
            var storedExpansionState = KryptonTreeView_Explorer.GetExpandedNodesState();

            // Clear the TreeView.
            KryptonTreeView_Explorer.Nodes.Clear();

            // Create the root node.
            TreeNode rootNode = new TreeNode
            {
                Text = Path.GetFileName(LoadedArchive.Location),
                ImageKey = "Folder"
            };

            // If the archive is now empty, create a new root node - otherwise, use the pre-existing one.
            rootNode.Tag = LoadedArchive.Data.Count == 0 ? new ArchiveDirectory(rootNode.Text) { IsRoot = true } : LoadedArchive.Data[0];

            // Add the root node by the name of the archive.
            KryptonTreeView_Explorer.Nodes.Add(rootNode);

            // Set active node to root.
            _ActiveNode = rootNode;

            // Recurse through the directory nodes.
            RecurseNodes(rootNode);

            // The name says it all.
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

            // Restore expanded nodes.
            KryptonTreeView_Explorer.RestoreExpandedNodesState(storedExpansionState);
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
                    ListViewItem node = new ListViewItem
                    (
                        new[]
                        {
                            // Name.
                            entry.Name,

                            // Type.
                            Resources.ParseFriendlyNameFromFileExtension(Marathon.Properties.Resources.FileTypes, entry.Name),

                            // Size.
                            StringHelper.ByteLengthToDecimalString(entry.UncompressedSize)
                        }
                    )
                    {
                        Tag = entry,
                        ImageKey = "File"
                    };

                    ListViewDark_Explorer.Items.Add(node);
                }

                // Update enabled state based on current directory.
                UpdateRibbonControls();

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
        /// Returns whether the input file name matches a file that exists in the active node.
        /// </summary>
        private bool ConflictingNameExistsInActiveNode(string name)
            => ((ArchiveDirectory)_ActiveNode.Tag).Data.Any(x => x.Name == name);

        /// <summary>
        /// Returns whether the file exists or not, with added warning.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private bool ReplaceFileNode(string fileName)
        {
            if (ConflictingNameExistsInActiveNode(fileName))
            {
                DialogResult overwrite = MarathonMessageBox.Show
                (
                    $"The destination already has a file named '{fileName}' - would you like to replace it?",
                    "Replace Files",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error
                );

                if (overwrite == DialogResult.Yes)
                {
                    // Remove any duplicates that somehow managed to get in.
                    ((ArchiveDirectory)_ActiveNode.Tag).Data.RemoveAll(x => x.Name == fileName);

                    // Increase the edit count.
                    _EditCount++;

                    // Overwrite the file.
                    return true;
                }

                // Skip adding this file.
                else
                    return false;
            }

            // No duplicate found, proceed as normal.
            else
                return true;
        }

        /// <summary>
        /// Returns the name with a duplicate indicator.
        /// </summary>
        private string AppendDuplicateIdentifier(string fileName)
        {
            string common = " - Copy",
                   dupe = StringHelper.AppendToFileName(fileName, common);

            // Cached result for the while loop when it occurs.
            string cache = dupe;

            int i = 1; // While loop iterations.

            // Recurse adding identifiers until there's no duplicate.
            while (ConflictingNameExistsInActiveNode(cache))
            {
                // Append iteration count, like Windows.
                cache = StringHelper.AppendToFileName(dupe, $" ({i})");

                i++; // Increase iteration count.
            }

            return cache;
        }

        /// <summary>
        /// Adds a file to the currently active node from a path.
        /// </summary>
        private void AddFile(string file, bool pasted = false)
        {
            string fileName = Path.GetFileName(file);

            /* Ensures the file doesn't exist already;
               also checking if the file was pasted so we can add a copy indicator. */
            if (!pasted && !ReplaceFileNode(fileName))
                return;

            // Create a new data entry using the location from the input file.
            ((ArchiveDirectory)_ActiveNode.Tag).Data.Add
            (
                new ArchiveFile()
                {
                    Name = pasted ? AppendDuplicateIdentifier(fileName) : fileName,
                    Location = file,
                    UncompressedSize = (uint)new FileInfo(file).Length,
                    Parent = (ArchiveDirectory)_ActiveNode.Tag
                }
            );

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);

            // Increase the edit count.
            _EditCount++;
        }

        /// <summary>
        /// Adds a file to the currently active node from a ListViewItem.
        /// </summary>
        private void AddFile(ListViewItem item, bool pasted = false)
        {
            ArchiveFile file = (ArchiveFile)item.Tag;

            string fileName = file.Name;

            /* Ensures the file doesn't exist already;
               also checking if the file was pasted so we can add a copy indicator. */
            if (!pasted && !ReplaceFileNode(fileName))
                return;

            byte[] data = file.Data;

            // Create a new data entry using the bytes from the input file.
            ((ArchiveDirectory)_ActiveNode.Tag).Data.Add
            (
                new ArchiveFile(fileName, data)
                {
                    Name = pasted ? AppendDuplicateIdentifier(fileName) : fileName,
                    UncompressedSize = (uint)data.Length,
                    Parent = (ArchiveDirectory)_ActiveNode.Tag
                }
            );

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);

            // Increase the edit count.
            _EditCount++;
        }

        /// <summary>
        /// Requests a path to add files from.
        /// </summary>
        private void AddFileDialog()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Title = "Please select a file...",
                Filter = Program.FileTypes[".*"],
                Multiselect = true
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                // Iterate through selection.
                foreach (string file in openDialog.FileNames)
                    AddFile(file);
            }
        }

        /// <summary>
        /// Adds a new folder to the selected node.
        /// </summary>
        private void AddFolder(TreeNode node, ArchiveDirectory directory)
        {
            // Create new directory node.
            TreeNode dirNode = new TreeNode()
            {
                Text = AppendDuplicateIdentifier("New folder")
            };

            // Set the tag to the new directory.
            dirNode.Tag = new ArchiveDirectory(dirNode.Text);

            // Add node to the TreeView.
            node.Nodes.Add(dirNode);

            // Add node tag to the archive as a directory.
            directory.Data.Add((ArchiveDirectory)dirNode.Tag);

            // Select the new node.
            KryptonTreeView_Explorer.SelectedNode = dirNode;

            // Enter edit mode.
            dirNode.BeginEdit();

            // Navigates to the selected node if valid.
            InitialiseFileItems(dirNode.Tag);

            // Increase the edit count.
            _EditCount++;
        }

        /// <summary>
        /// Copies the selected files to the clipboard.
        /// </summary>
        private void CopyFiles(bool cut = false)
        {
            // Clear the clipboard so we don't have tons of items left over.
            _Clipboard.Clear();

            // Decompress everything so we can copy properly.
            DecompressSelectedItems();

            // Add all ListViewItems to the clipboard.
            foreach (ListViewItem item in ListViewDark_Explorer.SelectedItems)
            {
                // Set the format image to a more transparent version.
                if (cut)
                    item.ImageKey = "PendingFile";

                // Add item to clipboard.
                _Clipboard.Add(item);
            }

            // Enable the Paste button if files were copied successfully.
            KryptonRibbonGroupButton_Clipboard_Paste.Enabled = _Clipboard.Count != 0;
        }

        /// <summary>
        /// Pastes the files from the clipboard to the active node.
        /// </summary>
        private void PasteFiles()
        {
            // Add all clipboard items to the active node.
            foreach (ListViewItem item in _Clipboard)
            {
                /* Easiest way to check and remove files pending a paste;
                   normally wouldn't accept something like a string comparison, but I'd rather do this
                   than convolute things further with another class for enum states. */
                if (item.ImageKey == "PendingFile")
                {
                    // TODO: This is ugly - make this into a function so we can refer back to this another time.
                    ((ArchiveDirectory)((ArchiveFile)item.Tag).Parent).Data.Remove((ArchiveFile)item.Tag);
                }

                // Add to node from clipboard.
                AddFile(item, true);
            }

            // Remove all pending files from the clipboard using the same condition as earlier.
            _Clipboard.RemoveAll(x => x.ImageKey == "PendingFile");

            // Validate enabled state if the files were cut.
            KryptonRibbonGroupButton_Clipboard_Paste.Enabled = _Clipboard.Count != 0;
        }

        /// <summary>
        /// Deletes a file from the currently active node.
        /// </summary>
        private void DeleteFile(ListViewItem file)
        {
            // Remove the file entry from the directory entry.
            ((ArchiveDirectory)_ActiveNode.Tag).Data.Remove((ArchiveFile)file.Tag);

            // Increase the edit count.
            _EditCount++;
        }

        /// <summary>
        /// Deletes all selected files in the ListView.
        /// </summary>
        private void DeleteFiles()
        {
            // Store this for easier reference.
            int selectedCount = ListViewDark_Explorer.SelectedItems.Count;

            /* Change the message based on the amount of selected items.
               Yes, we are just copying Windows. */
            string pluraliseMessage = "Are you sure that you want to permanently delete " +
                                      (selectedCount == 1 ? $"'{ListViewDark_Explorer.SelectedItems[0].Text}?'" : $"these {selectedCount} items?");

            DialogResult confirmMultiDelete = MarathonMessageBox.Show
            (
                pluraliseMessage,
                selectedCount == 1 ? "Delete File" : "Delete Multiple Items", // Change the title too, just like Windows.
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Delete all selected items.
            if (confirmMultiDelete == DialogResult.Yes)
            {
                foreach (ListViewItem selected in ListViewDark_Explorer.SelectedItems)
                {
                    DeleteFile(selected);
                }

                // Refresh current node.
                InitialiseFileItems(_ActiveNode.Tag);
            }
        }

        /// <summary>
        /// Deletes the selected folder in the TreeView.
        /// </summary>
        private void DeleteFolder()
        {
            DialogResult confirmFolderDelete = MarathonMessageBox.Show
            (
                $"Are you sure that you want to permanently delete this folder?",
                "Delete Folder",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Delete selected item.
            if (confirmFolderDelete == DialogResult.Yes)
            {
                // Store the directory and its parent.
                var dir = (ArchiveData)KryptonTreeView_Explorer.SelectedNode.Tag;
                var parent = (ArchiveDirectory)dir.Parent;

                // Remove the item from the parent.
                if (parent != null)
                    parent.Data.Remove(dir);

                // Reload the TreeView.
                InitialiseDirectoryTree();

                // Increase the edit count.
                _EditCount++;
            }
        }

        /// <summary>
        /// Invokes the rename events depending on selection count.
        /// </summary>
        private void InvokeRename()
        {
            // Stored for easier reference.
            int commonCount = ListViewDark_Explorer.SelectedItems.Count;

            if (commonCount == 1)
            {
                // Enter label edit mode.
                ListViewDark_Explorer.SelectedItems[0].BeginEdit();
            }
            else if (commonCount > 1)
            {
                // Launch Bulk Renamer with the ListView control.
                new BulkRenamer(ListViewDark_Explorer).ShowDialog();

                // Refresh current node upon closing.
                InitialiseFileItems(_ActiveNode.Tag);

                // Increase the edit count.
                _EditCount++;
            }
        }

        /// <summary>
        /// Renames a file in the currently active node.
        /// </summary>
        private void RenameFile(ListViewItem file, string newName)
        {
            // Change ArchiveData text and update the ListViewItem text.
            file.Text = ((ArchiveData)file.Tag).Name = newName.TrimEnd('.');

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);

            // Increase the edit count.
            _EditCount++;
        }

        /// <summary>
        /// Renames a directory in the TreeView.
        /// </summary>
        private void RenameFolder(TreeNode dir, string newName)
        {
            // Change ArchiveData text and update the TreeNode text.
            dir.Text = ((ArchiveData)dir.Tag).Name = newName;

            // Reload the TreeView.
            InitialiseDirectoryTree();

            // Increase the edit count.
            _EditCount++;
        }

        /// <summary>
        /// Extracts a single file to the requested directory.
        /// </summary>
        private string ExtractFile(ArchiveFile file)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Title = "Extract",
                Filter = Program.FileTypes[".*"],
                FileName = file.Name
            };

            // Extract single selected item.
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // Write the file to the location.
                File.WriteAllBytes(saveDialog.FileName, file.Decompress(LoadedArchive.Location, file));

                // Return the saved location.
                return saveDialog.FileName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Ensures the node isn't root before editing the label.
        /// </summary>
        private void KryptonTreeView_Explorer_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
            => e.CancelEdit = e.Node.Parent == null;

        /// <summary>
        /// Sets the new name of the directory after editing the label.
        /// </summary>
        private void KryptonTreeView_Explorer_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            /* This is rather counter-intuitive, but it allows
               the node to be updated with the ArchiveData... I hate everything. */
            e.CancelEdit = true;

            // Rename the folder if the string is populated.
            if (!string.IsNullOrEmpty(e.Label))
                RenameFolder(e.Node, e.Label);
        }

        /// <summary>
        /// Sets the new name of the file after editing the label.
        /// </summary>
        private void ListViewDark_Explorer_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // See above.
            e.CancelEdit = true;

            // Rename the file if the string is populated, removing any trailing extension markers.
            if (!string.IsNullOrEmpty(e.Label))
                RenameFile(ListViewDark_Explorer.Items[e.Item], e.Label);
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
                    menu.Items.Add
                    (
                        new ToolStripMenuItem
                        (
                            "Add",
                            Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_AddFile)),

                            delegate
                            {
                                AddFileDialog();
                            }
                        )
                    );

                    // Import context menu item.
                    menu.Items.Add
                    (
                        new ToolStripMenuItem
                        (
                            "Import",
                            Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_OpenFolder)),
                            
                            delegate
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

                                    // Increase the edit count.
                                    _EditCount++;
                                }
                            }
                        )
                    );

                    // Extract context menu item.
                    if (ListViewDark_Explorer.Items.Count != 0)
                    {
                        menu.Items.Add
                        (
                            new ToolStripMenuItem
                            (
                                "Extract",
                                Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_Export)),

                                delegate
                                {
                                    int count = ListViewDark_Explorer.SelectedItems.Count;

                                    /* These switches will be used frequently in cases where items can be operated on,
                                       either as a single element or multiple. If we want to change the behaviour of either
                                       operation, we can do so this way. */
                                    switch (count)
                                    {
                                        // Just a single item was selected.
                                        case 1:
                                        {
                                            ExtractFile((ArchiveFile)ListViewDark_Explorer.SelectedItems[0].Tag);

                                            break;
                                        }

                                        // More than one item was selected.
                                        case var multi when count > 1:
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

                                        // Default case when all else fails.
                                        default:
                                        {
                                            // Extract everything from the directory.
                                            ExtractDialog((ArchiveDirectory)_ActiveNode.Tag);

                                            break;
                                        }
                                    }
                                }
                            )
                        );
                    }

                    /* Paste context menu item - this'll be used later on,
                       just storing here before we determine when. Its appearance will be based on clipboard population. */
                    ToolStripMenuItem ctxPaste = new ToolStripMenuItem
                    (
                        "Paste",
                        Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_Paste)),

                        delegate
                        {
                            PasteFiles();
                        }
                    );

                    // Get the selected item at the current X/Y position.
                    ListViewItem selected = ListViewDark_Explorer.GetItemAt(e.X, e.Y);

                    /* WinForms is dumb and updates the ListViewItem selection state lazily when right-clicking.
                       This is a workaround for that to ensure at least one item is selected.
                       Just checking the count doesn't work and requires two right-clicks, which is painful. */
                    if (selected != null)
                    {
                        // Can I get uhh... ToolStripSeparator?
                        menu.Items.Add(new ToolStripSeparator());

                        // Copy context menu item.
                        menu.Items.Add
                        (
                            new ToolStripMenuItem
                            (
                                "Copy",
                                Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_Copy)),
                                
                                delegate
                                {
                                    CopyFiles();
                                }
                            )
                        );

                        // Add the Paste option if the clipboard is populated.
                        if (_Clipboard.Count != 0)
                            menu.Items.Add(ctxPaste);

                        /* You'd think I'd just use AddRange at this point,
                           but Microsoft decided that ToolStripSeparators aren't ToolStripItems. */
                        menu.Items.Add(new ToolStripSeparator());

                        // Delete context menu item.
                        menu.Items.Add
                        (
                            new ToolStripMenuItem
                            (
                                "Delete",
                                Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_RemoveFile)),

                                delegate
                                {
                                    DeleteFiles();
                                }
                            )
                        );

                        // Rename context menu item.
                        menu.Items.Add
                        (
                            new ToolStripMenuItem
                            (
                                "Rename",
                                Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_Rename)),

                                delegate
                                {
                                    InvokeRename();
                                }
                            )
                        );
                    }

                    // The selection was null, so that means nothing is selected.
                    else
                    {
                        // Add the Paste option if the clipboard is populated.
                        if (_Clipboard.Count != 0)
                        {
                            // Yeah... here's another separator.
                            menu.Items.Add(new ToolStripSeparator());

                            // And here's your paste option.
                            menu.Items.Add(ctxPaste);
                        }
                    }

                    // Display the assembled menu.
                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }

        /// <summary>
        /// Perform tasks upon clicking a TreeNode.
        /// </summary>
        private void KryptonTreeView_Explorer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            /* WinForms deselects the node for whatever reason when opening up the context menu,
               so we're forcing it to be selected again... */
            KryptonTreeView_Explorer.SelectedNode = e.Node;

            // Store the directory for later.
            ArchiveDirectory dir = (ArchiveDirectory)e.Node.Tag;

            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    // Add context menu item.
                    menu.Items.Add
                    (
                        new ToolStripMenuItem
                        (
                            "Add",
                            Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_AddFile)),
                                   
                            delegate
                            {
                                AddFolder(e.Node, dir);
                            }
                        )
                    );

                    // Import context menu item.
                    menu.Items.Add
                    (
                        new ToolStripMenuItem
                        (
                            "Import",
                            Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_OpenFolder)),
                            
                            delegate
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
                                        MarathonMessageBox.Show("Would you like to import the subdirectories?", string.Empty,
                                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                                    // Create new directory node.
                                    TreeNode dirNode = new TreeNode()
                                    {
                                        Text = Path.GetFileName(folderDialog.SelectedPath)
                                    };

                                    // Set the tag to the new directory.
                                    dirNode.Tag = new ArchiveDirectory(dirNode.Text)
                                    {
                                        Parent = (ArchiveDirectory)KryptonTreeView_Explorer.SelectedNode.Tag
                                    };

                                    // Add the selected path to the new node.
                                    LoadedArchive.AddDirectory(folderDialog.SelectedPath, (ArchiveDirectory)dirNode.Tag, includeSubDirectories);

                                    // Add node to the TreeView.
                                    e.Node.Nodes.Add(dirNode);

                                    // Add node tag to the archive as a directory.
                                    dir.Data.Add((ArchiveDirectory)dirNode.Tag);

                                    // Select the new node.
                                    KryptonTreeView_Explorer.SelectedNode = dirNode;

                                    // Enter edit mode.
                                    dirNode.BeginEdit();

                                    // Refresh views.
                                    InitialiseDirectoryTree();
                                    InitialiseFileItems(dirNode.Tag);

                                    // Increase the edit count.
                                    _EditCount++;
                                }
                            }
                        )
                    );

                    // Prettify the context menu. :)
                    menu.Items.Add(new ToolStripSeparator());

                    // Extract context menu item.
                    menu.Items.Add
                    (
                        new ToolStripMenuItem
                        (
                            "Extract",
                            Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_Export)),

                            delegate
                            {
                                ExtractDialog(dir);
                            }
                        )
                    );

                    // Delete context menu item.
                    menu.Items.Add
                    (
                        new ToolStripMenuItem
                        (
                            "Delete",
                            Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_RemoveFile)),

                            delegate
                            {
                                DeleteFolder();
                            }
                        )
                    );

                    // If the parent is null, we're at root and shouldn't allow renaming.
                    if (e.Node.Parent != null)
                    {
                        // Rename context menu item.
                        menu.Items.Add
                        (
                            new ToolStripMenuItem
                            (
                                "Rename",
                                Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Task_Strip_Rename)),

                                delegate
                                {
                                    e.Node.BeginEdit();
                                }
                            )
                        );
                    }

                    // Display the assembled menu.
                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }

        /// <summary>
        /// Perform tasks upon double clicking a TreeNode.
        /// </summary>
        private void KryptonTreeView_Explorer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    ActivateDirectoryNode(e.Node);

                    break;
                }
            }
        }

        /// <summary>
        /// Sets the current node to the input.
        /// </summary>
        private void ActivateDirectoryNode(TreeNode node)
        {
            // Update active node.
            _ActiveNode = node;

            // Navigates to the selected node if valid.
            InitialiseFileItems(node.Tag);

            // Set expanded state.
            node.Expand();

            // Shortened for easier reference.
            int @itemCount = ListViewDark_Explorer.Items.Count;

            // TODO: update the status strip in the workspace using the above integer.
        }

        /// <summary>
        /// Updates the enabled state of common ribbon controls.
        /// </summary>
        private void UpdateRibbonControls()
        {
            // Store this for easier reference.
            bool isSelection = ListViewDark_Explorer.SelectedItems.Count != 0;

            // Enable organise ribbon group items based on selection count.
            foreach (var item in KryptonRibbonGroupTriple_Organise_Delete_Rename.Items)
            {
                if (item is KryptonRibbonGroupButton ribbonButton)
                {
                    // Enable if there's at least a selection.
                    ribbonButton.Enabled = isSelection;
                }
            }

            // Enable clipboard ribbon group items based on selection count.
            foreach (var item in KryptonRibbonGroupTriple_Clipboard_Copy_Paste.Items)
            {
                if (item is KryptonRibbonGroupButton ribbonButton)
                {
                    // Enable Paste if the clipboard is populated.
                    if (ribbonButton == KryptonRibbonGroupButton_Clipboard_Paste)
                    {
                        ribbonButton.Enabled = _Clipboard.Count != 0;
                    }

                    // Otherwise, just enable if there's at least a selection.
                    else
                    {
                        // Include Cut in here as well...
                        KryptonRibbonGroupButton_Clipboard_Cut.Enabled = ribbonButton.Enabled = isSelection;
                    }
                }
            }
        }

        /// <summary>
        /// Events for when the selected index for ListViewDark_Explorer changes.
        /// </summary>
        private void ListViewDark_Explorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Shortened for easier reference.
            int @selectedCount = ListViewDark_Explorer.SelectedItems.Count;

            // Update the ribbon based on selection count.
            UpdateRibbonControls();

            // TODO: update the status strip in the workspace using the above integer.
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
                directory.Extract(folderDialog.SelectedPath);
        }

        /// <summary>
        /// Group event for RibbonAppButton_AppButtonMenuItems items.
        /// </summary>
        private void RibbonAppButton_AppButtonMenuItems_Click_Group(object sender, EventArgs e)
        {
            // Displays the extract dialog.
            if (sender == KryptonContextMenuItem_File_Extract || sender == KryptonRibbonQATButton_Extract)
            {
                ExtractDialog((ArchiveDirectory)LoadedArchive.Data[0]);
            }

            // Saves the loaded archive.
            else if (sender == KryptonContextMenuItem_File_Save || sender == KryptonRibbonQATButton_Save)
            {
                SaveArchive(LoadedArchive.Location);
            }

            // Saves the loaded archive to the desired location.
            else if (sender == KryptonContextMenuItem_File_SaveAs)
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
        }

        /// <summary>
        /// Group event for KryptonRibbonGroup_Clipboard items.
        /// </summary>
        private void KryptonRibbonGroup_Clipboard_Click_Group(object sender, EventArgs e)
        {
            // Copy items to clipboard.
            if (sender == KryptonRibbonGroupButton_Clipboard_Copy)
            {
                CopyFiles();
            }

            // Paste items from clipboard.
            else if (sender == KryptonRibbonGroupButton_Clipboard_Paste)
            {
                PasteFiles();
            }

            // Cut items to clipboard.
            else if(sender == KryptonRibbonGroupButton_Clipboard_Cut)
            {
                CopyFiles(true);
            }

            // Copy current folder structure as a string.
            else if (sender == KryptonRibbonGroupButton_Clipboard_CopyPath || sender == KryptonContextMenuItem_CopyRelativePath)
            {
                CopyPath();
            }

            // Copy current folder structure as a string (including the path to the archive).
            else if (sender == KryptonContextMenuItem_CopyFullPath)
            {
                CopyPath(true);
            }

            void CopyPath(bool fullPath = false)
            {
                List<string> paths = new List<string>();
                ArchiveData parent = (ArchiveData)_ActiveNode.Tag;

                while (parent != null)
                {
                    // Add the parent name to the list.
                    if (!string.IsNullOrEmpty(parent.Name))
                        paths.Add(parent.Name);

                    // Iterate to next parent.
                    parent = parent.Parent;
                }

                // Reverse the list to correct the path order.
                paths.Reverse();

                // Form the path with a string builder.
                {
                    StringBuilder path = new StringBuilder();

                    // Get the name of the selected file.
                    string selectedFile = ListViewDark_Explorer.SelectedItems.Count == 1 ?
                                          ((ArchiveData)ListViewDark_Explorer.SelectedItems[0].Tag).Name :
                                          string.Empty;

                    // Add the path to the loaded archive.
                    if (fullPath)
                        path.Append(LoadedArchive.Location);

                    // Join the list together to form a path.
                    if (paths.Count != 0)
                        path.Append((fullPath ? @"\" : "") + string.Join(@"\", paths) + @"\");

                    // Add the selected file to the path.
                    path.Append(selectedFile);

                    // Copy to clipboard.
                    if (!string.IsNullOrEmpty(path.ToString()))
                        Clipboard.SetText(path.ToString());
                }
            }
        }

        /// <summary>
        /// Group event for KryptonRibbonGroup_Organise items.
        /// </summary>
        private void KryptonRibbonGroupButton_Organise_Click_Group(object sender, EventArgs e)
        {
            // Delete the selected items from ListViewDark_Explorer.
            if (sender == KryptonRibbonGroupButton_Organise_Delete)
            {
                DeleteFiles();
            }

            // Invoke renaming or bulk rename tasks.
            else if (sender == KryptonRibbonGroupButton_Organise_Rename)
            {
                InvokeRename();
            }
        }

        /// <summary>
        /// Group event for KryptonRibbonGroup_Select items.
        /// </summary>
        private void KryptonRibbonGroup_Select_Click_Group(object sender, EventArgs e)
        {
            // Store all items so we can do whatever with them.
            List<ListViewItem> items = ListViewDark_Explorer.Items.OfType<ListViewItem>().ToList();

            // Select all items in ListViewDark_Explorer.
            if (sender == KryptonRibbonGroupButton_Select_SelectAll)
            {
                SetSelectedState(items, true);
            }

            // Deselect all items in ListViewDark_Explorer.
            else if (sender == KryptonRibbonGroupButton_Select_SelectNone)
            {
                SetSelectedState(items, false);
            }

            // Invert current selection in ListViewDark_Explorer.
            else if (sender == KryptonRibbonGroupButton_Select_InvertSelection)
            {
                // Store the unselected items before we deselect everything.
                List<ListViewItem> unselectedItems = ListViewDark_Explorer.Items.OfType<ListViewItem>().ToList()
                                                                                .Where(item => item.Selected == false).ToList();

                // Deselect everything.
                SetSelectedState(items, false);

                // Select all previously unselected items.
                SetSelectedState(unselectedItems, true);
            }

            // Selects things and stuff. lol
            void SetSelectedState(List<ListViewItem> items, bool selected)
                => items.OfType<ListViewItem>().ToList().ForEach(item => item.Selected = selected);
        }

        /// <summary>
        /// Saves the archive to the desired location.
        /// </summary>
        /// <param name="location">Location of the archive.</param>
        private void SaveArchive(string location)
        {
            // Decompress everything before repacking so we have valid data.
            LoadedArchive.Decompress(ref LoadedArchive.Data);

            // Save the modified archive.
            LoadedArchive.Save(location);

            // Set the new location.
            LoadedArchive.Location = location;

            // Reload the archive.
            LoadedArchive = LoadedArchive.Reload();

            // Refreshes the file view to the previously selected node.
            if (KryptonTreeView_Explorer.SelectedNode != null)
                ActivateDirectoryNode(KryptonTreeView_Explorer.SelectedNode);

            // Force garbage collection.
            GC.Collect();

            // Reset the edit count.
            _EditCount = 0;
        }

        /// <summary>
        /// Disposes the loaded archive upon exit.
        /// </summary>
        private void ArchiveExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Archive has been edited, so display the warning message.
            if (_EditCount != 0)
            {
                DialogResult saveResult = MarathonMessageBox.Show
                (
                    "This archive has been edited - do you want to save?",
                    string.Empty,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                switch (saveResult)
                {
                    case DialogResult.Yes:
                    {
                        // Save the archive.
                        SaveArchive(LoadedArchive.Location);

                        break;
                    }

                    case DialogResult.Cancel:
                    {
                        // Cancel the closing event if the result was DialogResult.Cancel...
                        e.Cancel = true;

                        break;
                    }
                }

                return;
            }

            // TODO: Dispose archive contents.
        }

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
                    AddFile(item, true);
                }
            }
        }

        /// <summary>
        /// Changes the cursor if data is present.
        /// </summary>
        private void ListViewDark_Explorer_DragEnter(object sender, DragEventArgs e)
            => e.Effect = e.AllowedEffect;

        /// <summary>
        /// Decompresses all selected items in ListViewDark_Explorer.
        /// </summary>
        private void DecompressSelectedItems()
        {
            // Decompress all files.
            foreach (ListViewItem item in ListViewDark_Explorer.SelectedItems)
            {
                // Store node for easier reference.
                ArchiveFile file = (ArchiveFile)item.Tag;

                // Decompress current file.
                file.Data = file.Decompress(_LoadedArchive.Location, file);
            }
        }

        /// <summary>
        /// Initialises the drag and drop events for ListViewItems.
        /// </summary>
        private void ListViewDark_Explorer_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Decompress everything so we can copy properly.
            DecompressSelectedItems();

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
            e.Item.ToolTipText = $"Type: {Resources.ParseFriendlyNameFromFileExtension(Marathon.Properties.Resources.FileTypes, file.Name)}\n" +
                                 $"Size: {StringHelper.ByteLengthToDecimalString(file.UncompressedSize)}";
        }

        /// <summary>
        /// Displays a tool tip for the hovered node.
        /// </summary>
        private void KryptonTreeView_Explorer_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            ArchiveDirectory dir = (ArchiveDirectory)e.Node.Tag;

            // Lists of directories and files in the root of the current node.
            string folders = string.Join(", ", dir.Data.OfType<ArchiveDirectory>().Select(x => x.Name)),
                   files   = string.Join(", ", dir.Data.OfType<ArchiveFile>().Select(x => x.Name));

            // Common length to truncate the above strings.
            int truncateLength = 45;

            // This is rather self-explanatory.
            e.Node.ToolTipText = $"Size: {StringHelper.ByteLengthToDecimalString(dir.TotalContentsSize)}\n" +

                                 // We don't want these to display when there aren't any files or folders, hence the conditions.
                                 (folders.Length == 0 ? string.Empty : $"Folders: {StringHelper.Truncate(folders, truncateLength)}\n") +
                                 (files.Length == 0 ? string.Empty : $"Files: {StringHelper.Truncate(files, truncateLength)}");
        }

        /// <summary>
        /// Adds a new folder to the currently active node upon clicking.
        /// </summary>
        private void KryptonRibbonGroupButton_New_Click_Group(object sender, EventArgs e)
        {
            if (sender == KryptonRibbonGroupButton_New_NewFolder)
            {
                AddFolder(_ActiveNode, (ArchiveDirectory)_ActiveNode.Tag);
            }
            else if (sender == KryptonRibbonGroupButton_New_NewItem)
            {
                AddFileDialog();
            }
        }

        /// <summary>
        /// Opens the selected file with Task Dashboard.
        /// </summary>
        private void ListViewDark_Explorer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Extract the selected file and store the path.
            string file = ExtractFile((ArchiveFile)ListViewDark_Explorer.SelectedItems[0].Tag);

            // Launch Task Dashboard with the input file.
            if (!string.IsNullOrEmpty(file))
                new TaskDashboard(DockPanel, file, TaskDashboard.TaskState.Open).ShowDialog(InheritanceRibbon);
        }
    }
}
