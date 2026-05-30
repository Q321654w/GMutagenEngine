using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Api.General.Pipelines.Interfaces;

public interface IAdapter : IAdapterMark {
}

public interface IRouter : IRouterMark {
}

public interface IFanOutRouter : IRouter, IFanOutRouterMark {
}

public interface IAdapterMark : ISelfMark<IAdapterMark> {
}

public interface IRouterMark : ISelfMark<IRouterMark> {
}

public interface IFanOutRouterMark : ISelfMark<IFanOutRouterMark> {
}
