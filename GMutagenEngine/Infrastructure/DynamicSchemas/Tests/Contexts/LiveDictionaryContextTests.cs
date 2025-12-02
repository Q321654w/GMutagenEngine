using System.Collections;
using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class LiveDictionaryContextTests
{
    [Fact]
    public void GetValue_WithPrimitiveType_ShouldReturnDictionaryValue()
    {
        var dict = new Hashtable { { "key1", 42 } };
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        valueSchema.Members[SchemaConstants.VALUE_PROPERTY] = new MockSchemaMemberInfo 
        { 
            Schema = new MockSchema { TargetType = typeof(int) },
            Name = SchemaConstants.VALUE_PROPERTY,
            Type = typeof(int),
            MemberType = MemberTypes.Property
        };
            
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        var result = context.GetValue("key1");
            
        Assert.NotNull(result);
        Assert.IsType<DictionaryValue>(result);
    }

    [Fact]
    public void GetValue_WithNonExistentKey_ShouldReturnNull()
    {
        var dict = new Hashtable();
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        var result = context.GetValue("nonexistent");
            
        Assert.Null(result);
    }

    [Fact]
    public void GetContext_WithComplexType_ShouldReturnItemContext()
    {
        var item = new object();
        var dict = new Hashtable { { "key1", item } };
        var itemContext = new ContextSnapshot();
            
        var valueSchema = new MockSchema { TargetType = typeof(object) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        registry.Add(item, itemContext);
            
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: false);
            
        var result = context.GetContext("key1");
            
        Assert.Equal(itemContext, result);
    }

    [Fact]
    public void GetContext_WithPrimitiveType_ShouldReturnNull()
    {
        var dict = new Hashtable { { "key1", 42 } };
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        var result = context.GetContext("key1");
            
        Assert.Null(result);
    }

    [Fact]
    public void SetValue_WithPrimitiveType_ShouldUpdateDictionary()
    {
        var dict = new Hashtable { { "key1", 42 } };
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        context.SetValue("key1", new MockValue(99));
            
        Assert.Equal(99, dict["key1"]);
    }

    [Fact]
    public void SetValue_WithNewKey_ShouldAddToDictionary()
    {
        var dict = new Hashtable();
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        context.SetValue("newKey", new MockValue(42));
            
        Assert.True(dict.Contains("newKey"));
        Assert.Equal(42, dict["newKey"]);
    }

    [Fact]
    public void SetValue_WithComplexType_ShouldThrowException()
    {
        var dict = new Hashtable();
        var valueSchema = new MockSchema { TargetType = typeof(object) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: false);
            
        Assert.Throws<InvalidOperationException>(() => 
            context.SetValue("key", new MockValue(new object())));
    }

    [Fact]
    public void AddItem_ShouldAddToDictionary()
    {
        var dict = new Hashtable();
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        context.AddItem("key1", 42);
            
        Assert.True(dict.Contains("key1"));
        Assert.Equal(42, dict["key1"]);
    }

    [Fact]
    public void RemoveItem_WithExistingKey_ShouldRemove()
    {
        var dict = new Hashtable { { "key1", 42 } };
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        context.RemoveItem("key1");
            
        Assert.False(dict.Contains("key1"));
    }

    [Fact]
    public void RemoveItem_WithNonExistentKey_ShouldNotThrow()
    {
        var dict = new Hashtable();
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        context.RemoveItem("nonexistent");
            
        Assert.Empty(dict);
    }

    [Fact]
    public void ContainsKey_WithExistingKey_ShouldReturnTrue()
    {
        var dict = new Hashtable { { "key1", 42 } };
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        Assert.True(context.ContainsKey("key1"));
    }

    [Fact]
    public void ContainsKey_WithNonExistentKey_ShouldReturnFalse()
    {
        var dict = new Hashtable();
        var valueSchema = new MockSchema { TargetType = typeof(int) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var registry = new ContextRegistry();
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: true);
            
        Assert.False(context.ContainsKey("nonexistent"));
    }

    [Fact]
    public void GetContexts_WithComplexType_ShouldReturnAllItemContexts()
    {
        var item1 = new object();
        var item2 = new object();
        var dict = new Hashtable { { "key1", item1 }, { "key2", item2 } };
        var context1 = new ContextSnapshot();
        var context2 = new ContextSnapshot();
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var valueSchema = new MockSchema { TargetType = typeof(object) };
        var registry = new ContextRegistry();
        registry.Add(item1, context1);
        registry.Add(item2, context2);
            
        var context = new LiveDictionaryContext(dict, valueSchema, keySchema, registry, isPrimitive: false);
            
        var contexts = context.GetContexts().ToList();
            
        Assert.Equal(2, contexts.Count);
        Assert.Contains(context1, contexts);
        Assert.Contains(context2, contexts);
    }
}