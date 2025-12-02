namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class RecursiveModel
{
    public string Name { get; set; }
    public RecursiveModel Child { get; set; }
}