namespace GMutagenEngine.Infrastructure.DotExporter
{
    public class DotSubgraphBuilder(DotBuilder parent, string name)
    {
        internal readonly DotSubgraph _subgraph = new(name);

        public DotSubgraphBuilder Set(DotAttribute attr, string value)
        {
            _subgraph.Set(attr, value);
            return this;
        }

        public DotSubgraphBuilder Label(string val) => Set(DotAttribute.Label, val);
        public DotSubgraphBuilder Color(DotColor color) => Set(DotAttribute.Color, color.ToAttributeKey());
        public DotSubgraphBuilder Style(DotStyle style) => Set(DotAttribute.Style, style.ToAttributeKey());

        public DotNodeBuilderWrapper AddNode(string id) => new(this, id);

        public DotBuilder Build()
        {
            parent.AddSubgraphInternal(_subgraph);
            return parent;
        }

        public class DotNodeBuilderWrapper(DotSubgraphBuilder subParent, string id)
            : BaseNodeBuilder<DotNodeBuilderWrapper>(id)
        {
            private bool _isBuilt;

            public DotSubgraphBuilder Build()
            {
                if (_isBuilt)
                    throw new InvalidOperationException("This node has already been built.");
                subParent._subgraph.AddNode(Node);
                _isBuilt = true;
                return subParent;
            }
        }
    }
}