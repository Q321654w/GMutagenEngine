using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;

namespace GMutagenEngine.Infrastructure.Builders.Sync
{
    public interface IParametrizedBuilder : IBuilder
    {
        IParametrizedBuilder Execute<TData>(TData data, string name)
            => Execute(data, new SingleId<string>(name));

        IParametrizedBuilder Execute<TData>(TData data, IId id);
    }
}