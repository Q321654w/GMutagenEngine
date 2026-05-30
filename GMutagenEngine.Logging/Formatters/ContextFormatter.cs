using Constants.Literals;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Logging.Formatters;

public sealed class ContextFormatter<TKey, TValue>(
    ISyncFuncHandler<object?, string>? objectFormatter = null)
    : ISyncFuncHandler<IIndexedSyncStorage<TKey, TValue>, string>
{
    private readonly ISyncFuncHandler<object?, string> _objectFormatter = objectFormatter ?? new ObjectFormatter();

    public string Handle(IIndexedSyncStorage<TKey, TValue> data)
    {
        var pairs = data
            .Select(pair => _objectFormatter.Handle(pair.Key) + Literals.Symbols.EQUALS_STRING + _objectFormatter.Handle(pair.Value))
            .ToArray();

        if (pairs.Length == 0)
            return Literals.Symbols.OPEN_BRACE_STRING + Literals.Symbols.CLOSE_BRACE_STRING;

        return Literals.Symbols.OPEN_BRACE_STRING
               + Literals.Whitespace.SPACE_STRING
               + string.Join(Literals.Punctuation.COMMA_SPACE_STRING, pairs)
               + Literals.Whitespace.SPACE_STRING
               + Literals.Symbols.CLOSE_BRACE_STRING;
    }

}
