using System.Runtime.CompilerServices;
using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.Identification.Utils;

public static class BehaviorChecker
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasBehavior(IdEqualityBehavior behavior, IdEqualityBehavior flag)
        => (behavior & flag) == flag;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IdEqualityBehavior GetBehavior(object? obj)
    {
        if (obj is IConfigurableId configurable)
            return configurable.EqualityBehavior;

#if ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityBehavior.AllCrossType;
#else
        var flag = IdEqualityBehavior.Strict;
#if ENABLE_STRUCTURAL_EQUALITY
        flag |= IdEqualityBehavior.StructuralEquality;
#endif
#if ENABLE_ORDERED_UNORDERED_EQUALITY
        flag |= IdEqualityBehavior.OrderedUnorderedEquality;
#endif
#if ENABLE_SINGLE_COMPOSITE_EQUALITY
        flag |= IdEqualityBehavior.SingleCompositeEquality;
#endif
        return flag;
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CanCrossCompare(object? left, object? right, IdEqualityBehavior requiredFlag)
    {
        var leftBehavior = GetBehavior(left);
        var rightBehavior = GetBehavior(right);
        return HasBehavior(leftBehavior, requiredFlag) || HasBehavior(rightBehavior, requiredFlag);
    }
}