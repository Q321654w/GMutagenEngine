using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Members.Interfaces;
using GMutagenEngine.Schemas.Realizations;
using GMutagenEngine.Schemas.Types.Realizations;

namespace GMutagenEngine.Schemas.Extraction;

public class DictionarySchemaHandler : ISyncFuncHandler<ReflectionContext, ISchema<string, string>>
{
    public ISchema<string, string> Handle(ReflectionContext data)
    {
        var schemaType = new Type<string>(data.DeclaredType.FullName);
        return new Schema<string, string>(schemaType, new Dictionary<string, IMember<string, string>>());
    }
}