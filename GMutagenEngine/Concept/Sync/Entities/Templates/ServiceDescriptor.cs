namespace GMutagenEngine.Concept.Sync.Entities.Templates;

public class ServiceDescriptor(Type serviceType) : IServiceDescriptor
{
    public Type ServiceType { get; set; } = serviceType;
}