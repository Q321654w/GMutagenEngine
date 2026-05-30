using System.Reflection;
using GMutagenEngine.Values.Factories.Reflective.Interfaces;
using GMutagenEngine.Values.Interfaces;
using GMutagenEngine.Values.Reflective.Realizations;

namespace GMutagenEngine.Values.Factories.Reflective.Realizations;

public class ReflectionValueFactory : IReflectionValueFactory
{
    public IValue Create(object instance, MemberInfo member)
    {
        if (member is FieldInfo field)
        {
            var valueType = typeof(FieldValue<>).MakeGenericType(field.FieldType);
            return (IValue)Activator.CreateInstance(valueType, instance, field)!;
        }

        if (member is PropertyInfo property)
        {
            var valueType = typeof(PropertyValue<>).MakeGenericType(property.PropertyType);
            return (IValue)Activator.CreateInstance(valueType, instance, property)!;
        }

        throw new InvalidOperationException($"Unsupported member type: {member.MemberType}");
    }
}