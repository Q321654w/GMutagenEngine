using System.Reflection;

namespace GMutagenEngine.Mediators.Protocols.Sync;

public sealed class MediatorContractProxy<TId> : DispatchProxy
{
    private ISyncMediator<TId>? _syncMediator;

    internal void Bind(ISyncMediator<TId> mediator) => _syncMediator = mediator;
    
    protected override object? Invoke(MethodInfo targetMethod, object?[]? args)
    {
        args ??= Array.Empty<object>();

        if (_syncMediator != null)
        {
            if (targetMethod.ReturnType == typeof(void))
            {
                if (args.Length == 1)
                    _syncMediator.Publish((TId)args[0]!);
                else if (args.Length == 2)
                    _syncMediator.Publish(args[0]!, (TId)args[1]!);
                else
                    throw new InvalidOperationException($"Unsupported signature: {targetMethod}");

                return null;
            }
            
            if (args.Length == 1)
            {
                var method = typeof(ISyncMediator<TId>)
                    .GetMethod(nameof(ISyncMediator<TId>.Send))!
                    .MakeGenericMethod(targetMethod.ReturnType);

                return method.Invoke(_syncMediator, new[] { args[0]! });
            }

            throw new InvalidOperationException($"Unsupported signature: {targetMethod}");
        }
        

        throw new InvalidOperationException("Mediator not bound");
    }
}

public static class MediatorContracts
{
    public static TContract Create<TContract, TId>(ISyncMediator<TId> mediator)
        where TContract : class
    {
        var proxy = DispatchProxy.Create<TContract, MediatorContractProxy<TId>>();
        ((MediatorContractProxy<TId>)(object)proxy).Bind(mediator);
        return proxy;
    }
    
}