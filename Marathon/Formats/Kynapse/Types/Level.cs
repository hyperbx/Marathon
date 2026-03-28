using Marathon.Extensions;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Level : IKynapseElementSerializable
    {
        private const string _nameOfTicksPerFrame = "Tpf";
        private const string _nameOfTimeManagement = "TimeMgt";

        public string Name { get; set; }

        public double OneMeter { get; set; }

        public int MaxEntity { get; set; }

        public double TicksPerFrame { get; set; }

        public GlobalServices GlobalServices { get; set; } = new();

        public TimeManagement TimeManagement { get; set; } = new();

        public Entities Entities { get; set; } = [];

        public Brains Brains { get; set; } = [];

        public Agents Agents { get; set; } = [];

        public Services Services { get; set; } = [];

        public Level() { }

        public Level(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Level(XElement in_element)
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
                    case KynapseElementType.Property:
                    {
                        switch (child.Name)
                        {
                            case nameof(OneMeter):
                                OneMeter = double.Parse(child.Value);
                                break;

                            case nameof(MaxEntity):
                                MaxEntity = int.Parse(child.Value);
                                break;

                            case _nameOfTicksPerFrame:
                                TicksPerFrame = double.Parse(child.Value);
                                break;
                        }

                        break;
                    }

                    case KynapseElementType.Object:
                    {
                        switch (child.Type)
                        {
                            case nameof(GlobalServices):
                                GlobalServices.FromKynapseElement(child);
                                break;

                            case _nameOfTimeManagement:
                                TimeManagement.FromKynapseElement(child);
                                break;

                            case nameof(Entities):
                                Entities.FromKynapseElement(child);
                                break;

                            case nameof(Brains):
                                Brains.FromKynapseElement(child);
                                break;

                            case nameof(Agents):
                                Agents.FromKynapseElement(child);
                                break;

                            case nameof(Services):
                                Services.FromKynapseElement(child);
                                break;
                        }

                        break;
                    }
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Level));

            result.AddChild(new KynapseElement(nameof(OneMeter), OneMeter));
            result.AddChild(new KynapseElement(nameof(MaxEntity), MaxEntity));
            result.AddChild(new KynapseElement(_nameOfTicksPerFrame, TicksPerFrame));
            result.AddChild(GlobalServices.ToKynapseElement());
            result.AddChild(TimeManagement.ToKynapseElement());
            result.AddChild(Entities.ToKynapseElement());
            result.AddChild(Brains.ToKynapseElement());
            result.AddChild(Agents.ToKynapseElement());
            result.AddChild(Services.ToKynapseElement());

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(nameof(Name), Name);
            OneMeter = in_element.GetDescendantElementValue(nameof(OneMeter), OneMeter);
            MaxEntity = in_element.GetDescendantElementValue(nameof(MaxEntity), MaxEntity);
            TicksPerFrame = in_element.GetDescendantElementValue(_nameOfTicksPerFrame, TicksPerFrame);
            GlobalServices.FromXElement(in_element);
            TimeManagement.FromXElement(in_element);
            Entities.FromXElement(in_element);
            Brains.FromXElement(in_element);
            Agents.FromXElement(in_element);
            Services.FromXElement(in_element);
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Level));

            result.Add(new XAttribute(nameof(Name), Name));
            result.Add(new XElement(nameof(OneMeter), OneMeter));
            result.Add(new XElement(nameof(MaxEntity), MaxEntity));
            result.Add(new XElement(_nameOfTicksPerFrame, TicksPerFrame));
            result.Add(GlobalServices.ToXElement());
            result.Add(TimeManagement.ToXElement());
            result.Add(Entities.ToXElement());
            result.Add(Brains.ToXElement());
            result.Add(Agents.ToXElement());
            result.Add(Services.ToXElement());

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
