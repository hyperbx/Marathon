namespace Marathon.Formats.Archive
{
    public class U8Archive : FileBase
    {
        // Generic VS stuff to allow creating an object that instantly loads a file.
        public U8Archive() { }
        public U8Archive(string filepath, bool sonic06 = true, bool extract = false)
        {
            Load(filepath, sonic06);

            if (extract)
                Extract($@"{Path.GetDirectoryName(filepath)}\{Path.GetFileNameWithoutExtension(filepath)}");
        }

        // Classes for this format.
        public class FileNode
        {
            /// <summary>
            /// The name of this node.
            /// </summary>
            public string Name { get; set; } = "";

            /// <summary>
            /// The bytes that make up this node.
            /// </summary>
            public byte[] Data { get; set; } = Array.Empty<byte>();

            public override string ToString() => Name;
        }

        // Actual data presented to the end user.
        public List<FileNode> Data = new();

        // Internal value used for extraction.
        private static bool isSonic06 = true;

        // Internal value used for saving.
        private static int StringTableLength = 0;

        /// <summary>
        /// Loads and parses this format's file.
        /// </summary>
        /// <param name="filepath">The path to the file to load and parse.</param>
        /// <param name="sonic06">Whether this is a U8 archive from Sonic '06, which modifies the format to support compression.</param>
        public void Load(string filepath, bool sonic06 = true)
        {
            // Set the internal value used for extracting.
            isSonic06 = sonic06;

            // Set up a value to store the node length.
            uint nodeLength = 0x0C;
            if (sonic06)
                nodeLength = 0x10;

            // Set up a list of names and a list of final indices.
            List<string> names = new();
            List<uint> finalIndices = new();

            // Set up a string to hold the full filepath of each file.
            string fullPath = "";

            // Set up Marathon's BinaryReader.
            BinaryReaderEx reader = new(File.OpenRead(filepath), true);

            // Read the Uª8- signature.
            reader.ReadSignature(0x55AA382D);

            // Read the offset to the root node.
            uint firstNodeOffset = reader.ReadUInt32();

            // Read the size of the node table.
            uint nodeTableSize = reader.ReadUInt32();

            // Read the size of the file table (including the string table).
            uint fileTableOffset = reader.ReadUInt32();

            // Skip four seemingly useless integers.
            reader.JumpAhead(0x10);

            // Check that this first node is a directory.
            if (reader.ReadBoolean() != true)
                throw new Exception($"First node in U8 archive {filepath} doesn't appear to be a root node?");

            // Check that this first node's string offset is 0.
            if (reader.ReadUInt24() != 0)
                throw new Exception($"First node in U8 archive {filepath} doesn't appear to be a root node?");

            // Read this root's parent index???
            uint rootParent = reader.ReadUInt32();

            // Read the count of nodes in this archive (minus 1, as it includes the root node).
            uint nodeCount = reader.ReadUInt32() - 1;

            // If this U8 archive is a Sonic '06 one, then skip the uncompressed size, as its useless for directories.
            if (sonic06)
                reader.JumpAhead(0x04);

            // Calculate the location of the string table.
            uint stringTableLocation = (uint)(reader.BaseStream.Position + (nodeCount * nodeLength));

            for (int nodeIndex = 0; nodeIndex < nodeCount; nodeIndex++)
            {
                // Check if this node index (minus 1) is present in the final indices list, if so, remove its name from the full path.
                for (int finalIndex = finalIndices.Count - 1; finalIndex >= 0; finalIndex--)
                    if (nodeIndex == finalIndices[finalIndex] - 1)
                        fullPath = fullPath.Remove(fullPath.Length - (names[finalIndex].Length + 1));

                // Read whether or not this node is a directory.
                bool isDirectory = reader.ReadBoolean();

                // Read the offset to this node's name.
                uint nameOffset = reader.ReadUInt24();

                // Save our current position so we can jump back after reading the node name.
                long position = reader.BaseStream.Position;

                // Jump to the string table for this file, plus the offset for this node's name.
                reader.JumpTo(stringTableLocation + nameOffset);

                // Read this node's name.
                string nodeName = reader.ReadNullTerminatedString();

                // Jump back for the rest of this node.
                reader.JumpTo(position);

                // Check if this node is a directory or not.
                if (isDirectory)
                {
                    // Read this directory's parent index.
                    uint parentIndex = reader.ReadUInt32();

                    // Read the index of the first node that ISN'T part of this directory.
                    uint fistNodeNotPart = reader.ReadUInt32();

                    // Save this directory's name and final index.
                    names.Add(nodeName);
                    finalIndices.Add(fistNodeNotPart);

                    // Add this directory's name to the full path.
                    fullPath += $@"{nodeName}\";

                    // If this U8 archive is a Sonic '06 one, then skip the uncompressed size, as its useless for directories.
                    if (sonic06)
                        reader.JumpAhead(0x04);
                }
                else
                {
                    // Read the offset to this file's data.
                    uint dataOffset = reader.ReadUInt32();

                    // Read this file's size.
                    int fileSize = reader.ReadInt32();

                    // If this U8 archive is a Sonic '06 one, then skip the uncompressed size, as we don't need it for reading.
                    if (sonic06)
                        reader.JumpAhead(0x04);

                    // Save our current position so we can jump back for the next file.
                    position = reader.BaseStream.Position;

                    // Jump to this file's offset.
                    reader.JumpTo(dataOffset);

                    // Create a file node and read its data.
                    FileNode file = new()
                    {
                        Name = fullPath + nodeName,
                        Data = reader.ReadBytes(fileSize)
                    };

                    // If this U8 archive is a Sonic '06 one, then decompress the data.
                    if (sonic06)
                        file.Data = ZlibStream.Decompress(file.Data);

                    // Save this file.
                    Data.Add(file);

                    // Jump back for the next file.
                    reader.JumpTo(position);
                }
            }

            // Close Marathon's BinaryReader.
            reader.Close();
        }

        /// <summary>
        /// File nodes used for saving.
        /// </summary>
        private class Node
        {
            /// <summary>
            /// The name of this node.
            /// </summary>
            public string Name { get; set; } = "";

            /// <summary>
            /// This node's children, if any.
            /// </summary>
            public List<Node>? Children { get; set; }

            /// <summary>
            /// This node's binary data, if any.
            /// </summary>
            public byte[]? Binary { get; set; }

            /// <summary>
            /// The index of this node.
            /// </summary>
            public uint Index { get; set; }

            public override string ToString() => Name;
        }

        /// <summary>
        /// Saves this format's file.
        /// </summary>
        /// <param name="filepath">The path to save to.</param>
        /// <param name="sonic06">Whether to save this U8 archive in Sonic '06's modified version.</param>
        /// <param name="compressionLevel">If we're saving a Sonic '06 U8 archive, what level of compression should be applied to the files?</param>
        public void Save(string filepath, bool sonic06 = true, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            // Sort the data in case any has been appended to the file list.
            Data = Data.OrderBy(x => x.Name).ToList();

            // Reset the string table length.
            StringTableLength = 0;

            // Set up the root node.
            Node root = new() { Name = "Root", Children = new() };

            // Set up a node index value.
            uint nodeIndex = 1;

            // Loop through each file in this archive.
            foreach (FileNode file in Data)
            {
                // Set a reference to the root node as our parent node.
                Node parentNode = root;

                // Split this file's path to each individual node.
                string[] nodeNameSplit = file.Name.Split('\\');

                // Loop through each node name in this filename.
                for (int nodeNameIndex = 0; nodeNameIndex < nodeNameSplit.Length; nodeNameIndex++)
                {
                    // If our parent node doesn't have any children, then initialise its list.
                    parentNode.Children ??= new();

                    // Get a reference to the node in the parent's child nodes with this node's name.
                    Node? newParentNode = parentNode.Children.Where(x => x.Name == nodeNameSplit[nodeNameIndex]).FirstOrDefault();

                    // Check if we HAVEN'T found a node mathing this node's name already.
                    if (newParentNode == null)
                    {
                        // Create a new node.
                        Node newNode = new() { Name = nodeNameSplit[nodeNameIndex], Index = nodeIndex };

                        // Increment the node index.
                        nodeIndex++;

                        // If this node is the final one, then set its binary data to this file's.
                        if (nodeNameIndex == nodeNameSplit.Length - 1)
                            newNode.Binary = file.Data;

                        // Add our new node to the parent's child node list.
                        parentNode.Children.Add(newNode);

                        // Change the reference to the parent node to our new node.
                        parentNode = newNode;
                    }

                    // If we have found a node matching this node's name the change the reference to the parent node to it.
                    else
                        parentNode = newParentNode;
                }
            }

            // Set up Marathon's BinaryWriter.
            BinaryWriterEx writer = new(File.Create(filepath), true);

            // Write the Uª8- signature.
            writer.Write(0x55AA382D);

            // Add an offset for the root node.
            writer.AddOffset("RootNodeOffset");

            // Add a placeholder for the node and string tables size.
            writer.Write("SIZE");

            // Add an offset to the file data.
            writer.AddOffset("FileDataOffset");

            // Write 0x10 reserved bytes.
            // TODO: Do these impact anything?
            writer.WriteNulls(0x10);

            // Fill in the offset for the root node.
            writer.FillOffset("RootNodeOffset");

            // Write each node for this file.
            WriteNodeTable(writer, root, 0, sonic06, compressionLevel);

            // Write the string table for this file.
            WriteNodeName(writer, root);

            // Jump back to 0x08.
            writer.BaseStream.Position = 0x08;

            // Calculate and fill in the length of the node and string tables.
            writer.Write((uint)(writer.BaseStream.Length - 0x20));

            // Jump back to where we were.
            writer.BaseStream.Position = writer.BaseStream.Length;

            // Realign to 0x20.
            writer.FixPadding(0x20);

            // Fill in the offset for the file data.
            writer.FillOffset("FileDataOffset");

            // Write the files in this archive.
            WriteNodeBinary(writer, root);

            // Close Marathon's BinaryWriter.
            writer.Close();
        }

        /// <summary>
        /// Writes a node entry for the node table.
        /// </summary>
        /// <param name="writer">The BinaryWriterEx we're using.</param>
        /// <param name="node">The node we're currently writing.</param>
        /// <param name="parentIndex">The index of this node's parent.</param>
        /// <param name="sonic06">Whether to save this U8 archive in Sonic '06's modified version.</param>
        /// <param name="compressionLevel">If we're saving a Sonic '06 U8 archive, what level of compression should be applied to the files?</param>
        private void WriteNodeTable(BinaryWriterEx writer, Node node, uint parentIndex, bool sonic06, CompressionLevel compressionLevel)
        {
            // Check if this node has children and write whether its a directory or not.
            if (node.Children == null)
                writer.Write(false);
            else
                writer.Write(true);

            // Check if this is the root node.
            if (node.Name == "Root")
            {
                // Write a 0 for the string table offset.
                writer.WriteInt24(0x00);

                // Increment the length by 1.
                StringTableLength += 1;
            }

            // If this is not a root nood.
            else
            {
                // Write the length of the string table for the offset.
                writer.WriteInt24(StringTableLength);

                // Increment the stirng table length, including the null terminator.
                StringTableLength += node.Name.Length + 1;
            }

            // Check if this node has children.
            if (node.Children != null)
            {
                // Check if this node is the root.
                if (node.Name == "Root")
                {
                    // Write 4 nulls, as the root shouldn't have a parent index.
                    writer.WriteNulls(0x04);

                    // Calculate the amount of nodes in total.
                    writer.Write(GetNodeChildCount(node, 1));
                }

                // If this node is not a root.
                else
                {
                    // Write this node's parent index.
                    writer.Write(parentIndex);

                    // Write the index of the first node not in this node's children.
                    writer.Write(GetFinalNodeInChildren(node, 0) + 1);
                }
            }

            // If this node doesn't have children, assume its a file.
            else
            {
                if (node.Binary == null)
                    throw new Exception("Node has no children AND no binary data!");

                // Add an offset for this node's data.
                writer.AddOffset($"FileNode{node.Index}DataOffset");

                // Store the uncompressed size of this node's data.
                int uncompressed = node.Binary.Length;

                if (sonic06)
                {
                    // Compress this node's data.
                    node.Binary = ZlibStream.Compress(node.Binary, compressionLevel);

                    // Write the compressed size of this node.
                    writer.Write(node.Binary.Length);
                }

                // Write the uncompressed size of this node.
                writer.Write(uncompressed);
            }

            // If this is a Sonic '06 archive, then make sure the node entry is padded up to 0x10.
            if (sonic06)
                writer.FixPadding(0x10);

            // If this node has children, then loop through and write their nodes too.
            if (node.Children != null)
                foreach (Node child in node.Children)
                    WriteNodeTable(writer, child, node.Index, sonic06, compressionLevel);
        }

        /// <summary>
        /// Gets the count of child notes in a node.
        /// </summary>
        /// <param name="node">The node we're getting the count of children for.</param>
        /// <param name="childCount">The count of children currently calculated.</param>
        /// <returns></returns>
        private int GetNodeChildCount(Node node, int childCount)
        {
            // If this node has any children, then loop through each of them and update the child count.
            if (node.Children != null)
                foreach (Node childNode in node.Children)
                    childCount = GetNodeChildCount(childNode, childCount + 1);

            // Return the child count.
            return childCount;
        }

        /// <summary>
        /// Gets the index of the final node in this node's children.
        /// </summary>
        /// <param name="node">The node we're getting the index of the final child for.</param>
        /// <param name="finalIndex">The current final index.</param>
        /// <returns></returns>
        private uint GetFinalNodeInChildren(Node node, uint finalIndex)
        {
            // If this node has any children, then loop through each of them and update the final index.
            if (node.Children != null)
                foreach (Node childNode in node.Children)
                    finalIndex = GetFinalNodeInChildren(childNode, childNode.Index);

            // Return the final index.
            return finalIndex;
        }

        /// <summary>
        /// Writes the name for a node.
        /// </summary>
        /// <param name="writer">The BinaryWriterEx we're using.</param>
        /// <param name="node">The node we're currently writing the name of.</param>
        private void WriteNodeName(BinaryWriterEx writer, Node node)
        {
            // If this is the root node, then just write a single null.
            if (node.Name == "Root")
                writer.WriteNull();

            // If not, then write the node name, including a null terminator.
            else
                writer.WriteNullTerminatedString(node.Name);

            // If this node has any children, then loop through and write their names too.
            if (node.Children != null)
                foreach (var child in node.Children)
                    WriteNodeName(writer, child);
        }

        /// <summary>
        /// Writes a node's binary data.
        /// </summary>
        /// <param name="writer">The BinaryWriterEx we're using.</param>
        /// <param name="node">The node we're currently writing the data of.</param>
        private void WriteNodeBinary(BinaryWriterEx writer, Node node)
        {
            // Check if this node has any binary data.
            if (node.Binary != null)
            {
                // Fill in the offset for this node.
                writer.FillOffset($"FileNode{node.Index}DataOffset");

                // Write this node's binary data.
                writer.Write(node.Binary);

                // Realign to 0x20 bytes.
                writer.FixPadding(0x20);
            }

            // If this node has any children, then write their data too.
            if (node.Children != null)
                foreach (var child in node.Children)
                    WriteNodeBinary(writer, child);
        }

        /// <summary>
        /// Extracts the files in this format to disc.
        /// </summary>
        /// <param name="directory">The directory to extract to.</param>
        public void Extract(string directory)
        {
            // Create the extraction directory.
            Directory.CreateDirectory(directory);

            // Loop through each node to extract.
            foreach (FileNode node in Data)
            {
                // Get this file's name.
                string fileName = node.Name;

                // Print the name of the file we're extracting.
                Console.WriteLine($"Extracting {fileName}.");

                // Create directory paths if needed.
                if (!Directory.Exists($@"{directory}\{Path.GetDirectoryName(fileName)}"))
                    Directory.CreateDirectory($@"{directory}\{Path.GetDirectoryName(fileName)}");

                // Extract the file.
                File.WriteAllBytes($@"{directory}\{fileName}", node.Data);
            }
        }

        /// <summary>
        /// Imports files from a directory into this format.
        /// </summary>
        /// <param name="directory">The directory to import.</param>
        public void Import(string directory)
        {
            // Loop through each file in the directory.
            foreach (string file in Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories))
            {
                // Read this file's name (stripping out the directory name in the search) and binary data.
                FileNode node = new()
                {
                    Name = file.Replace($@"{directory}\", ""),
                    Data = File.ReadAllBytes(file)
                };

                // Save this file.
                Data.Add(node);
            }
        }
    }
}
