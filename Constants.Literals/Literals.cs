namespace Constants.Literals;

public static class Literals
{
    public static class Symbols
    {
        public const string DOT_STRING = ".";
        public const char DOT_CHAR = '.';

        public const string COMMA_STRING = ",";
        public const char COMMA_CHAR = ',';

        public const string SEMICOLON_STRING = ";";
        public const char SEMICOLON_CHAR = ';';

        public const string COLON_STRING = ":";
        public const char COLON_CHAR = ':';

        public const string PIPE_STRING = "|";
        public const char PIPE_CHAR = '|';

        public const string SLASH_STRING = "/";
        public const char SLASH_CHAR = '/';

        public const string BACKSLASH_STRING = "\\";
        public const char BACKSLASH_CHAR = '\\';

        public const string DASH_STRING = "-";
        public const char DASH_CHAR = '-';

        public const string UNDERSCORE_STRING = "_";
        public const char UNDERSCORE_CHAR = '_';

        public const string PLUS_STRING = "+";
        public const char PLUS_CHAR = '+';

        public const string EQUALS_STRING = "=";
        public const char EQUALS_CHAR = '=';

        public const string ASTERISK_STRING = "*";
        public const char ASTERISK_CHAR = '*';

        public const string AMPERSAND_STRING = "&";
        public const char AMPERSAND_CHAR = '&';

        public const string PERCENT_STRING = "%";
        public const char PERCENT_CHAR = '%';

        public const string QUESTION_STRING = "?";
        public const char QUESTION_CHAR = '?';

        public const string EXCLAMATION_STRING = "!";
        public const char EXCLAMATION_CHAR = '!';

        public const string QUOTE_STRING = "\"";
        public const char QUOTE_CHAR = '"';

        public const string SINGLE_QUOTE_STRING = "'";
        public const char SINGLE_QUOTE_CHAR = '\'';

        public const string OPEN_PAREN_STRING = "(";
        public const char OPEN_PAREN_CHAR = '(';

        public const string CLOSE_PAREN_STRING = ")";
        public const char CLOSE_PAREN_CHAR = ')';

        public const string OPEN_BRACKET_STRING = "[";
        public const char OPEN_BRACKET_CHAR = '[';

        public const string CLOSE_BRACKET_STRING = "]";
        public const char CLOSE_BRACKET_CHAR = ']';

        public const string OPEN_BRACE_STRING = "{";
        public const char OPEN_BRACE_CHAR = '{';

        public const string CLOSE_BRACE_STRING = "}";
        public const char CLOSE_BRACE_CHAR = '}';

        public const string LESS_THAN_STRING = "<";
        public const char LESS_THAN_CHAR = '<';

        public const string GREATER_THAN_STRING = ">";
        public const char GREATER_THAN_CHAR = '>';

        public const string BACKTICK_STRING = "`";
        public const char BACKTICK_CHAR = '`';

        public const string AT_STRING = "@";
        public const char AT_CHAR = '@';

        public const string HASH_STRING = "#";
        public const char HASH_CHAR = '#';

        public const string DOLLAR_STRING = "$";
        public const char DOLLAR_CHAR = '$';

        public const string CARET_STRING = "^";
        public const char CARET_CHAR = '^';

        public const string TILDE_STRING = "~";
        public const char TILDE_CHAR = '~';
    }

    public static class Whitespace
    {
        public const string SPACE_STRING = " ";
        public const char SPACE_CHAR = ' ';

        public const string TAB_STRING = "\t";
        public const char TAB_CHAR = '\t';

        public const string NEW_LINE_STRING = "\n";
        public const char NEW_LINE_CHAR = '\n';

        public const string CARRIAGE_RETURN_STRING = "\r";
        public const char CARRIAGE_RETURN_CHAR = '\r';

        public const string CR_LF_STRING = "\r\n";
        public const string TAB_SPACE_STRING = "\t ";
    }

    public static class Operators
    {
        public const string PLUS_STRING = "+";
        public const string MINUS_STRING = "-";
        public const string MULTIPLY_STRING = "*";
        public const string DIVIDE_STRING = "/";
        public const string MODULO_STRING = "%";

        public const string AND_STRING = "&";
        public const string OR_STRING = "|";
        public const string XOR_STRING = "^";
        public const string NOT_STRING = "!";
        public const string DOUBLE_AMPERSAND_STRING = "&&";
        public const string DOUBLE_PIPE_STRING = "||";

        public const string EQUAL_STRING = "==";
        public const string NOT_EQUAL_STRING = "!=";
        public const string LESS_THAN_STRING = "<";
        public const string GREATER_THAN_STRING = ">";
        public const string LESS_EQUAL_STRING = "<=";
        public const string GREATER_EQUAL_STRING = ">=";

        public const string INCREMENT_STRING = "++";
        public const string DECREMENT_STRING = "--";

        public const string LEFT_SHIFT_STRING = "<<";
        public const string RIGHT_SHIFT_STRING = ">>";

        public const string NULL_CONDITIONAL_DOT_STRING = "?.";
        public const string NULL_CONDITIONAL_INDEX_STRING = "?[";
        public const string NULL_COALESCING_STRING = "??";
        public const string NULL_COALESCING_ASSIGNMENT_STRING = "??=";

        public const string LAMBDA_STRING = "=>";
        public const string SCOPE_RESOLUTION_STRING = "::";
        public const string PARAMS_STRING = "...";
        public const string ARROW_STRING = "->";

        public const string ADD_ASSIGN_STRING = "+=";
        public const string SUBTRACT_ASSIGN_STRING = "-=";
        public const string MULTIPLY_ASSIGN_STRING = "*=";
        public const string DIVIDE_ASSIGN_STRING = "/=";
        public const string MODULO_ASSIGN_STRING = "%=";
        public const string AND_ASSIGN_STRING = "&=";
        public const string OR_ASSIGN_STRING = "|=";
        public const string XOR_ASSIGN_STRING = "^=";
        public const string LEFT_SHIFT_ASSIGN_STRING = "<<=";
        public const string RIGHT_SHIFT_ASSIGN_STRING = ">>=";
    }

    public static class Punctuation
    {
        public const string COMMA_SPACE_STRING = ", ";
        public const string SEMICOLON_SPACE_STRING = "; ";
        public const string COLON_SPACE_STRING = ": ";
        public const string DOT_SPACE_STRING = ". ";

        public const string SLASH_SLASH_STRING = "//";
        public const string BACKSLASH_BACKSLASH_STRING = "\\\\";

        public const string COMMENT_BEGIN_STRING = "/*";
        public const string COMMENT_END_STRING = "*/";
        public const string XML_COMMENT_BEGIN_STRING = "<!--";
        public const string XML_COMMENT_END_STRING = "-->";

        public const string DOUBLE_OPEN_BRACE_STRING = "{{";
        public const string DOUBLE_CLOSE_BRACE_STRING = "}}";
        public const string DOUBLE_OPEN_BRACKET_STRING = "[[";
        public const string DOUBLE_CLOSE_BRACKET_STRING = "]]";
    }

    public static class Keywords
    {
        public const string NULL_STRING = "null";
        public const string NONE_STRING = "none";
        public const string TRUE_STRING = "true";
        public const string FALSE_STRING = "false";
    }

    public static class ControlCharacters
    {
        public const char NULL_CHAR = '\0';
        public const string NULL_STRING = "\0";

        public const char BACKSPACE_CHAR = '\b';
        public const string BACKSPACE_STRING = "\b";

        public const char FORM_FEED_CHAR = '\f';
        public const string FORM_FEED_STRING = "\f";

        public const char VERTICAL_TAB_CHAR = '\v';
        public const string VERTICAL_TAB_STRING = "\v";
    }

    public static class Strings
    {
        public const string EMPTY_STRING = "";
    }
}
