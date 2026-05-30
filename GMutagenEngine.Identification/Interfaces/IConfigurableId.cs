using GMutagenEngine.Identification.Constants;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Interfaces;

public interface IConfigurableId : IConfigurableIdMark {
    IdEqualityBehavior EqualityBehavior { get; }
}
public interface IConfigurableIdMark : ISelfMark<IConfigurableIdMark> {
}
