using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extensions;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts.Generation;

public class SchemaExtractionTests
{
    [Fact]
    public void Extract_SimpleClass_ExtractsAllMembers()
    {
        // Arrange & Act
        var schema = typeof(Person).ToSchema();

        // Assert
        Assert.NotNull(schema);
        Assert.Equal(typeof(Person), schema.TargetType);
        Assert.True(schema.Members.ContainsKey("Name"));
        Assert.True(schema.Members.ContainsKey("Age"));
        Assert.True(schema.Members.ContainsKey("Address"));
    }

    [Fact]
    public void Extract_GenericList_ExtractsElementType()
    {
        // Arrange & Act
        var schema = typeof(List<int>).ToSchema();

        // Assert
        Assert.NotNull(schema);
        Assert.True(schema.Members.ContainsKey(SchemaConstants.ELEMENT_KEY));

        var elementMember = schema.Members[SchemaConstants.ELEMENT_KEY];
        Assert.Equal(typeof(int), elementMember.Schema.TargetType);
    }

    [Fact]
    public void Extract_Dictionary_ExtractsKeyValueTypes()
    {
        // Arrange & Act
        var schema = typeof(Dictionary<string, int>).ToSchema();

        // Assert
        Assert.NotNull(schema);
        Assert.True(schema.Members.ContainsKey(SchemaConstants.ELEMENT_KEY));

        var kvpSchema = schema.Members[SchemaConstants.ELEMENT_KEY].Schema;
        Assert.True(kvpSchema.Members.ContainsKey(SchemaConstants.KEY_PROPERTY));
        Assert.True(kvpSchema.Members.ContainsKey(SchemaConstants.VALUE_PROPERTY));
    }

    [Fact]
    public void Extract_PrimitiveType_CreatesPrimitiveSchema()
    {
        // Arrange & Act
        var schema = typeof(int).ToSchema();

        // Assert
        Assert.NotNull(schema);
        Assert.Equal(typeof(int), schema.TargetType);
    }
}