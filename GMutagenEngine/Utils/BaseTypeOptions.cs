namespace GMutagenEngine.Utils
{
    [Flags]
    public enum BaseTypeOptions
    {
        None = 0,

        IncludeSelf = 1 << 0,
        IncludeInterfaces = 1 << 1,
        IncludeGenericDefinition = 1 << 2,
        IncludeNullableUnderlying = 1 << 3,

        IncludeBaseTypes = 1 << 4,

        Default = IncludeSelf | IncludeBaseTypes,
        All = IncludeSelf | IncludeInterfaces | IncludeGenericDefinition | IncludeNullableUnderlying | IncludeBaseTypes
    }
    
    [Flags]
    public enum PruningRules
    {
        None = 0,
        
        PruneGenericDefinition = 1 << 0,
        PruneNullable = 1 << 1,
        PruneBaseTypes = 1 << 2,
        PruneInterfaces = 1 << 3,
        
        All = PruneGenericDefinition | PruneNullable | PruneBaseTypes | PruneInterfaces
    }
}