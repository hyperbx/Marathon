using System;
using Marathon.IO.Interfaces;

namespace Marathon.IO
{
    public class BINAHeader : IHeader
    {
        public uint FileSize,         // First UInt32 in the header that defines the overall size of the file.
                    FinalTableLength; // Second UInt32 in the header that defines the size of the file after the header and before the footer.

        // Format version number defined two bytes prior to the signature.
        public ushort Version;

        // General flag for endianness set later on.
        public bool IsBigEndian;

        // File signature written near the end of the header for some horrific reason.
        public const string Signature = "BINA";

        // Endian flags defined one byte prior to the signature.
        public const char BigEndianFlag    = 'B',
                          LittleEndianFlag = 'L';

        public uint FinalTableOffset;
        public bool IsFooterMagicPresent = false;

        public const string FooterMagic = "bvh";
        public const uint FooterMagic2 = 0x10, PointerLength = 0x20;

        public BINAHeader(ushort version = 1, bool isBigEndian = true)
        {
            IsBigEndian = isBigEndian;
            Version = version;
        }

        public BINAHeader(BinaryReaderEx reader)
        {
            IsBigEndian = true;
            Read(reader);
        }

        public virtual void Read(BinaryReaderEx reader)
        {
            long position = reader.BaseStream.Position;

            // Jump to signature...
            reader.BaseStream.Position += 0x14;
            reader.IsBigEndian = false;

            // Get version string and endianness...
            uint flags = reader.ReadUInt32();
            string versionString = "xyz"; // Just 3 chars that would fail ushort.TryParse

            unsafe
            {
                // Set endianness flag...
                reader.IsBigEndian = IsBigEndian = (char)((flags & 0xFF000000) >> 24) == BigEndianFlag;

                /* Quick way to grab the last 3 bytes from the flags UInt32
                 * (which are chars) and stuff them into a string that we can
                 * then safely parse into a ushort via ushort.TryParse... */
                fixed (char* vp = versionString)
                {
                    *vp   = (char)((flags & 0xFF0000) >> 16);
                    vp[1] = (char)((flags & 0xFF00) >> 16);
                    vp[2] = (char)(flags & 0xFF);
                }
            }

            if (!ushort.TryParse(versionString, out Version))
                Console.WriteLine("WARNING: BINA header version was invalid! ({0})", versionString);

            // Return to beginning of the header now to read it with the correct endianness...
            reader.BaseStream.Position = position;

            FileSize = reader.ReadUInt32();
            FinalTableOffset = reader.ReadUInt32();
            FinalTableLength = reader.ReadUInt32();

            // TODO: Unknown.
            uint UnknownUInt32_1 = reader.ReadUInt32();
            if (UnknownUInt32_1 != 0) Console.WriteLine($"WARNING: {nameof(UnknownUInt32_1)} is not zero! ({UnknownUInt32_1})");

            // TODO: Unknown - possibly a flag?
            ushort UnknownUInt16_1 = reader.ReadUInt16();
            if (UnknownUInt16_1 != 0) Console.WriteLine($"WARNING: {nameof(UnknownUInt16_1)} is not zero! ({UnknownUInt16_1})");

            // TODO: Unknown - possibly node count?
            IsFooterMagicPresent = reader.ReadUInt16() == 1;
            reader.JumpAhead(4);

            reader.ReadSignature(4, Signature, false);

            // TODO: Unknown - possibly additional data length?
            uint UnknownUInt32_2 = reader.ReadUInt32();
            if (UnknownUInt32_2 != 0) Console.WriteLine($"WARNING: {nameof(UnknownUInt32_2)} is not zero! ({UnknownUInt32_2})");

            reader.Offset = (uint)reader.BaseStream.Position;
        }

        public virtual void PrepareWrite(BinaryWriterEx writer)
        {
            writer.WriteNulls(PointerLength);
            writer.Offset = PointerLength;
            writer.IsBigEndian = IsBigEndian;
        }

        public virtual void FinishWrite(BinaryWriterEx writer)
        {
            writer.Write(FileSize);
            writer.Write(FinalTableOffset);
            writer.Write(FinalTableLength);

            // TODO: Unknown - possibly padding?
            writer.WriteNulls(4);

            // TODO: Unknown - possibly a flag?
            writer.WriteNulls(2);
            writer.Write(IsFooterMagicPresent ? (ushort)1 : (ushort)0);

            string versionString = Version.ToString();

            if (versionString.Length < 3)
                writer.WriteNulls((uint)(3 - versionString.Length));

            writer.WriteSignature(versionString);
            writer.Write(IsBigEndian ? BigEndianFlag : LittleEndianFlag);
            writer.WriteSignature(Signature);

            // TODO: Unknown.
            writer.WriteNulls(4);
        }

        public virtual void WriteFooterMagic(BinaryWriterEx writer)
        {
            // TODO: Unknown.
            writer.Write(FooterMagic2);

            writer.WriteNulls(4);
            writer.WriteNullTerminatedString(FooterMagic);
        }
    }
}
