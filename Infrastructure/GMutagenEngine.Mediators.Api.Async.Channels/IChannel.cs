using System.Numerics;
using GMutagenEngine.Mediators.Api.Async.Topics;
using GMutagenEngine.Mediators.Api.General.Channnels;
using GMutagenEngine.Mediators.Async;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Mediators.Api.Async.Channels;

public class AsyncSingleChannel<TId, TMessage>(IAsyncSingleTopic<TId> topic) : IAsyncSingleChannel<TId, TMessage>
{
    public TId Id
    {
        get => topic.Id;
        set => topic.Id = value;
    }
    
    public IAsyncMediator<TId> Mediator   
    {
        get => topic.Mediator;
        set => topic.Mediator = value;
    }
}

public class AsyncFanOutChannel<TId, TMessage>(IAsyncFanOutTopic<TId> topic)
    : IAsyncFanOutChannel<TId, TMessage>
{
    public TId Id
    {
        get => topic.Id;
        set => topic.Id = value;
    }
    
    public IAsyncFanOutMediator<TId> Mediator   
    {
        get => topic.Mediator;
        set => topic.Mediator = value;
    }
}


public interface IAsyncSingleChannel<TId, TMessage>
    : ISingleChannel<TId, TMessage>, IAsyncChannelMark
{
    IAsyncMediator<TId> Mediator { get; set; }
}

public interface IAsyncChannelMark : ISelfMark<IAsyncChannelMark>
{
}

public interface IAsyncFanOutChannel<TId, TMessage>
    : IFanOutChannel<TId, TMessage>, IAsyncChannelMark
{
    IAsyncFanOutMediator<TId> Mediator { get; set; }
}

public static class SingleChannelExtensions
{
    public static Task Publish<TId, TMessage>(
        this IAsyncSingleChannel<TId, TMessage> topic,
        TMessage data)
        => topic.Mediator.Publish(data, topic.Id);

    public static Task<TOut?> Send<TId, TMessage, TOut>(
        this IAsyncSingleChannel<TId, TMessage> topic,
        TMessage data)
        => topic.Mediator.Send<TMessage, TOut>(data, topic.Id);

    public static Task PublishRelative<TId, TMessage>(
        this IAsyncSingleChannel<TId, TMessage> topic,
        TId relativeId,
        TMessage data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(data, topic.Id + relativeId);

    public static Task PublishAbsolute<TId, TMessage>(
        this IAsyncSingleChannel<TId, TMessage> topic,
        TId absoluteId,
        TMessage data)
        => topic.Mediator.Publish(data, absoluteId);

    public static Task<TOut?> SendRelative<TId, TMessage, TOut>(
        this IAsyncSingleChannel<TId, TMessage> topic,
        TId relativeId,
        TMessage data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TMessage, TOut>(data, topic.Id + relativeId);

    public static Task<TOut?> SendAbsolute<TId, TMessage, TOut>(
        this IAsyncSingleChannel<TId, TMessage> topic,
        TId absoluteId,
        TMessage data)
        => topic.Mediator.Send<TMessage, TOut>(data, absoluteId);
}

public static class FanOutChannelExtensions
{
    public static Task Publish<TId, TMessage>(
        this IAsyncFanOutChannel<TId, TMessage> topic,
        TMessage data)
        => topic.Mediator.Publish(data, topic.Id);

    public static IAsyncEnumerable<TOut?> Send<TId, TMessage, TOut>(
        this IAsyncFanOutChannel<TId, TMessage> topic,
        TMessage data)
        => topic.Mediator.Send<TMessage, TOut>(data, topic.Id);

    public static Task PublishRelative<TId, TMessage>(
        this IAsyncFanOutChannel<TId, TMessage> topic,
        TId relativeId,
        TMessage data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(data, topic.Id + relativeId);

    public static Task PublishAbsolute<TId, TMessage>(
        this IAsyncFanOutChannel<TId, TMessage> topic,
        TId absoluteId,
        TMessage data)
        => topic.Mediator.Publish(data, absoluteId);

    public static IAsyncEnumerable<TOut?> SendRelative<TId, TMessage, TOut>(
        this IAsyncFanOutChannel<TId, TMessage> topic,
        TId relativeId,
        TMessage data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TMessage, TOut>(data, topic.Id + relativeId);

    public static IAsyncEnumerable<TOut?> SendAbsolute<TId, TMessage, TOut>(
        this IAsyncFanOutChannel<TId, TMessage> topic,
        TId absoluteId,
        TMessage data)
        => topic.Mediator.Send<TMessage, TOut>(data, absoluteId);
}