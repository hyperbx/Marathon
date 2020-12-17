// ParticleTextureBank.cs is licensed under the MIT License:
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
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO.Headers;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Particles
{
    /// <summary>
    /// <para>File base for the PTB format.</para>
    /// <para>Used in SONIC THE HEDGEHOG.</para>
    /// </summary>
    public class ParticleTextureBank : FileBase
    {
        public ParticleTextureBank(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".xml":
                    ImportXML(file);
                    break;
                default:
                    Load(file);
                    break;
            }
        }

        public class Particle
        {
            public string Name, FileName;
            public uint Unknown1, Unknown2; // TODO: Figure out what these are, probably flags?
        }

        public const string Signature = "BTEP", Extension = ".peb";

        public List<Particle> Particles = new List<Particle>();
        public string Name;

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            string signature = reader.ReadSignature(4);
            if (signature != Signature) throw new InvalidSignatureException(Signature, signature);

            reader.JumpAhead(8);

            uint particleCount = reader.ReadUInt32();

            Name = new string(reader.ReadChars(32));

            uint OffsetTablePosition = reader.ReadUInt32();

            for(int i = 0; i < particleCount; i++)
            {
                Particle particle = new Particle()
                {
                    Name     = new string(reader.ReadChars(32)),
                    FileName = new string(reader.ReadChars(128)),
                    Unknown1 = reader.ReadUInt32(),
                    Unknown2 = reader.ReadUInt32()
                };

                Particles.Add(particle);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            writer.WriteSignature(Signature);
            writer.WriteNulls(8);
            writer.Write(Particles.Count);
            writer.Write(string.Concat(Name.Take(32)));
            writer.AddOffset("offsetTable");

            // Particles.
            writer.FillInOffset("offsetTable", true);

            for (int i = 0; i < Particles.Count; i++)
            {
                writer.Write(string.Concat(Particles[i].Name.Take(32)));
                writer.Write(string.Concat(Particles[i].FileName.Take(128)));
                writer.Write(Particles[i].Unknown1);
                writer.Write(Particles[i].Unknown2);
            }

            // Write the footer.
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filepath)
        {
            // Root element.
            XElement rootElem = new XElement("PTB");
            XAttribute nameElem = new XAttribute("Name", Name.Replace("\0", ""));
            rootElem.Add(nameElem);

            // Particles elements.
            foreach (Particle particle in Particles)
            {
                XElement particleElem         = new XElement("Particle");
                XElement particleNameElem     = new XElement("Name", particle.Name.Replace("\0", ""));
                XElement particleFileNameElem = new XElement("FileName", particle.FileName.Replace("\0", ""));
                XElement particleUnknown1Elem = new XElement("Unknown1", particle.Unknown1);
                XElement particleUnknown2Elem = new XElement("Unknown2", particle.Unknown2);

                // Add Nodes to appropriate XML Elements.
                particleElem.Add(particleNameElem, particleFileNameElem, particleUnknown1Elem, particleUnknown2Elem);
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
            Name = xml.Root.Attribute("Name").Value.PadRight(32, '\0');

            // Loop through particle nodes.
            foreach (XElement particleElem in xml.Root.Elements("Particle"))
            {
                Particle particle = new Particle
                {
                    Name     = particleElem.Element("Name").Value.PadRight(32, '\0'),
                    FileName = particleElem.Element("FileName").Value.PadRight(128, '\0'),
                    Unknown1 = uint.Parse(particleElem.Element("Unknown1").Value),
                    Unknown2 = uint.Parse(particleElem.Element("Unknown2").Value)
                };
                Particles.Add(particle);
            }
        }
    }
}
