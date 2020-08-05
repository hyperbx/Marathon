using System;
using Marathon.IO.Exceptions;
using Marathon.IO.Formats.Scripts.Lua.Decompiler;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class BHeader
    {
        public const uint signature = 0x1B4C7561;
        public static readonly byte[] luacTail = { 0x19, 0x93, 0x0D, 0x0A, 0x1A, 0x0A };

        public readonly bool debug = false;

        public readonly Version version;
        public readonly BIntegerType integer;
        public readonly BSizeTType sizeT;
        public readonly LBooleanType @bool;
        public readonly LNumberType number;
        public readonly LStringType @string;
        public readonly LConstantType constant;
        public readonly LLocalType local;
        public readonly LUpvalueType upvalue;
        public readonly LFunctionType function;
        public readonly ICodeExtract extractor;

        public BHeader(ExtendedBinaryReader buffer)
        {
            // Read script signature.
            uint _signature = buffer.ReadUInt32();
            if (_signature != signature)
                throw new InvalidSignatureException(signature.ToString(), _signature.ToString());

            int versionNumber = 0xFF & buffer.ReadByte();

            switch (versionNumber)
            {
                case 0x50:
                {
                    version = Version.LUA50;
                    break;
                }

                case 0x51:
                {
                    version = Version.LUA51;
                    break;
                }

                case 0x52:
                {
                    version = Version.LUA52;
                    break;
                }

                default:
                {
                    int major = versionNumber >> 4;
                    int minor = versionNumber & 0x0F;

                    throw new Exception($"The input chunk's Lua version is {major}.{minor}; unluac can only handle Lua 5.0, Lua 5.1 and Lua 5.2.");
                }
            }

            if (debug)
                Console.WriteLine($"-- version: 0x{versionNumber:X}");

            if (version.hasFormat())
            {
                int format = 0xFF & buffer.ReadByte();

                if (format != 0)
                    throw new Exception($"The input chunk reports a non-standard Lua format: {format}");

                if (debug)
                    Console.WriteLine($"-- format: {format}");
            }

            int endianness = 0xFF & buffer.ReadByte();

            switch (endianness)
            {
                case 0:
                {
                    buffer.IsBigEndian = true;
                    break;
                }

                case 1:
                {
                    buffer.IsBigEndian = false;
                    break;
                }

                default:
                {
                    throw new Exception($"The input chunk reports an invalid endianness: {endianness}");
                }
            }

            if (debug)
                Console.WriteLine($"-- endianness: {endianness}" + (endianness == 0 ? " (big)" : " (little)"));

            int intSize = 0xFF & buffer.ReadByte();

            if (debug)
                Console.WriteLine($"-- int size: {intSize}");

            integer = new BIntegerType(intSize);

            int sizeTSize = 0xFF & buffer.ReadByte();

            if (debug)
                Console.WriteLine($"-- size_t size: {sizeTSize}");

            sizeT = new BSizeTType(sizeTSize);

            int instructionSize = 0xFF & buffer.ReadByte();

            if (debug)
                Console.WriteLine($"-- instruction size: {instructionSize}");

            if (instructionSize != 4)
                throw new Exception($"The input chunk reports an unsupported instruction size: {instructionSize} bytes");

            if (version == Version.LUA50)
            {
                int sizeOp = 0xFF & buffer.ReadByte(),
                    sizeA = 0xFF & buffer.ReadByte(),
                    sizeB = 0xFF & buffer.ReadByte(),
                    sizeC = 0xFF & buffer.ReadByte();

                extractor = new Code50(sizeOp, sizeA, sizeB, sizeC);
            }
            else
                extractor = new Code51();

            int lNumberSize = 0xFF & buffer.ReadByte();

            if (debug)
                Console.WriteLine($"-- Lua number size: {lNumberSize}");

            if (version == Version.LUA50)
            {
                number = new LNumberType(lNumberSize, false);
                buffer.ReadInt64();
            }
            else
            {
                int lNumberIntegralCode = 0xFF & buffer.ReadByte();

                if (debug)
                    Console.WriteLine($"-- Lua number integral code: {lNumberIntegralCode}");

                if (lNumberIntegralCode > 1)
                    throw new Exception($"The input chunk reports an invalid code for lua number integralness: {lNumberIntegralCode}");

                bool lNumberIntegral = lNumberIntegralCode == 1;

                number = new LNumberType(lNumberSize, lNumberIntegral);
            }

            @bool = new LBooleanType();
            @string = new LStringType();
            constant = new LConstantType();
            local = new LLocalType();
            upvalue = new LUpvalueType();
            function = version.getLFunctionType();

            if (version.hasHeaderTail())
            {
                for (int i = 0; i < luacTail.Length; i++)
                {
                    byte readBuffer = buffer.ReadByte();

                    if (readBuffer != luacTail[i])
                        throw new InvalidSignatureException(luacTail[i].ToString(), readBuffer.ToString());
                }
            }
        }
    }
}
