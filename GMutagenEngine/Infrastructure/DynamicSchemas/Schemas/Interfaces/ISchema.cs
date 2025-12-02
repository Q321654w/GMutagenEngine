using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces.Marks;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces
{
    public interface ISchema : ISchemaMark
    {
        Type? TargetType { get; set; }
        Dictionary<string, ISchemaMemberInfo> Members { get; }
    }
}