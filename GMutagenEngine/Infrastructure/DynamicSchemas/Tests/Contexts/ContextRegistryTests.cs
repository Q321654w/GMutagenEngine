using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class ContextRegistryTests
{
    [Fact]
    public void Add_ShouldStoreContext()
    {
        var registry = new ContextRegistry();
        var obj = new object();
        var context = new ContextSnapshot();
            
        registry.Add(obj, context);
            
        var result = registry.Get(obj);
        Assert.Equal(context, result);
    }
        
    [Fact]
    public void Get_WithNonExistentKey_ShouldThrowKeyNotFoundException()
    {
        var registry = new ContextRegistry();
        var obj = new object();
            
        Assert.Throws<KeyNotFoundException>(() => registry.Get(obj));
    }


    [Fact]
    public void TryGet_WithExistingKey_ShouldReturnTrueAndContext()
    {
        var registry = new ContextRegistry();
        var obj = new object();
        var context = new ContextSnapshot();
        registry.Add(obj, context);
            
        var success = registry.TryGet(obj, out var result);
            
        Assert.True(success);
        Assert.Equal(context, result);
    }

    [Fact]
    public void TryGet_WithNonExistentKey_ShouldReturnFalse()
    {
        var registry = new ContextRegistry();
        var obj = new object();
            
        var success = registry.TryGet(obj, out var result);
            
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public void Exists_WithExistingKey_ShouldReturnTrue()
    {
        var registry = new ContextRegistry();
        var obj = new object();
        var context = new ContextSnapshot();
        registry.Add(obj, context);
            
        Assert.True(registry.Exists(obj));
    }

    [Fact]
    public void Exists_WithNonExistentKey_ShouldReturnFalse()
    {
        var registry = new ContextRegistry();
        var obj = new object();
            
        Assert.False(registry.Exists(obj));
    }

    [Fact]
    public void Remove_ShouldRemoveContext()
    {
        var registry = new ContextRegistry();
        var obj = new object();
        var context = new ContextSnapshot();
        registry.Add(obj, context);
            
        var removed = registry.Remove(obj);
            
        Assert.Equal(context, removed);
        Assert.False(registry.Exists(obj));
    }

    [Fact]
    public void Clear_ShouldRemoveAllContexts()
    {
        var registry = new ContextRegistry();
        var obj1 = new object();
        var obj2 = new object();
        registry.Add(obj1, new ContextSnapshot());
        registry.Add(obj2, new ContextSnapshot());
            
        registry.Clear();
            
        Assert.False(registry.Exists(obj1));
        Assert.False(registry.Exists(obj2));
    }

    [Fact]
    public void GetAll_ShouldReturnAllContexts()
    {
        var registry = new ContextRegistry();
        var context1 = new ContextSnapshot();
        var context2 = new ContextSnapshot();
        registry.Add(new object(), context1);
        registry.Add(new object(), context2);
            
        var all = registry.GetAll().ToList();
            
        Assert.Equal(2, all.Count);
        Assert.Contains(context1, all);
        Assert.Contains(context2, all);
    }
}