using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations
{
    public class Schema : ISchema
    {
        public Type? TargetType { get; set; }
        public Dictionary<string, ISchemaMemberInfo> Members { get; } = new();

        public void AddMember(ISchemaMemberInfo memberInfo)
        {
            Members[memberInfo.Name] = memberInfo;
        }
    }
}