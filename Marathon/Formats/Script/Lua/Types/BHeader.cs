using Amicitia.IO.Binary;
using Marathon.Formats.Script.Lua.Decompiler.Extractors;
using Marathon.Formats.Script.Lua.Version;
using Marathon.IO;
using Marathon.IO.Extensions;
using System;

namespace Marathon.Formats.Script.Lua.Types
{
    public class BHeader
    {
        private readonly byte[] _signature = [ 0x1B, 0x4C, 0x75, 0x61 ];
        private readonly byte[] _tail = [ 0x19, 0x93, 0x0D, 0x0A, 0x1A, 0x0A ];

        public VersionBase Version { get; }

        public BIntegerType Integer { get; }

        public BSizeTType SizeT { get; }

        public LBooleanType Boolean { get; }

        public LNumberType Number { get; }

        public LStringType String { get; }

        public LConstantType Constant { get; }

        public LLocalType Local { get; }

        public LUpvalueType Upvalue { get; }

        public LFunctionType Function { get; }

        public ICodeExtractor Extractor { get; }

        public BHeader(BinaryObjectReaderEx in_reader)
        {
            in_reader.CheckSignature(_signature);

            var versionNumber = in_reader.Read<LuaVersion>();

            switch (versionNumber)
            {
                case LuaVersion.Lua50:
                {
                    Version = new Version50();
                    break;
                }

                case LuaVersion.Lua51:
                {
                    Version = new Version51();
                    break;
                }

                case LuaVersion.Lua52:
                {
                    Version = new Version52();
                    break;
                }

                default:
                {
                    var major = (byte)versionNumber >> 4;
                    var minor = (byte)versionNumber & 0x0F;

                    throw new Exception($"Unsupported Lua version: {major}.{minor}");
                }
            }

            if (Version.HasFormat())
            {
                var format = in_reader.Read<byte>();

                if (format != 0)
                    throw new Exception($"Unsupported Lua format: {format}");
            }

            var endianness = in_reader.Read<byte>();

            in_reader.Endianness = endianness switch
            {
                0 => Endianness.Big,
                1 => Endianness.Little,
                _ => throw new Exception($"Invalid endianness: {endianness}"),
            };

            Integer = new BIntegerType(in_reader.Read<byte>());
            SizeT = new BSizeTType(in_reader.Read<byte>());

            var instrSize = in_reader.Read<byte>();

            if (instrSize != 4)
                throw new Exception($"Unsupported instruction size: {instrSize}");

            if (Version.Version == LuaVersion.Lua50)
            {
                Extractor = new CodeExtractor50(in_reader.Read<byte>(), in_reader.Read<byte>(), in_reader.Read<byte>(), in_reader.Read<byte>());
            }
            else
            {
                Extractor = new CodeExtractor51();
            }

            var lNumberSize = in_reader.Read<byte>();

            if (Version.Version == LuaVersion.Lua50)
            {
                Number = new LNumberType(lNumberSize, false);

                in_reader.JumpAhead(8);
            }
            else
            {
                var lNumberIntegralCode = in_reader.Read<byte>();

                if (lNumberIntegralCode > 1)
                    throw new Exception($"Invalid Lua number integral: {lNumberIntegralCode}");

                var lNumberIntegral = lNumberIntegralCode == 1;

                Number = new LNumberType(lNumberSize, lNumberIntegral);
            }

            Boolean = new LBooleanType();
            String = new LStringType();
            Constant = new LConstantType();
            Local = new LLocalType();
            Upvalue = new LUpvalueType();
            Function = Version.GetLFunctionType();

            if (!Version.HasHeaderTail())
                return;
            
            in_reader.CheckSignature(_tail);
        }
    }
}
