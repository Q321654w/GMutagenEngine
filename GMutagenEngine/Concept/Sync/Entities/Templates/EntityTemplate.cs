namespace GMutagenEngine.Concept.Sync.Entities.Templates
{
    public class EntityTemplate(IEnumerable<IServiceDescriptor> services) : IEntityTemplate
    {
        private readonly List<IServiceDescriptor> _services = new(services);

        public IEnumerable<IServiceDescriptor> Services => _services;
    }
}