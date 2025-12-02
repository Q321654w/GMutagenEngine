using GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees.Nodes;

namespace GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees
{
    public interface ITree<TKey, TValue> : INode<TKey, TValue> where TKey : notnull
    {
        void Set(IEnumerable<TKey> path, TValue value);
        bool TryGet(IEnumerable<TKey> path, out TValue? value);
        TValue? Get(IEnumerable<TKey> path);
        bool Remove(IReadOnlyList<TKey> path);
        bool TryGetNode(IEnumerable<TKey> path, out INode<TKey, TValue>? node);
        INode<TKey, TValue> GetNode(IEnumerable<TKey> path);
    }
}
