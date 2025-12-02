using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;

namespace GMutagenEngine.Infrastructure.Identification.Tagging;

public interface ITag : IId
{
}

public class Tag<T>(T? value) : SingleId<T>(value)
{
}