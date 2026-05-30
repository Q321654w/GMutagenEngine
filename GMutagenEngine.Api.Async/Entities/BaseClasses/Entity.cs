using GMutagenEngine.Api.General.Interfaces;
using GMutagenEngine.Mediators.Api.Async.Topics;
using GMutagenEngine.Storing.Storages.Async.Simple;

namespace GMutagenEngine.Api.Async.Entities.BaseClasses;

public class Entity<TId, TMethodId, TValueId>(TId id, IAsyncSingleTopic<TMethodId> topic, IAsyncStorage<TValueId> valueStorage) : IEntity<TId>
{
    public TId Id { get; set; } = id;
    
    public IAsyncSingleTopic<TMethodId> Topic { get; set; } = topic;
    public IAsyncStorage<TValueId> ValueStorage { get; set; } = valueStorage;

    public Entity(IAsyncSingleTopic<TMethodId> topic, IAsyncStorage<TValueId> valueStorage) : this(default, topic, valueStorage)
    {
    }
   
}