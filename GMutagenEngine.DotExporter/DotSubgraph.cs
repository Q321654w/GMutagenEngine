using System.Text;
using Constants.Literals;

namespace GMutagenEngine.DotExporter;

public class DotSubgraph(string name)
{
    private readonly Dictionary<DotAttribute, string> _attributes = new();
    private readonly List<DotNode> _nodes = new();

    public void Set(DotAttribute key, string value) => _attributes[key] = value;

    public void AddNode(DotNode node) => _nodes.Add(node);

    public string Build(bool directed)
    {
        var sb = new StringBuilder();
        sb.AppendLine(DotConstants.SUBGRAPH_PREFIX
                      + name
                      + Literals.Whitespace.SPACE_STRING
                      + Literals.Symbols.OPEN_BRACE_STRING);
        foreach (var attr in _attributes)
            sb.AppendLine($"{DotConstants.INDENT}{DotConstants.INDENT}{attr.Key.ToAttributeKey()}{DotConstants.EQUALS}{DotConstants.QUOTE}{attr.Value}{DotConstants.QUOTE}{DotConstants.SEMICOLON}");
        foreach (var node in _nodes)
            sb.AppendLine(DotConstants.INDENT + DotConstants.INDENT + node.Build());
        sb.Append(DotConstants.INDENT + Literals.Symbols.CLOSE_BRACE_STRING);
        return sb.ToString();
    }
}
