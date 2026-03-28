using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Mesh : KynapseElementRawData
    {
        public Mesh() { }

        public Mesh(KynapseElement in_element) : base(in_element) { }

        public Mesh(XElement in_element) : base(in_element) { }

        public override string GetRootName()
        {
            return nameof(Mesh);
        }

        public override string GetNameAttribute()
        {
            return "name";
        }
    }
}
