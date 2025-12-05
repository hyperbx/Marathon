using Marathon.IO.Types.BINA;

namespace Marathon.Formats.Acroarts.Chunks
{
    public interface INode
    {
        void Read(BINAReader in_reader);

        void Write(BINAWriter in_writer);
    }
}
