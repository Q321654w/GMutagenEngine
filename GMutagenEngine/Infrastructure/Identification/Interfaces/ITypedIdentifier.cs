namespace GMutagenEngine.Infrastructure.Identification.Interfaces;

public interface ITypedIdentifier : IUnorderedCompositeId
{
    Type OwnerType { get; }
    object? Value { get; }
}

public interface ITypedIdentifier<TOwner, out TValue> : ITypedIdentifier
{
    new TValue? Value { get; }
}