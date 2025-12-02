namespace GMutagenEngine.Concept.Sync.Entities.Templates;

public interface IEntityModule
{
    IEnumerable<IServiceDescriptor> Services { get; }
}