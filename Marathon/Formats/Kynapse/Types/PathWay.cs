using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class PathWay : KynapseElementRawData
    {
        public PathWay() { }

        public PathWay(KynapseElement in_element) : base(in_element) { }

        public PathWay(XElement in_element) : base(in_element) { }

        public override string GetRootName()
        {
            return nameof(PathWay);
        }
    }
}
