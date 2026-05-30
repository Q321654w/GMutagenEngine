using GMutagenEngine.Identification.Identifiable.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Members.Interfaces.Marks;

namespace GMutagenEngine.Schemas.Members.Interfaces;

public interface IMember<TSchemaId, TMemberId> : IIdentifiable<TMemberId>, ISchemaMemberInfoMark, IMemberMark {
    ISchema<TSchemaId, TMemberId> Schema { get; set; }
}
public interface IMemberMark : ISelfMark<IMemberMark> {
}
