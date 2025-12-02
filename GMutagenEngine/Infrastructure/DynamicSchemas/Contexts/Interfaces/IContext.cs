using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces.Marks;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces
{
    public interface IContext : IContextMark
    {
        bool ExistValue(string path);
        bool ExistContext(string path);
        IValue? GetValue(string path);
        IValue? GetValue(string[] parts);
        IContext? GetContext(string path);
        IContext? GetContext(string[] parts);
        IEnumerable<IContext> GetContexts();
        IEnumerable<IValue> GetValues();
        IEnumerable<IValue> GetAllValues();
        IEnumerable<IContext> GetAllContexts();
    }
    
}