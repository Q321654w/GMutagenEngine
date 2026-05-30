using GMutagenEngine.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Pipelines.General;

public interface IPipeline<TId, TNodeId> : IIdentifiable<TId>, IPipelineMark
    where TNodeId : notnull
{
    PipelineDefinition<TId, TNodeId> Definition { get; }
}
