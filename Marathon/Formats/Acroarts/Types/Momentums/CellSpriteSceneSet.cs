using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class CellSpriteSceneSet : IMomentumParamSet
    {
        public string SceneName { get; set; }

        public uint UnknownField1 { get; set; }

        public int UnknownField2 { get; set; }

        public float UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public CellSpriteSceneSet() { }

        public CellSpriteSceneSet(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var sceneNameOffset = in_reader.Read<uint>();
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<int>();
            UnknownField3 = in_reader.Read<float>();
            UnknownField4 = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(sceneNameOffset),
                () => SceneName = MomentumString.Read(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var sceneNameOffset = in_writer.Reserve<uint>();
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);

            MomentumString.Write(in_writer, SceneName, sceneNameOffset);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
