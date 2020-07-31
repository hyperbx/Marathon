using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Marathon.Toolkit.Helpers
{
    public static class TreeViewExtensions
    {
        /// <summary>
        /// Stores the current expansion state of all TreeNodes.
        /// </summary>
        public static List<string> GetExpansionState(this TreeNodeCollection treeNodeCollection)
            => treeNodeCollection.Descendants().Where(node => node.IsExpanded).Select(node => node.FullPath).ToList();

        /// <summary>
        /// Sets the expansion state of all TreeNodes.
        /// </summary>
        public static void SetExpansionState(this TreeNodeCollection nodes, List<string> storedExpansionState)
        {
            foreach (TreeNode node in nodes.Descendants().Where(node => storedExpansionState.Contains(node.FullPath)))
                node.Expand();
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
