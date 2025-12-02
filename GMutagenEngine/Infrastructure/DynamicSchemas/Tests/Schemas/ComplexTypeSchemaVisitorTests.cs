using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class ComplexTypeSchemaVisitorTests
{
    [Fact]
    public void Visit_AtMaxDepth_ReturnsEmptySchema()
    {
        var visitor = new ComplexTypeSchemaVisitor();
        var extractor = SchemeExtractorPresets.CreateDefault();
        var settings = new SchemaExtractorSettings(extractor)
        {
            MaxDepth = 0
        };

        var schema = visitor.Visit(typeof(SimpleModel), settings, 0);

        Assert.NotNull(schema);
        Assert.NotEmpty(schema.Members);
        Assert.True(schema.Members.All(m => m.Value.Schema == null));
    }

    [Fact]
    public void Visit_ExcludedType_ReturnsNull()
    {
        var visitor = new ComplexTypeSchemaVisitor();
        var extractor = SchemeExtractorPresets.CreateDefault();
        var settings = new SchemaExtractorSettings(extractor);
        settings.ExcludedTypes.Add(typeof(SimpleModel));

        var schema = visitor.Visit(typeof(SimpleModel), settings, 0);

        Assert.Null(schema);
    }
}