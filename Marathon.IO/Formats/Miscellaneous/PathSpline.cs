// PathSpline.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 Knuxfan24
 * Copyright (c) 2021 HyperBE32
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

using System.IO;
using System.Collections.Generic;
using Marathon.IO.Headers;

namespace Marathon.IO.Formats.Miscellaneous
{
    /// <summary>
    /// <para>File base for the PATH format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for splines.</para>
    /// </summary>
    ///
    /// Old MaxScripts created by Paraxade0 were heavily used in the writing of this class.
    public class PathSpline : FileBase
    {
        // TODO: Exporting and Importing, Testing to understand what the flags and Invec/Outvec Positions effect, So Basically Everything.

        public PathSpline() { }

        public PathSpline(string file)
        {
            switch (Path.GetExtension(file))
            {
                // TODO: add importing.

                default:
                    Load(file);
                    break;
            }
        }

        public class PathEntry
        {
            public uint Flag1, Flag2, NodeNumber;

            public List<SplineEntry> Splines = new List<SplineEntry>();

            public Vector3 Position;

            public Quaternion Rotation;

            public string Name;
        }

        public class SplineEntry
        {
            public List<VertexEntry> Vertices = new List<VertexEntry>();
        }

        public class VertexEntry
        {
            public uint Flag;

            public Vector3 Position, InvecPosition, OutvecPosition;
        }

        public const string Extension = ".path";

        public List<PathEntry> Paths = new List<PathEntry>();

        public override void Load(Stream stream)
        {
            BINAReader reader = new BINAReader(stream);
            reader.ReadHeader();

            uint PathTableOffset = reader.ReadUInt32(); // Where the table of paths is.
            uint PathTableCount = reader.ReadUInt32();  // How many entries are in the table of paths.
            uint NodeTableOffset = reader.ReadUInt32(); // Where the table of nodes is.
            uint NodeTableCount = reader.ReadUInt32();  // How many enteries are in the table of nodes, seems to always be the same as PathTableCount?

            // Jump to the Path Table's Offset for safety's sake, should always be ox30 really.
            reader.JumpTo(PathTableOffset, true);

            // Loop through based on PathTableCount.
            for (int i = 0; i < PathTableCount; i++)
            {
                PathEntry path = new PathEntry();
                uint PathOffset = reader.ReadUInt32();  // Offset to the path data.
                uint SplineCount = reader.ReadUInt32(); // How many splines make up this path.
                path.Flag1 = reader.ReadUInt32();       // Read this path's flag.

                // Store position in file.
                long position = reader.BaseStream.Position;

                // Jump to position of this path's data.
                reader.JumpTo(PathOffset, true);

                uint SplineOffset = reader.ReadUInt32(); // Offset to the spines of this path.
                uint VertexCount = reader.ReadUInt32();  // How many verticies make up this path's splines.
                path.Flag2 = reader.ReadUInt32();        // Read this path's second flag.

                // Jump to the position of this path's splines.
                reader.JumpTo(SplineOffset, true);

                // Read this path's splines.
                for (int s = 0; s < SplineCount; s++)
                {
                    SplineEntry spline = new SplineEntry();
                    for (int v = 0; v < VertexCount; v++)
                    {
                        VertexEntry vertex = new VertexEntry
                        {
                            Flag = reader.ReadUInt32(),
                            Position = reader.ReadVector3(),
                            InvecPosition = reader.ReadVector3(),
                            OutvecPosition = reader.ReadVector3()
                        };

                        spline.Vertices.Add(vertex);
                    }
                    path.Splines.Add(spline);
                }

                // Save path entry and return to the previously stored position.
                Paths.Add(path);
                reader.JumpTo(position);
            }

            // Jump to the Node Table's offset.
            reader.JumpTo(NodeTableOffset, true);

            // Loop through based on NodeTableCount.
            for (int i = 0; i < NodeTableCount; i++)
            {
                Paths[i].NodeNumber = reader.ReadUInt32();    // Unknown, usually the same as the path's number sequentially, but not always.
                Paths[i].Position   = reader.ReadVector3();
                Paths[i].Rotation   = reader.ReadQuaternion();

                // Offset to the path's name.
                uint NameOffset = reader.ReadUInt32();

                // Store position in file.
                long position = reader.BaseStream.Position;

                // Read the path's name.
                reader.JumpTo(NameOffset, true);
                Paths[i].Name = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next node.
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            writer.AddOffset("PathTableOffset");
            writer.Write(Paths.Count);

            writer.AddOffset("NodeTableOffset");
            writer.Write(Paths.Count); // Node Table Count seems to always be the same as PathTableCount?

            // Path Table
            writer.FillInOffset("PathTableOffset", true);
            for(int i = 0; i < Paths.Count; i++)
            {
                writer.AddOffset($"Path{i}Offset");
                writer.Write(Paths[i].Splines.Count);
                writer.Write(Paths[i].Flag1);
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.FillInOffset($"Path{i}Offset", true);
                writer.AddOffset($"Path{i}SplineOffset");
                writer.Write(Paths[i].Splines[0].Vertices.Count);
                writer.Write(Paths[i].Flag2);
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.FillInOffset($"Path{i}SplineOffset", true);
                for(int s = 0; s < Paths[i].Splines.Count; s++)
                {
                    for(int v = 0; v < Paths[i].Splines[s].Vertices.Count; v++)
                    {
                        writer.Write(Paths[i].Splines[s].Vertices[v].Flag);
                        writer.Write(Paths[i].Splines[s].Vertices[v].Position);
                        writer.Write(Paths[i].Splines[s].Vertices[v].InvecPosition);
                        writer.Write(Paths[i].Splines[s].Vertices[v].OutvecPosition);
                    }
                }
            }

            // Node Table
            writer.FillInOffset("NodeTableOffset", true);
            for (int i = 0; i < Paths.Count; i++)
            {
                writer.Write(Paths[i].NodeNumber);
                writer.Write(Paths[i].Position);
                writer.Write(Paths[i].Rotation);
                writer.AddString($"Path{i}Name", Paths[i].Name);
            }

            writer.FinishWrite(Header);
        }
    }
}
