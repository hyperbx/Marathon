// ReflectionZone.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
 * Copyright (c) 2020 Knuxfan24
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
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO.Headers;
using Marathon.IO.Helpers;

namespace Marathon.IO.Formats.Meshes
{
    /// <summary>
    /// File base for the Sonic '06 RAB format.
    /// </summary>
    public class ReflectionZone : FileBase
    {
        // TODO: Fix footer not writing correctly...

        public class Reflection
        {
            public float Z_Rotation, Length, Y_Rotation, Height;
            public List<Vector3> Vertices = new List<Vector3>();
        }

        public const string Extension = ".rab";

        public List<Reflection> Reflections = new List<Reflection>();

        public override void Load(Stream stream)
        {
            BINAReader reader = new BINAReader(stream);
            reader.ReadHeader();

            uint reflectionTableCount  = reader.ReadUInt32(),
                 reflectionTableOffset = reader.ReadUInt32(),
                 entryTableCount       = reader.ReadUInt32(),
                 entryTableOffset      = reader.ReadUInt32();

            reader.JumpTo(reflectionTableOffset, true);

            for (int i = 0; i < reflectionTableCount; i++)
                Reflections.Add(new Reflection
                                {
                                    Z_Rotation = reader.ReadSingle(),
                                    Length     = reader.ReadSingle(),
                                    Y_Rotation = reader.ReadSingle(),
                                    Height     = reader.ReadSingle()
                                });

            reader.JumpTo(entryTableOffset, true);

            for (int i = 0; i < entryTableCount; i++)
            {
                uint vertexCount       = reader.ReadUInt32(),
                     vertexTableOffset = reader.ReadUInt32(),
                     reflectionIndex   = reader.ReadUInt32();

                long position = reader.BaseStream.Position;

                reader.JumpTo(vertexTableOffset, true);

                for (int v = 0; v < vertexCount; v++)
                    Reflections[(int)reflectionIndex].Vertices.Add(reader.ReadVector3());

                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAv1Header header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(stream, header);

            writer.Write((uint)Reflections.Count);
            writer.AddOffset("reflectionTableOffset");

            writer.Write((uint)Reflections.Count);
            writer.AddOffset("entryTableOffset");

            for (int i = 0; i < Reflections.Count; i++)
            {
                writer.FillInOffset("reflectionTableOffset", true, true, false);

                writer.Write(Reflections[i].Z_Rotation);
                writer.Write(Reflections[i].Length);
                writer.Write(Reflections[i].Y_Rotation);
                writer.Write(Reflections[i].Height);
            }

            for (int i = 0; i < Reflections.Count; i++)
            {
                writer.FillInOffset("entryTableOffset", true, true, false);
                writer.Write(Reflections[i].Vertices.Count);

                writer.AddOffset($"vertexTableOffset_{i}");
                writer.Write((uint)i);
            }

            for (int i = 0; i < Reflections.Count; i++)
            {
                writer.FillInOffset($"vertexTableOffset_{i}", true, true, false);

                foreach (Vector3 vector in Reflections[i].Vertices)
                    writer.WriteByType<Vector3>(vector);
            }

            writer.FinishWrite(header);
        }

        /// <summary>
        /// Exports the offsets and vertices to an XML.
        /// TODO: Make less proprietary...
        /// </summary>
        public void ExportXML(string destination)
        {
            XElement rootElem = new XElement("RAB");

            // Reflections
            for (int i = 0; i < Reflections.Count; i++)
            {
                XElement reflectionElem = new XElement("Reflection");

                XElement positionElem = new XElement("Position");

                positionElem.Add(new XElement("Height", Reflections[i].Height));
                positionElem.Add(new XElement("Length", Reflections[i].Length));
                positionElem.Add(new XElement("Z Rotation", Reflections[i].Z_Rotation));
                positionElem.Add(new XElement("Y Rotation", Reflections[i].Y_Rotation));

                reflectionElem.Add(positionElem);

                foreach (Vector3 vector in Reflections[i].Vertices)
                {
                    XElement verticesElem = new XElement("Vertex");

                    verticesElem.Add(new XElement("X", vector.X));
                    verticesElem.Add(new XElement("Y", vector.Y));
                    verticesElem.Add(new XElement("Z", vector.Z));

                    reflectionElem.Add(verticesElem);
                }

                rootElem.Add(reflectionElem);
            }

            XDocument xml = new XDocument(rootElem);
            xml.Save(destination);
        }

        /// <summary>
        /// Imports the offsets and vertices from an XML.
        /// TODO: Make less proprietary...
        /// </summary>
        public void ImportXML(string filePath)
        {
            XDocument xml = XDocument.Load(filePath);

            // Reflections
            foreach (XElement reflectionElem in xml.Root.Elements("Reflection"))
            {
                Reflection entry = new Reflection();

                // Position
                foreach (XElement positionElem in reflectionElem.Elements("Position"))
                {
                    float.TryParse(positionElem.Element("Height").Value, out entry.Height);
                    float.TryParse(positionElem.Element("Length").Value, out entry.Length);
                    float.TryParse(positionElem.Element("Z Rotation").Value, out entry.Z_Rotation);
                    float.TryParse(positionElem.Element("Y Rotation").Value, out entry.Y_Rotation);
                }

                // Vertices
                foreach (XElement vertexElem in reflectionElem.Elements("Vertex"))
                {
                    float.TryParse(vertexElem.Element("X").Value, out float X);
                    float.TryParse(vertexElem.Element("Y").Value, out float Y);
                    float.TryParse(vertexElem.Element("Z").Value, out float Z);

                    entry.Vertices.Add(new Vector3(X, Y, Z));
                }

                Reflections.Add(entry);
            }
        }
    }
}
