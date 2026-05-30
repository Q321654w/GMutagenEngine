using GMutagenEngine.Identification.Identifiable.Interfaces;
using GMutagenEngine.Identification.Tagging.Interfaces;

namespace GMutagenEngine.Identification.Identifiable.Realizations;

public class AdvancedIdentifiable<TId> : Identifiable<TId>, IAdvancedIdentifiable<TId>
{
    public TId Id { get; set; }
    public HashSet<ITag> Tags { get; set; }
    
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