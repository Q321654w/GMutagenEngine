using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Adapters.Sync.Api.Marks;

public interface ISyncAdapterMark : ISelfMark<ISyncAdapterMark>
{
}

public interface ISyncAdapterInMark : ISelfMark<ISyncAdapterInMark>
{
}

public interface ISyncAdapterOutMark : ISelfMark<ISyncAdapterOutMark>
{
}

public interface ISyncAdapterInOutMark : ISelfMark<ISyncAdapterInOutMark>
{
}

public interface ISyncAdapterFanOutMark : ISelfMark<ISyncAdapterFanOutMark>
{
}

public interface ISyncAdapterInFanOutMark : ISelfMark<ISyncAdapterInFanOutMark>
{
}
