using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Identification.Interfaces;

public interface ITypedIdentifier : IUnorderedCompositeId, ITypedIdentifierMark {
    Type OwnerType { get; }
    object? Value { get; }
}

public interface ITypedIdentifier<TOwner, out TValue> : ITypedIdentifier, ITypedIdentifierMark {
    new TValue? Value { get; }
}
public interface ITypedIdentifierMark : ISelfMark<ITypedIdentifierMark> {
}
