using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Agents : KynapseElementList<Agent>
    {
        public override string RootName => nameof(Agents);

        public override string ItemName => nameof(Agent);

        public Agent this[string in_name] => this.FirstOrDefault(x => x.Name == in_name);

        public Agents() { }

        public Agents(KynapseElement in_element) : base(in_element) { }

        public Agents(XElement in_element) : base(in_element) { }
    }
}
