namespace GMutagenEngine.Infrastructure.Identification.Interfaces;

public interface IOrderedCompositeId : ICompositeId
{
    IReadOnlyList<IId> Components { get; }
}