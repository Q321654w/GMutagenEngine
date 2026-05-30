using GMutagenEngine.Schemas.Types.Interfaces;

namespace GMutagenEngine.Schemas.Types.Realizations;

public class Type<TId>(TId id) : IType<TId>
{
    public TId Id { get; set; } = id;

    public Type() : this(default)
    {
    }
}