using System.Numerics;
using GMutagenEngine.Mediators.Api.General.Channnels;
using GMutagenEngine.Mediators.Api.Sync.Topics;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Mediators.Api.Sync.Channels;

public class SyncSingleChannel<TId, TMessage>(ISyncSingleTopic<TId> topic)
    : ISyncSingleChannel<TId, TMessage>
{
    public TId Id
    {
        get => topic.Id;
        set => topic.Id = value;
    }
    
    public ISyncMediator<TId> Mediator   
    {
        get => topic.Mediator;
        set => topic.Mediator = value;
    }
}

public class SyncFanOutChannel<TId, TMessage>(ISyncFanOutTopic<TId> topic)
    : ISyncFanOutChannel<TId, TMessage>
{
    public TId Id
    {
        get => topic.Id;
        set => topic.Id = value;
    }
    
    public ISyncFanOutMediator<TId> Mediator   
    {
        get => topic.Mediator;
        set => topic.Mediator = value;
    }
}

public interface ISyncSingleChannel<TId, TMessage>
    : ISingleChannel<TId, TMessage>, ISyncChannelMark
{
    ISyncMediator<TId> Mediator { get; set; }
}

public interface ISyncChannelMark : ISelfMark<ISyncChannelMark>
{
}

public interface ISyncFanOutChannel<TId, TMessage>
    : IFanOutChannel<TId, TMessage>, ISyncChannelMark
{
    ISyncFanOutMediator<TId> Mediator { get; set; }
}


public static class SingleChannelExtensions
{
    public static void Publish<TId, TMessage>(
        this ISyncSingleChannel<TId, TMessage> topic,
        TMessage data)
        => topic.Mediator.Publish(data, topic.Id);

    public static TOut? Send<TId, TMessage, TOut>(
        this ISyncSingleChannel<TId, TMessage> topic,
        TMessage data)
        => topic.Mediator.Send<TMessage, TOut>(data, topic.Id);

    public static void PublishRelative<TId, TMessage>(
        this ISyncSingleChannel<TId, TMessage> topic,
        TId relativeId,
        TMessage data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(data, topic.Id + relativeId);

    public static void PublishAbsolute<TId, TMessage>(
        this ISyncSingleChannel<TId, TMessage> topic,
        TId absoluteId,
        TMessage data)
        => topic.Mediator.Publish(data, absoluteId);

    public static TOut? SendRelative<TId, TMessage, TOut>(
        this ISyncSingleChannel<TId, TMessage> topic,
        TId relativeId,
        TMessage data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TMessage, TOut>(data, topic.Id + relativeId);

    public static TOut? SendAbsolute<TId, TMessage, TOut>(
        this ISyncSingleChannel<TId, TMessage> topic,
        TId absoluteId,
        TMessage data)
        => topic.Mediator.Send<TMessage, TOut>(data, absoluteId);
}

public static class FanOutChannelExtensions
{
    public static void Publish<TId, TMessage>(
        this ISyncFanOutChannel<TId, TMessage> topic,
        TMessage data)
        => topic.Mediator.Publish(data, topic.Id);

    public static IEnumerable<TOut?> Send<TId, TMessage, TOut>(
        this ISyncFanOutChannel<TId, TMessage> topic,
        TMessage data)
        => topic.Mediator.Send<TMessage, TOut>(data, topic.Id);

    public static void PublishRelative<TId, TMessage>(
        this ISyncFanOutChannel<TId, TMessage> topic,
        TId relativeId,
        TMessage data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(data, topic.Id + relativeId);

    public static void PublishAbsolute<TId, TMessage>(
        this ISyncFanOutChannel<TId, TMessage> topic,
        TId absoluteId,
        TMessage data)
        => topic.Mediator.Publish(data, absoluteId);

    public static IEnumerable<TOut?> SendRelative<TId, TMessage, TOut>(
        this ISyncFanOutChannel<TId, TMessage> topic,
        TId relativeId,
        TMessage data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TMessage, TOut>(data, topic.Id + relativeId);

    public static IEnumerable<TOut?> SendAbsolute<TId, TMessage, TOut>(
        this ISyncFanOutChannel<TId, TMessage> topic,
        TId absoluteId,
        TMessage data)
        => topic.Mediator.Send<TMessage, TOut>(data, absoluteId);
}
