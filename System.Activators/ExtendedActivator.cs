namespace System;

public static class ExtendedActivator
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