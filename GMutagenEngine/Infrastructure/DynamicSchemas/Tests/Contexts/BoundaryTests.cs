using System.Collections;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations.GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class BoundaryTests
{
    [Fact]
    public void LiveCollection_SetValue_WithLargeIndex_ShouldExpandCorrectly()
    {
        var list = new ArrayList { 1 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        context.SetValue("10", new MockValue(99));
            
        Assert.Equal(11, list.Count);
        Assert.Equal(99, list[10]);
    }

    [Fact]
    public void ContextSnapshot_MergeWithSelf_ShouldNotCauseIssues()
    {
        var context = new ContextSnapshot();
        context.SetValue("key", new MockValue("value"));
            
        // This shouldn't cause issues but is a potential edge case
        context.Merge(context);
            
        Assert.Single(context.Values);
    }

    [Fact]
    public void GetValue_WithVeryLongPath_ShouldWork()
    {
        var root = new ContextSnapshot();
        var current = root;
            
        // Build a chain of 10 levels
        for (int i = 0; i < 10; i++)
        {
            var next = new ContextSnapshot();
            current.AttachChild($"level{i}", next);
            current = next;
        }
            
        var value = new MockValue("deep");
        current.SetValue("value", value);
            
        var path = string.Join("/", Enumerable.Range(0, 10).Select(i => $"level{i}")) + "/value";
        var result = root.GetValue(path);
            
        Assert.Equal(value, result);
    }

    [Fact]
    public void ExistValue_WithNestedPath_ShouldReturnCorrectResult()
    {
        var root = new ContextSnapshot();
        var child = new ContextSnapshot();
        root.AttachChild("child", child);
        child.SetValue("value", new MockValue("test"));
            
        Assert.True(root.ExistValue("child/value"));
        Assert.False(root.ExistValue("child/nonexistent"));
    }

    [Fact]
    public void ExistContext_WithNestedPath_ShouldReturnCorrectResult()
    {
        var root = new ContextSnapshot();
        var child = new ContextSnapshot();
        var grandchild = new ContextSnapshot();
        root.AttachChild("child", child);
        child.AttachChild("grandchild", grandchild);
            
        Assert.True(root.ExistContext("child/grandchild"));
        Assert.False(root.ExistContext("child/nonexistent"));
    }
}