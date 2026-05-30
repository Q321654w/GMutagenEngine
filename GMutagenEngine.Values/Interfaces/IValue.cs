using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Values.Interfaces;

public interface IValue : IValueMark {
    Type ValueType { get; }
    object Value { get; set; }
}

public interface IValue<T> : IValue, IValueMark {
    new T Value { get; set; }
}
public interface IValueMark : ISelfMark<IValueMark> {
}
