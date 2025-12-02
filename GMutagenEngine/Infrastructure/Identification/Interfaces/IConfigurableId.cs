using GMutagenEngine.Infrastructure.Identification.Constants;

namespace GMutagenEngine.Infrastructure.Identification.Interfaces;

public interface IConfigurableId
{
    IdEqualityBehavior EqualityBehavior { get; }
}