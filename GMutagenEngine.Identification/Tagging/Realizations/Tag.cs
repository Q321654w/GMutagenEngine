using GMutagenEngine.Identification.Realizations.Single;

namespace GMutagenEngine.Identification.Tagging.Realizations;

public class Tag<T>(T? value) : SingleId<T>(value)
{
}