using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Entities : KynapseElementList<Entity>
    {
        public override string RootName => nameof(Entities);

        public override string ItemName => nameof(Entity);

        public Entities() { }

        public Entities(KynapseElement in_element) : base(in_element) { }

        public Entities(XElement in_element) : base(in_element) { }
    }
}
