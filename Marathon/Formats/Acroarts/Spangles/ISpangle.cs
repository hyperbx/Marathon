using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public interface ISpangle
    {
        public void Read(BINAReader in_reader, AcroartsBinaryLeafData leafData);
        public void Write(BINAWriter in_writer);
    }
}
