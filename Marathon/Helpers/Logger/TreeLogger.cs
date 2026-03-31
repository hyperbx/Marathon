using System.Collections.Generic;

namespace Marathon.Helpers
{
    public class TreeLogger
    {
        private static readonly List<NodeType> _depthList = [];

        public static void Log(string in_message, LogLevel in_logLevel, int in_depth = 0, NodeType in_nodeType = NodeType.Leaf)
        {
            while (_depthList.Count <= in_depth)
                _depthList.Add(NodeType.Leaf);

            _depthList[in_depth] = in_nodeType;

            var prefix = string.Empty;

            for (int i = 0; i < in_depth; i++)
                prefix += _depthList[i] == NodeType.End ? "    " : "│   ";

            var branch = in_depth == 0 || in_nodeType == NodeType.Root
                ? string.Empty
                : (in_nodeType == NodeType.End ? "└── " : "├── ");

            Logger.Log(prefix + branch + in_message, in_logLevel, null);
        }

        public static void Log(LogLevel in_logLevel, int in_depth = 0)
        {
            var prefix = string.Empty;

            for (int i = 0; i < in_depth; i++)
                prefix += _depthList[i] == NodeType.End ? "    " : "│   ";

            Logger.Log(prefix + "│", in_logLevel, null);
        }

        public static void Log(int in_depth = 0)
        {
            Log(LogLevel.None, in_depth);
        }

        public static void Log(string in_message, int in_depth = 0, NodeType in_nodeType = NodeType.Leaf)
        {
            Log(in_message, LogLevel.None, in_depth, in_nodeType);
        }

        public static void Utility(string in_message, int in_depth = 0, NodeType in_nodeType = NodeType.Leaf)
        {
            Log(in_message, LogLevel.Utility, in_depth, in_nodeType);
        }

        public static void Warning(string in_message, int in_depth = 0, NodeType in_nodeType = NodeType.Leaf)
        {
            Log(in_message, LogLevel.Warning, in_depth, in_nodeType);
        }

        public static void Error(string in_message, int in_depth = 0, NodeType in_nodeType = NodeType.Leaf)
        {
            Log(in_message, LogLevel.Error, in_depth, in_nodeType);
        }

        public enum NodeType
        {
            Root,
            Leaf,
            End
        }
    }
}
