using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class ContextSnapshotTests
{
    [Fact]
    public void SetValue_ShouldStoreValue()
    {
        var context = new ContextSnapshot();
        var value = new MockValue("test");
            
        context.SetValue("key", value);
            
        Assert.Single(context.Values);
        Assert.Equal(value, context.Values["key"]);
    }

    [Fact]
    public void GetValue_WithSimplePath_ShouldReturnValue()
    {
        var context = new ContextSnapshot();
        var value = new MockValue("test");
        context.SetValue("key", value);
            
        var result = context.GetValue("key");
            
        Assert.Equal(value, result);
    }

    [Fact]
    public void GetValue_WithNestedPath_ShouldTraverseContexts()
    {
        var parent = new ContextSnapshot();
        var child = new ContextSnapshot();
        var value = new MockValue("nested");
            
        child.SetValue("childKey", value);
        parent.AttachChild("childContext", child);
            
        var result = parent.GetValue("childContext/childKey");
            
        Assert.Equal(value, result);
    }

    [Fact]
    public void GetValue_WithNonExistentPath_ShouldReturnNull()
    {
        var context = new ContextSnapshot();
            
        var result = context.GetValue("nonexistent");
            
        Assert.Null(result);
    }

    [Fact]
    public void AttachChild_ShouldStoreContext()
    {
        var parent = new ContextSnapshot();
        var child = new ContextSnapshot();
            
        parent.AttachChild("child", child);
            
        Assert.Single(parent.Contexts);
        Assert.Equal(child, parent.Contexts["child"]);
    }

    [Fact]
    public void GetContext_WithSimplePath_ShouldReturnContext()
    {
        var parent = new ContextSnapshot();
        var child = new ContextSnapshot();
        parent.AttachChild("child", child);
            
        var result = parent.GetContext("child");
            
        Assert.Equal(child, result);
    }

    [Fact]
    public void GetContext_WithNestedPath_ShouldTraverseContexts()
    {
        var root = new ContextSnapshot();
        var middle = new ContextSnapshot();
        var leaf = new ContextSnapshot();
            
        middle.AttachChild("leaf", leaf);
        root.AttachChild("middle", middle);
            
        var result = root.GetContext("middle/leaf");
            
        Assert.Equal(leaf, result);
    }

    [Fact]
    public void ExistValue_WhenValueExists_ShouldReturnTrue()
    {
        var context = new ContextSnapshot();
        context.SetValue("key", new MockValue("test"));
            
        Assert.True(context.ExistValue("key"));
    }

    [Fact]
    public void ExistValue_WhenValueDoesNotExist_ShouldReturnFalse()
    {
        var context = new ContextSnapshot();
            
        Assert.False(context.ExistValue("key"));
    }

    [Fact]
    public void ExistContext_WhenContextExists_ShouldReturnTrue()
    {
        var parent = new ContextSnapshot();
        var child = new ContextSnapshot();
        parent.AttachChild("child", child);
            
        Assert.True(parent.ExistContext("child"));
    }

    [Fact]
    public void GetContexts_ShouldReturnAllChildContexts()
    {
        var parent = new ContextSnapshot();
        var child1 = new ContextSnapshot();
        var child2 = new ContextSnapshot();
            
        parent.AttachChild("child1", child1);
        parent.AttachChild("child2", child2);
            
        var contexts = parent.GetContexts().ToList();
            
        Assert.Equal(2, contexts.Count);
        Assert.Contains(child1, contexts);
        Assert.Contains(child2, contexts);
    }

    [Fact]
    public void GetValues_ShouldReturnAllValues()
    {
        var context = new ContextSnapshot();
        var value1 = new MockValue("test1");
        var value2 = new MockValue("test2");
            
        context.SetValue("key1", value1);
        context.SetValue("key2", value2);
            
        var values = context.GetValues().ToList();
            
        Assert.Equal(2, values.Count);
        Assert.Contains(value1, values);
        Assert.Contains(value2, values);
    }

    [Fact]
    public void Merge_ShouldCombineValues()
    {
        var context1 = new ContextSnapshot();
        var context2 = new ContextSnapshot();
            
        context1.SetValue("key1", new MockValue("value1"));
        context2.SetValue("key2", new MockValue("value2"));
            
        context1.Merge(context2);
            
        Assert.Equal(2, context1.Values.Count);
        Assert.True(context1.ExistValue("key1"));
        Assert.True(context1.ExistValue("key2"));
    }

    [Fact]
    public void Merge_ShouldOverwriteExistingValues()
    {
        var context1 = new ContextSnapshot();
        var context2 = new ContextSnapshot();
            
        var oldValue = new MockValue("old");
        var newValue = new MockValue("new");
            
        context1.SetValue("key", oldValue);
        context2.SetValue("key", newValue);
            
        context1.Merge(context2);
            
        Assert.Equal(newValue, context1.Values["key"]);
    }

    [Fact]
    public void Merge_ShouldMergeNestedContexts()
    {
        var context1 = new ContextSnapshot();
        var context2 = new ContextSnapshot();
        var child1 = new ContextSnapshot();
        var child2 = new ContextSnapshot();
            
        child1.SetValue("childKey1", new MockValue("value1"));
        child2.SetValue("childKey2", new MockValue("value2"));
            
        context1.AttachChild("child", child1);
        context2.AttachChild("child", child2);
            
        context1.Merge(context2);
            
        var mergedChild = context1.GetContext("child") as ContextSnapshot;
        Assert.NotNull(mergedChild);
        Assert.Equal(2, mergedChild.Values.Count);
    }
}