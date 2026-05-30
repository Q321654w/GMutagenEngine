using GMutagenEngine.Identification.Identifiable.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Schemas.Members.Interfaces;
using GMutagenEngine.Schemas.Types.Interfaces;
using GMutagenEngine.Schemas.Types.Interfaces.Marks;

namespace GMutagenEngine.Schemas.Interfaces;

public interface ISchema<TSchemaId, TMemberId> : IIdentifiable<TSchemaId>, ISchemaMark {
    IType<TSchemaId> Type { get; set; }
    IDictionary<TMemberId, IMember<TSchemaId, TMemberId>> Members { get; set; }
}
public interface ISchemaMark : ISelfMark<ISchemaMark> {
}
