using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces.Marks;
using GMutagenEngine.Concept.Sync.Values.Interfaces;

namespace GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces
{
    public interface IDefaultValueFactory : IValueFactoryMark
    {
        IValue Create(object? value, Type type);
    }
}