using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Identifiable.Interfaces;

public interface IIdentifiable<TId> : IIdentifiableMark {
    public TId Id { get; set; }
}
public interface IIdentifiableMark : ISelfMark<IIdentifiableMark> {
}
