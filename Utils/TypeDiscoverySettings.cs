using GMutagenEngine.Handlers.Funcs.Interfaces;

namespace Utils;

public class TypeDiscoverySettings
{
    public BaseTypeOptions Options { get; set; } = BaseTypeOptions.All;
    public int TypeLimit { get; set; } = 16;
    public ISyncFuncHandler<Type, bool>? Filter { get; set; }
    public PruningRules Pruning { get; set; } = PruningRules.None;
}