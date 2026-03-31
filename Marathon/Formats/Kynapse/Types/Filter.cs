using Marathon.Extensions;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Filter : IKynapseElementSerializable
    {
        private const string _nameOfName = "name";

        public string Name { get; set; }

        public string Class { get; set; }

        public ReferencePoint? ReferencePoint { get; set; } = null;

        public List<(string Name, object Value)> Properties { get; set; } = [];

        public Filter() { }

        public Filter(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Filter(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;

            foreach (var child in in_element.Children)
            {
                switch (child.GetElementType())
                {
                    case KynapseElementType.Leaf:
                    {
                        if (child.Name == nameof(Class))
                        {
                            Class = child.Value;
                        }
                        else
                        {
                            Properties.Add((child.Name, child.Value));
                        }

                        break;
                    }

                    case KynapseElementType.Folder:
                    {
                        if (child.Type != nameof(ReferencePoint))
                            continue;

                        ReferencePoint = new ReferencePoint(child);

                        break;
                    }
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Filter));

            result.AddChild(new KynapseElement(nameof(Class), Class));
            result.AddChild(ReferencePoint.ToKynapseElement());

            foreach (var property in Properties)
                result.AddChild(new KynapseElement(property.Name, property.Value));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(_nameOfName, Name);

            foreach (var element in in_element.Elements())
            {
                switch (element.Name.ToString())
                {
                    case nameof(Class):
                        Class = element.GetElementValue(Class);
                        break;

                    case nameof(ReferencePoint):
                        ReferencePoint = new ReferencePoint(element);
                        break;

                    default:
                        Properties.Add((element.Name.ToString(), element.Value));
                        break;
                }
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Filter));

            result.Add(new XAttribute(_nameOfName, Name));
            result.Add(new XElement(nameof(Class), Class));

            if (ReferencePoint != null)
                result.Add(ReferencePoint.ToXElement());

            foreach (var property in Properties)
                result.Add(new XElement(property.Name, property.Value));

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
