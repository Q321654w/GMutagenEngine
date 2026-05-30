using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Interfaces;

public interface ICompositeId : IId, ICompositeIdMark {
}
public interface ICompositeIdMark : ISelfMark<ICompositeIdMark> {
}
