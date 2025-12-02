using GMutagenEngine.Concept.Sync.Values.Factories.Default.Realizations;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations.GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts.Generation;

public class LiveCollectionContextTests
{
    private readonly ISchemaRegistry _schemaRegistry = new SchemaRegistry();
    private readonly IContextRegistry _contextRegistry = new ContextRegistry();

    [Fact]
    public void GetValue_PrimitiveList_ReturnsCorrectValue()
    {
        // Arrange
        var list = new List<int> { 10, 20, 30 };
        var schema = list.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, list) as LiveCollectionContext;

        // Act
        var value = context.GetValue("1");

        // Assert
        Assert.NotNull(value);
        Assert.Equal(20, value.Value);
    }

    [Fact]
    public void GetValue_InvalidIndex_ReturnsNull()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };
        var schema = list.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, list) as LiveCollectionContext;

        // Act
        var value = context.GetValue("99");

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void AddItem_AddsToList()
    {
        // Arrange
        var list = new List<int> { 1, 2 };
        var schema = list.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, list) as LiveCollectionContext;

        // Act
        context.AddItem(3);

        // Assert
        Assert.Equal(3, context.Count);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void RemoveAt_RemovesFromList()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };
        var schema = list.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, list) as LiveCollectionContext;

        // Act
        context.RemoveAt(1);

        // Assert
        Assert.Equal(2, context.Count);
        Assert.Equal(new[] { 1, 3 }, list);
    }

    [Fact]
    public void GetItemContext_ComplexType_ReturnsContext()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person { Name = "Alice", Age = 25 },
            new Person { Name = "Bob", Age = 30 }
        };
        var schema = people.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, people) as LiveCollectionContext;

        // Act
        var itemContext = context.GetItemContext(0);

        // Assert
        Assert.NotNull(itemContext);
        var name = itemContext.GetValue("Name");
        Assert.Equal("Alice", name.Value);
    }

    [Fact]
    public void GetContexts_ComplexList_ReturnsAllItemContexts()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person { Name = "Alice" },
            new Person { Name = "Bob" }
        };
        var schema = people.GetType().ToSchema();
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, people) as LiveCollectionContext;

        // Act
        var contexts = context.GetContexts().ToList();

        // Assert
        Assert.Equal(2, contexts.Count);
    }
}