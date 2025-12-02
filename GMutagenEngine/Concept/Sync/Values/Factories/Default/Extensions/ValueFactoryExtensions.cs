using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Concept.Sync.Values.Factories.Default.Extensions
{
    public static class ValueFactoryExtensions
    {
        public static IValue Create(this IDefaultValueFactory defaultValueFactory, Type type)
        {
            var defaultValue = FrameworkActivator.CreateInstance(type);
            return defaultValueFactory.Create(defaultValue, type);
        }

        public static IValue<T> Create<T>(this IDefaultValueFactory defaultValueFactory, object val)
        {
            var value = defaultValueFactory.Create(val, typeof(T));
            return (IValue<T>)value;
        }

        public static IValue<T> Create<T>(this IDefaultValueFactory defaultValueFactory)
        {
            var value = defaultValueFactory.Create(default, typeof(T));
            return (IValue<T>)value;
        }
    }
}