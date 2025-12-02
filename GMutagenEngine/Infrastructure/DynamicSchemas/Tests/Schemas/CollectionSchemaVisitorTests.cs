using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class CollectionSchemaVisitorTests
{
    [Fact]
    public void Visit_ArrayType_ExtractsElementType()
    {
        var visitor = new CollectionSchemaVisitor();
        var extractor = SchemeExtractorPresets.CreateDefault();
        var settings = new SchemaExtractorSettings(extractor);

        var schema = visitor.Visit(typeof(int[]), settings, 0);

        Assert.NotNull(schema);
        Assert.Equal(schema.Members.Count, 8);
        Assert.Equal(typeof(int), schema.Members[SchemaConstants.ELEMENT_KEY].Type);
    }

    [Fact]
    public void Visit_GenericList_ExtractsElementType()
    {
        var visitor = new CollectionSchemaVisitor();
        var extractor = SchemeExtractorPresets.CreateDefault();
        var settings = new SchemaExtractorSettings(extractor);

        var schema = visitor.Visit(typeof(List<string>), settings, 0);

        Assert.NotNull(schema);
        Assert.Equal(typeof(string), schema.Members[SchemaConstants.ELEMENT_KEY].Type);
    }
}