using Marathon.Extensions;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class AdditionalData : IKynapseElementSerializable
    {
        public string Class { get; set; }

        public RawData RawData { get; set; } = new();

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
                if (child.GetElementType() != KynapseElementType.Leaf || child.Name != nameof(Class))
                    continue;

                Class = child.Value;

                break;
            }

            RawData.FromKynapseElement(in_element);
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(string.Empty, nameof(AdditionalData));

            result.AddChild(new KynapseElement(nameof(Class), Class));
            result.AddChild(RawData.ToKynapseElement());

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Class = in_element.GetDescendantElementValue(nameof(Class), Class);
            RawData.FromXElement(in_element);
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(AdditionalData));

            result.Add(new XElement(nameof(Class), Class));
            result.Add(RawData.ToXElement());

            return result;
        }

        public override string ToString()
        {
            return Class;
        }
    }
}
