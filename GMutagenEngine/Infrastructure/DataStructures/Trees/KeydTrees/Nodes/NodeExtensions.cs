namespace GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees.Nodes;

public static class NodeExtensions
{
    public static INode<TKey, TValue> GetOrAddChild<TKey, TValue>(this INode<TKey, TValue> currentNode, TKey key)
        where TKey : notnull
    {
        if (!currentNode.Children.TryGetValue(key, out var node))
        {
            node = new Node<TKey, TValue>(key);
            currentNode.Children[key] = node;
        }

        return node;
    }

    public static bool TryGetChild<TKey, TValue>(this INode<TKey, TValue> currentNode, TKey key,
        out INode<TKey, TValue>? node) where TKey : notnull
    {
        return currentNode.Children.TryGetValue(key, out node);
    }

    public static bool RemoveChild<TKey, TValue>(this INode<TKey, TValue> currentNode, TKey key) where TKey : notnull
    {
        return currentNode.Children.Remove(key);
    }

    public static IEnumerable<INode<TKey, TValue>> Traverse<TKey, TValue>(this INode<TKey, TValue> currentNode) where TKey : notnull
    {
        yield return currentNode;
        foreach (var child in currentNode.Children.Values)
        {
            foreach (var nested in child.Traverse())
                yield return nested;
        }
    }
}