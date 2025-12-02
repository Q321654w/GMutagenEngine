namespace GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees.Nodes
{
    public interface INode<TKey, TValue>
        where TKey : notnull
    {
        TKey Key { get; }
        TValue? Value { get; set; }

        Dictionary<TKey, INode<TKey, TValue>> Children { get; }
    }
}