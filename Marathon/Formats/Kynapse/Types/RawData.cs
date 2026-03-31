using Marathon.Extensions;
using Marathon.IO.Types.FileSystem;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class RawData : IKynapseElementSerializable
    {
        public IFile File { get; set; }

        public string Path { get; internal set; }

        public RawData() { }

        public RawData(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public RawData(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            var binary = in_element.Children.First(x => x.GetElementType() == KynapseElementType.RawData);

            if (binary.File == null)
                throw new FileNotFoundException("This Kynapse element has no file data.");

            File = binary.File;
        }

        public KynapseElement ToKynapseElement()
        {
            return new KynapseElement()
            {
                File = File
            };
        }

        public void FromXElement(XElement in_element)
        {
            File = new VirtualDirectory().CreateFile(in_element.GetDescendantElementValue<string>(nameof(RawData)));
        }

        public XElement ToXElement()
        {
            return new XElement(nameof(RawData))
            {
                Value = File.Name
            };
        }

        public override string ToString()
        {
            return File == null
                ? base.ToString()
                : File.ToString();
        }
    }
}
