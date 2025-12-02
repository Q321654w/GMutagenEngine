using System.Text.RegularExpressions;
using GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees.Parsers;

namespace GMutagenEngine.Infrastructure
{
    public class FrameworkStringPathParser : IPathParser<string, string>
    {
        private static readonly Regex _regex = new(@"([A-Za-z_]\w*)|\[(\d+)\]", RegexOptions.Compiled);

        public IEnumerable<string> Parse(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path cannot be empty.", nameof(path));

            var matches = _regex.Matches(path);
            return matches.Select(m => m.Value.Trim('[', ']'));
        }
    }
}