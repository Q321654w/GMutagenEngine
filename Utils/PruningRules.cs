namespace Utils;

[Flags]
public enum PruningRules
{
    None = 0,
        
    PruneGenericDefinition = 1 << 0,
    PruneNullable = 1 << 1,
    PruneBaseTypes = 1 << 2,
    PruneInterfaces = 1 << 3,
    PruneAbstract = 1 << 4,
        
    All = PruneGenericDefinition | PruneNullable | PruneBaseTypes | PruneInterfaces | PruneAbstract
}