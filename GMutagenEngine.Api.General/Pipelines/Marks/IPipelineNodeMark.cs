using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Api.General.Pipelines.Marks;

public interface IAdapterMark : ISelfMark<IAdapterMark> {
}

public interface IRouterMark : ISelfMark<IRouterMark> {
}

public interface IFanOutRouterMark : IRouterMark, ISelfMark<IFanOutRouterMark> {
}
