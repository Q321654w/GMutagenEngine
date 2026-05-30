using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Routers.Sync.Api.Marks;

public interface ISyncRouterMark : ISelfMark<ISyncRouterMark>
{
}

public interface ISyncRouterInMark : ISelfMark<ISyncRouterInMark>
{
}

public interface ISyncRouterOutMark : ISelfMark<ISyncRouterOutMark>
{
}

public interface ISyncRouterInOutMark : ISelfMark<ISyncRouterInOutMark>
{
}

public interface ISyncRouterFanOutMark : ISelfMark<ISyncRouterFanOutMark>
{
}

public interface ISyncRouterInFanOutMark : ISelfMark<ISyncRouterInFanOutMark>
{
}
