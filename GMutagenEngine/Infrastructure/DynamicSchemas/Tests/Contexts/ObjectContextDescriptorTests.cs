using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class ObjectContextDescriptorTests
{
    private class TestObject
    {
        public TestObject Child { get; set; }
    }

    [Fact]
    public void ResolveContext_WithValidMemberInfo_ShouldReturnContext()
    {
        var instance = new TestObject();
        var child = new TestObject();
        instance.Child = child;
            
        var childContext = new ContextSnapshot();
        var registry = new ContextRegistry();
        registry.Add(child, childContext);
            
        var memberInfo = new MockSchemaMemberInfo
        {
            Name = "Child",
            Type = typeof(TestObject),
            Info = typeof(TestObject).GetProperty("Child"),
            MemberType = MemberTypes.Property,
            Getter = obj => ((TestObject)obj).Child
        };
            
        var descriptor = new ObjectContextDescriptor(instance, memberInfo);
            
        var result = descriptor.ResolveContext(registry);
            
        Assert.Equal(childContext, result);
    }

    [Fact]
    public void ResolveContext_WithNullMemberInfo_ShouldReturnNull()
    {
        var instance = new TestObject();
        var registry = new ContextRegistry();
            
        var memberInfo = new MockSchemaMemberInfo
        {
            Name = "Child",
            Type = typeof(TestObject),
            Info = null,
            MemberType = MemberTypes.Property,
            Getter = obj => ((TestObject)obj).Child
        };
            
        var descriptor = new ObjectContextDescriptor(instance, memberInfo);
            
        var result = descriptor.ResolveContext(registry);
            
        Assert.Null(result);
    }

    [Fact]
    public void ResolveContext_WithNullValue_ShouldReturnNull()
    {
        var instance = new TestObject { Child = null };
        var registry = new ContextRegistry();
            
        var memberInfo = new MockSchemaMemberInfo
        {
            Name = "Child",
            Type = typeof(TestObject),
            Info = typeof(TestObject).GetProperty("Child"),
            MemberType = MemberTypes.Property,
            Getter = obj => ((TestObject)obj).Child
        };
            
        var descriptor = new ObjectContextDescriptor(instance, memberInfo);
            
        var result = descriptor.ResolveContext(registry);
            
        Assert.Null(result);
    }

    [Fact]
    public void ResolveContext_WithUnregisteredValue_ShouldReturnNull()
    {
        var instance = new TestObject();
        var child = new TestObject();
        instance.Child = child;
            
        var registry = new ContextRegistry();
            
        var memberInfo = new MockSchemaMemberInfo
        {
            Name = "Child",
            Type = typeof(TestObject),
            Info = typeof(TestObject).GetProperty("Child"),
            MemberType = MemberTypes.Property,
            Getter = obj => ((TestObject)obj).Child
        };
            
        var descriptor = new ObjectContextDescriptor(instance, memberInfo);
        Assert.Throws<KeyNotFoundException>(() => descriptor.ResolveContext(registry));
    }
}