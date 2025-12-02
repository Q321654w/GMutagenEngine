using GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees.Nodes;

namespace GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees
{
    public class Tree<TKey, TValue> : ITree<TKey, TValue>
        where TKey : notnull
    {
        private readonly Node<TKey, TValue> _root = new(default!); // корень без ключа

        public TKey Key => _root.Key;

        public TValue? Value
        {
            get => _root.Value;
            set => _root.Value = value;
        }

        public Dictionary<TKey, INode<TKey, TValue>> Children => _root.Children;

        public INode<TKey, TValue> GetOrAddChild(TKey key) => _root.GetOrAddChild(key);
        public bool TryGetChild(TKey key, out INode<TKey, TValue>? node) => _root.TryGetChild(key, out node);
        public bool RemoveChild(TKey key) => _root.RemoveChild(key);
        public IEnumerable<INode<TKey, TValue>> Traverse() => _root.Traverse();

        public void Set(IEnumerable<TKey> path, TValue value)
        {
            INode<TKey, TValue> node = _root;

            foreach (var key in path)
                node = node.GetOrAddChild(key);

            node.Value = value;
        }

        public bool TryGet(IEnumerable<TKey> path, out TValue? value)
        {
            if (TryGetNode(path, out var node))
            {
                value = node!.Value;
                return !EqualityComparer<TValue?>.Default.Equals(value, default);
            }

            value = default;
            return false;
        }

        public TValue? Get(IEnumerable<TKey> path)
        {
            TryGet(path, out var value);
            return value;
        }

        public bool Remove(IReadOnlyList<TKey> path)
        {
            return RemoveRecursive(_root, path, 0);
        }

        public bool TryGetNode(IEnumerable<TKey> path, out INode<TKey, TValue>? node)
        {
            node = _root;

            foreach (var key in path)
            {
                if (!node.TryGetChild(key, out var child))
                {
                    node = null;
                    return false;
                }

                node = child;
            }

            return true;
        }
    
        public INode<TKey, TValue> GetNode(IEnumerable<TKey> path)
        {
            if (!TryGetNode(path, out var node))
                throw new KeyNotFoundException($"Node with path [{string.Join("/", path)}] not found.");

            return node!;
        }

        private bool RemoveRecursive(INode<TKey, TValue> current, IReadOnlyList<TKey> parts, int index)
        {
            if (index >= parts.Count)
                return false;

            var key = parts[index];
            if (!current.TryGetChild(key, out var childNode))
                return false;

            if (index == parts.Count - 1)
                return current.RemoveChild(key);

            var removed = RemoveRecursive(childNode!, parts, index + 1);

            if (removed && childNode!.Children.Count == 0 && childNode.Value == null)
                current.RemoveChild(key);

            return removed;
        }
    }
}
