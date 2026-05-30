using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Interfaces;

public interface ISingleId : IId, ISingleIdMark {
    object? Value { get; }
}

public interface ISingleId<out T> : ISingleId, ISingleIdMark {
    new T? Value { get; }
}
public interface ISingleIdMark : ISelfMark<ISingleIdMark> {
}
