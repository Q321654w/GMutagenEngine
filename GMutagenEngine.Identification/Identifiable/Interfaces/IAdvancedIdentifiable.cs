using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Identifiable.Interfaces;

public interface IAdvancedIdentifiable<TId> : IIdentifiable<TId>, ITagable, IAdvancedIdentifiableMark {

}
public interface IAdvancedIdentifiableMark : ISelfMark<IAdvancedIdentifiableMark> {
}
