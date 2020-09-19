// CommonPackage.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 Knuxfan24
 * Copyright (c) 2020 HyperPolygon64
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
using Marathon.IO.Helpers;
using Marathon.IO.Headers;

namespace Marathon.IO.Formats.Miscellaneous
{
    /// <summary>
    /// File base for the Sonic '06 Common.bin format.
    /// </summary>
    public class CommonPackage : FileBase
    {
        // TODO: Understand the unknown values.
        public class ObjectEntry
        {
            public string PropName, ModelName, HKXName, TimeEvent, MaterialAnimation, LuaScript, UnknownString_1, BreakObject, Explosion, ParticleFile, ParticleName, SoundBank, SoundName;
            public uint UnknownUInt32_1, Collision, UnknownUInt32_2, Rigidity, Damage, Potency, UnknownUInt32_3, Health, UnknownUInt32_4, PsiBehaviour;
            public float UnknownFloat_1, DebrisLifetimeBase, DebrisLifetimeModifier;
        }

        public List<ObjectEntry> Entries = new List<ObjectEntry>();

        public override void Load(Stream stream)
        {
            BINAReader reader = new BINAReader(stream);
            reader.ReadHeader();

            // Store first offset after the header.
            long startPosition = reader.BaseStream.Position;

            // Store the offset to the first string entry (repurposed as offset table length).
            uint offsetTableLength = reader.ReadUInt32();

            // Jump back to the first offset so we can iterate through the loop.
            reader.JumpTo(startPosition);

            // Read stream until we've reached the end of the offset table.
            while (reader.BaseStream.Position < offsetTableLength)
            {
                ObjectEntry @object = new ObjectEntry();

                uint objectNameOffset = reader.ReadUInt32();            // Offset to this object's name.
                uint modelOffset = reader.ReadUInt32();                 // Offset to this object's model file.
                uint havokOffset = reader.ReadUInt32();                 // Offset to this object's havok file.
                uint timeEventOffset = reader.ReadUInt32();             // Offset to an Unknown string.
                uint materialAnimationOffset = reader.ReadUInt32();     // Offset to this object's material animation file.
                uint luaScriptOffset = reader.ReadUInt32();             // Offset to this object's Lua Script.
                @object.UnknownUInt32_1 = reader.ReadUInt32();          // Unknown UInt Value. (seems to screw with how the object breaks???)
                uint unknownString1Offset = reader.ReadUInt32();        // Offset to an Unknown string.
                @object.Collision = reader.ReadUInt32();                // This object's collision flag.
                @object.UnknownUInt32_2 = reader.ReadUInt32();          // Unknown UInt Value. (seems to screw with this object's physics and the Homing Attack???)
                @object.Rigidity = reader.ReadUInt32();                 // This object's rigidity value. (not too sure that's the right name for that.
                                                                        // 0 = Standard
                                                                        // 1 = Colliable Debris
                                                                        // 2 = Colliable but non intersectable debries
                                                                        // 3 = Debris
                @object.Damage = reader.ReadUInt32();                   // The damage this object does when colliding with an enemy with force.
                @object.UnknownFloat_1 = reader.ReadSingle();           // Unknown Floating Point Value. (seems to be something regarding the object breaking from gravity or something????)
                @object.Potency = reader.ReadUInt32();                  // How much force an attack needs to have to damage this object. (not sure potency is the right name for that)
                @object.UnknownUInt32_3 = reader.ReadUInt32();          // Unknown UInt Value.
                @object.Health = reader.ReadUInt32();                   // This object's health value. (How many times this object needs to be attacked to break?)
                @object.DebrisLifetimeBase = reader.ReadSingle();       // The amount of time this object lasts if it is setup as a set of debris.
                @object.DebrisLifetimeModifier = reader.ReadSingle();   // Modifier to randomly(?) increase the time for debris to disappear to add variety.
                @object.UnknownUInt32_4 = reader.ReadUInt32();          // Unknown UInt Value.
                uint breakObjectOffset = reader.ReadUInt32();           // Offset to what should be spawned (if anything) upon breaking this object.
                uint explosionOffset = reader.ReadUInt32();             // Offset to what entry in Explosion.bin should be used upon breaking this object.
                uint particleFileOffset = reader.ReadUInt32();          // Offset to the particle file used upon breaking this object.
                uint particleNameOffset = reader.ReadUInt32();          // Offset to the particle used upon breaking this object.
                uint soundBankOffset = reader.ReadUInt32();             // Offset to the sound bank used upon breaking this object.
                uint soundNameOffset = reader.ReadUInt32();             // Offset to the sound used upon breaking this object.
                @object.PsiBehaviour = reader.ReadUInt32();             // How this object behaves when using Silver's pyschokenesis.
                                                                        // 0 = Can't be picked up
                                                                        // 1 = Normal Grab
                                                                        // 2 = Kingdom Valley Pendulum
                                                                        // 3 = Super Fast Projectile Launch

                // Store current position to jump back to for the next entry.
                long position = reader.BaseStream.Position;

                // Read all the string values.
                reader.JumpTo(objectNameOffset, true);
                @object.PropName = reader.ReadNullTerminatedString();
                reader.JumpTo(modelOffset, true);
                @object.ModelName = reader.ReadNullTerminatedString();
                reader.JumpTo(havokOffset, true);
                @object.HKXName = reader.ReadNullTerminatedString();
                reader.JumpTo(timeEventOffset, true);
                @object.TimeEvent = reader.ReadNullTerminatedString();
                reader.JumpTo(materialAnimationOffset, true);
                @object.MaterialAnimation = reader.ReadNullTerminatedString();
                reader.JumpTo(luaScriptOffset, true);
                @object.LuaScript = reader.ReadNullTerminatedString();
                reader.JumpTo(unknownString1Offset, true);
                @object.UnknownString_1 = reader.ReadNullTerminatedString();
                reader.JumpTo(breakObjectOffset, true);
                @object.BreakObject = reader.ReadNullTerminatedString();
                reader.JumpTo(explosionOffset, true);
                @object.Explosion = reader.ReadNullTerminatedString();
                reader.JumpTo(particleFileOffset, true);
                @object.ParticleFile = reader.ReadNullTerminatedString();
                reader.JumpTo(particleNameOffset, true);
                @object.ParticleName = reader.ReadNullTerminatedString();
                reader.JumpTo(soundBankOffset, true);
                @object.SoundBank = reader.ReadNullTerminatedString();
                reader.JumpTo(soundNameOffset, true);
                @object.SoundName = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next object.
                reader.JumpTo(position);

                // Save object entry into the Entries list.
                Entries.Add(@object);
            }
        }


        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            // Write the objects.
            for(int i = 0; i < Entries.Count; i++)
            {
                writer.AddString($"object{i}Name", Entries[i].PropName);
                writer.AddString($"object{i}ModelName", Entries[i].ModelName);
                writer.AddString($"object{i}HKXName", Entries[i].HKXName);
                writer.AddString($"object{i}TimeEvent", Entries[i].TimeEvent);
                writer.AddString($"object{i}MaterialAnimation", Entries[i].MaterialAnimation);
                writer.AddString($"object{i}LuaScript", Entries[i].LuaScript);
                writer.Write(Entries[i].UnknownUInt32_1);
                writer.AddString($"object{i}UnknownString1", Entries[i].UnknownString_1);
                writer.Write(Entries[i].Collision);
                writer.Write(Entries[i].UnknownUInt32_2);
                writer.Write(Entries[i].Rigidity);
                writer.Write(Entries[i].Damage);
                writer.Write(Entries[i].UnknownFloat_1);
                writer.Write(Entries[i].Potency);
                writer.Write(Entries[i].UnknownUInt32_3);
                writer.Write(Entries[i].Health);
                writer.Write(Entries[i].DebrisLifetimeBase);
                writer.Write(Entries[i].DebrisLifetimeModifier);
                writer.Write(Entries[i].UnknownUInt32_4);
                writer.AddString($"object{i}BreakObject", Entries[i].BreakObject);
                writer.AddString($"object{i}Explosion", Entries[i].Explosion);
                writer.AddString($"object{i}ParticleFile", Entries[i].ParticleFile);
                writer.AddString($"object{i}ParticleName", Entries[i].ParticleName);
                writer.AddString($"object{i}SoundBank", Entries[i].SoundBank);
                writer.AddString($"object{i}SoundName", Entries[i].SoundName);
                writer.Write(Entries[i].PsiBehaviour);
            }

            // Write the footer.
            writer.WriteNulls(4);
            writer.FinishWrite(Header);
        }

        public void ExportXML(string filepath)
        {
            // Root element.
            XElement rootElem = new XElement("Common");

            // Object elements.
            foreach (ObjectEntry obj in Entries)
            {
                XElement objElem = new XElement("Object");
                XAttribute NameAttr = new XAttribute("ObjectName", obj.PropName);
                XElement ModelElem = new XElement("Model", obj.ModelName);
                XElement HavokElem = new XElement("Havok", obj.HKXName);
                XElement TimeEventElem = new XElement("TimeEvent", obj.TimeEvent);
                XElement MaterialAnimElem = new XElement("MaterialAnimation", obj.MaterialAnimation);
                XElement LuaElem = new XElement("LuaScript", obj.LuaScript);
                XElement UnknownUInt1Elem = new XElement("UnknownUInt1", obj.UnknownUInt32_1);
                XElement UnknownString1Elem = new XElement("UnknownString1", obj.UnknownString_1);
                XElement CollisionElem = new XElement("ColisionFlag", obj.Collision);
                XElement UnknownUInt2Elem = new XElement("UnknownUInt2", obj.UnknownUInt32_2);
                XElement RigidityElem = new XElement("Rigidity", obj.Rigidity);
                XElement DamageElem = new XElement("Damage", obj.Damage);
                XElement UnknownFloat1Elem = new XElement("UnknownFloat1", obj.UnknownFloat_1);
                XElement PotencyElem = new XElement("Potency", obj.Potency);
                XElement UnknownUInt3Elem = new XElement("UnknownUInt3", obj.UnknownUInt32_3);
                XElement HealthElem = new XElement("Health", obj.Health);
                XElement DebrisLifetimeBaseElem = new XElement("DebrisLifetimeBase", obj.DebrisLifetimeBase);
                XElement DebrisLifetimeModifierElem = new XElement("DebrisLifetimeModifier", obj.DebrisLifetimeModifier);
                XElement UnknownUInt4Elem = new XElement("UnknownUInt4", obj.UnknownUInt32_4);
                XElement BreakObjectElem = new XElement("BreakObject", obj.BreakObject);
                XElement ExplosionElem = new XElement("Explosion", obj.Explosion);

                XAttribute ParticleBankAttr = new XAttribute("ParticleBank", obj.ParticleFile);
                XElement ParticleElem = new XElement("Particle", obj.ParticleName);
                ParticleElem.Add(ParticleBankAttr);

                XAttribute SoundBankAttr = new XAttribute("SoundBank", obj.SoundBank);
                XElement SoundElem = new XElement("Sound", obj.SoundName);
                SoundElem.Add(SoundBankAttr);

                XElement PsiBehaviourElem = new XElement("PsiBehaviour", obj.PsiBehaviour);

                objElem.Add(NameAttr, ModelElem, HavokElem, TimeEventElem, MaterialAnimElem, LuaElem, UnknownUInt1Elem, UnknownString1Elem, CollisionElem, UnknownUInt2Elem,
                    RigidityElem, DamageElem, UnknownFloat1Elem, PotencyElem, UnknownUInt3Elem, HealthElem, DebrisLifetimeBaseElem, DebrisLifetimeModifierElem, UnknownUInt4Elem,
                    BreakObjectElem, ExplosionElem, ParticleElem, SoundElem, PsiBehaviourElem);
                rootElem.Add(objElem);
            }

            // Save XML.
            XDocument xml = new XDocument(rootElem);
            xml.Save(filepath);
        }

        public void ImportXML(string filepath)
        {
            // Load XML.
            XDocument xml = XDocument.Load(filepath);

            // Loop through object nodes.
            foreach (XElement objectElem in xml.Root.Elements("Object"))
            {
                // Read Object Values.
                ObjectEntry @object = new ObjectEntry
                {
                    PropName = objectElem.Attribute("ObjectName").Value,
                    ModelName = objectElem.Element("Model").Value,
                    HKXName = objectElem.Element("Havok").Value,
                    TimeEvent = objectElem.Element("TimeEvent").Value,
                    MaterialAnimation = objectElem.Element("MaterialAnimation").Value,
                    LuaScript = objectElem.Element("LuaScript").Value,
                    UnknownUInt32_1 = uint.Parse(objectElem.Element("UnknownUInt1").Value),
                    UnknownString_1 = objectElem.Element("UnknownString1").Value,
                    Collision = uint.Parse(objectElem.Element("ColisionFlag").Value),
                    UnknownUInt32_2 = uint.Parse(objectElem.Element("UnknownUInt2").Value),
                    Rigidity = uint.Parse(objectElem.Element("Rigidity").Value),
                    Damage = uint.Parse(objectElem.Element("Damage").Value),
                    UnknownFloat_1 = float.Parse(objectElem.Element("UnknownFloat1").Value),
                    Potency = uint.Parse(objectElem.Element("Potency").Value),
                    UnknownUInt32_3 = uint.Parse(objectElem.Element("UnknownUInt3").Value),
                    Health = uint.Parse(objectElem.Element("Health").Value),
                    DebrisLifetimeBase = float.Parse(objectElem.Element("DebrisLifetimeBase").Value),
                    DebrisLifetimeModifier = float.Parse(objectElem.Element("DebrisLifetimeModifier").Value),
                    UnknownUInt32_4 = uint.Parse(objectElem.Element("UnknownUInt4").Value),
                    BreakObject = objectElem.Element("BreakObject").Value,
                    Explosion = objectElem.Element("Explosion").Value,
                    ParticleFile = objectElem.Element("Particle").Attribute("ParticleBank").Value,
                    ParticleName = objectElem.Element("Particle").Value,
                    SoundBank = objectElem.Element("Sound").Attribute("SoundBank").Value,
                    SoundName = objectElem.Element("Sound").Value,
                    PsiBehaviour = uint.Parse(objectElem.Element("PsiBehaviour").Value)
                };

                // Add Object to Entries List.
                Entries.Add(@object);
            }
        }
    }
}
