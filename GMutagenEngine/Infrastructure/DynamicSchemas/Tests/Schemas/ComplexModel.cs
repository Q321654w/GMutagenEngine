namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class ComplexModel
{
    public SimpleModel Nested { get; set; }
    public List<string> Tags { get; set; }
    public Dictionary<string, int> Metadata { get; set; }
}