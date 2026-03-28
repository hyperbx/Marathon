using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class KynapseElementList<T> : List<T> where T : IKynapseElementSerializable, new()
    {
        public virtual string RootName { get; set; }

        public virtual string ItemName { get; set; }

        public KynapseElementList() { }

        public KynapseElementList(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public KynapseElementList(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Object || child.Type != ItemName)
                    continue;

                var item = new T();
                item.FromKynapseElement(child);

                Add(item);
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(string.Empty, RootName);

            foreach (var item in this)
                result.AddChild(item.ToKynapseElement());

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            var root = in_element;

            if (root.Name != RootName)
                root = in_element.Element(RootName);

            foreach (var element in root.Descendants())
            {
                if (element.Name != ItemName)
                    continue;

                var item = new T();
                item.FromXElement(element);

                Add(item);
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(RootName);

            foreach (var item in this)
                result.Add(item.ToXElement());

            return result;
        }
    }
}
