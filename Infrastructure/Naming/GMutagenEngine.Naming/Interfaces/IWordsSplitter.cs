using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Naming.Interfaces;

public interface IWordsSplitter : ISyncFuncHandler<string, string[]>, IStringSplitterMark
{
}

public interface IStringSplitterMark : ISelfMark<IStringSplitterMark>
{
}
