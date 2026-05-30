using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Values.Arithmetic.Interfaces;

public interface ISubtract<T> : ISubtractMark {
    void Subtract(T delta);
}
public interface ISubtractMark : ISelfMark<ISubtractMark> {
}
