using GMutagenEngine.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Pipelines.General;

public sealed class PipelineDefinition<TId, TNodeId> : IIdentifiable<TId>
    where TNodeId : notnull
{
    public TId Id { get; set; }
    public List<PipelineNode<TNodeId>> Nodes { get; set; }
    public List<PipelineEdge<TNodeId>> Edges { get; set; }
}
