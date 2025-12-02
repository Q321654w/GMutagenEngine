using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces.Marks;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces
{
    public interface ISchemaMemberInfo : ISchemaMemberInfoMark
    {
        string? Name { get; set; }
        Type? Type { get; set; }
        MemberInfo? Info { get; set; }
        MemberTypes MemberType { get; set; }
        ISchema? Schema { get; set; }

        Func<object, object>? Getter { get; set; }
        Action<object, object>? Setter { get; set; }
    }
}