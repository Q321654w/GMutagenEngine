using System.Runtime.CompilerServices;
using GMutagenEngine.Infrastructure.Builders.Common;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;

namespace GMutagenEngine.Infrastructure.Builders.Async
{
    public interface IBuilder
    {
        Task<IBuilder> Execute(string name)
            => Execute(new SingleId<string>(name));

        Task<IBuilder> Execute(IId id);


        string NameOf<T>([CallerMemberName] string? name = null)
            => MethodId<T>.For(name);
    }
}