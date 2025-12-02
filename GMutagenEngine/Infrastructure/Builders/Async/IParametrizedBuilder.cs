using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;

namespace GMutagenEngine.Infrastructure.Builders.Async
{
    public interface IParametrizedBuilder : IBuilder
    {
        Task<IParametrizedBuilder> Execute<TData>(TData data, string name)
            => Execute<TData>(data, new SingleId<string>(name));

        Task<IParametrizedBuilder> Execute<TData>(TData data, IId id);
    }
}