using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class _acmBlurBelt : IACM
    {
        public List<byte> Data { get; set; }
        public _acmBlurBelt() { }

        public _acmBlurBelt(BINAReader in_reader, uint count)
        {
            Read(in_reader, count);
        }

        public void Read(BINAReader in_reader, uint count)
        {
            var DefaultPosition = 0x50;
            var Position = in_reader.Position;
            Data = in_reader.ReadArray<byte>((int)(count * 4)).ToList();
            Logger.Warning($"_acmBlurBelt at {Position:x} Count:{count}");
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
