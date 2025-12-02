using GMutagenEngine.Infrastructure.Identification.Tagging;

namespace GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;

public interface IAdvancedIdentifiable<out TId> : IIdentifiable<TId>
{
    public HashSet<ITag> Tags { get; }
}