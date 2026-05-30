using GMutagenEngine.Identification.Tagging.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Identifiable.Interfaces;

public interface ITagable : ITagableMark {
    public HashSet<ITag> Tags { get; set; }
}
public interface ITagableMark : ISelfMark<ITagableMark> {
}
