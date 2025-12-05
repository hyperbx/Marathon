using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class _acmRotateNormal : IACM
    {
        //Degrees
        public float AngleX;
        public float AngleY;
        public float AngleZ;
        public _acmRotateNormal() { }

        public _acmRotateNormal(BINAReader in_reader, uint count)
        {
            Read(in_reader, count);
        }

        public void Read(BINAReader in_reader, uint count)
        {
            var DefaultPosition = 0x50;
            var Position = in_reader.Position;
            AngleX = in_reader.ReadSingle();
            AngleY = in_reader.ReadSingle();
            AngleZ = in_reader.ReadSingle();
            Logger.Warning($"_acmRotateNormal at {Position:x} Count:{count} AngleX {AngleX} AngleY {AngleY} AngleZ {AngleZ}");
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
