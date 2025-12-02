using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class SchemaExtractorTests
{
    private SchemaExtractor CreateExtractor()
    {
        return SchemeExtractorPresets.CreateDefault();
    }

    [Fact]
    public void Extract_PrimitiveType_ReturnsSchemaWithTargetType()
    {
        var extractor = CreateExtractor();

        var schema = extractor.Extract(typeof(int));

        Assert.NotNull(schema);
        Assert.Equal(typeof(int), schema.TargetType);
        Assert.Empty(schema.Members);
    }

    [Theory]
    [InlineData(typeof(string))]
    [InlineData(typeof(int))]
    [InlineData(typeof(long))]
    [InlineData(typeof(decimal))]
    [InlineData(typeof(bool))]
    [InlineData(typeof(DateTime))]
    [InlineData(typeof(Guid))]
    public void Extract_AllPrimitiveTypes_ReturnsValidSchema(Type primitiveType)
    {
        var extractor = CreateExtractor();

        var schema = extractor.Extract(primitiveType);

        Assert.NotNull(schema);
        Assert.Equal(primitiveType, schema.TargetType);
    }

    [Fact]
    public void Extract_SimpleClass_ExtractsPropertiesAndFields()
    {
        var extractor = CreateExtractor();

        var schema = extractor.Extract(typeof(SimpleModel));

        Assert.NotNull(schema);
        Assert.Equal(typeof(SimpleModel), schema.TargetType);
        Assert.Equal(2, schema.Members.Count);
        Assert.True(schema.Members.ContainsKey("Id"));
        Assert.True(schema.Members.ContainsKey("Name"));
    }

    [Fact]
    public void Extract_ComplexType_ExtractsNestedSchemas()
    {
        var extractor = CreateExtractor();

        var schema = extractor.Extract(typeof(ComplexModel));

        Assert.NotNull(schema);
        Assert.Equal(3, schema.Members.Count);
            
        var nestedMember = schema.Members["Nested"];
        Assert.NotNull(nestedMember.Schema);
        Assert.Equal(typeof(SimpleModel), nestedMember.Schema.TargetType);
    }

    [Fact]
    public void Extract_ListType_ExtractsElementSchema()
    {
        var extractor = CreateExtractor();

        var schema = extractor.Extract(typeof(List<string>));

        Assert.NotNull(schema);
        Assert.Equal(schema.Members.Count, 4);
        Assert.True(schema.Members.ContainsKey(SchemaConstants.ELEMENT_KEY));
            
        var elementMember = schema.Members[SchemaConstants.ELEMENT_KEY];
        Assert.Equal(typeof(string), elementMember.Type);
    }
        
    [Fact]
    public void Extract_IEnumerableCollectionTypes_ExtractsElementSchema()
    {
        var type = typeof(IEnumerable<int>);
        var extractor = CreateExtractor();

        var schema = extractor.Extract(type);

        Assert.NotNull(schema);
        Assert.Equal(schema.Members.Count, 1);
        Assert.True(schema.Members.ContainsKey(SchemaConstants.ELEMENT_KEY));
    }
        
    [Fact]
    public void Extract_ICollectionTypes_ExtractsElementSchema()
    {
        var type = typeof(ICollection<int>);
        var extractor = CreateExtractor();

        var schema = extractor.Extract(type);

        Assert.NotNull(schema);
        Assert.Equal(schema.Members.Count, 3);
        Assert.True(schema.Members.ContainsKey(SchemaConstants.ELEMENT_KEY));
    }
        
        
    [Fact]
    public void Extract_ArraCollectionTypes_ExtractsElementSchema()
    {
        var type = typeof(int[]);
        var extractor = CreateExtractor();

        var schema = extractor.Extract(type);

        Assert.NotNull(schema);
        Assert.Equal(schema.Members.Count, 8);
        Assert.True(schema.Members.ContainsKey(SchemaConstants.ELEMENT_KEY));
    }

    [Fact]
    public void Extract_DictionaryType_ExtractsKeyValuePairSchema()
    {
        var extractor = CreateExtractor();

        var schema = extractor.Extract(typeof(Dictionary<string, int>));

        Assert.NotNull(schema);
        Assert.Single(schema.Members);
            
        var kvpMember = schema.Members[SchemaConstants.ELEMENT_KEY];
        Assert.NotNull(kvpMember.Schema);
        Assert.Equal(2, kvpMember.Schema.Members.Count);
        Assert.True(kvpMember.Schema.Members.ContainsKey(SchemaConstants.KEY_PROPERTY));
        Assert.True(kvpMember.Schema.Members.ContainsKey(SchemaConstants.VALUE_PROPERTY));
    }

    [Fact]
    public void Extract_WithMaxDepth_StopsAtMaxDepth()
    {
        var extractor = CreateExtractor();
        var settings = new SchemaExtractorSettings(extractor)
        {
            MaxDepth = 1
        };

        var schema = extractor.Extract(typeof(ComplexModel), settings);

        Assert.NotNull(schema);
        var nestedMember = schema.Members["Nested"];
        Assert.Null(nestedMember.Schema);
    }

    [Fact]
    public void Extract_RecursiveType_HandlesRecursionWithMaxDepth()
    {
        var extractor = CreateExtractor();
        var settings = new SchemaExtractorSettings(extractor)
        {
            MaxDepth = 3
        };

        var schema = extractor.Extract(typeof(RecursiveModel), settings);

        Assert.NotNull(schema);
        Assert.NotNull(schema.Members["Child"].Schema);
    }

    [Fact]
    public void Extract_WithExcludedTypes_SkipsExcludedTypes()
    {
        var extractor = CreateExtractor();
        var settings = new SchemaExtractorSettings(extractor);
        settings.ExcludedTypes.Add(typeof(string));

        var schema = extractor.Extract(typeof(SimpleModel), settings);

        Assert.NotNull(schema);
        Assert.Null(schema.Members["Name"].Schema);
    }

    [Fact]
    public void Extract_WithExcludedTypePredicate_SkipsMatchingTypes()
    {
        var extractor = CreateExtractor();
        var settings = new SchemaExtractorSettings(extractor)
        {
            ExcludedTypePredicate = t => t == typeof(string)
        };

        var schema = extractor.Extract(typeof(SimpleModel), settings);

        Assert.NotNull(schema);
        Assert.Null(schema.Members["Name"].Schema);
    }

    [Fact]
    public void Extract_WithCustomBindingFlags_RespectsFlags()
    {
        var extractor = CreateExtractor();
        var settings = new SchemaExtractorSettings(extractor)
        {
            FieldsBindingFlags = BindingFlags.Public | BindingFlags.Instance
        };

        var schema = extractor.Extract(typeof(ModelWithFields), settings);

        Assert.NotNull(schema);
        Assert.True(schema.Members.ContainsKey("PublicField"));
        Assert.False(schema.Members.ContainsKey("PrivateField"));
    }

    [Fact]
    public void Extract_UnsupportedType_ThrowsException()
    {
        var storage = new SchemaVisitorStorage();
        var extractor = new SchemaExtractor(storage);

        var ex = Assert.Throws<Exception>(() => extractor.Extract(typeof(int)));
        Assert.Contains("Could not find any visitor", ex.Message);
    }

    [Fact]
    public void Extract_NestedCollection_ExtractsCorrectly()
    {
        var extractor = CreateExtractor();

        var schema = extractor.Extract(typeof(List<List<int>>));

        Assert.NotNull(schema);
        var elementMember = schema.Members[SchemaConstants.ELEMENT_KEY];
        Assert.NotNull(elementMember.Schema);
        Assert.Equal(typeof(List<int>), elementMember.Type);
    }
}