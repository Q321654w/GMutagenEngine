namespace GMutagenEngine.Infrastructure.Identification.Interfaces;

public interface ISingleId : IId
{
    object? Value { get; }
}

public interface ISingleId<out T> : ISingleId
{
    new T? Value { get; }
}