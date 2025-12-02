namespace GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;

public interface IIdentifiable<out TId>
{
    public TId Id { get; }
}