using Constants.Literals;

namespace GMutagenEngine.DotExporter;

public static class DotConstants
{
    public const string INDENT = Literals.Whitespace.SPACE_STRING
                                 + Literals.Whitespace.SPACE_STRING
                                 + Literals.Whitespace.SPACE_STRING
                                 + Literals.Whitespace.SPACE_STRING;

    public const string DIGRAPH = "digraph" + Literals.Whitespace.SPACE_STRING;
    public const string GRAPH = "graph" + Literals.Whitespace.SPACE_STRING;
    public const string SUBGRAPH_PREFIX = INDENT + "subgraph" + Literals.Whitespace.SPACE_STRING + "cluster_";

    public const string ARROW = "->";
    public const string LINE = "--";

    public const string QUOTE = Literals.Symbols.QUOTE_STRING;
    public const string SEMICOLON = Literals.Symbols.SEMICOLON_STRING;
    public const string L_BRACKET = Literals.Whitespace.SPACE_STRING + Literals.Symbols.OPEN_BRACKET_STRING;
    public const string R_BRACKET = Literals.Symbols.CLOSE_BRACKET_STRING;
    public const string COMMA = Literals.Punctuation.COMMA_SPACE_STRING;
    public const string EQUALS = Literals.Symbols.EQUALS_STRING;
}
