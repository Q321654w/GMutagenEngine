using System.Runtime.CompilerServices;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.Identification.Utils;

public static class IdEqualityHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreValuesEqual(object? left, object? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreOrderedComponentsEqual(IReadOnlyList<IId> left, IReadOnlyList<IId> right)
    {
        var count = left.Count;
        if (count != right.Count) return false;

        for (int i = 0; i < count; i++)
        {
            if (!Equals(left[i], right[i]))
                return false;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreUnorderedComponentsEqual(IReadOnlySet<IId> left, IReadOnlySet<IId> right)
    {
        if (left.Count != right.Count) return false;
        return left.SetEquals(right);
    }

#if !DISABLE_CROSS_TYPE_EQUALITY
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryCrossTypeEquality(object? left, object? right)
    {
#if ENABLE_STRUCTURAL_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        if (TryStructuralEquality(left, right, out var structuralResult))
            return structuralResult;
#endif

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        if (TrySingleCompositeEquality(left, right, out var singleCompositeResult))
            return singleCompositeResult;
#endif

#if ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        if (TryOrderedUnorderedEquality(left, right, out var orderedUnorderedResult))
            return orderedUnorderedResult;
#endif

        return false;
    }

#if ENABLE_STRUCTURAL_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryStructuralEquality(object? left, object? right, out bool result)
    {
        result = false;

        if (!BehaviorChecker.CanCrossCompare(left, right, IdEqualityBehavior.StructuralEquality))
            return false;

        if (left is ISingleId leftSingle && right is ISingleId rightSingle)
        {
            result = AreValuesEqual(leftSingle.Value, rightSingle.Value);
            return true;
        }

        if (left is IOrderedCompositeId leftOrdered && right is IOrderedCompositeId rightOrdered)
        {
            result = AreOrderedComponentsEqual(leftOrdered.Components, rightOrdered.Components);
            return true;
        }

        if (left is IUnorderedCompositeId leftUnordered && right is IUnorderedCompositeId rightUnordered)
        {
            result = AreUnorderedComponentsEqual(leftUnordered.Components, rightUnordered.Components);
            return true;
        }

        return false;
    }
#endif

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TrySingleCompositeEquality(object? left, object? right, out bool result)
    {
        result = false;

#if !ENABLE_CROSS_TYPE_EQUALITY
        if (!BehaviorChecker.CanCrossCompare(left, right, IdEqualityBehavior.SingleCompositeEquality))
            return false;
#endif

        if (left is ISingleId single && right is ICompositeId composite)
        {
            if (composite is IOrderedCompositeId ordered && ordered.Components.Count == 1)
            {
                result = Equals(single, ordered.Components[0]);
                return true;
            }

            if (composite is IUnorderedCompositeId unordered && unordered.Components.Count == 1)
            {
                result = Equals(single, unordered.Components.First());
                return true;
            }
        }

        if (left is ICompositeId composite2 && right is ISingleId single2)
        {
            if (composite2 is IOrderedCompositeId ordered && ordered.Components.Count == 1)
            {
                result = Equals(ordered.Components[0], single2);
                return true;
            }

            if (composite2 is IUnorderedCompositeId unordered && unordered.Components.Count == 1)
            {
                result = Equals(unordered.Components.First(), single2);
                return true;
            }
        }

        return false;
    }
#endif

#if ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryOrderedUnorderedEquality(object? left, object? right, out bool result)
    {
        result = false;

#if !ENABLE_CROSS_TYPE_EQUALITY
        if (!BehaviorChecker.CanCrossCompare(left, right, IdEqualityBehavior.OrderedUnorderedEquality))
            return false;
#endif

        if (left is IOrderedCompositeId orderedLeft && right is IUnorderedCompositeId unorderedRight)
        {
            if (orderedLeft.Components.Count != unorderedRight.Components.Count)
                return false;

            result = orderedLeft.Components.All(c => unorderedRight.Components.Contains(c));
            return true;
        }

        if (left is IUnorderedCompositeId unorderedLeft && right is IOrderedCompositeId orderedRight)
        {
            if (unorderedLeft.Components.Count != orderedRight.Components.Count)
                return false;

            result = orderedRight.Components.All(c => unorderedLeft.Components.Contains(c));
            return true;
        }

        return false;
    }
#endif
#endif
}