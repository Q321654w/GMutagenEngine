namespace GMutagenEngine.Infrastructure.Identification.Interfaces;

public interface IUnorderedCompositeId : ICompositeId
{
    IReadOnlySet<IId> Components { get; }
}