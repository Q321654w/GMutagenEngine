using System.Linq.Expressions;
using GMutagenEngine.Identification.Realizations.Single;
using GMutagenEngine.Mediators.Api.General.Channnels;
using GMutagenEngine.Pipelines.General;

namespace GMutagenEngine.Pipelines.Assembler;

public class TypedPipeline : GenericIdPipeline
{
    public TypedPipeline()
    {
        Id = new SingleId<Type>(GetType());
    }
}

public sealed class PipelineBuilder<TPipelineId, TNodeId>
    where TNodeId : notnull
{
    public List<PipelineNode<TNodeId>> Nodes { get; set; } = new();
    public List<PipelineEdge<TNodeId>> Edges { get; set; } = new();

    public HandlerBuilder<TPipelineId, TNodeId, THandler> Add<THandler>(TNodeId nodeId)
    {
        Nodes.Add(new PipelineNode<TNodeId>(nodeId, typeof(THandler)));
        return new HandlerBuilder<TPipelineId, TNodeId, THandler>(this, nodeId);
    }
    
    public HandlerBuilder<TPipelineId, TNodeId, THandler> Call<THandler>(
        Expression<Func<THandler, IChannel>> selector,
        TNodeId from,
        params TNodeId[] to)
    {
        var body = selector.Body;
        if (body is UnaryExpression unary && unary.NodeType == ExpressionType.Convert)
            body = unary.Operand;

        if (body is not MemberExpression memberExpr)
            throw new InvalidOperationException(
                "Selector must be a direct member access, e.g. f => f.SyncChannel");

        var member = memberExpr.Member;
        var propertyType = memberExpr.Type;

        if (!propertyType.IsGenericType || propertyType.GetGenericArguments().Length != 2)
            throw new InvalidOperationException(
                $"Channel type {propertyType} must have exactly two generic arguments.");

        var genericArgs = propertyType.GetGenericArguments();

        foreach (var target in to)
        {
            Edges.Add(new PipelineEdge<TNodeId>(
                from,
                new PipelinePort(member, propertyType, genericArgs[0], genericArgs[1]),
                target));
        }

        if (Nodes.All(n => !n.Id!.Equals(from)))
            throw new InvalidOperationException(
                $"Node with Id '{from}' not found. Add it first using .Add<THandler>(nodeId).");

        return new HandlerBuilder<TPipelineId, TNodeId, THandler>(this, from);
    }
}

public sealed class HandlerBuilder<TPipelineId, TNodeId, THandler>
    where TNodeId : notnull
{
    private readonly PipelineBuilder<TPipelineId, TNodeId> _root;
    private readonly TNodeId _current;
    internal HandlerBuilder(PipelineBuilder<TPipelineId, TNodeId> root, TNodeId current)
    {
        _root = root;
        _current = current;
    }
    
    public HandlerBuilder<TPipelineId, TNodeId, THandler> Call(
        Expression<Func<THandler, IChannel>> selector,
        params TNodeId[] to)
    {
        return _root.Call(selector, _current, to);
    }
    
    public HandlerBuilder<TPipelineId, TNodeId, TOtherHandler> Call<TOtherHandler>(
        Expression<Func<TOtherHandler, IChannel>> selector,
        TNodeId from,
        params TNodeId[] to)
    {
        return _root.Call(selector, from, to);
    }
    
    public HandlerBuilder<TPipelineId, TNodeId, TNext> Add<TNext>(TNodeId nodeId)
    {
        return _root.Add<TNext>(nodeId);
    }
}