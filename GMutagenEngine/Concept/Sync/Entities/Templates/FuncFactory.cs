namespace GMutagenEngine.Concept.Sync.Entities.Templates;

public class FuncFactory<T>(Func<T> func) : IFactory<T>
{
    private Func<T> Func { get; } = func;

    public T Create() => Func();
}