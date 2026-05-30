using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Interfaces;

public interface IIdOptimized : IId, IIdOptimizedMark {
}
public interface IIdOptimizedMark : ISelfMark<IIdOptimizedMark> {
}
