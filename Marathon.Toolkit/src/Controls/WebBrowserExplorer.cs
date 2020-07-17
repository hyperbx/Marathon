using System;
using System.IO;
using System.Web;
using System.Drawing;
using Marathon.Components;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace Marathon.Controls
{
    public partial class WebBrowserExplorer : UserControl
    {
        private string _CurrentAddress;
        private bool _ShowDirectoryTree;

        [Description("The current address navigated to by the TreeView and WebBrowser controls."), DefaultValue(@"C:\")]
        public string CurrentAddress
        {
            get => _CurrentAddress;
            
            set
            {
                // Decode the URL to prevent the path from using HTML encoded characters.
                _CurrentAddress = HttpUtility.UrlDecode(value.Replace("file:///", "").Replace("/", @"\"));

                // Set text boxes to use the current address.
                TextBox_Address.Text = CurrentAddress;

                // Refresh the TreeView nodes only if the directory tree is available.
                if (!SplitContainer_TreeView.Panel1Collapsed) RefreshNodes();
            }
        }

        [Description("Displays a TreeView control for iterating through directories."), DefaultValue(true)]
        public bool ShowDirectoryTree
        {
            get => _ShowDirectoryTree;

            set
            {
                if (_ShowDirectoryTree = SplitContainer_TreeView.Panel1Collapsed = value)
                {
                    RefreshNodes();
                    ToolTip_Information.SetToolTip(ButtonFlat_ShowDirectoryTree, "Hide directory tree");
                }
                else
                    ToolTip_Information.SetToolTip(ButtonFlat_ShowDirectoryTree, "Show directory tree");
            }
        }

        [Description("Hides all navigation options."), DefaultValue(false)]
        public bool HideNavigationBar
        {
            set
            {
                if (value) SplitContainer_TreeView.Panel1Collapsed = SplitContainer_WebBrowser.Panel1Collapsed = true;
            }
        }

        /// <summary>
        /// Refresh the TreeView nodes.
        /// </summary>
        private void RefreshNodes()
        {
            if (!string.IsNullOrEmpty(CurrentAddress))
            {
                // Store the current expanded nodes before refreshing...
                List<string> storedExpansionState = TreeView_Explorer.Nodes.GetExpansionState();

                TreeView_Explorer.Nodes.Clear();

                DirectoryInfo directory = new DirectoryInfo(CurrentAddress);

                try
                {
                    if (directory.Exists)
                    {
                        // Create parent directory node...
                        TreeView_Explorer.Nodes.Add(new TreeNode
                        {
                            Text = "..",
                            Tag = "..",
                            ImageKey = "Folder"
                        });

                        GetDirectories(directory.GetDirectories());

                        // Restore expanded nodes.
                        TreeView_Explorer.Nodes.SetExpansionState(storedExpansionState);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    GoUp(); // Back out of restricted directory...
                }
            }
        }

        /// <summary>
        /// Populate the TreeView control with all root directories.
        /// </summary>
        private void GetDirectories(DirectoryInfo[] subDirectories)
        {
            foreach (DirectoryInfo directory in subDirectories)
            {
                try
                {
                    if (directory.Exists && !directory.Attributes.HasFlag(FileAttributes.System))
                    {
                        TreeNode dirNode = new TreeNode
                        {
                            Text = directory.Name,
                            Tag = directory,
                            ImageKey = "Folder"
                        };

                        TreeView_Explorer.Nodes.Add(dirNode);

                        if (directory.GetDirectories().Length != 0)
                            dirNode.Nodes.Add(new TreeNode("Loading..."));
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // Ignored...
                }
            }
        }

        /// <summary>
        /// Populate the TreeView control with all subdirectories.
        /// </summary>
        private void GetDirectories(DirectoryInfo[] subDirectories, TreeNode node)
        {
            node.Nodes.Clear();

            foreach (DirectoryInfo directory in subDirectories)
            {
                try
                {
                    if (directory.Exists && !directory.Attributes.HasFlag(FileAttributes.System))
                    {
                        TreeNode dirNode = new TreeNode
                        {
                            Text = directory.Name,
                            Tag = directory,
                            ImageKey = "Folder"
                        };

                        node.Nodes.Add(dirNode);

                        if (directory.GetDirectories().Length != 0)
                            dirNode.Nodes.Add(new TreeNode("Loading..."));
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // Ignored...
                }
            }
        }

        public WebBrowserExplorer()
        {
            InitializeComponent();

            // Curse you, WinForms designer...
            SplitContainer_WebBrowser.SplitterWidth = 1;
        }

        /// <summary>
        /// Set the current directory for the WebBrowser control on load.
        /// </summary>
        private void WebBrowserExplorer_Load(object sender, EventArgs e)
            => WebBrowser_Explorer.Url = new Uri(CurrentAddress);

        /// <summary>
        /// Updates the current directory and watches over it to update the TreeView control.
        /// </summary>
        private void WebBrowser_Explorer_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // Sets the back button's Enabled state depending on history length.
            SetNavigationButtonState(ButtonFlat_Back, Properties.Resources.WebBrowserExplorer_Back_Enabled,
                                     Properties.Resources.WebBrowserExplorer_Back_Disabled, WebBrowser_Explorer.CanGoBack);

            // Sets the forward button's Enabled state depending on history length.
            SetNavigationButtonState(ButtonFlat_Forward, Properties.Resources.WebBrowserExplorer_Forward_Enabled,
                                     Properties.Resources.WebBrowserExplorer_Forward_Disabled, WebBrowser_Explorer.CanGoForward);

            // Update current directory.
            CurrentAddress = e.Url.ToString();

            // Check if the directory actually exists before watching it.
            if (Directory.Exists(CurrentAddress))
            {
                // Watch the current directory.
                FileSystemWatcher fileSystemWatcher = new FileSystemWatcher {
                    Path = CurrentAddress,
                    NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = false
                };

                // Subscribe to identical event, since they all use the event arguments.
                fileSystemWatcher.Changed += FileSystemWatcher_Changed;
                fileSystemWatcher.Created += FileSystemWatcher_Changed;
                fileSystemWatcher.Deleted += FileSystemWatcher_Changed;

                // Create local event handler, since it uses different event arguments.
                fileSystemWatcher.Renamed += (renamedSender, renamedEventArgs) =>
                {
                    // Invoke action only if the changed argument was a directory.
                    if (Directory.Exists(renamedEventArgs.FullPath) || Path.GetExtension(renamedEventArgs.FullPath) == string.Empty)
                        Invoke(new Action(RefreshNodes));
                };
            }
        }

        /// <summary>
        /// Invoke action only if the changed argument was a directory.
        /// </summary>
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (Directory.Exists(e.FullPath) || Path.GetExtension(e.FullPath) == string.Empty)
                Invoke(new Action(RefreshNodes));
        }

        /// <summary>
        /// Sets the state of the navigation button by sender.
        /// </summary>
        private void SetNavigationButtonState(object sender, Bitmap enabled, Bitmap disabled, bool state)
        {
            if (((ButtonFlat)sender).Enabled = state)
                ((ButtonFlat)sender).BackgroundImage = enabled;
            else
                ((ButtonFlat)sender).BackgroundImage = disabled;
        }

        /// <summary>
        /// Navigates to the selected node if valid.
        /// </summary>
        private void TreeView_Explorer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node.Tag.GetType().Equals(typeof(string)) && (string)e.Node.Tag == "..") GoUp();
                else
                {
                    if (!ChangeDirectoryOnValidation(((DirectoryInfo)e.Node.Tag).FullName))
                        TreeView_Explorer.Nodes.Remove(e.Node);
                }
            }
        }

        /// <summary>
        /// Navigates to the input path if valid.
        /// </summary>
        private void TextBox_Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!ChangeDirectoryOnValidation(((TextBox)sender).Text))
                    MessageBox.Show($"{Name} can't find '{((TextBox)sender).Text}'. Check the spelling and try again.",
                                    Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ensures the input path is always a directory and always valid.
        /// </summary>
        private bool ChangeDirectoryOnValidation(string directory)
        {
            if (File.Exists(directory))
            {
                WebBrowser_Explorer.Url = new Uri(CurrentAddress = Path.GetDirectoryName(directory));
                return true;
            }
            else if (Directory.Exists(directory))
            {
                WebBrowser_Explorer.Url = new Uri(CurrentAddress = directory);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Navigates back and forward using the dedicated mouse buttons.
        /// </summary>
        private void TreeView_Explorer_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.XButton1:
                    WebBrowser_Explorer.GoBack();
                    break;
                case MouseButtons.XButton2:
                    WebBrowser_Explorer.GoForward();
                    break;
            }
        }

        /// <summary>
        /// Navigates to the parent directory of the CurrentAddress property.
        /// </summary>
        private void GoUp() => ChangeDirectoryOnValidation(Path.GetFullPath(Path.Combine(CurrentAddress, "..")));

        /// <summary>
        /// Navigation buttons for the WebBrowser control.
        /// </summary>
        private void ButtonFlat_Navigation_Click(object sender, EventArgs e)
        {
            // Navigate back a directory...
            if (sender.Equals(ButtonFlat_Back))
                WebBrowser_Explorer.GoBack();

            // Navigate forward a directory...
            else if (sender.Equals(ButtonFlat_Forward))
                WebBrowser_Explorer.GoForward();

            // Inverts the current mode for the ShowDirectoryTree property.
            else if (sender.Equals(ButtonFlat_ShowDirectoryTree))
                ShowDirectoryTree = !ShowDirectoryTree;

            // Set the clipboard to the current address.
            else if (sender.Equals(ButtonFlat_Clipboard))
                Clipboard.SetText(CurrentAddress);

            // Navigate to the parent directory...
            else if (sender.Equals(ButtonFlat_Up))
                GoUp();
        }

        /// <summary>
        /// Gets the subdirectories for the current node, rather than loading all at once.
        /// </summary>
        private void TreeView_Explorer_AfterExpand(object sender, TreeViewEventArgs e)
            => GetDirectories(((DirectoryInfo)e.Node.Tag).GetDirectories(), e.Node);
    }
}
