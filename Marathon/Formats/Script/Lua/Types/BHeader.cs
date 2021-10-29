using System;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua.Types
{
    public class BHeader
    {
        public static readonly byte[] Signature = { 0x1B, 0x4C, 0x75, 0x61 };
        public static readonly byte[] LuaTail = { 0x19, 0x93, 0x0D, 0x0A, 0x1A, 0x0A };

        public readonly Version Version;
        public readonly BIntegerType Integer;
        public readonly BSizeTType SizeT;
        public readonly LBooleanType Bool;
        public readonly LNumberType Number;
        public readonly LStringType String;
        public readonly LConstantType Constant;
        public readonly LLocalType Local;
        public readonly LUpvalueType Upvalue;
        public readonly LFunctionType Function;
        public readonly ICodeExtract Extractor;

        public BHeader(BinaryReaderEx reader)
        {
            // Read script signature.
            reader.ReadSignature(4, Signature, false);

            int versionNumber = reader.ReadByte();

            switch (versionNumber)
            {
                case 0x50:
                {
                    Version = Version.LUA50;
                    break;
                }

                case 0x51:
                {
                    Version = Version.LUA51;
                    break;
                }

                case 0x52:
                {
                    Version = Version.LUA52;
                    break;
                }

                default:
                {
                    int major = versionNumber >> 4;
                    int minor = versionNumber & 0x0F;

                    throw new Exception($"The input chunk's Lua version is {major}.{minor}; Marathon can only handle Lua 5.0, Lua 5.1 and Lua 5.2.");
                }
            }

            Console.WriteLine($"Version: 0x{versionNumber:X}");

            if (Version.HasFormat())
            {
                int format = reader.ReadByte();

                if (format != 0)
                    throw new Exception($"The input chunk reports a non-standard Lua format: {format}");

                Console.WriteLine($"Chunk format: {format}");
            }

            int endianness = reader.ReadByte();

            switch (endianness)
            {
                case 0:
                    reader.IsBigEndian = true;
                    break;

                case 1:
                    reader.IsBigEndian = false;
                    break;

                default:
                    throw new Exception($"The input chunk reports an invalid endianness: {endianness}");
            }

            Console.WriteLine($"Endianness: {endianness}" + (endianness == 0 ? " (big)" : " (little)"));

            int intSize = reader.ReadByte();

            Console.WriteLine($"int size: {intSize}");

            Integer = new BIntegerType(intSize);

            int sizeTSize = reader.ReadByte();

            Console.WriteLine($"size_t size: {sizeTSize}");

            SizeT = new BSizeTType(sizeTSize);

            int instructionSize = reader.ReadByte();

            Console.WriteLine($"Instruction size: {instructionSize}");

            if (instructionSize != 4)
                throw new Exception($"The input chunk reports an unsupported instruction size: {instructionSize} bytes");

            if (Version == Version.LUA50)
            {
                Extractor = new Code50(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            }
            else
            {
                Extractor = new Code51();
            }

            int lNumberSize = reader.ReadByte();

            Console.WriteLine($"Number size: {lNumberSize}");

            if (Version == Version.LUA50)
            {
                Number = new LNumberType(lNumberSize, false);
                reader.ReadInt64();
            }
            else
            {
                int lNumberIntegralCode = reader.ReadByte();

                Console.WriteLine($"Number integral code: {lNumberIntegralCode}");

                if (lNumberIntegralCode > 1)
                    throw new Exception($"The input chunk reports an invalid code for lua number integralness: {lNumberIntegralCode}");

                bool lNumberIntegral = lNumberIntegralCode == 1;

                Number = new LNumberType(lNumberSize, lNumberIntegral);
            }

            Bool = new LBooleanType();
            String = new LStringType();
            Constant = new LConstantType();
            Local = new LLocalType();
            Upvalue = new LUpvalueType();
            Function = Version.GetLFunctionType();

            if (Version.HasHeaderTail())
                reader.ReadSignature(6, LuaTail);
        }
    }
}
