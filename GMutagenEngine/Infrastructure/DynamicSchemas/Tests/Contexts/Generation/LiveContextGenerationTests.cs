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

public class LiveContextGenerationTests
{
    private readonly ISchemaRegistry _schemaRegistry = new SchemaRegistry();
    private readonly IContextRegistry _contextRegistry = new ContextRegistry();

    [Fact]
    public void Generate_SimpleObject_CreatesLiveContext()
    {
        // Arrange
        var person = new Person
        {
            Name = "John Doe",
            Age = 30
        };

        // Act
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        Assert.NotNull(context);
        Assert.IsType<LiveContext>(context);

        var liveContext = context as LiveContext;
        Assert.Equal(person, liveContext.Instance);
    }

    [Fact]
    public void Generate_PrimitiveProperties_ReturnsCorrectValues()
    {
        // Arrange
        var container = new PrimitiveContainer
        {
            IntValue = 42,
            StringValue = "test",
            BoolValue = true,
            DoubleValue = 3.14
        };

        // Act
        var context = container.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        var intValue = context.GetValue("IntValue");
        Assert.NotNull(intValue);
        Assert.Equal(42, intValue.Value);

        var stringValue = context.GetValue("StringValue");
        Assert.NotNull(stringValue);
        Assert.Equal("test", stringValue.Value);

        var boolValue = context.GetValue("BoolValue");
        Assert.NotNull(boolValue);
        Assert.Equal(true, boolValue.Value);

        var doubleValue = context.GetValue("DoubleValue");
        Assert.NotNull(doubleValue);
        Assert.Equal(3.14, doubleValue.Value);
    }

    [Fact]
    public void Generate_NestedObject_ReturnsNestedContext()
    {
        // Arrange
        var person = new Person
        {
            Name = "Jane",
            Age = 25,
            Address = new Address
            {
                Street = "123 Main St",
                City = "Springfield",
                ZipCode = 12345
            }
        };

        // Act
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        var addressContext = context.GetContext("Address");
        Assert.NotNull(addressContext);

        var streetValue = addressContext.GetValue("Street");
        Assert.NotNull(streetValue);
        Assert.Equal("123 Main St", streetValue.Value);
    }

    [Fact]
    public void Generate_NestedPath_ReturnsCorrectValue()
    {
        // Arrange
        var person = new Person
        {
            Name = "Bob",
            Age = 35,
            Address = new Address
            {
                Street = "456 Oak Ave",
                City = "Metropolis",
                ZipCode = 54321
            }
        };

        // Act
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        var cityValue = context.GetValue("Address/City");
        Assert.NotNull(cityValue);
        Assert.Equal("Metropolis", cityValue.Value);
    }


    [Fact]
    public void Generate_ListOfPrimitives_CreatesCollectionContext()
    {
        // Arrange
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        var schema = numbers.GetType().ToSchema();

        // Act
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, numbers);

        // Assert
        Assert.NotNull(context);
        Assert.IsType<LiveCollectionContext>(context);

        var collectionContext = context as LiveCollectionContext;
        Assert.Equal(5, collectionContext.Count);
    }

    [Fact]
    public void Generate_ListOfObjects_ReturnsCorrectItems()
    {
        // Arrange
        var company = new Company
        {
            Name = "Tech Corp",
            Employees = new List<Person>
            {
                new Person { Name = "Alice", Age = 28 },
                new Person { Name = "Bob", Age = 32 }
            }
        };

        // Act
        var context = company.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        var employeesContext = context.GetContext("Employees");
        Assert.NotNull(employeesContext);

        var firstEmployee = employeesContext.GetContext("0");
        Assert.NotNull(firstEmployee);

        var firstName = firstEmployee.GetValue("Name");
        Assert.Equal("Alice", firstName.Value);
    }

    [Fact]
    public void Generate_DictionaryWithPrimitives_CreatesDictionaryContext()
    {
        // Arrange
        var dict = new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };
        var schema = dict.GetType().ToSchema();

        // Act
        var generator = LiveContextGeneratorPresets.CreateDefault(
            _schemaRegistry, _contextRegistry,
            new ReflectionValueFactory(), new DefaultValueFactory());
        var context = generator.Generate(schema, null, null, dict);

        // Assert
        Assert.NotNull(context);
        Assert.IsType<LiveDictionaryContext>(context);

        var dictContext = context as LiveDictionaryContext;
        Assert.True(dictContext.ContainsKey("one"));
    }

    [Fact]
    public void ExistValue_ExistingPath_ReturnsTrue()
    {
        // Arrange
        var person = new Person { Name = "Test", Age = 20 };
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Act & Assert
        Assert.True(context.ExistValue("Name"));
        Assert.True(context.ExistValue("Age"));
    }

    [Fact]
    public void ExistValue_NonExistingPath_ReturnsFalse()
    {
        // Arrange
        var person = new Person { Name = "Test", Age = 20 };
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Act & Assert
        Assert.False(context.ExistValue("NonExistent"));
    }

    [Fact]
    public void ExistContext_ExistingNestedObject_ReturnsTrue()
    {
        // Arrange
        var person = new Person
        {
            Name = "Test",
            Address = new Address { City = "NYC" }
        };
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Act & Assert
        Assert.True(context.ExistContext("Address"));
    }

    [Fact]
    public void GetAllValues_ReturnsAllPrimitiveValues()
    {
        // Arrange
        var person = new Person
        {
            Name = "John",
            Age = 30,
            Address = new Address
            {
                Street = "Main St",
                City = "NYC",
                ZipCode = 10001
            }
        };
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Act
        var allValues = context.GetAllValues().ToList();

        // Assert
        Assert.NotEmpty(allValues);
        Assert.Contains(allValues, v => v.Value.Equals("John"));
        Assert.Contains(allValues, v => v.Value.Equals(30));
        Assert.Contains(allValues, v => v.Value.Equals("Main St"));
    }

    [Fact]
    public void GetAllContexts_ReturnsAllNestedContexts()
    {
        // Arrange
        var person = new Person
        {
            Name = "John",
            Address = new Address { City = "NYC" }
        };
        var context = person.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Act
        var allContexts = context.GetAllContexts().ToList();

        // Assert
        Assert.NotEmpty(allContexts);
        Assert.Contains(allContexts, c => c is LiveContext);
    }
}