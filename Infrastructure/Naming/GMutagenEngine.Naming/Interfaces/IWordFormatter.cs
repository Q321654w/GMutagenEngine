using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Naming.Interfaces;

public interface IWordFormatter : ISyncFuncHandler<string, string>, IWordFormatterMark
{
}

public interface IWordFormatterMark : ISelfMark<IWordFormatterMark>
{
}
