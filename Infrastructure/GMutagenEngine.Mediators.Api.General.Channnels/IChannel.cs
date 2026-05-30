using GMutagenEngine.Identification.Identifiable.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Mediators.Api.General.Channnels;

public interface IChannel : IChannelMark
{
    
}

public interface IChannel<TId, TMessage> : IIdentifiable<TId>, IChannel
{
    Type MessageType => typeof(TMessage);
}

public interface ISingleChannel<TId, TMessage> : IChannel<TId, TMessage>
{
}

public interface IFanOutChannel<TId, TMessage> : IChannel<TId, TMessage>
{
}

public interface IChannelMark : ISelfMark<IChannelMark>
{
}