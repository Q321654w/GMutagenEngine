using GMutagenEngine.Concept.Sync.Values.Factories.Default.Realizations;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts.Generation;

public class LiveDictionaryContextTests
{
    private readonly ISchemaRegistry _schemaRegistry = new SchemaRegistry();
    private readonly IContextRegistry _contextRegistry = new ContextRegistry();

    [Fact]
    public void GetValue_PrimitiveDictionary_ReturnsCorrectValue()
    {
        // Arrange
        var dict = new Dictionary<string, int> { ["key1"] = 100 };
        var schema = dict.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, dict) as LiveDictionaryContext;

        // Act
        var value = context.GetValue("key1");

        // Assert
        Assert.NotNull(value);
        Assert.Equal(100, value.Value);
    }

    [Fact]
    public void ContainsKey_ExistingKey_ReturnsTrue()
    {
        // Arrange
        var dict = new Dictionary<string, string> { ["test"] = "value" };
        var schema = dict.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, dict) as LiveDictionaryContext;

        // Act & Assert
        Assert.True(context.ContainsKey("test"));
    }

    [Fact]
    public void AddItem_AddsKeyValuePair()
    {
        // Arrange
        var dict = new Dictionary<string, int>();
        var schema = dict.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, dict) as LiveDictionaryContext;

        // Act
        context.AddItem("newKey", 42);

        // Assert
        Assert.True(context.ContainsKey("newKey"));
        Assert.Equal(42, dict["newKey"]);
    }

    [Fact]
    public void RemoveItem_RemovesKeyValuePair()
    {
        // Arrange
        var dict = new Dictionary<string, int> { ["key1"] = 1, ["key2"] = 2 };
        var schema = dict.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, dict) as LiveDictionaryContext;

        // Act
        context.RemoveItem("key1");

        // Assert
        Assert.False(context.ContainsKey("key1"));
        Assert.Single(dict);
    }

    [Fact]
    public void GetItemContext_ComplexValue_ReturnsContext()
    {
        // Arrange
        var dict = new Dictionary<string, Person>
        {
            ["person1"] = new Person { Name = "Alice", Age = 30 }
        };
        var schema = dict.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, dict) as LiveDictionaryContext;

        // Act
        var itemContext = context.GetItemContext("person1");

        // Assert
        Assert.NotNull(itemContext);
        var name = itemContext.GetValue("Name");
        Assert.Equal("Alice", name.Value);
    }
}