namespace GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees.Parsers
{
    public interface IPathParser<in TIn, out TKey>
    {
        IEnumerable<TKey> Parse(TIn path);
    }
}