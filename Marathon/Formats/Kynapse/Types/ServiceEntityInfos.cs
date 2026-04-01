using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class ServiceEntityInfos : KynapseElementList<ServiceEntityInfo>
    {
        public override string RootName => "EntityInfos";

        public override string ItemName => "EntityInfo";

        public ServiceEntityInfo this[string in_name] => this.FirstOrDefault(x => x.Name == in_name);

        public ServiceEntityInfos() { }

        public ServiceEntityInfos(KynapseElement in_element) : base(in_element) { }

        public ServiceEntityInfos(XElement in_element) : base(in_element) { }
    }
}
