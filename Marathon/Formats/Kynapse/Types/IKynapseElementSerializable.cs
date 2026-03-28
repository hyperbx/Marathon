using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public interface IKynapseElementSerializable
    {
        void FromKynapseElement(KynapseElement in_element);

        KynapseElement ToKynapseElement();

        void FromXElement(XElement in_element);

        XElement ToXElement();
    }
}
