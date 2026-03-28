using Marathon.Extensions;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class AdditionalData : IKynapseElementSerializable
    {
        public string Class { get; set; }

        public string RawData { get; set; }

        public AdditionalData() { }

        public AdditionalData(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public AdditionalData(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Property || child.Name != nameof(Class))
                    continue;

                Class = child.Value;

                break;
            }

            RawData = in_element.File;
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(string.Empty, nameof(AdditionalData));

            result.AddChild(new KynapseElement(nameof(Class), Class));
            result.AddChild(new KynapseElement() { File = RawData });

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Class = in_element.GetDescendantElementValue(nameof(Class), Class);
            RawData = in_element.GetDescendantElementValue(nameof(RawData), RawData);
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(AdditionalData));

            result.Add(new XElement(nameof(Class), Class));
            result.Add(new XElement(nameof(RawData), RawData));

            return result;
        }

        public override string ToString()
        {
            return Class;
        }
    }
}
