using Marathon.Extensions;
using System;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class KynapseElementRawData : IKynapseElementSerializable
    {
        public string Name { get; set; }

        public RawData RawData { get; set; } = new();

        public KynapseElementRawData() { }

        public KynapseElementRawData(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public KynapseElementRawData(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;
            RawData.FromKynapseElement(in_element);
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, GetRootName());

            result.AddChild(RawData.ToKynapseElement());

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(GetNameAttribute(), Name);
            RawData.FromXElement(in_element);
        }

        public XElement ToXElement()
        {
            var result = new XElement(GetRootName());

            result.Add(new XAttribute(GetNameAttribute(), Name));
            result.Add(RawData.ToXElement());

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
