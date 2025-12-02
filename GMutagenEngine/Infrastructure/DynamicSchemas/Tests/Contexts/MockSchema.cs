using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts;

public class MockSchema : ISchema
{
    public Type? TargetType { get; set; }
    public Dictionary<string, ISchemaMemberInfo> Members { get; } = new();
}