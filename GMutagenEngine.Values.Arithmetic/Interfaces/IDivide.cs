using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Values.Arithmetic.Interfaces;

public interface IDivide<T> : IDivideMark {
    void Divide(T divisor);
}
public interface IDivideMark : ISelfMark<IDivideMark> {
}
