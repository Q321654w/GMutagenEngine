using GMutagenEngine.Utils.Clr;
using System.Collections;

namespace GMutagenEngine.Utils
{
    public static class FrameworkStandardTypes
    {
        public static List<Type> Primitive { get; } =
            StandardTypes.ValueTypes
                .Concat(StandardTypes.String)
                .Concat(StandardTypes.Character)
                .Distinct()
                .ToList();
        public static List<Type> Dictionaries { get; } =
            StandardTypes.DictionaryOnly;
        
        public static List<Type> Collections { get; } =
            StandardTypes.CollectionInterfaces
                .Concat(StandardTypes.CollectionConcrete)
                .Concat(new List<Type> { typeof(Array), typeof(IEnumerable) })
                .Distinct()
                .ToList();
    }

    public static class FrameworkActivator
    {
        public static object? CreateInstance(Type type)
        {
            if (type == typeof(string))
                return string.Empty;

            if (type.IsArray)
            {
                return Array.CreateInstance(type.GetElementType()!, 0);
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return null;
            }

            try
            {
                return Activator.CreateInstance(type);
            }
            catch
            {
                return null;
            }
        }
    }
}
