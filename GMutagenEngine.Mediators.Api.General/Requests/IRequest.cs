using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Mediators.Api.General.Requests;

public interface IRequest<TResponse> : IRequestMark {
}
public interface IRequestMark : ISelfMark<IRequestMark> {
}
