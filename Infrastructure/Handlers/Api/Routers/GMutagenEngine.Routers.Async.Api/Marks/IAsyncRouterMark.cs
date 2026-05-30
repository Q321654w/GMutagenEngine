using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Routers.Async.Api.Marks;

public interface IAsyncRouterMark : ISelfMark<IAsyncRouterMark>
{
}

public interface IAsyncRouterInMark : ISelfMark<IAsyncRouterInMark>
{
}

public interface IAsyncRouterOutMark : ISelfMark<IAsyncRouterOutMark>
{
}

public interface IAsyncRouterInOutMark : ISelfMark<IAsyncRouterInOutMark>
{
}

public interface IAsyncRouterFanOutMark : ISelfMark<IAsyncRouterFanOutMark>
{
}

public interface IAsyncRouterInFanOutMark : ISelfMark<IAsyncRouterInFanOutMark>
{
}
