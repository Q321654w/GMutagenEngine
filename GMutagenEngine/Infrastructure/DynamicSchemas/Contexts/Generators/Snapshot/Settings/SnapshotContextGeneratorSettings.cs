using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings
{
    public class SnapshotContextGeneratorSettings(ISnapshotContextGenerator generator)
    {
        public ISnapshotContextGenerator Generator { get; } = generator;
    
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