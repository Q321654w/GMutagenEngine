using System.Reflection;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces
{
    public static class SchemaMemberInfoExtensions
    {
        public static object? GetMemberValue(this ISchemaMemberInfo memberInfo, object obj)
        {
            if (memberInfo.Getter != null)
                return memberInfo.Getter(obj);
        
            if (memberInfo.Info is FieldInfo field)
                return field.GetValue(obj);
        
            if (memberInfo.Info is PropertyInfo property && property.CanRead)
                return property.GetValue(obj);

            return null;
        }
    }
}