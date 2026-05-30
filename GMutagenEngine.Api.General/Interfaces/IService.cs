using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Api.General.Interfaces;

public interface IService : IServiceMark
{
}
public interface IServiceMark : ISelfMark<IServiceMark>
{
}