namespace GMutagenEngine.Concept.Sync.Entities.Templates;

public class EntityModule : IEntityModule
{
    private readonly List<IServiceDescriptor> _services = new();

    public IEnumerable<IServiceDescriptor> Services => _services;

    public EntityModule(IEnumerable<IServiceDescriptor> services)
    {
        _services.AddRange(services);
    }
}