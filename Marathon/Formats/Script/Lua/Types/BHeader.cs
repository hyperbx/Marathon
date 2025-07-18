using Amicitia.IO.Binary;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.IO;
using Marathon.IO.Extensions;
using System;

namespace Marathon.Formats.Script.Lua.Types
{
    public class BHeader
    {
        private readonly byte[] _signature = [ 0x1B, 0x4C, 0x75, 0x61 ];

        public BIntegerType Integer { get; }

        public BSizeTType SizeT { get; }

        public LBooleanType Boolean { get; }

        public LNumberType Number { get; }

        public LStringType String { get; }

        public LConstantType Constant { get; }

        public LLocalType Local { get; }

        public LUpvalueType Upvalue { get; }

        public LFunctionType Function { get; }

        public CodeExtractor Extractor { get; }

        public BHeader(BinaryObjectReaderEx in_reader)
        {
            in_reader.CheckSignature(_signature);

            var version = in_reader.Read<byte>();

            if (version != 0x50)
            {
                var major = version >> 4;
                var minor = version & 0x0F;

                throw new Exception($"Unsupported Lua version: {major}.{minor}");
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

            Extractor = new CodeExtractor(in_reader.Read<byte>(), in_reader.Read<byte>(), in_reader.Read<byte>(), in_reader.Read<byte>());

            var lNumberSize = in_reader.Read<byte>();

            Number = new LNumberType(lNumberSize, false);

            in_reader.JumpAhead(8);

            Boolean = new LBooleanType();
            String = new LStringType();
            Constant = new LConstantType();
            Local = new LLocalType();
            Upvalue = new LUpvalueType();
            Function = new LFunctionType();
        }
    }
}
