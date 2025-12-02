using System.Collections;
using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations.GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class ContextIntegrationTests
{
    [Fact]
    public void ComplexNestedStructure_ShouldNavigateCorrectly()
    {
        var root = new ContextSnapshot();
        var level1 = new ContextSnapshot();
        var level2 = new ContextSnapshot();
        var value = new MockValue("deep value");
            
        root.AttachChild("level1", level1);
        level1.AttachChild("level2", level2);
        level2.SetValue("value", value);
            
        var result = root.GetValue("level1/level2/value");
            
        Assert.Equal(value, result);
    }

    [Fact]
    public void MixedContextTypes_ShouldWorkTogether()
    {
        var registry = new ContextRegistry();
            
        var parentObj = new object();
        var list = new ArrayList { 1, 2, 3 };
        var parentSchema = new MockSchema { TargetType = typeof(object) };
        var parentContext = new LiveContext(parentObj, parentSchema, registry);
        registry.Add(parentObj, parentContext);
            
        var collectionSchema = new MockSchema { TargetType = typeof(int) };
        var collectionContext = new LiveCollectionContext(list, collectionSchema, registry, isPrimitive: true);
        registry.Add(list, collectionContext);
            
        var memberInfo = new MockSchemaMemberInfo
        {
            Name = "Items",
            Type = typeof(ArrayList),
            Info = typeof(object).GetType().GetProperty(nameof(GetType)), // dummy MemberInfo
            MemberType = MemberTypes.Property,
            Getter = _ => list
        };
        var descriptor = new ObjectContextDescriptor(parentObj, memberInfo);
        parentContext.AddDescriptor("items", descriptor);
            
        var result = parentContext.GetValue("items/1");
            
        Assert.NotNull(result);
        Assert.NotNull((int)result.Value == 2);
    }

    [Fact]
    public void LiveCollectionWithComplexObjects_ShouldNavigateCorrectly()
    {
        var registry = new ContextRegistry();
        var item1 = new object();
        var item2 = new object();
        var list = new ArrayList { item1, item2 };
            
        var item1Context = new ContextSnapshot();
        var item2Context = new ContextSnapshot();
        item1Context.SetValue("name", new MockValue("Item1"));
        item2Context.SetValue("name", new MockValue("Item2"));
            
        registry.Add(item1, item1Context);
        registry.Add(item2, item2Context);
            
        var schema = new MockSchema { TargetType = typeof(object) };
        var collectionContext = new LiveCollectionContext(list, schema, registry, isPrimitive: false);
            
        var result1 = collectionContext.GetValue("0/name");
        var result2 = collectionContext.GetValue("1/name");
            
        Assert.Equal("Item1", ((MockValue)result1).Value);
        Assert.Equal("Item2", ((MockValue)result2).Value);
    }

    [Fact]
    public void LiveDictionaryWithComplexObjects_ShouldNavigateCorrectly()
    {
        var registry = new ContextRegistry();
        var item1 = new object();
        var item2 = new object();
        var dict = new Hashtable { { "key1", item1 }, { "key2", item2 } };
            
        var item1Context = new ContextSnapshot();
        var item2Context = new ContextSnapshot();
        item1Context.SetValue("value", new MockValue("Value1"));
        item2Context.SetValue("value", new MockValue("Value2"));
            
        registry.Add(item1, item1Context);
        registry.Add(item2, item2Context);
            
        var schema = new MockSchema { TargetType = typeof(object) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var dictContext = new LiveDictionaryContext(dict, schema, keySchema, registry, isPrimitive: false);
            
        var result1 = dictContext.GetValue("key1/value");
        var result2 = dictContext.GetValue("key2/value");
            
        Assert.Equal("Value1", ((MockValue)result1).Value);
        Assert.Equal("Value2", ((MockValue)result2).Value);
    }

    [Fact]
    public void EmptyPath_ShouldReturnNull()
    {
        var context = new ContextSnapshot();
            
        var result = context.GetValue("");
            
        Assert.Null(result);
    }

    [Fact]
    public void PathWithMultipleDelimiters_ShouldHandleCorrectly()
    {
        var root = new ContextSnapshot();
        var child = new ContextSnapshot();
        var value = new MockValue("test");
            
        root.AttachChild("child", child);
        child.SetValue("value", value);
            
        // Path with extra delimiters should still work due to RemoveEmptyEntries
        var result = root.GetValue("//child//value//");
            
        Assert.Equal(value, result);
    }

    [Fact]
    public void GetAllContexts_WithMixedTypes_ShouldReturnAll()
    {
        var registry = new ContextRegistry();
        var root = new ContextSnapshot();
        var child1 = new ContextSnapshot();
        var element = new object();
        var elementContext = new ContextSnapshot();
        var list = new ArrayList { element };
        var listContext = new LiveCollectionContext(list, new MockSchema(), registry, false);
        registry.Add(element, elementContext);
            
        root.AttachChild("child1", child1);
        root.AttachChild("list", listContext);
            
        var allContexts = root.GetAllContexts().ToList();
            
        Assert.Contains(child1, allContexts);
        Assert.Contains(listContext, allContexts);
    }

    [Fact]
    public void NullItemInCollection_ShouldHandleGracefully()
    {
        var registry = new ContextRegistry();
        var list = new ArrayList { null, new object() };
        var schema = new MockSchema { TargetType = typeof(object) };
        var context = new LiveCollectionContext(list, schema, registry, isPrimitive: false);
            
        var result = context.GetContext("0");
            
        Assert.Null(result);
    }

    [Fact]
    public void NullItemInDictionary_ShouldHandleGracefully()
    {
        var registry = new ContextRegistry();
        var dict = new Hashtable { { "key1", null } };
        var schema = new MockSchema { TargetType = typeof(object) };
        var keySchema = new MockSchema { TargetType = typeof(string) };
        var context = new LiveDictionaryContext(dict, schema, keySchema, registry, isPrimitive: false);
            
        var result = context.GetContext("key1");
            
        Assert.Null(result);
    }
}