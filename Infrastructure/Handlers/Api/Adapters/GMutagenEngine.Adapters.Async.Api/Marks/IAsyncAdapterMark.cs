using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Adapters.Async.Api.Marks;

public interface IAsyncAdapterMark : ISelfMark<IAsyncAdapterMark>
{
}

public interface IAsyncAdapterInMark : ISelfMark<IAsyncAdapterInMark>
{
}

public interface IAsyncAdapterOutMark : ISelfMark<IAsyncAdapterOutMark>
{
}

public interface IAsyncAdapterInOutMark : ISelfMark<IAsyncAdapterInOutMark>
{
}

public interface IAsyncAdapterFanOutMark : ISelfMark<IAsyncAdapterFanOutMark>
{
}

public interface IAsyncAdapterInFanOutMark : ISelfMark<IAsyncAdapterInFanOutMark>
{
}
