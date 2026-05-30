using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Interfaces;

public interface IOrderedCompositeId : ICompositeId, IOrderedCompositeIdMark {
    IReadOnlyList<IId> Components { get; }
}
public interface IOrderedCompositeIdMark : ISelfMark<IOrderedCompositeIdMark> {
}
