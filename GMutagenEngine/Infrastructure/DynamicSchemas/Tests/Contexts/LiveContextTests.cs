using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class LiveContextTests
{
    private class TestObject
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [Fact]
    public void SetValue_ShouldStoreValue()
    {
        var instance = new TestObject();
        var schema = new MockSchema { TargetType = typeof(TestObject) };
        var registry = new ContextRegistry();
        var context = new LiveContext(instance, schema, registry);
            
        var value = new MockValue("test");
        context.SetValue("key", value);
            
        var result = context.GetValue("key");
        Assert.Equal(value, result);
    }

    [Fact]
    public void AddDescriptor_ShouldStoreDescriptor()
    {
        var instance = new TestObject();
        var childInstance = new TestObject();
        var schema = new MockSchema { TargetType = typeof(TestObject) };
        var registry = new ContextRegistry();
        var context = new LiveContext(instance, schema, registry);
            
        var childContext = new ContextSnapshot();
        registry.Add(childInstance, childContext);
            
        var memberInfo = new MockSchemaMemberInfo
        {
            Name = "Child",
            Type = typeof(TestObject),
            Info = typeof(TestObject).GetProperty(nameof(TestObject.Name)),
            MemberType = MemberTypes.Property,
            Getter = _ => childInstance
        };
        var descriptor = new ObjectContextDescriptor(instance, memberInfo);
            
        context.AddDescriptor("child", descriptor);
            
        var result = context.GetContext("child");
        Assert.Equal(childContext, result);
    }

    [Fact]
    public void GetContext_WithNestedPath_ShouldTraverseDescriptors()
    {
        var instance = new TestObject();
        var childInstance = new TestObject();
        var schema = new MockSchema { TargetType = typeof(TestObject) };
        var registry = new ContextRegistry();
        var context = new LiveContext(instance, schema, registry);
            
        var childContext = new ContextSnapshot();
        var grandChildContext = new ContextSnapshot();
        childContext.AttachChild("grandchild", grandChildContext);
            
        registry.Add(childInstance, childContext);
            
        var memberInfo = new MockSchemaMemberInfo
        {
            Name = "Child",
            Type = typeof(TestObject),
            Info = typeof(TestObject).GetProperty(nameof(TestObject.Name)),
            MemberType = MemberTypes.Property,
            Getter = _ => childInstance
        };
        var descriptor = new ObjectContextDescriptor(instance, memberInfo);
            
        context.AddDescriptor("child", descriptor);
            
        var result = context.GetContext("child/grandchild");
        Assert.Equal(grandChildContext, result);
    }

    [Fact]
    public void GetValue_WithNestedPath_ShouldTraverseDescriptors()
    {
        var instance = new TestObject();
        var childInstance = new TestObject();
        var schema = new MockSchema { TargetType = typeof(TestObject) };
        var registry = new ContextRegistry();
        var context = new LiveContext(instance, schema, registry);
            
        var childContext = new ContextSnapshot();
        var nestedValue = new MockValue("nested");
        childContext.SetValue("prop", nestedValue);
            
        registry.Add(childInstance, childContext);
            
        var memberInfo = new MockSchemaMemberInfo
        {
            Name = "Child",
            Type = typeof(TestObject),
            Info = typeof(TestObject).GetProperty(nameof(TestObject.Name)),
            MemberType = MemberTypes.Property,
            Getter = _ => childInstance
        };
        var descriptor = new ObjectContextDescriptor(instance, memberInfo);
            
        context.AddDescriptor("child", descriptor);
            
        var result = context.GetValue("child/prop");
        Assert.Equal(nestedValue, result);
    }

    [Fact]
    public void AttachChild_ShouldThrowNotSupportedException()
    {
        var instance = new TestObject();
        var schema = new MockSchema { TargetType = typeof(TestObject) };
        var registry = new ContextRegistry();
        var context = new LiveContext(instance, schema, registry);
            
        Assert.Throws<NotSupportedException>(() => 
            context.AttachChild("child", new ContextSnapshot()));
    }

    [Fact]
    public void GetContexts_ShouldReturnAllDescriptorContexts()
    {
        var instance = new TestObject();
        var child1 = new TestObject();
        var child2 = new TestObject();
        var schema = new MockSchema { TargetType = typeof(TestObject) };
        var registry = new ContextRegistry();
        var context = new LiveContext(instance, schema, registry);
            
        var context1 = new ContextSnapshot();
        var context2 = new ContextSnapshot();
        registry.Add(child1, context1);
        registry.Add(child2, context2);
            
        var descriptor1 = new ObjectContextDescriptor(instance, 
            new MockSchemaMemberInfo 
            { 
                Name = "Child1",
                Type = typeof(TestObject),
                Info = typeof(TestObject).GetProperty(nameof(TestObject.Name)),
                MemberType = MemberTypes.Property,
                Getter = _ => child1 
            });
        var descriptor2 = new ObjectContextDescriptor(instance, 
            new MockSchemaMemberInfo 
            { 
                Name = "Child2",
                Type = typeof(TestObject),
                Info = typeof(TestObject).GetProperty(nameof(TestObject.Name)),
                MemberType = MemberTypes.Property,
                Getter = _ => child2 
            });
            
        context.AddDescriptor("child1", descriptor1);
        context.AddDescriptor("child2", descriptor2);
            
        var contexts = context.GetContexts().ToList();
            
        Assert.Equal(2, contexts.Count);
        Assert.Contains(context1, contexts);
        Assert.Contains(context2, contexts);
    }

    [Fact]
    public void Instance_ShouldReturnOriginalInstance()
    {
        var instance = new TestObject { Name = "Test" };
        var schema = new MockSchema { TargetType = typeof(TestObject) };
        var registry = new ContextRegistry();
        var context = new LiveContext(instance, schema, registry);
            
        Assert.Equal(instance, context.Instance);
    }

    [Fact]
    public void Schema_ShouldReturnOriginalSchema()
    {
        var instance = new TestObject();
        var schema = new MockSchema { TargetType = typeof(TestObject) };
        var registry = new ContextRegistry();
        var context = new LiveContext(instance, schema, registry);
            
        Assert.Equal(schema, context.Schema);
    }
}