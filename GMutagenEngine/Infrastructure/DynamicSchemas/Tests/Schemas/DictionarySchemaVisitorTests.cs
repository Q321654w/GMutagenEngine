using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class DictionarySchemaVisitorTests
{
    [Fact]
    public void Visit_DictionaryType_ExtractsKeyAndValueTypes()
    {
        var visitor = new DictionarySchemaVisitor();
        var extractor = SchemeExtractorPresets.CreateDefault();
        var settings = new SchemaExtractorSettings(extractor);

        var schema = visitor.Visit(typeof(Dictionary<string, int>), settings, 0);

        Assert.NotNull(schema);
        var kvpSchema = schema.Members[SchemaConstants.ELEMENT_KEY].Schema;
        Assert.Equal(typeof(string), kvpSchema.Members[SchemaConstants.KEY_PROPERTY].Type);
        Assert.Equal(typeof(int), kvpSchema.Members[SchemaConstants.VALUE_PROPERTY].Type);
    }

    [Fact]
    public void Visit_IDictionary_ExtractsKeyAndValueTypes()
    {
        var visitor = new DictionarySchemaVisitor();
        var extractor = SchemeExtractorPresets.CreateDefault();
        var settings = new SchemaExtractorSettings(extractor);

        var schema = visitor.Visit(typeof(IDictionary<int, string>), settings, 0);

        Assert.NotNull(schema);
        var kvpSchema = schema.Members[SchemaConstants.ELEMENT_KEY].Schema;
        Assert.Equal(typeof(int), kvpSchema.Members[SchemaConstants.KEY_PROPERTY].Type);
        Assert.Equal(typeof(string), kvpSchema.Members[SchemaConstants.VALUE_PROPERTY].Type);
    }
}