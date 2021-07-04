namespace Marathon.IO.Interfaces
{
    public interface IHeader
    {
        /// <summary>
        /// Reads the header.
        /// </summary>
        void Read(BinaryReaderEx reader);

        /// <summary>
        /// Writes null bytes to be filled in later with header data by FinishWrite and sets the offset of the given writer if necessary.
        /// This should be used before writing anything else to the file.
        /// </summary>
        void PrepareWrite(BinaryWriterEx writer);

        /// <summary>
        /// Writes the actual header data.
        /// This should be used after all other data has been written to the file - including the footer.
        /// This does not automatically jump back to the beginning of the stream!
        /// </summary>
        void FinishWrite(BinaryWriterEx writer);
    }
}