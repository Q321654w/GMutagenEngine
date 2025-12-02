using GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Tagging;

namespace GMutagenEngine.Infrastructure.Identification.Identifiable.Realizations;

public class AdvancedIdentifiable<TId> : Identifiable<TId>, IAdvancedIdentifiable<TId>
{
    public TId Id { get; }
    public HashSet<ITag> Tags { get; }
    
    public AdvancedIdentifiable(TId id, HashSet<ITag> tags) : base(id)
    {
        Id = id;
        Tags = tags;
    }
    
    public AdvancedIdentifiable(TId id) : this(id, new HashSet<ITag>())
    {
        Id = id;
    }
    
    public AdvancedIdentifiable()
    {

    }
}