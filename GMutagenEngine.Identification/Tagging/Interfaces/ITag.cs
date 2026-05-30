using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Tagging.Interfaces;

public interface ITag : IId, ITagMark {
}
public interface ITagMark : ISelfMark<ITagMark> {
}
