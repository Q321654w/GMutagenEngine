using GMutagenEngine.Schemas.Extraction;
using GMutagenEngine.Schemas.Extraction.Api;

namespace GMutagenEngine.UnitTests.SchemaExtractors;

public class SchemaExtractorTests
{
    private const int AGE_FIELD_VALUE = 11;
    private const int AGE_PROPERTY_VALUE = 10;
    private const string NAME_FIELD_VALUE = "asd1";
    private const string NAME_PROPERTY_VALUE = "asd";

    private const string KEY1 = "key";
    private const int KEY1_VALUE = 1;

    private const string KEY2 = "key2";
    private const string KEY2_VALUE = "value";

    private const int BASIC_MEMBERS_COUNT = 4;
    private const int EXTENDED_MEMBERS_COUNT = 6;
    private const int EMPTY_MEMBERS_COUNT = 0;
    private const int DYNAMIC_MEMBERS_COUNT = 2;

    private static readonly string INT_TYPE = typeof(int).FullName!;
    private static readonly string STRING_TYPE = typeof(string).FullName!;
    private static readonly string DICTIONARY_TYPE = typeof(Dictionary<string, object>).FullName!;
    private static readonly string LIST_TYPE = typeof(List<object>).FullName!;

    public class Dummy1
    {
        public int AgeProperty { get; set; } = AGE_PROPERTY_VALUE;
        public string NameProperty { get; set; } = NAME_PROPERTY_VALUE;

        public int AgeField = AGE_FIELD_VALUE;
        public string NameField = NAME_FIELD_VALUE;
    }

    public class Dummy2
    {
        public int AgeProperty { get; set; } = AGE_PROPERTY_VALUE;
        public string NameProperty { get; set; } = NAME_PROPERTY_VALUE;

        public Dictionary<string, object> DynamicDictionaryProperty { get; set; } = new()
        {
            { KEY1, KEY1_VALUE }
        };

        public int AgeField = AGE_FIELD_VALUE;
        public string NameField = NAME_FIELD_VALUE;

        public Dictionary<string, object> DynamicDictionaryField = new()
        {
            { KEY1, KEY1_VALUE }
        };
    }

    public class Dummy3
    {
        public int AgeProperty { get; set; } = AGE_PROPERTY_VALUE;
        public string NameProperty { get; set; } = NAME_PROPERTY_VALUE;

        public List<object> DynamicCollectionProperty { get; set; } = new()
        {
            KEY1_VALUE
        };

        public int AgeField = AGE_FIELD_VALUE;
        public string NameField = NAME_FIELD_VALUE;

        public List<object> DynamicCollectionField = new()
        {
            KEY1_VALUE
        };
    }

    public class Dummy4
    {
        public int AgeProperty { get; set; } = AGE_PROPERTY_VALUE;
        public string NameProperty { get; set; } = NAME_PROPERTY_VALUE;

        [DynamicObject]
        public Dictionary<string, object> DynamicDictionaryProperty { get; set; } = new()
        {
            { KEY1, KEY1_VALUE },
            { KEY2, KEY2_VALUE }
        };

        public int AgeField = AGE_FIELD_VALUE;
        public string NameField = NAME_FIELD_VALUE;

        [DynamicObject] public Dictionary<string, object> DynamicDictionaryField = new()
        {
            { KEY1, KEY1_VALUE },
            { KEY2, KEY2_VALUE }
        };
    }

    public class Dummy5
    {
        public int AgeProperty { get; set; } = AGE_PROPERTY_VALUE;
        public string NameProperty { get; set; } = NAME_PROPERTY_VALUE;

        [DynamicObject]
        public List<object> DynamicCollectionProperty { get; set; } = new()
        {
            KEY1_VALUE,
            KEY2_VALUE
        };

        public int AgeField = AGE_FIELD_VALUE;
        public string NameField = NAME_FIELD_VALUE;

        [DynamicObject] public List<object> DynamicCollectionField = new()
        {
            KEY1_VALUE,
            KEY2_VALUE
        };
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestBasicExtraction()
    {
        var schemaExtractor = SchemaExtractorInitializationExtensions.CreateDefault();
        var dummy1 = new Dummy1();
        var schema = schemaExtractor.Extract(dummy1);

        Assert.That(schema.Members.Count, Is.EqualTo(BASIC_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy1.AgeField)].Id, Is.EqualTo(nameof(Dummy1.AgeField)));
        Assert.That(schema.Members[nameof(Dummy1.AgeField)].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy1.AgeField)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy1.NameField)].Id, Is.EqualTo(nameof(Dummy1.NameField)));
        Assert.That(schema.Members[nameof(Dummy1.NameField)].Schema.Type.Id, Is.EqualTo(STRING_TYPE));
        Assert.That(schema.Members[nameof(Dummy1.NameField)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy1.AgeProperty)].Id, Is.EqualTo(nameof(Dummy1.AgeProperty)));
        Assert.That(schema.Members[nameof(Dummy1.AgeProperty)].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy1.AgeProperty)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy1.NameProperty)].Id, Is.EqualTo(nameof(Dummy1.NameProperty)));
        Assert.That(schema.Members[nameof(Dummy1.NameProperty)].Schema.Type.Id, Is.EqualTo(STRING_TYPE));
        Assert.That(schema.Members[nameof(Dummy1.NameProperty)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));
    }

    [Test]
    public void TestExtractionWithDictionary()
    {
        var schemaExtractor = SchemaExtractorInitializationExtensions.CreateDefault();
        var dummy2 = new Dummy2();
        var schema = schemaExtractor.Extract(dummy2);

        Assert.That(schema.Members.Count, Is.EqualTo(EXTENDED_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy2.AgeField)].Id, Is.EqualTo(nameof(Dummy2.AgeField)));
        Assert.That(schema.Members[nameof(Dummy2.AgeField)].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy2.AgeField)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy2.NameField)].Id, Is.EqualTo(nameof(Dummy2.NameField)));
        Assert.That(schema.Members[nameof(Dummy2.NameField)].Schema.Type.Id, Is.EqualTo(STRING_TYPE));
        Assert.That(schema.Members[nameof(Dummy2.NameField)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy2.DynamicDictionaryField)].Id, Is.EqualTo(nameof(Dummy2.DynamicDictionaryField)));
        Assert.That(schema.Members[nameof(Dummy2.DynamicDictionaryField)].Schema.Type.Id, Is.EqualTo(DICTIONARY_TYPE));
        Assert.That(schema.Members[nameof(Dummy2.DynamicDictionaryField)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy2.AgeProperty)].Id, Is.EqualTo(nameof(Dummy2.AgeProperty)));
        Assert.That(schema.Members[nameof(Dummy2.AgeProperty)].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy2.AgeProperty)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy2.NameProperty)].Id, Is.EqualTo(nameof(Dummy2.NameProperty)));
        Assert.That(schema.Members[nameof(Dummy2.NameProperty)].Schema.Type.Id, Is.EqualTo(STRING_TYPE));
        Assert.That(schema.Members[nameof(Dummy2.NameProperty)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy2.DynamicDictionaryProperty)].Id, Is.EqualTo(nameof(Dummy2.DynamicDictionaryProperty)));
        Assert.That(schema.Members[nameof(Dummy2.DynamicDictionaryProperty)].Schema.Type.Id, Is.EqualTo(DICTIONARY_TYPE));
        Assert.That(schema.Members[nameof(Dummy2.DynamicDictionaryProperty)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));
    }

    [Test]
    public void TestExtractionWithCollection()
    {
        var schemaExtractor = SchemaExtractorInitializationExtensions.CreateDefault();
        var dummy3 = new Dummy3();
        var schema = schemaExtractor.Extract(dummy3);

        Assert.That(schema.Members.Count, Is.EqualTo(EXTENDED_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy3.AgeField)].Id, Is.EqualTo(nameof(Dummy3.AgeField)));
        Assert.That(schema.Members[nameof(Dummy3.AgeField)].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy3.AgeField)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy3.NameField)].Id, Is.EqualTo(nameof(Dummy3.NameField)));
        Assert.That(schema.Members[nameof(Dummy3.NameField)].Schema.Type.Id, Is.EqualTo(STRING_TYPE));
        Assert.That(schema.Members[nameof(Dummy3.NameField)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy3.DynamicCollectionField)].Id, Is.EqualTo(nameof(Dummy3.DynamicCollectionField)));
        Assert.That(schema.Members[nameof(Dummy3.DynamicCollectionField)].Schema.Type.Id, Is.EqualTo(LIST_TYPE));
        Assert.That(schema.Members[nameof(Dummy3.DynamicCollectionField)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy3.AgeProperty)].Id, Is.EqualTo(nameof(Dummy3.AgeProperty)));
        Assert.That(schema.Members[nameof(Dummy3.AgeProperty)].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy3.AgeProperty)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy3.NameProperty)].Id, Is.EqualTo(nameof(Dummy3.NameProperty)));
        Assert.That(schema.Members[nameof(Dummy3.NameProperty)].Schema.Type.Id, Is.EqualTo(STRING_TYPE));
        Assert.That(schema.Members[nameof(Dummy3.NameProperty)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy3.DynamicCollectionProperty)].Id, Is.EqualTo(nameof(Dummy3.DynamicCollectionProperty)));
        Assert.That(schema.Members[nameof(Dummy3.DynamicCollectionProperty)].Schema.Type.Id, Is.EqualTo(LIST_TYPE));
        Assert.That(schema.Members[nameof(Dummy3.DynamicCollectionProperty)].Schema.Members.Count, Is.EqualTo(EMPTY_MEMBERS_COUNT));
    }

    [Test]
    public void TestExtractionWithDynamicDictionary()
    {
        var schemaExtractor = SchemaExtractorInitializationExtensions.CreateDefault();
        var dummy4 = new Dummy4();
        var schema = schemaExtractor.Extract(dummy4);

        Assert.That(schema.Members.Count, Is.EqualTo(EXTENDED_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryProperty)].Schema.Members.Count, Is.EqualTo(DYNAMIC_MEMBERS_COUNT));
        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryProperty)].Schema.Members[KEY1].Id, Is.EqualTo(KEY1));
        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryProperty)].Schema.Members[KEY1].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryProperty)].Schema.Members[KEY2].Id, Is.EqualTo(KEY2));
        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryProperty)].Schema.Members[KEY2].Schema.Type.Id, Is.EqualTo(STRING_TYPE));

        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryField)].Schema.Members.Count, Is.EqualTo(DYNAMIC_MEMBERS_COUNT));
        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryField)].Schema.Members[KEY1].Id, Is.EqualTo(KEY1));
        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryField)].Schema.Members[KEY1].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryField)].Schema.Members[KEY2].Id, Is.EqualTo(KEY2));
        Assert.That(schema.Members[nameof(Dummy4.DynamicDictionaryField)].Schema.Members[KEY2].Schema.Type.Id, Is.EqualTo(STRING_TYPE));
    }

    [Test]
    public void TestExtractionWithDynamicCollection()
    {
        var schemaExtractor = SchemaExtractorInitializationExtensions.CreateDefault();
        var dummy5 = new Dummy5();
        var schema = schemaExtractor.Extract(dummy5);

        Assert.That(schema.Members.Count, Is.EqualTo(EXTENDED_MEMBERS_COUNT));

        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionProperty)].Schema.Members.Count, Is.EqualTo(DYNAMIC_MEMBERS_COUNT));
        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionProperty)].Schema.Members[0.ToString()].Id, Is.EqualTo(0.ToString()));
        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionProperty)].Schema.Members[0.ToString()].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionProperty)].Schema.Members[1.ToString()].Id, Is.EqualTo(1.ToString()));
        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionProperty)].Schema.Members[1.ToString()].Schema.Type.Id, Is.EqualTo(STRING_TYPE));

        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionField)].Schema.Members.Count, Is.EqualTo(DYNAMIC_MEMBERS_COUNT));
        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionField)].Schema.Members[0.ToString()].Id, Is.EqualTo(0.ToString()));
        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionField)].Schema.Members[0.ToString()].Schema.Type.Id, Is.EqualTo(INT_TYPE));
        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionField)].Schema.Members[1.ToString()].Id, Is.EqualTo(1.ToString()));
        Assert.That(schema.Members[nameof(Dummy5.DynamicCollectionField)].Schema.Members[1.ToString()].Schema.Type.Id, Is.EqualTo(STRING_TYPE));
    }
}