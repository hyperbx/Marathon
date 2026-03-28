using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class ServiceEntityInfos : KynapseElementList<ServiceEntityInfo>
    {
        public override string RootName => "EntityInfos";

        public override string ItemName => "EntityInfo";

        public ServiceEntityInfos() { }

        public ServiceEntityInfos(KynapseElement in_element) : base(in_element) { }

        public ServiceEntityInfos(XElement in_element) : base(in_element) { }
    }
}
