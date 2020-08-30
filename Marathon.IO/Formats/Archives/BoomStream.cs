using System.IO;
using System.Collections.Generic;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Archives
{
    /// <summary>
    /// File base for the Sonic Boom: Rise of Lyric WIIU.STREAM format.
    /// </summary>
    public class BoomStream : FileBase
    {
        public class Entry
        {
            public uint CompressedSize,
                        DecompressedSize;

            public ulong Hash;

            public string Name;

            public byte[] Data;
        }

        public const string Signature = "strm",
                            Extension = ".wiiu.stream";

        public List<Entry> Entries = new List<Entry>();

        public override void Load(Stream stream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            // Read file signature.
            string signature = reader.ReadString();
            if (signature != Signature)
                throw new InvalidSignatureException(Signature, signature);

            while (reader.BaseStream.Position != stream.Length)
            {
                Entry entry = new Entry()
                {
                    CompressedSize = reader.ReadUInt32(),
                    DecompressedSize = reader.ReadUInt32(),
                    Hash = reader.ReadUInt64(),
                    Name = reader.ReadNullTerminatedString()
                };

                if (entry.CompressedSize == 0)
                    entry.Data = reader.ReadBytes((int)entry.DecompressedSize);

                else
                {
                    entry.Data = reader.ReadBytes((int)entry.CompressedSize);

                    for (int i = 0; i < entry.CompressedSize; i++)
                    {
                        if ((entry.Data[i] & 0x40) != 0)
                        {
                            // TODO
                        }
                    }
                }
            }
        }

        public override void Save(Stream stream)
        {
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(stream, true);

            writer.WriteSignature(Signature);

            foreach (Entry entry in Entries)
            {
                writer.Write(0); // TODO
                writer.Write(entry.DecompressedSize);
                writer.Write(entry.Hash);
                writer.WriteNullTerminatedString(entry.Name);
                writer.Write(entry.Data);
            }
        }
    }
}
