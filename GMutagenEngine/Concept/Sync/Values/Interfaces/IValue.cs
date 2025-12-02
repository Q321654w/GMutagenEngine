namespace GMutagenEngine.Concept.Sync.Values.Interfaces
{
    public interface IValue
    {
        object Value { get; set; }
        Type ValueType { get; }
    }

    public interface IValue<T> : IValue
    {
        new T Value { get; set; }
    }
}