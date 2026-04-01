using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Entities : KynapseElementList<Entity>
    {
        public override string RootName => nameof(Entities);

        public override string ItemName => nameof(Entity);

        public Entity this[string in_name] => this.FirstOrDefault(x => x.Name == in_name);

        public Entities() { }

        public Entities(KynapseElement in_element) : base(in_element) { }

        public Entities(XElement in_element) : base(in_element) { }
    }
}
