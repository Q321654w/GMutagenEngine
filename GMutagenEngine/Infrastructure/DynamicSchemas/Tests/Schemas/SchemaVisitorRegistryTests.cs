using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Realizations;
using GMutagenEngine.Utils;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class SchemaVisitorRegistryTests
{
    [Fact]
    public void Add_SingleVisitor_StoresForAllSupportedTypes()
    {
        var storage = new SchemaVisitorStorage();
        var visitor = new PrimitiveSchemaVisitor();

        storage.Add(visitor);

        foreach (var type in FrameworkStandardTypes.Primitive)
        {
            Assert.True(storage.TryGet(type, out var retrieved));
            Assert.Same(visitor, retrieved);
        }
    }

    [Fact]
    public void TryGet_ExistingType_ReturnsTrue()
    {
        var storage = new SchemaVisitorStorage();
        storage.AddDefaults();

        var result = storage.TryGet(typeof(int), out var visitor);

        Assert.True(result);
        Assert.NotNull(visitor);
        Assert.IsType<PrimitiveSchemaVisitor>(visitor);
    }

    [Fact]
    public void TryGet_NonExistingType_ReturnsFalse()
    {
        var storage = new SchemaVisitorStorage();

        var result = storage.TryGet(typeof(int), out var visitor);

        Assert.False(result);
        Assert.Null(visitor);
    }

    [Fact]
    public void GetAll_ReturnsAllVisitors()
    {
        var storage = new SchemaVisitorStorage();
        storage.AddDefaults();

        var visitors = storage.GetAll().ToList();

        Assert.NotEmpty(visitors);
    }
}