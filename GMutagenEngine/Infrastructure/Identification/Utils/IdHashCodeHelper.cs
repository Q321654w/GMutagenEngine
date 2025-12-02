using System.Runtime.CompilerServices;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.Identification.Utils;

public static class IdHashCodeHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ComputeSingleHash(object? value)
        => value?.GetHashCode() ?? 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ComputeOrderedHash(IReadOnlyList<IId> components)
    {
        var count = components.Count;
        if (count == 0) return 0;
        if (count == 1) return components[0]?.GetHashCode() ?? 0;

        unchecked
        {
            int hash = 17;
            for (int i = 0; i < count; i++)
            {
                hash = hash * 31 + (components[i]?.GetHashCode() ?? 0);
            }

            return hash;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ComputeUnorderedHash(IReadOnlySet<IId> components)
    {
        if (components.Count == 0) return 0;

        unchecked
        {
            int hash = 0;
            foreach (var component in components)
            {
                hash ^= component?.GetHashCode() ?? 0;
            }

            return hash;
        }
    }
}