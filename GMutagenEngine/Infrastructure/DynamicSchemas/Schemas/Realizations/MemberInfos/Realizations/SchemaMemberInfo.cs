using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Realizations
{
    public class SchemaMemberInfo : ISchemaMemberInfo
    {
        public string? Name { get; set; }
        public Type? Type { get; set; }
        public MemberInfo? Info { get; set; }
        public MemberTypes MemberType { get; set; }
        public ISchema? Schema { get; set; }
        public Func<object, object> Getter { get; set; }
        public Action<object, object> Setter { get; set; }
    }
}