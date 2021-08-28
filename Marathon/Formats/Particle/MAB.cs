using System;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Particle
{
    public class MAB : FileBase // TODO: Identify what 'MAB' is...
    {
        public MAB() { }

        public MAB(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                default:
                {
                    Load(file);

                    break;
                }
            }
        }

        public const string Signature = "MRAB",
                            Extension = ".mab";

        public override void Load(Stream stream)
        {
            BinaryReaderEx reader = new(stream, true);

            reader.ReadSignature(4, Signature);

            // MRAB Chunk, contains a BINA Header for some reason.
            uint mrabChunkSize = reader.ReadUInt32();
            uint fileSize = reader.ReadUInt32();
            uint UnknownUInt32_1 = reader.ReadUInt32(); // Always zero.

            BINAHeader bina = new();
            bina.Read(reader);

            reader.Offset = mrabChunkSize + 0x20; // MRAB chunk size + BINA header size

            uint abdaOffset = reader.ReadUInt32(); // Always 0x20.
            uint dataLength = reader.ReadUInt32(); // Seems to be the length from the end of the BINA Header to the start of the ABRS chunk?
            reader.JumpTo(abdaOffset, true);

            // ABDA Chunk
            string ChunkID = new(reader.ReadChars(0x04));
            uint UnknownUInt32_2 = reader.ReadUInt32();         // Seemed like the length of the ABDA chunk and the ABDT chunk, but isn't?
            uint Length = reader.ReadUInt32();
            uint UnknownUInt32_3 = reader.ReadUInt32();         // Always zero.
            uint Version = reader.ReadUInt32();                 // Should always be 2006020901 (probably a timestamp - requires match by game executable).
            uint ABDTCount = reader.ReadUInt32();               // How many ABDT chunks this file contains.
            uint SizeUntilPOF0 = reader.ReadUInt32();           // Includes the header of the ABDA chunk and goes down to the first POF0 chunk.
            uint UnknownUInt32_4 = reader.ReadUInt32();         // Always zero.
            uint Length2ElectricBoogaloo = reader.ReadUInt32(); // Same as Length, for some reason.
            int HasExtraData = reader.ReadInt32();

            if (HasExtraData == -1)
            {
                // TODO: Read extra data after this flag...
            }

            // TODO: Finish this?
        } 
    }
}
