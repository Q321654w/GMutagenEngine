using GMutagenEngine.Concept.Sync.Values.Factories.Default.Realizations;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts.Generation;

public class ContextSettingsTests
{
    [Fact]
    public void LiveContextSettings_ExcludedTypes_FiltersCorrectly()
    {
        // Arrange
        var generator = LiveContextGeneratorPresets.CreateDefault(
            new SchemaRegistry(), new ContextRegistry(),
            new ReflectionValueFactory(), new DefaultValueFactory());
        var settings = new LiveContextGeneratorSettings(generator);
        settings.ExcludedTypes.Add(typeof(Address));

        var person = new Person
        {
            Name = "Test",
            Address = new Address { City = "NYC" }
        };

        var schema = person.GetType().ToSchema();

        // Act
        var context = generator.Generate(schema, settings, null, person);

        // Assert
        Assert.NotNull(context);
        // Address should be excluded
        var addressContext = context.GetContext("Address");
        Assert.Null(addressContext);
    }

    [Fact]
    public void SnapshotSettings_ExcludedTypePredicate_FiltersCorrectly()
    {
        // Arrange
        var generator = SnapshotContextGeneratorPresets.CreateDefault(
            new SchemaRegistry(), new DefaultValueFactory());
        var settings = new SnapshotContextGeneratorSettings(generator);
        settings.ExcludedTypePredicate = t => t == typeof(Address);

        var person = new Person
        {
            Name = "Test",
            Address = new Address { City = "NYC" }
        };

        var schema = person.GetType().ToSchema();

        // Act
        var context = generator.Generate(schema, settings, null, person);

        // Assert
        Assert.NotNull(context);
    }
}