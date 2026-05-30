using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Naming.Interfaces;

public interface IStringFormatter : ISyncFuncHandler<string, string>, IStringFormatterMark
{
}

public interface IStringFormatterMark : ISelfMark<IStringFormatterMark>
{
}
