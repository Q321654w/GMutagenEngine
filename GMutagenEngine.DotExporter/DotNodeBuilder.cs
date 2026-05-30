namespace GMutagenEngine.DotExporter;

public class DotNodeBuilder(DotBuilder parent, string id) : BaseNodeBuilder<DotNodeBuilder>(id)
{
    private readonly DotBuilder _parent = parent ?? throw new ArgumentNullException(nameof(parent));
    private bool _isBuilt;

    public DotBuilder Build()
    {
        if (_isBuilt)
            throw new InvalidOperationException("This node has already been built.");
        _parent.AddNodeInternal(Node);
        _isBuilt = true;
        return _parent;
    }
}