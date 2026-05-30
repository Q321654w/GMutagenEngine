using GMutagenEngine.Identification.Realizations.Ordered;
using GMutagenEngine.Identification.Realizations.Single;

namespace GMutagenEngine.Mediators.General;

public class TypePairId : OrderedMultipleId
{
    public TypePairId(Type inType, Type outType) : base(new SingleId<Type>(inType), new SingleId<Type>(outType))
    {
    }
}