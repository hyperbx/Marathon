using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace Marathon.Toolkit.Helpers
{
    public static class TreeViewExtensions
    {
        public class TreeViewState
        {
            public TreeViewState(List<string> expandedNodes, TreeNode topNode, TreeNode selectedNode)
            {
                ExpandedNodes = expandedNodes;
                TopNodePath = topNode != null ? topNode.FullPath : null;
                SelectedNodePath = selectedNode != null ? selectedNode.FullPath : null;
            }

            public readonly List<string> ExpandedNodes = null;

            public readonly string TopNodePath = string.Empty,
                                   SelectedNodePath = string.Empty;
        }

        /// <summary>
        /// Stores the current expansion state of all TreeNodes.
        /// </summary>
        public static TreeViewState GetExpandedNodesState(this KryptonTreeView tree)
        {
            var expandedNodesList = new List<string>();

            foreach (TreeNode node in tree.Nodes)
            {
                UpdateExpandedList(ref expandedNodesList, node);
            }

            return new TreeViewState(expandedNodesList, tree.TopNode, tree.SelectedNode);
        }

        /// <summary>
        /// Restores the absolute state of the nodes.
        /// </summary>
        public static void RestoreExpandedNodesState(this KryptonTreeView tree, TreeViewState state)
        {
            tree.BeginUpdate();

            foreach (TreeNode node in tree.Nodes)
            {
                foreach (var nodeState in state.ExpandedNodes)
                {
                    ExpandNodes(node, nodeState);
                }
            }

            tree.TopNode = FindNodeFromPath(tree, state.TopNodePath);
            tree.SelectedNode = FindNodeFromPath(tree, state.SelectedNodePath);
            tree.Focus();

            tree.EndUpdate();
        }

        /// <summary>
        /// Locates the TreeNode from a path.
        /// </summary>
        static TreeNode FindNodeFromPath(KryptonTreeView tree, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            List<string> elements = path.Split(tree.PathSeparator.ToCharArray()).ToList();

            TreeNode curNode = tree.Nodes.FindByText(elements[0]);

            if (curNode == null)
                return null;

            foreach (string element in elements.Skip(1))
            {
                if (curNode.Nodes.FindByText(element) != null)
                    curNode = curNode.Nodes.FindByText(element);
                else
                    break;
            }

            return curNode;
        }

        /// <summary>
        /// Locates the TreeNode by name.
        /// </summary>
        static TreeNode FindByText(this TreeNodeCollection tnc, string text)
        {
            foreach (TreeNode node in tnc)
            {
                if (node.Text == text)
                    return node;
            }

            return null;
        }

        /// <summary>
        /// Updates the expanded nodes.
        /// </summary>
        static void UpdateExpandedList(ref List<string> expNodeList, TreeNode node)
        {
            if (node.IsExpanded)
                expNodeList.Add(node.FullPath);

            foreach (TreeNode n in node.Nodes)
            {
                if (n.IsExpanded)
                    UpdateExpandedList(ref expNodeList, n);
            }
        }

        /// <summary>
        /// Expands the nodes recursively.
        /// </summary>
        static void ExpandNodes(TreeNode node, string nodeFullPath)
        {
            if (node.FullPath == nodeFullPath)
                node.Expand();

            foreach (TreeNode n in node.Nodes)
            {
                if (n.Nodes.Count > 0)
                    ExpandNodes(n, nodeFullPath);
            }
        }

        /// <summary>
        /// Flattens all TreeNodes into a list.
        /// </summary>
        public static IEnumerable<TreeNode> Descendants(this TreeNodeCollection treeNodeCollection)
        {
            foreach (TreeNode node in treeNodeCollection.OfType<TreeNode>())
            {
                yield return node;

                foreach (TreeNode child in node.Nodes.Descendants())
                    yield return child;
            }
        }
    }
}
