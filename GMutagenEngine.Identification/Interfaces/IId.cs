using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Interfaces;

public interface IId : IIdMark {
}
public interface IIdMark : ISelfMark<IIdMark> {
}
