using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class SchemaIntegrationTests
{
    [Fact]
    public void CompleteWorkflow_ExtractAndNavigateSchema()
    {
        var extractor = SchemeExtractorPresets.CreateDefault();

        var schema = extractor.Extract(typeof(ComplexModel));

        Assert.NotNull(schema);
        Assert.Equal(typeof(ComplexModel), schema.TargetType);
            
        var tagsSchema = schema.Members["Tags"].Schema;
        Assert.Equal(typeof(List<string>), tagsSchema.TargetType);
            
        var metadataSchema = schema.Members["Metadata"].Schema;
        Assert.Equal(typeof(Dictionary<string, int>), metadataSchema.TargetType);
    }

    [Fact]
    public void RealWorldScenario_ExtractSchemaWithAllFeatures()
    {
        var extractor = SchemeExtractorPresets.CreateDefault();
        var settings = new SchemaExtractorSettings(extractor)
        {
            MaxDepth = 10,
            PropertiesBindingFlags = BindingFlags.Public | BindingFlags.Instance
        };
        settings.ExcludedTypes.Add(typeof(Guid));

        var schema = extractor.Extract(typeof(ComplexModel), settings);

        Assert.NotNull(schema);
        Assert.True(schema.Members.Count > 0);
    }
}