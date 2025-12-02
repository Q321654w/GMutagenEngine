using GMutagenEngine.Concept.Sync.Values.Realizations.Default;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts.Generation;

public class SnapshotContextGenerationTests
{
    private readonly ISchemaRegistry _schemaRegistry = new SchemaRegistry();

    [Fact]
    public void Generate_SimpleObject_CreatesSnapshot()
    {
        // Arrange
        var person = new Person { Name = "John", Age = 30 };

        // Act
        var context = person.ToSnapshotContext(_schemaRegistry);

        // Assert
        Assert.NotNull(context);
        Assert.IsType<ContextSnapshot>(context);
    }

    [Fact]
    public void Generate_PrimitiveValues_CapturesCorrectly()
    {
        // Arrange
        var container = new PrimitiveContainer
        {
            IntValue = 42,
            StringValue = "test",
            BoolValue = true
        };

        // Act
        var context = container.ToSnapshotContext(_schemaRegistry);

        // Assert
        var intValue = context.GetValue("IntValue");
        Assert.Equal(42, intValue.Value);

        var stringValue = context.GetValue("StringValue");
        Assert.Equal("test", stringValue.Value);
    }

    [Fact]
    public void Generate_NestedObject_CapturesHierarchy()
    {
        // Arrange
        var person = new Person
        {
            Name = "Jane",
            Address = new Address { City = "Boston", ZipCode = 02101 }
        };

        // Act
        var context = person.ToSnapshotContext(_schemaRegistry);

        // Assert
        var cityValue = context.GetValue("Address/City");
        Assert.Equal("Boston", cityValue.Value);
    }

    [Fact]
    public void Generate_ListSnapshot_CapturesAllItems()
    {
        // Arrange
        var company = new Company
        {
            Name = "Corp",
            Employees = new List<Person>
            {
                new Person { Name = "Alice", Age = 28 },
                new Person { Name = "Bob", Age = 32 }
            }
        };

        // Act
        var context = company.ToSnapshotContext(_schemaRegistry);

        // Assert
        var employeesContext = context.GetContext("Employees");
        Assert.NotNull(employeesContext);

        var firstEmployee = employeesContext.GetContext("0");
        Assert.NotNull(firstEmployee);
    }

    [Fact]
    public void Merge_TwoSnapshots_MergesCorrectly()
    {
        var snapshot1 = new ContextSnapshot();
        snapshot1.SetValue("key1", new IntValue(10));

        var snapshot2 = new ContextSnapshot();
        snapshot2.SetValue("key2", new IntValue(20));

        snapshot1.Merge(snapshot2);

        Assert.Equal(10, snapshot1.GetValue("key1").Value);
        Assert.Equal(20, snapshot1.GetValue("key2").Value);
    }

    [Fact]
    public void Generate_FromType_CreatesEmptySnapshot()
    {
        var context = typeof(Person).ToSnapshotContext(_schemaRegistry);

        Assert.NotNull(context);
        var nameValue = context.GetValue("Name");
        Assert.NotNull(nameValue);
    }
}