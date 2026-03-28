using Marathon.Extensions;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class KynapseElementClassPropertyList : IKynapseElementSerializable
    {
        public string Name { get; set; }

        public string Class { get; set; }

        public List<(string Name, object Value)> Properties { get; set; } = [];

        public KynapseElementClassPropertyList() { }

        public KynapseElementClassPropertyList(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public KynapseElementClassPropertyList(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;

            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Property)
                    continue;

                if (child.Name == nameof(Class))
                {
                    Class = child.Value;
                }
                else
                {
                    Properties.Add((child.Name, child.Value));
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, GetRootName());

            result.AddChild(new KynapseElement(nameof(Class), Class));

            foreach (var property in Properties)
                result.AddChild(new KynapseElement(property.Name, property.Value));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(GetNameAttribute(), Name);

            foreach (var element in in_element.Elements())
            {
                if (element.Name.ToString() == nameof(Class))
                {
                    Class = element.GetElementValue(Class);
                }
                else
                {
                    Properties.Add((element.Name.ToString(), element.Value));
                }
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(GetRootName());

            result.Add(new XAttribute(GetNameAttribute(), Name));
            result.Add(new XElement(nameof(Class), Class));

            foreach (var property in Properties)
                result.Add(new XElement(property.Name, property.Value));

            return result;
        }

        public virtual string GetRootName()
        {
            throw new NotImplementedException();
        }

        public virtual string GetNameAttribute()
        {
            return "Name";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
