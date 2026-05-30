using GMutagenEngine.Mediators.Api.General.Topics;
using GMutagenEngine.MetaData.Runtime.Marks;
using System.Numerics;

namespace GMutagenEngine.Mediators.Api.Sync.Topics;

public class SyncSingleTopic<TId>(
    TId id,
    ISyncMediator<TId> mediator)
    : ISyncSingleTopic<TId>
{
    public TId Id { get; set; } = id;
    public ISyncMediator<TId> Mediator { get; set; } = mediator;
}

public class SyncFanOutTopic<TId>(
    TId id,
    ISyncFanOutMediator<TId> mediator)
    : ISyncFanOutTopic<TId>
{
    public TId Id { get; set; } = id;
    public ISyncFanOutMediator<TId> Mediator { get; set; } = mediator;
}



public interface ISyncSingleTopic<TId>
    : ISingleTopic<TId>, ISyncTopicMark
{
    ISyncMediator<TId> Mediator { get; set; }
}

public interface ISyncFanOutTopic<TId>
    : IFanOutTopic<TId>, ISyncTopicMark
{
    ISyncFanOutMediator<TId> Mediator { get; set; }
}



public static class SingleTopicExtensions
{
    public static void Publish<TId>(this ISyncSingleTopic<TId> syncTopic) 
        => syncTopic.Mediator.Publish(syncTopic.Id);

    public static void Publish<TId, TIn>(this ISyncSingleTopic<TId> syncTopic, TIn data)
        => syncTopic.Mediator.Publish(data, syncTopic.Id);

    public static TOut? Send<TId, TOut>(this ISyncSingleTopic<TId> syncTopic) 
        => syncTopic.Mediator.Send<TOut>(syncTopic.Id);

    public static TOut? Send<TId, TIn, TOut>(this ISyncSingleTopic<TId> syncTopic, TIn data)
        => syncTopic.Mediator.Send<TIn, TOut>(data, syncTopic.Id);

    public static void PublishRelative<TId>(this ISyncSingleTopic<TId> syncTopic, TId relativeId)
        where TId : IAdditionOperators<TId, TId, TId> 
        => syncTopic.Mediator.Publish(syncTopic.Id + relativeId);

    public static void PublishRelative<TId, TIn>(this ISyncSingleTopic<TId> syncTopic, TId relativeId, TIn data)
        where TId : IAdditionOperators<TId, TId, TId> 
        => syncTopic.Mediator.Publish(data, syncTopic.Id + relativeId);

    public static void PublishAbsolute<TId>(this ISyncSingleTopic<TId> syncTopic, TId absoluteId)
        => syncTopic.Mediator.Publish(absoluteId);

    public static void PublishAbsolute<TId, TIn>(this ISyncSingleTopic<TId> syncTopic, TId absoluteId, TIn data) 
        => syncTopic.Mediator.Publish(data, absoluteId);

    public static TOut? SendRelative<TId, TOut>(this ISyncSingleTopic<TId> syncTopic, TId relativeId)
        where TId : IAdditionOperators<TId, TId, TId> 
        => syncTopic.Mediator.Send<TOut>(syncTopic.Id + relativeId);

    public static TOut? SendRelative<TId, TIn, TOut>(this ISyncSingleTopic<TId> syncTopic, TId relativeId, TIn data)
        where TId : IAdditionOperators<TId, TId, TId> 
        => syncTopic.Mediator.Send<TIn, TOut>(data, syncTopic.Id + relativeId);

    public static TOut? SendAbsolute<TId, TOut>(this ISyncSingleTopic<TId> syncTopic, TId absoluteId)
        => syncTopic.Mediator.Send<TOut>(absoluteId);

    public static TOut? SendAbsolute<TId, TIn, TOut>(this ISyncSingleTopic<TId> syncTopic, TId absoluteId, TIn data) 
        => syncTopic.Mediator.Send<TIn, TOut>(data, absoluteId);
}

public static class FanOutTopicExtensions
{
    public static void Publish<TId>(this ISyncFanOutTopic<TId> topic)
        => topic.Mediator.Publish(topic.Id);

    public static void Publish<TId, TIn>(this ISyncFanOutTopic<TId> topic, TIn data) 
        => topic.Mediator.Publish(data, topic.Id);

    public static IEnumerable<TOut?> Send<TId, TOut>(this ISyncFanOutTopic<TId> topic) 
        => topic.Mediator.Send<TOut>(topic.Id);

    public static IEnumerable<TOut?> Send<TId, TIn, TOut>(this ISyncFanOutTopic<TId> topic, TIn data)
        => topic.Mediator.Send<TIn, TOut>(data, topic.Id);

    public static void PublishRelative<TId>(this ISyncFanOutTopic<TId> topic, TId relativeId)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Publish(topic.Id + relativeId);

    public static void PublishRelative<TId, TIn>(this ISyncFanOutTopic<TId> topic, TId relativeId, TIn data)
        where TId : IAdditionOperators<TId, TId, TId> 
        => topic.Mediator.Publish(data, topic.Id + relativeId);

    public static void PublishAbsolute<TId>(this ISyncFanOutTopic<TId> topic, TId absoluteId)
        => topic.Mediator.Publish(absoluteId);

    public static void PublishAbsolute<TId, TIn>(this ISyncFanOutTopic<TId> topic, TId absoluteId, TIn data)
        => topic.Mediator.Publish(data, absoluteId);

    public static IEnumerable<TOut?> SendRelative<TId, TOut>(this ISyncFanOutTopic<TId> topic, TId relativeId)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TOut>(topic.Id + relativeId);

    public static IEnumerable<TOut?> SendRelative<TId, TIn, TOut>(this ISyncFanOutTopic<TId> topic, TId relativeId, TIn data)
        where TId : IAdditionOperators<TId, TId, TId>
        => topic.Mediator.Send<TIn, TOut>(data, topic.Id + relativeId);

    public static IEnumerable<TOut?> SendAbsolute<TId, TOut>(this ISyncFanOutTopic<TId> topic, TId absoluteId) 
        => topic.Mediator.Send<TOut>(absoluteId);

    public static IEnumerable<TOut?> SendAbsolute<TId, TIn, TOut>(this ISyncFanOutTopic<TId> topic, TId absoluteId, TIn data) 
        => topic.Mediator.Send<TIn, TOut>(data, absoluteId);
}

public interface ISyncTopicMark : ISelfMark<ISyncTopicMark>
{
}