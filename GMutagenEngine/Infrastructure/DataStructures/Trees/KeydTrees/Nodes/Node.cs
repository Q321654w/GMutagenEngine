namespace GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees.Nodes
{
    internal class Node<TKey, TValue>(TKey key) : INode<TKey, TValue>
        where TKey : notnull
    {
        public TKey Key { get; } = key;
        public TValue? Value { get; set; }

        private readonly Dictionary<TKey, INode<TKey, TValue>> _children = new();
        public Dictionary<TKey, INode<TKey, TValue>> Children => _children;
    }
}
