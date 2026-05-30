namespace Utils;

[Flags]
public enum BaseTypeOptions
{
    None = 0,

    IncludeSelf = 1 << 0,
    IncludeInterfaces = 1 << 1,
    IncludeGenericDefinition = 1 << 2,
    IncludeNullableUnderlying = 1 << 3,
    IncludeAbstract = 1 << 4,
    IncludeBaseTypes = 1 << 5,

    Default = IncludeSelf | IncludeBaseTypes,
    All = IncludeSelf | IncludeInterfaces | IncludeGenericDefinition | IncludeNullableUnderlying | IncludeBaseTypes | IncludeAbstract
}