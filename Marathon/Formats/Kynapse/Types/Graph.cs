using Marathon.Extensions;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Graph : IKynapseElementSerializable
    {
        public string Name { get; set; }

        public RawData RawData { get; set; } = new();

        public KynapseElementLeaves Properties { get; set; } = [];

        public List<AdditionalData> AdditionalData { get; set; } = [];

        public Graph() { }

        public Graph(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Graph(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;
            RawData.FromKynapseElement(in_element);

            foreach (var child in in_element.Children)
            {
                switch (child.GetElementType())
                {
                    case KynapseElementType.Leaf:
                        Properties.Add(new KynapseElementLeaf(child.Name, child.Value));
                        break;

                    case KynapseElementType.Folder:
                    {
                        if (child.Type != nameof(AdditionalData))
                            continue;

                        AdditionalData.Add(new AdditionalData(child));

                        break;
                    }
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Graph));

            result.AddChild(RawData.ToKynapseElement());

            foreach (var property in Properties)
                result.AddChild(new KynapseElement(property.Name, property.Value));

            foreach (var additionalData in AdditionalData)
                result.AddChild(additionalData.ToKynapseElement());

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(nameof(Name), Name);

            foreach (var element in in_element.Elements())
            {
                switch (element.Name.ToString())
                {
                    case nameof(RawData):
                        RawData.FromXElement(in_element);
                        break;

                    case nameof(AdditionalData):
                        AdditionalData.Add(new AdditionalData(element));
                        break;

                    default:
                        Properties.Add(new KynapseElementLeaf(element.Name.ToString(), element.Value));
                        break;
                }
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Graph));

            result.Add(new XAttribute(nameof(Name), Name));
            result.Add(RawData.ToXElement());

            foreach (var property in Properties)
                result.Add(new XElement(property.Name, property.Value));

            foreach (var additionalData in AdditionalData)
                result.Add(additionalData.ToXElement());

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
