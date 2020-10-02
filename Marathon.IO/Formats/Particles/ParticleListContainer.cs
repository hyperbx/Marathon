// ParticleListContainer.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
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

namespace Marathon.IO.Formats.Particles
{
    /// <summary>
    /// <para>File base for the PLC format.</para>
    /// <para>Used in SONIC THE HEDGEHOG.</para>
    /// </summary>
    public class ParticleListContainer : FileBase
    {
        public class Particle
        {
            public string Name1, Name2, FileName; // TODO: not sure what Name2 actually is - sometimes it's the same as Name1, other times it's not, sometimes it's empty.
            public uint Unknown;                  // TODO: Unknown - flags?
        }

        public const string Extension = ".plc";

        public string Name;
        public List<Particle> Particles = new List<Particle>();
        
        public override void Load(Stream fileStream)
        {
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            uint NameOffset = reader.ReadUInt32();
            long pos = reader.BaseStream.Position;
            reader.JumpTo(NameOffset, true);
            Name = reader.ReadNullTerminatedString();
            reader.JumpTo(pos);

            uint EntryTableOffset = reader.ReadUInt32();
            uint EntryCount = reader.ReadUInt32();

            for(int i = 0; i < EntryCount; i++)
            {
                Particle particle = new Particle();

                uint particleName1Offset    = reader.ReadUInt32();
                uint particleName2Offset    = reader.ReadUInt32();
                uint particleFileNameOffset = reader.ReadUInt32();

                particle.Unknown = reader.ReadUInt32(); // TODO: Unknown - flags?

                pos = reader.BaseStream.Position;

                reader.JumpTo(particleName1Offset, true);
                particle.Name1 = reader.ReadNullTerminatedString();

                reader.JumpTo(particleName2Offset, true);
                particle.Name2 = reader.ReadNullTerminatedString();

                reader.JumpTo(particleFileNameOffset, true);
                particle.FileName = reader.ReadNullTerminatedString();

                Particles.Add(particle);
                reader.JumpTo(pos);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            writer.AddString("name", Name);
            writer.AddOffset("entryTable");
            writer.Write(Particles.Count);

            // Particles.
            writer.FillInOffset("entryTable", true);

            for (int i = 0; i < Particles.Count; i++)
            {
                writer.AddString($"particle{i}Name1", Particles[i].Name1);
                writer.AddString($"particle{i}Name2", Particles[i].Name2);
                writer.AddString($"particle{i}FileName", Particles[i].FileName);
                writer.Write(Particles[i].Unknown);
            }

            // Write the footer.
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filepath)
        {
            // Root element.
            XElement rootElem = new XElement("PLC");
            XAttribute nameElem = new XAttribute("Name", Name);
            rootElem.Add(nameElem);

            // Particles elements.
            foreach (Particle particle in Particles)
            {
                XElement particleElem         = new XElement("Particle");
                XElement particleName1Elem1   = new XElement("Name1", particle.Name1);
                XElement particleName1Elem2   = new XElement("Name2", particle.Name2);
                XElement particleFileNameElem = new XElement("FileName", particle.FileName);
                XElement particleUnknownElem  = new XElement("Unknown", particle.Unknown);

                // Add Nodes to appropriate XML Elements.
                particleElem.Add(particleName1Elem1, particleName1Elem2, particleFileNameElem, particleUnknownElem);
                rootElem.Add(particleElem);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filepath);
        }

        public void ImportXML(string filepath)
        {
            // Load XML and get Name value.
            XDocument xml = XDocument.Load(filepath);
            Name = xml.Root.Attribute("Name").Value;

            // Loop through particle nodes.
            foreach (XElement particleElem in xml.Root.Elements("Particle"))
            {
                Particle particle = new Particle
                {
                    Name1 = particleElem.Element("Name1").Value,
                    Name2 = particleElem.Element("Name2").Value,
                    FileName = particleElem.Element("FileName").Value,
                    Unknown = uint.Parse(particleElem.Element("Unknown").Value)
                };

                Particles.Add(particle);
            }
        }
    }
}
