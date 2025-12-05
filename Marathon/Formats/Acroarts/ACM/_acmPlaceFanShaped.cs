using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class _acmPlaceFanShaped : IACM
    {
        public uint Start { get; set; }
        public uint End { get; set; }

        //End - Start
        public _acmPlaceFanShaped() { }

        public _acmPlaceFanShaped(BINAReader in_reader, uint count)
        {
            Read(in_reader, count);
        }

        public void Read(BINAReader in_reader, uint count)
        {
            var DefaultPosition = 0x50;
            var Position = in_reader.Position;
            Start = in_reader.Read<uint>();
            End = in_reader.Read<uint>();
            Logger.Warning($"_acmPlaceFanShaped at {Position:x} Count:{count} Start {Start} End {End}");
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
