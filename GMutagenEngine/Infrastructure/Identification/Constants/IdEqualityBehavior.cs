namespace GMutagenEngine.Infrastructure.Identification.Constants;

[Flags]
public enum IdEqualityBehavior
{
    Strict = 0,
    SingleCompositeEquality = 1 << 0,
    OrderedUnorderedEquality = 1 << 1,
    StructuralEquality = 1 << 2,
    AllCrossType = SingleCompositeEquality | OrderedUnorderedEquality | StructuralEquality
}