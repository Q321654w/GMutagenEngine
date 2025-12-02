using System.Reflection;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Interfaces.Marks;
using GMutagenEngine.Concept.Sync.Values.Interfaces;

namespace GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Interfaces
{
    public interface IReflectionValueFactory : IReflectionValueFactoryMark
    {
        IValue Create(object instance, MemberInfo member);
    }
}