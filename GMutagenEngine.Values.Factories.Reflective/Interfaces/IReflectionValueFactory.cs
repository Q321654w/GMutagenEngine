using System.Reflection;
using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Values.Factories.Reflective.Interfaces.Marks;
using GMutagenEngine.Values.Interfaces;

namespace GMutagenEngine.Values.Factories.Reflective.Interfaces;

public interface IReflectionValueFactory : IReflectionValueFactoryMark {
    IValue Create(object instance, MemberInfo member);
}
public interface IReflectionValueFactoryMark : ISelfMark<IReflectionValueFactoryMark> {
}
