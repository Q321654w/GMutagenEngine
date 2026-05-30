using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Naming.Interfaces;

public interface IWordsFormatter : ISyncFuncHandler<IEnumerable<string>, IEnumerable<string>>, IWordsFormatterMark
{
}

public interface IWordsFormatterMark : ISelfMark<IWordsFormatterMark>
{
}
