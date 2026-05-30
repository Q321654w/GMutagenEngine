using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Values.Arithmetic.Interfaces;

public interface IMultiply<T> : IMultiplyMark {
    void Multiply(T factor);
}
public interface IMultiplyMark : ISelfMark<IMultiplyMark> {
}
