using GMutagenEngine.Api.General.Interfaces;
using GMutagenEngine.Mediators;
using GMutagenEngine.Mediators.Api.Sync.Topics;
using GMutagenEngine.Storing.Storages.Sync.Simple;

namespace GMutagenEngine.Api.Sync.Entities.BaseClasses;

public class Entity<TId, TMethodId, TValueId>(TId id, ISyncSingleTopic<TMethodId> topic, ISyncStorage<TValueId> valueStorage) : IEntity<TId>
{
    public TId Id { get; set; } = id;
    
    public ISyncSingleTopic<TMethodId> Topic { get; set; } = topic;
    public ISyncStorage<TValueId> ValueStorage { get; set; } = valueStorage;

    public Entity(ISyncSingleTopic<TMethodId> topic, ISyncStorage<TValueId> valueStorage) : this(default, topic, valueStorage)
    {
    }
}