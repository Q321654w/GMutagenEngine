using GMutagenEngine.Pipelines.General;

namespace GMutagenEngine.Pipelines.Assembler;

public class Pipeline<TId, TNodeId> : IPipeline<TId, TNodeId>
    where TNodeId : notnull
{
    public TId Id { get; set; }
    public PipelineDefinition<TId, TNodeId> Definition { get; set; }
    
    public PipelineBuilder<TId, TNodeId> Create()
    {
        return new PipelineBuilder<TId, TNodeId>();
    }
}
