using GMutagenEngine.MetaData.Runtime.Marks;

namespace GmutagenEngine.Requests;

public interface IResponse : IDisposable, IResponseMark {
}

public interface IResponseMark : ISelfMark<IResponseMark>
{
}