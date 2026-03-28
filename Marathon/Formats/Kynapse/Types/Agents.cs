using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Agents : KynapseElementList<Agent>
    {
        public override string RootName => nameof(Agents);

        public override string ItemName => nameof(Agent);

        public Agents() { }

        public Agents(KynapseElement in_element) : base(in_element) { }

        public Agents(XElement in_element) : base(in_element) { }
    }
}
