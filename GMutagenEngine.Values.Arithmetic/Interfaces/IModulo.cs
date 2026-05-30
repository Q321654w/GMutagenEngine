using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Values.Arithmetic.Interfaces;

public interface IModulo<T> : IModuloMark {
    void Modulo(T divisor);
}
public interface IModuloMark : ISelfMark<IModuloMark> {
}
