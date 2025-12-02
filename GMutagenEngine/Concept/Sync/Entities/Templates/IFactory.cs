using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Concept.Sync.Entities.Templates;

public interface IFactory<out TOut>
{
    TOut Create();
}

public interface IFactory<in TIn, out TOut>
{
    TOut Create(TIn data);
}

public interface IIdBasedFactory<out TOut> : IFactory<IId, TOut>
{
}