using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class ContextBaseTests
{
    [Fact]
    public void GetAllContexts_ShouldReturnAllNestedContexts()
    {
        var root = new ContextSnapshot();
        var child1 = new ContextSnapshot();
        var child2 = new ContextSnapshot();
        var grandchild = new ContextSnapshot();
            
        root.AttachChild("child1", child1);
        root.AttachChild("child2", child2);
        child1.AttachChild("grandchild", grandchild);
            
        var allContexts = root.GetAllContexts().ToList();
            
        Assert.Equal(3, allContexts.Count);
        Assert.Contains(child1, allContexts);
        Assert.Contains(child2, allContexts);
        Assert.Contains(grandchild, allContexts);
    }

    [Fact]
    public void GetAllValues_ShouldReturnAllNestedValues()
    {
        var root = new ContextSnapshot();
        var child = new ContextSnapshot();
            
        var value1 = new MockValue("value1");
        var value2 = new MockValue("value2");
        var value3 = new MockValue("value3");
            
        root.SetValue("rootValue", value1);
        root.AttachChild("child", child);
        child.SetValue("childValue1", value2);
        child.SetValue("childValue2", value3);
            
        var allValues = root.GetAllValues().ToList();
            
        Assert.Equal(3, allValues.Count);
        Assert.Contains(value1, allValues);
        Assert.Contains(value2, allValues);
        Assert.Contains(value3, allValues);
    }

    [Fact]
    public void GetAllValues_WithDeepNesting_ShouldReturnAllValues()
    {
        var root = new ContextSnapshot();
        var level1 = new ContextSnapshot();
        var level2 = new ContextSnapshot();
        var level3 = new ContextSnapshot();
            
        var v1 = new MockValue("v1");
        var v2 = new MockValue("v2");
        var v3 = new MockValue("v3");
        var v4 = new MockValue("v4");
            
        root.SetValue("v1", v1);
        root.AttachChild("l1", level1);
        level1.SetValue("v2", v2);
        level1.AttachChild("l2", level2);
        level2.SetValue("v3", v3);
        level2.AttachChild("l3", level3);
        level3.SetValue("v4", v4);
            
        var allValues = root.GetAllValues().ToList();
            
        Assert.Equal(4, allValues.Count);
    }
}