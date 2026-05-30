using GMutagenEngine.Mediators.Api.General.Topics;
using GMutagenEngine.Mediators.Async;
using GMutagenEngine.MetaData.Runtime.Marks;
using System.Numerics;

namespace GMutagenEngine.Mediators.Api.Async.Topics;

public class AsyncSingleTopic<TId>(
    TId id,
    IAsyncMediator<TId> mediator)
    : IAsyncSingleTopic<TId>
{
    public TId Id { get; set; } = id;

    public IAsyncMediator<TId> Mediator { get; set; } = mediator;
}

public class AsyncFanOutTopic<TId>(
    TId id,
    IAsyncFanOutMediator<TId> mediator)
    : IAsyncFanOutTopic<TId>
{
    public TId Id { get; set; } = id;

    public IAsyncFanOutMediator<TId> Mediator { get; set; } = mediator;
}



public interface IAsyncSingleTopic<TId>
    : ISingleTopic<TId>, IAsyncTopicMark
{
    IAsyncMediator<TId> Mediator { get; set; }
}

public interface IAsyncFanOutTopic<TId>
    : IFanOutTopic<TId>, IAsyncTopicMark
{
    IAsyncFanOutMediator<TId> Mediator { get; set; }
}



public static class SingleTopicExtensions
{
    public static Task Publish<TId>(
        this IAsyncSingleTopic<TId> topic)
        => topic.Mediator.Publish(id: topic.Id);

    public static Task Publish<TId, TIn>(
        this IAsyncSingleTopic<TId> topic,
        TIn data)
        => topic.Mediator.Publish(data, topic.Id);

    public static Task<TOut?> Send<TId, TOut>(
        this IAsyncSingleTopic<TId> topic)
        => topic.Mediator.Send<TOut>(topic.Id);

    public static Task<TOut?> Send<TId, TIn, TOut>(
        this IAsyncSingleTopic<TId> topic,
        TIn data)
        => topic.Mediator.Send<TIn, TOut>(data, topic.Id);

    public static Task PublishRelative<TId>(
        this IAsyncSingleTopic<TId> topic,
        TId relativeId)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(id: topic.Id + relativeId);

    public static Task PublishRelative<TId, TIn>(
        this IAsyncSingleTopic<TId> topic,
        TId relativeId,
        TIn data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(data, topic.Id + relativeId);

    public static Task PublishAbsolute<TId>(
        this IAsyncSingleTopic<TId> topic,
        TId absoluteId)
        => topic.Mediator.Publish(id: absoluteId);

    public static Task PublishAbsolute<TId, TIn>(
        this IAsyncSingleTopic<TId> topic,
        TId absoluteId,
        TIn data)
        => topic.Mediator.Publish(data, absoluteId);

    public static Task<TOut?> SendRelative<TId, TOut>(
        this IAsyncSingleTopic<TId> topic,
        TId relativeId)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TOut>(topic.Id + relativeId);

    public static Task<TOut?> SendRelative<TId, TIn, TOut>(
        this IAsyncSingleTopic<TId> topic,
        TId relativeId,
        TIn data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TIn, TOut>(data, topic.Id + relativeId);

    public static Task<TOut?> SendAbsolute<TId, TOut>(
        this IAsyncSingleTopic<TId> topic,
        TId absoluteId)
        => topic.Mediator.Send<TOut>(absoluteId);

    public static Task<TOut?> SendAbsolute<TId, TIn, TOut>(
        this IAsyncSingleTopic<TId> topic,
        TId absoluteId,
        TIn data)
        => topic.Mediator.Send<TIn, TOut>(data, absoluteId);
}

public static class FanOutTopicExtensions
{
    public static Task Publish<TId>(
        this IAsyncFanOutTopic<TId> topic)
        => topic.Mediator.Publish(id: topic.Id);

    public static Task Publish<TId, TIn>(
        this IAsyncFanOutTopic<TId> topic,
        TIn data)
        => topic.Mediator.Publish(data, topic.Id);

    public static IAsyncEnumerable<TOut?> Send<TId, TOut>(
        this IAsyncFanOutTopic<TId> topic)
        => topic.Mediator.Send<TOut>(topic.Id);

    public static IAsyncEnumerable<TOut?> Send<TId, TIn, TOut>(
        this IAsyncFanOutTopic<TId> topic,
        TIn data)
        => topic.Mediator.Send<TIn, TOut>(data, topic.Id);

    public static Task PublishRelative<TId>(
        this IAsyncFanOutTopic<TId> topic,
        TId relativeId)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(id: topic.Id + relativeId);

    public static Task PublishRelative<TId, TIn>(
        this IAsyncFanOutTopic<TId> topic,
        TId relativeId,
        TIn data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(data, topic.Id + relativeId);

    public static Task PublishAbsolute<TId>(
        this IAsyncFanOutTopic<TId> topic,
        TId absoluteId)
        => topic.Mediator.Publish(id: absoluteId);

    public static Task PublishAbsolute<TId, TIn>(
        this IAsyncFanOutTopic<TId> topic,
        TId absoluteId,
        TIn data)
        => topic.Mediator.Publish(data, absoluteId);

    public static IAsyncEnumerable<TOut?> SendRelative<TId, TOut>(
        this IAsyncFanOutTopic<TId> topic,
        TId relativeId)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TOut>(topic.Id + relativeId);

    public static IAsyncEnumerable<TOut?> SendRelative<TId, TIn, TOut>(
        this IAsyncFanOutTopic<TId> topic,
        TId relativeId,
        TIn data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TIn, TOut>(data, topic.Id + relativeId);

    public static IAsyncEnumerable<TOut?> SendAbsolute<TId, TOut>(
        this IAsyncFanOutTopic<TId> topic,
        TId absoluteId)
        => topic.Mediator.Send<TOut>(absoluteId);

    public static IAsyncEnumerable<TOut?> SendAbsolute<TId, TIn, TOut>(
        this IAsyncFanOutTopic<TId> topic,
        TId absoluteId,
        TIn data)
        => topic.Mediator.Send<TIn, TOut>(data, absoluteId);
}



public interface IAsyncTopicMark : ISelfMark<IAsyncTopicMark>
{
}