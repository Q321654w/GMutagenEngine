using System.Collections;
using System.Reflection;
using GMutagenEngine.Mediators;
using GMutagenEngine.Middlewares.Sync.Interfaces;
using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Members.Interfaces;
using GMutagenEngine.Schemas.Members.Realizations;
using GMutagenEngine.Schemas.Realizations;
using GMutagenEngine.Schemas.Types.Realizations;

namespace GMutagenEngine.Schemas.Extraction;

public class DynamicDictionaryBehavior(ISyncMediator mediator)
    : IInOutMiddleware<ReflectionContext, ISchema<string, string>>
{
    public ISchema<string, string> Invoke(ReflectionContext context, Func<ReflectionContext, ISchema<string, string>> next)
    {
        if (context.TargetInstance is not IDictionary dict ||
            context.MemberInfo == null ||
            context.MemberInfo.GetCustomAttribute<DynamicObjectAttribute>() == null)
            return next(context);
        
        var type = new Type<string>(context.DeclaredType.FullName);
        var schema = new Schema<string, string>(type, new Dictionary<string, IMember<string, string>>());

        foreach (var key in dict.Keys)
        {
            var value = dict[key];
            var valueType = value?.GetType();
            var memberContext = new ReflectionContext(valueType, valueType, value, null, context);
            var child = mediator.Send<ReflectionContext, ISchema<string, string>>(memberContext);
            schema.Members.Add(key.ToString(), new Member<string, string>(key.ToString(), child));
        }

        return schema;
    }
}