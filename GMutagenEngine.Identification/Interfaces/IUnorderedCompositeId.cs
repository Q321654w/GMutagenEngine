using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Interfaces;

public interface IUnorderedCompositeId : ICompositeId, IUnorderedCompositeIdMark {
    IReadOnlySet<IId> Components { get; }
}
public interface IUnorderedCompositeIdMark : ISelfMark<IUnorderedCompositeIdMark> {
}
