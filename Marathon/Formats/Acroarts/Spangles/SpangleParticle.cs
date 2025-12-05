using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class SpangleParticle : ISpangle
    {
        public SpangleParticle() { }

        public SpangleParticle(BINAReader in_reader, AcroartsBinaryLeafData leafData)
        {
            Read(in_reader, leafData);
        }

        public void Read(BINAReader in_reader, AcroartsBinaryLeafData leafData)
        {
            var DefaultPosition = 0x50;
            var Position = in_reader.Position;
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
