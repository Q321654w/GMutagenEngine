using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings
{
    public class LiveContextGeneratorSettings(ILiveContextGenerator generator)
    {
        public ILiveContextGenerator Generator { get; } = generator;
    
        public readonly HashSet<Type> ExcludedTypes = new();

        public Func<Type, bool>? ExcludedTypePredicate { get; set; }

        public bool ShouldHandle(Type type)
        {
            if (ExcludedTypePredicate?.Invoke(type) == true)
                return false;

            return !ExcludedTypes.Contains(type);
        }
    }
}