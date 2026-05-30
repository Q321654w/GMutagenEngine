using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Members.Interfaces;

namespace GMutagenEngine.Schemas.Members.Realizations;

public class Member<TId, TMemberId>(TMemberId id, ISchema<TId, TMemberId> schema) : IMember<TId, TMemberId>
{
    public TMemberId Id { get; set; } = id;
    public ISchema<TId, TMemberId> Schema { get; set; } = schema;
}