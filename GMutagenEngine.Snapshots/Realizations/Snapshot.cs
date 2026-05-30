using GMutagenEngine.Snapshots.Interfaces;

namespace GMutagenEngine.Snapshots.Realizations;

public class Snapshot<TId, TData>(TId id, TData data) : ISnapshot<TId, TData>
{
    public TId Id { get; set; } = id;
    public TData Data { get; } = data;
}