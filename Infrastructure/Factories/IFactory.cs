using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace Factories;

public interface IFactory<out TOut> : ISyncFuncHandler<TOut>, IFactoryMark 
{
    TOut Create();
    TOut ISyncFuncHandler<TOut>.Handle() => Create();
}

public interface IFactory<in TIn, out TOut> : ISyncFuncHandler<TIn, TOut>, IFactoryMark 
{
    TOut Create(TIn input);
    TOut ISyncFuncHandler<TIn, TOut>.Handle(TIn input) => Create(input);
}

public interface IFactoryMark : ISelfMark<IFactoryMark>
{
}

