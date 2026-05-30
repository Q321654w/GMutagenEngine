using GMutagenEngine.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Identification.Identifiable.Realizations;

public class Identifiable<TId> : IIdentifiable<TId>
{
    public TId Id { get; set; }
    
    public Identifiable(TId id)
    {
        Id = id;
    }
    
    public Identifiable()
    {
    
    }
}