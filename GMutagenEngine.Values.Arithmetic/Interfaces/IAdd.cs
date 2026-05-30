using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Values.Arithmetic.Interfaces;

public interface IAdd<T> : IAddMark {
    void Add(T delta);
}
public interface IAddMark : ISelfMark<IAddMark> {
}
