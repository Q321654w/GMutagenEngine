using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Mediators;
using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Storing.Registries.Sync.Indexed;
using Utils;

namespace GMutagenEngine.Schemas.Extraction;

public class SchemaRouter(IIndexedSyncRegistry<Type, ISyncFuncHandlerInOut> mediatorRegistry,
    ISyncMediator<Type> mediator,
    IIndexedSyncRegistry<Type, int> typePriorityRegistry)
    : ISyncFuncHandler<ReflectionContext, ISchema<string, string>>
{
    public ISchema<string, string>? Handle(ReflectionContext data)
    {
        var type = data.ActualType;
        var typeDiscovery = type.AsTypeDiscovery()
            .ToArray()
            .OrderBy(typePriorityRegistry.Get);

        foreach (var child in typeDiscovery)
        {
            if(mediatorRegistry.TryGet(child, out _))
                return mediator.Send<ReflectionContext, ISchema<string, string>>(data, child);
        }

        throw new InvalidOperationException($"Can not find handler for any of types of an {type}");
    }
}