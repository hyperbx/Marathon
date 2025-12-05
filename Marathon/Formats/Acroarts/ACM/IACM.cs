using Marathon.IO.Types.BINA;

namespace Marathon.Formats.Acroarts.Chunks
{
    public interface IACM
    {
        void Read(BINAReader in_reader,uint count);

        void Write(BINAWriter in_writer);

    }
}
