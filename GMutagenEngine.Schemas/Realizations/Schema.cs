using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Members.Interfaces;
using GMutagenEngine.Schemas.Types.Interfaces;
using GMutagenEngine.Schemas.Types.Realizations;

namespace GMutagenEngine.Schemas.Realizations;

public class Schema<TId, TMemberId>(IType<TId> type, IDictionary<TMemberId, IMember<TId, TMemberId>> members) : ISchema<TId, TMemberId> 
    where TId : notnull
    where TMemberId : notnull
{
    public TId Id { get; set; } = type.Id;
    public IType<TId> Type { get; set; } = type;
    public IDictionary<TMemberId, IMember<TId, TMemberId>> Members { get; set; } = members;

    public Schema() : this(new Type<TId>(), new Dictionary<TMemberId, IMember<TId, TMemberId>>())
    {
    }
}