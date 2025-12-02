using GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.Identification.Identifiable.Realizations;

public class Identifiable<TId> : IIdentifiable<TId>
{
    public TId Id { get; }
    
    public Identifiable(TId id)
    {
        Id = id;
    }
    
    public Identifiable()
    {
    
    }
}