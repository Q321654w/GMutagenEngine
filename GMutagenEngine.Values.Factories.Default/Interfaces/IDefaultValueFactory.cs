using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Values.Factories.Default.Interfaces.Marks;
using GMutagenEngine.Values.Interfaces;

namespace GMutagenEngine.Values.Factories.Default.Interfaces;

public interface IDefaultValueFactory : IValueFactoryMark, IDefaultValueFactoryMark {
    IValue Create(object? value, Type type);
}
public interface IDefaultValueFactoryMark : ISelfMark<IDefaultValueFactoryMark> {
}
