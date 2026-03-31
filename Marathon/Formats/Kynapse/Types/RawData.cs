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

        public string Path { get; set; }

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

            if (binary.File != null)
            {
                File = binary.File;
            }
            else if (System.IO.File.Exists(binary.Path))
            {
                File = new PhysicalFile(binary.Path);
            }
            else
            {
                throw new FileNotFoundException("This Kynapse element has no file data.");
            }

            Path = binary.Path;
        }

        public KynapseElement ToKynapseElement()
        {
            return new KynapseElement()
            {
                File = File,
                Path = Path
            };
        }

        public void FromXElement(XElement in_element)
        {
            Path = in_element.GetDescendantElementValue<string>(nameof(RawData));
        }

        public XElement ToXElement()
        {
            return new XElement(nameof(RawData))
            {
                Value = Path
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
