using System.Collections;

namespace GMutagenEngine.Infrastructure.DataStructures.Trees.KeydTrees;

public readonly struct KeyPath<TKey>(IEnumerable<TKey> parts) : IEnumerable<TKey>
    where TKey : notnull
{
    private readonly IReadOnlyList<TKey> _parts = parts.ToArray();

    public int Length => _parts.Count;
    public TKey this[int index] => _parts[index];

    public IEnumerator<TKey> GetEnumerator() => _parts.GetEnumerator();

    public override string ToString() => string.Join("/", _parts);
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}