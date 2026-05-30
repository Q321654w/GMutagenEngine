namespace GmutagenEngine.Requests.Http.Constants
{
    public static class HttpFormats
    {
        public const string BEARER_TOKEN_FORMAT = "{0} {1}";
        public const string BASIC_AUTH_FORMAT = "{0} {1}";
        public const string USER_AGENT_FORMAT = "{0}" + HttpFormatSymbols.SLASH + "{1}";
        public const string QUERY_PARAM_FORMAT = "{0}{1}" + HttpFormatSymbols.EQUALS + "{2}";
        public const string FORM_DATA_FORMAT = "{0}" + HttpFormatSymbols.EQUALS + "{1}";
    }
}