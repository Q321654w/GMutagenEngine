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

public class DynamicCollectionBehavior(ISyncMediator mediator)
    : IInOutMiddleware<ReflectionContext, ISchema<string, string>>
{
    public ISchema<string, string> Invoke(ReflectionContext context, Func<ReflectionContext, ISchema<string, string>> next)
    {
        if (context.TargetInstance is not ICollection collection ||
            context.MemberInfo == null ||
            context.MemberInfo.GetCustomAttribute<DynamicObjectAttribute>() == null)
            return next(context);

        var type = new Type<string>(context.DeclaredType.FullName);
        var schema = new Schema<string, string>(type, new Dictionary<string, IMember<string, string>>());
        var index = 0;

        foreach (var value in collection)
        {
            var valueType = value?.GetType();
            var memberContext = new ReflectionContext(valueType, valueType, value, null, context);
            var child = mediator.Send<ReflectionContext, ISchema<string, string>>(memberContext);
            
            if (child == null)
                throw new NullReferenceException();
            
            var member = new Member<string, string>(index.ToString(), child);
            schema.Members.Add(index.ToString(), member);
            index++;
        }

        return schema;
    }
}