using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Ordered;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;

namespace GMutagenEngine.Infrastructure.Mediators.Sync;

public class DoubleTypedId(SingleId<Type> first, SingleId<Type> second, IId value)
    : OrderedMultipleIdCached(first, second, value)
{
    public static DoubleTypedId From(SingleId<Type> first, SingleId<Type> second, IId value)
        => new(first, second, value);

    public static DoubleTypedId FromFirst(SingleId<Type> first, IId value = null)
        => new(first, null, value);

    public static DoubleTypedId FromSecond(SingleId<Type> second, IId value = null)
        => new(null, second, value);

    public static DoubleTypedId FromValue(IId value = null)
        => new(null, null, value);
}