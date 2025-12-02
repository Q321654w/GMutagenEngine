using GMutagenEngine.Concept.Sync.Values.Factories.Default.Realizations;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Extensions;
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

public class EdgeCaseTests
{
    private readonly ISchemaRegistry _schemaRegistry = new SchemaRegistry();
    private readonly IContextRegistry _contextRegistry = new ContextRegistry();


    [Fact]
    public void Generate_NullNestedObject_HandlesGracefully()
    {
        // Arrange
        var person = new Person { Name = "Test", Address = null };

        // Act
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        Assert.NotNull(context);
        var addressContext = context.GetContext("Address");
        Assert.Null(addressContext);
    }

    [Fact]
    public void GetValue_InvalidPath_ReturnsNull()
    {
        // Arrange
        var person = new Person { Name = "Test" };
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Act
        var value = context.GetValue("Invalid/Path/Here");

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Generate_EmptyList_CreatesEmptyContext()
    {
        // Arrange
        var company = new Company
        {
            Name = "Empty Corp",
            Employees = new List<Person>()
        };

        // Act
        var context = company.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        var employeesContext = context.GetContext("Employees");
        Assert.NotNull(employeesContext);

        var collectionContext = employeesContext as LiveCollectionContext;
        Assert.Equal(0, collectionContext.Count);
    }

    [Fact]
    public void Generate_EmptyDictionary_CreatesEmptyContext()
    {
        // Arrange
        var dict = new Dictionary<string, int>();
        var schema = dict.GetType().ToSchema();

        // Act
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, dict);

        // Assert
        Assert.NotNull(context);
        var dictContext = context as LiveDictionaryContext;
        Assert.NotNull(dictContext);
    }
}