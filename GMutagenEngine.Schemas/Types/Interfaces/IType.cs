using GMutagenEngine.Identification.Identifiable.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Schemas.Types.Interfaces;

public interface IType<TId> : IIdentifiable<TId>, ITypeMark {
   
}
public interface ITypeMark : ISelfMark<ITypeMark> {
}
