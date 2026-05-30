using System.Reflection;
using System.Runtime.CompilerServices;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Mediators;
using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Members.Interfaces;
using GMutagenEngine.Schemas.Members.Realizations;
using GMutagenEngine.Schemas.Realizations;
using GMutagenEngine.Schemas.Types.Realizations;

namespace GMutagenEngine.Schemas.Extraction;

public sealed class ObjectSchemaHandler(ISyncMediator mediator) : ISyncFuncHandler<ReflectionContext, ISchema<string, string>>
{
    private static readonly BindingFlags MemberFlags =
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic;

    public ISchema<string, string> Handle(ReflectionContext context)
    {
        var schemaType = new Type<string>(context.DeclaredType.FullName);
        var schema = new Schema<string, string>(schemaType, new Dictionary<string, IMember<string, string>>());

        AddFields(schema, context);
        AddProperties(schema, context);

        return schema;
    }
    
    private void AddFields(Schema<string, string> schema, ReflectionContext context)
    {
        var fields = context.DeclaredType.GetFields(MemberFlags);

        foreach (var field in fields)
        {
            if (IsCompilerGenerated(field))
                continue;

            var value = GetFieldValue(field, context);
            var childSchema = BuildChildSchema(
                field.FieldType,
                value,
                field,
                context
            );

            schema.Members.Add(field.Name,
                new Member<string, string>(field.Name, childSchema)
            );
        }
    }

    private static object? GetFieldValue(FieldInfo field, ReflectionContext context)
    {
        return context.TargetInstance != null
            ? field.GetValue(context.TargetInstance)
            : null;
    }

    private void AddProperties(Schema<string, string> schema, ReflectionContext context)
    {
        var properties = context.DeclaredType.GetProperties(MemberFlags);

        foreach (var property in properties)
        {
            if (IsCompilerGenerated(property))
                continue;

            var value = GetPropertyValue(property, context);
            var childSchema = BuildChildSchema(
                property.PropertyType,
                value,
                property,
                context
            );

            schema.Members.Add(property.Name,
                new Member<string, string>(property.Name, childSchema)
            );
        }
    }

    private static object? GetPropertyValue(PropertyInfo property, ReflectionContext context)
    {
        return context.TargetInstance != null
            ? property.GetValue(context.TargetInstance)
            : null;
    }

    private ISchema<string, string> BuildChildSchema(Type declaredType,
        object? value,
        MemberInfo member, ReflectionContext context)
    {
        var runtimeType = value?.GetType();

        return mediator.Send<ReflectionContext, ISchema<string, string>>(
            new ReflectionContext(
                declaredType,
                runtimeType,
                value,
                member,
                context)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsCompilerGenerated(MemberInfo member)
    {
        return member.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
    }
}