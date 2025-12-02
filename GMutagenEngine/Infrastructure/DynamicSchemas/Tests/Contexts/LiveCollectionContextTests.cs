using System.Collections;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations.GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class LiveCollectionContextTests
{
    [Fact]
    public void GetValue_WithPrimitiveType_ShouldReturnListValue()
    {
        var list = new List<int> { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        var result = context.GetValue("1");
            
        Assert.NotNull(result);
        Assert.IsType<ListValue>(result);
    }

    [Fact]
    public void GetValue_WithInvalidIndex_ShouldReturnNull()
    {
        var list = new List<int> { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        var result = context.GetValue("10");
            
        Assert.Null(result);
    }

    [Fact]
    public void GetValue_WithNegativeIndex_ShouldReturnNull()
    {
        var list = new List<int> { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        var result = context.GetValue("-1");
            
        Assert.Null(result);
    }

    [Fact]
    public void GetValue_WithNonNumericIndex_ShouldReturnNull()
    {
        var list = new List<int> { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        var result = context.GetValue("abc");
            
        Assert.Null(result);
    }

    [Fact]
    public void GetContext_WithComplexType_ShouldReturnItemContext()
    {
        var item = new object();
        var list = new ArrayList { item };
        var itemContext = new ContextSnapshot();
        var schema = new MockSchema { TargetType = typeof(object) };
        var registry = new ContextRegistry();
        registry.Add(item, itemContext);
            
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: false);
            
        var result = context.GetContext("0");
            
        Assert.Equal(itemContext, result);
    }

    [Fact]
    public void GetContext_WithPrimitiveType_ShouldReturnNull()
    {
        var list = new List<int> { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        var result = context.GetContext("0");
            
        Assert.Null(result);
    }

    [Fact]
    public void GetContext_WithNestedPath_ShouldTraverseToChild()
    {
        var item = new object();
        var list = new ArrayList { item };
        var itemContext = new ContextSnapshot();
        var nestedValue = new MockValue("nested");
        itemContext.SetValue("prop", nestedValue);
            
        var schema = new MockSchema { TargetType = typeof(object) };
        var registry = new ContextRegistry();
        registry.Add(item, itemContext);
            
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: false);
            
        var result = context.GetValue("0/prop");
            
        Assert.Equal(nestedValue, result);
    }

    [Fact]
    public void SetValue_WithPrimitiveType_ShouldUpdateList()
    {
        var list = new ArrayList { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        context.SetValue("1", new MockValue(99));
            
        Assert.Equal(99, list[1]);
    }

    [Fact]
    public void SetValue_WithIndexBeyondCount_ShouldExpandList()
    {
        var list = new ArrayList { 1 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        context.SetValue("3", new MockValue(99));
            
        Assert.Equal(4, list.Count);
        Assert.Equal(99, list[3]);
    }

    [Fact]
    public void SetValue_WithComplexType_ShouldThrowException()
    {
        var list = new ArrayList();
        var schema = new MockSchema { TargetType = typeof(object) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: false);
            
        Assert.Throws<InvalidOperationException>(() => 
            context.SetValue("0", new MockValue(new object())));
    }

    [Fact]
    public void GetContexts_WithComplexType_ShouldReturnAllItemContexts()
    {
        var item1 = new object();
        var item2 = new object();
        var list = new ArrayList { item1, item2 };
        var context1 = new ContextSnapshot();
        var context2 = new ContextSnapshot();
            
        var schema = new MockSchema { TargetType = typeof(object) };
        var registry = new ContextRegistry();
        registry.Add(item1, context1);
        registry.Add(item2, context2);
            
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: false);
            
        var contexts = context.GetContexts().ToList();
            
        Assert.Equal(2, contexts.Count);
        Assert.Contains(context1, contexts);
        Assert.Contains(context2, contexts);
    }

    [Fact]
    public void GetContexts_WithPrimitiveType_ShouldReturnEmpty()
    {
        var list = new List<int> { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        var contexts = context.GetContexts().ToList();
            
        Assert.Empty(contexts);
    }

    [Fact]
    public void AddItem_ShouldAddToList()
    {
        var list = new ArrayList { 1, 2 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        context.AddItem(3);
            
        Assert.Equal(3, list.Count);
        Assert.Equal(3, list[2]);
    }

    [Fact]
    public void RemoveAt_WithValidIndex_ShouldRemoveItem()
    {
        var list = new ArrayList { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        context.RemoveAt(1);
            
        Assert.Equal(2, list.Count);
        Assert.Equal(3, list[1]);
    }

    [Fact]
    public void RemoveAt_WithInvalidIndex_ShouldNotThrow()
    {
        var list = new ArrayList { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        context.RemoveAt(10);
            
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void Count_ShouldReturnListCount()
    {
        var list = new ArrayList { 1, 2, 3 };
        var schema = new MockSchema { TargetType = typeof(int) };
        var registry = new ContextRegistry();
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: true);
            
        Assert.Equal(3, context.Count);
    }

    [Fact]
    public void GetItemContext_WithValidIndex_ShouldReturnContext()
    {
        var item = new object();
        var list = new ArrayList { item };
        var itemContext = new ContextSnapshot();
            
        var schema = new MockSchema { TargetType = typeof(object) };
        var registry = new ContextRegistry();
        registry.Add(item, itemContext);
            
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: false);
            
        var result = context.GetItemContext(0);
            
        Assert.Equal(itemContext, result);
    }
}