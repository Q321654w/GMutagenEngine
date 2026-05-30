using GMutagenEngine.Mediators;
using GMutagenEngine.Schemas.Interfaces;

namespace GMutagenEngine.Schemas.Extraction;

public class SchemaExtractor(ISyncMediator mediator)
    : ISchemaExtractor
{
    public ISchema<string, string>? Extract(Type declaredType, object? instance = null)
    {
        var actualType = instance?.GetType() ?? declaredType;
        var context = new ReflectionContext(declaredType, actualType, instance, null);
        var result = mediator.Send<ReflectionContext, ISchema<string, string>>(context);
        return result;
    }
}