using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class TimeManagement : IKynapseElementSerializable
    {
        private const string _nameOfRoot = "TimeMgt";
        private const string _nameOfAperiodics = "Aperiodic";
        private const string _nameOfPeriodics = "Periodic";

        public Dictionary<string, double> Estimations { get; set; } = [];

        public List<Aperiodic> Aperiodics { get; set; } = [];

        public List<Periodic> Periodics { get; set; } = [];

        public TimeManagement() { }

        public TimeManagement(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public TimeManagement(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Folder)
                    continue;

                switch (child.Type)
                {
                    case nameof(Estimations):
                    {
                        foreach (var subChild in child.Children)
                            Estimations.Add(subChild.Name, double.Parse(subChild.Value));

                        break;
                    }

                    case _nameOfAperiodics:
                        Aperiodics.Add(new Aperiodic(child));
                        break;

                    case _nameOfPeriodics:
                        Periodics.Add(new Periodic(child));
                        break;
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(string.Empty, _nameOfRoot);

            var estimations = new KynapseElement(string.Empty, nameof(Estimations));

            foreach (var estimation in Estimations)
                estimations.AddChild(new KynapseElement(estimation.Key, estimation.Value));

            result.Children.Add(estimations);

            foreach (var aperiodic in Aperiodics)
                result.AddChild(aperiodic.ToKynapseElement());

            foreach (var periodic in Periodics)
                result.AddChild(periodic.ToKynapseElement());

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            var timeMgt = in_element.Element(_nameOfRoot);

            if (timeMgt == null)
                return;

            foreach (var element in timeMgt.Elements())
            {
                switch (element.Name.ToString())
                {
                    case nameof(Estimations):
                    {
                        foreach (var descendant in element.Descendants())
                            Estimations.Add(descendant.Name.ToString(), double.Parse(descendant.Value));

                        break;
                    }

                    case _nameOfAperiodics:
                        Aperiodics.Add(new Aperiodic(element));
                        break;

                    case _nameOfPeriodics:
                        Periodics.Add(new Periodic(element));
                        break;
                }
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(_nameOfRoot);

            if (Estimations.Count > 0)
            {
                var estimations = new XElement(nameof(Estimations));

                foreach (var estimation in Estimations)
                    estimations.Add(new XElement(estimation.Key, estimation.Value));

                result.Add(estimations);
            }

            foreach (var aperiodic in Aperiodics)
                result.Add(aperiodic.ToXElement());

            foreach (var periodic in Periodics)
                result.Add(periodic.ToXElement());

            return result;
        }
    }
}
