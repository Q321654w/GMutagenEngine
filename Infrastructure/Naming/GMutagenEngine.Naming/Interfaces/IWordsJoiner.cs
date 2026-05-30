using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Naming.Interfaces;

public interface IWordsJoiner : ISyncFuncHandler<IEnumerable<string>, string>, IWordsJoinerMark
{
}

public interface IWordsJoinerMark : ISelfMark<IWordsJoinerMark>
{
}
