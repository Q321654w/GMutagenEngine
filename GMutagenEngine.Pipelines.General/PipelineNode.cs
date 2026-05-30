namespace GMutagenEngine.Pipelines.General;

public sealed record PipelineNode<TNodeId>(
    TNodeId Id,
    Type HandlerType);

