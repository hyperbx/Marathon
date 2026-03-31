using Marathon.Extensions;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Aperiodic : IKynapseElementSerializable
    {
        private const string _nameOfTicksPerFrame = "Tpf";

        public string Name { get; set; }

        public double Priority { get; set; }

        public double TicksPerFrame { get; set; }

        public int MaxCall { get; set; }

        public Aperiodic() { }

        public Aperiodic(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Aperiodic(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;

            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Leaf)
                    continue;

                switch (child.Name)
                {
                    case nameof(Priority):
                        Priority = double.Parse(child.Value);
                        break;

                    case _nameOfTicksPerFrame:
                        TicksPerFrame = double.Parse(child.Value);
                        break;

                    case nameof(MaxCall):
                        MaxCall = int.Parse(child.Value);
                        break;
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Aperiodic));

            result.AddChild(new KynapseElement(nameof(Priority), Priority));
            result.AddChild(new KynapseElement(_nameOfTicksPerFrame, TicksPerFrame));
            result.AddChild(new KynapseElement(nameof(MaxCall), MaxCall));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(nameof(Name), Name);
            Priority = in_element.GetDescendantElementValue(nameof(Priority), Priority);
            TicksPerFrame = in_element.GetDescendantElementValue(_nameOfTicksPerFrame, TicksPerFrame);
            MaxCall = in_element.GetDescendantElementValue(nameof(MaxCall), MaxCall);
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Aperiodic));

            result.Add(new XAttribute(nameof(Name), Name));
            result.Add(new XElement(nameof(Priority), Priority));
            result.Add(new XElement(_nameOfTicksPerFrame, TicksPerFrame));
            result.Add(new XElement(nameof(MaxCall), MaxCall));

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
