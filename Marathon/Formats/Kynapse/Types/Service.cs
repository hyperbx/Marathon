using Marathon.Extensions;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Service : IKynapseElementSerializable
    {
        private const string _nameOfGraphs = "Graph";
        private const string _nameOfTraversals = "Traversal";
        private const string _nameOfPathWays = "PathWay";
        private const string _nameOfMeshes = "Mesh";

        public string Name { get; set; }

        public string Class { get; set; }

        public KynapseElementLeaves Properties { get; set; } = [];

        public Filters Filters { get; set; } = [];

        public ServiceEntityInfos EntityInfos { get; set; } = [];

        public Profiles Profiles { get; set; } = [];

        public List<Graph> Graphs { get; set; } = [];

        public List<Traversal> Traversals { get; set; } = [];

        public List<PathWay> PathWays { get; set; } = [];

        public List<Mesh> Meshes { get; set; } = [];

        public Service() { }

        public Service(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Service(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;

            foreach (var child in in_element.Children)
            {
                switch (child.GetElementType())
                {
                    case KynapseElementType.Leaf:
                    {
                        if (child.Name == nameof(Class))
                        {
                            Class = child.Value;
                        }
                        else
                        {
                            Properties.Add(new KynapseElementLeaf(child.Name, child.Value));
                        }

                        break;
                    }

                    case KynapseElementType.Folder:
                    {
                        switch (child.Type)
                        {
                            case nameof(Filters):
                                Filters.FromKynapseElement(child);
                                break;

                            case nameof(EntityInfos):
                                EntityInfos.FromKynapseElement(child);
                                break;

                            case nameof(Profiles):
                                Profiles.FromKynapseElement(child);
                                break;

                            case _nameOfGraphs:
                                Graphs.Add(new Graph(child));
                                break;

                            case _nameOfTraversals:
                                Traversals.Add(new Traversal(child));
                                break;
                            
                            case _nameOfPathWays:
                                PathWays.Add(new PathWay(child));
                                break;
                            
                            case _nameOfMeshes:
                                Meshes.Add(new Mesh(child));
                                break;
                        }

                        break;
                    }
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Service));

            result.AddChild(new KynapseElement(nameof(Class), Class));

            foreach (var property in Properties)
                result.AddChild(new KynapseElement(property.Name, property.Value));

            if (Filters.Count > 0)
                result.AddChild(Filters.ToKynapseElement());

            if (EntityInfos.Count > 0)
                result.AddChild(EntityInfos.ToKynapseElement());

            if (Profiles.Count > 0)
                result.AddChild(Profiles.ToKynapseElement());

            foreach (var graph in Graphs)
                result.AddChild(graph.ToKynapseElement());

            foreach (var traversal in Traversals)
                result.AddChild(traversal.ToKynapseElement());

            foreach (var pathWay in PathWays)
                result.AddChild(pathWay.ToKynapseElement());

            foreach (var mesh in Meshes)
                result.AddChild(mesh.ToKynapseElement());

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(nameof(Name), Name);

            foreach (var element in in_element.Elements())
            {
                switch (element.Name.ToString())
                {
                    case nameof(Class):
                        Class = element.GetElementValue(Class);
                        break;

                    case nameof(Filters):
                        Filters.FromXElement(in_element);
                        break;

                    case nameof(EntityInfos):
                        EntityInfos.FromXElement(in_element);
                        break;

                    case nameof(Profiles):
                        Profiles.FromXElement(in_element);
                        break;

                    case _nameOfGraphs:
                    case _nameOfTraversals:
                    case _nameOfPathWays:
                    case _nameOfMeshes:
                    {
                        if (element.HasAttributes)
                            break;

                        goto default;
                    }

                    default:
                        Properties.Add(new KynapseElementLeaf(element.Name.ToString(), element.Value));
                        break;
                }
            }

            foreach (var graph in in_element.Elements(_nameOfGraphs))
            {
                if (!graph.HasAttributes)
                    continue;

                Graphs.Add(new Graph(graph));
            }

            foreach (var traversal in in_element.Elements(_nameOfTraversals))
            {
                if (!traversal.HasAttributes)
                    continue;

                Traversals.Add(new Traversal(traversal));
            }

            foreach (var pathWay in in_element.Elements(_nameOfPathWays))
            {
                if (!pathWay.HasAttributes)
                    continue;

                PathWays.Add(new PathWay(pathWay));
            }

            foreach (var mesh in in_element.Elements(_nameOfMeshes))
            {
                if (!mesh.HasAttributes)
                    continue;

                Meshes.Add(new Mesh(mesh));
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Service));

            result.Add(new XAttribute(nameof(Name), Name));
            result.Add(new XElement(nameof(Class), Class));

            foreach (var property in Properties)
                result.Add(new XElement(property.Name, property.Value));

            if (Filters.Count > 0)
                result.Add(Filters.ToXElement());

            if (EntityInfos.Count > 0)
                result.Add(EntityInfos.ToXElement());

            if (Profiles.Count > 0)
                result.Add(Profiles.ToXElement());

            foreach (var graph in Graphs)
                result.Add(graph.ToXElement());
            
            foreach (var traversal in Traversals)
                result.Add(traversal.ToXElement());
            
            foreach (var pathWay in PathWays)
                result.Add(pathWay.ToXElement());

            foreach (var mesh in Meshes)
                result.Add(mesh.ToXElement());

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
