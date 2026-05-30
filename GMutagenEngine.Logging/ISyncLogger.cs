using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Logging;

public interface ISyncLogger<in TContext> : ISyncLoggerMark {
    void Log(TContext context);
}

public interface ISyncLogger<T, in TContext> : ISyncLogger<TContext>, ISyncLoggerMark {
}
public interface ISyncLoggerMark : ISelfMark<ISyncLoggerMark> {
}
