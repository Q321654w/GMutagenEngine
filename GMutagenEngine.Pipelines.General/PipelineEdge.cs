using System.Reflection;

namespace GMutagenEngine.Pipelines.General;

public sealed record PipelinePort(
    MemberInfo Member,
    Type ChannelType,
    Type MessageType, 
    Type ChannelIdType);

public sealed record PipelineEdge<TNodeId>(
    TNodeId From,
    PipelinePort Port,
    TNodeId To);