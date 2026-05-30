using GMutagenEngine.Values.Factories.Default.Interfaces;
using GMutagenEngine.Values.Interfaces;

namespace GMutagenEngine.Values.Factories.Default.Api;

public static class ValueFactoryExtensions
{
    public static IValue Create(this IDefaultValueFactory defaultValueFactory, Type type)
    {
        var defaultValue = ExtendedActivator.CreateInstance(type);
        return defaultValueFactory.Create(defaultValue, type);
    }

    public static IValue<T> Create<T>(this IDefaultValueFactory defaultValueFactory)
        => (IValue<T>)defaultValueFactory.Create(typeof(T));

    public static IValue<T> Create<T>(this IDefaultValueFactory defaultValueFactory, object? val)
    {
        var value = defaultValueFactory.Create(val, typeof(T));
        return (IValue<T>)value;
    }

    public static IValue<T> Create<T>(this IDefaultValueFactory defaultValueFactory, T val)
        => defaultValueFactory.Create<T>((object?)val);
    
    public static IValue Create(this IDefaultValueFactory defaultValueFactory, object val)
        => defaultValueFactory.Create(val, val.GetType());
}