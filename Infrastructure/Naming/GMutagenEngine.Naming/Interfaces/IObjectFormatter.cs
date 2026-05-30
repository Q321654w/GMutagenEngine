using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Naming.Interfaces;

public interface IObjectFormatter : ISyncFuncHandler<object, string>, IObjectFormatterMark
{
}

public interface IObjectFormatter<in TObject> : ISyncFuncHandler<TObject, string>, IObjectFormatterMark
{
}

public interface IObjectFormatterMark : ISelfMark<IObjectFormatterMark>
{
}
