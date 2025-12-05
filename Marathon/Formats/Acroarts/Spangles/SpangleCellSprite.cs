using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class SpangleCellSprite : ISpangle
    {
        public SpangleCellSprite() { }

        public SpangleCellSprite(BINAReader in_reader, AcroartsBinaryLeafData leafData)
        {
            Read(in_reader, leafData);
        }

        public void Read(BINAReader in_reader, AcroartsBinaryLeafData leafData)
        {
            var DefaultPosition = 0x50;
            var Position = in_reader.Position;

            if (leafData.ResourceIndices2.Count > 0 && leafData.ResourceIndices2.Count < 2)
            {
                Logger.Warning($"SpangleCellSprite leafData.ResourceIndices2.Count < 2, should be in range >1<N");
            }
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
