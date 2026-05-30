using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Schemas.Interfaces;

namespace GMutagenEngine.Schemas.Extraction;

public interface ISchemaExtractor : ISchemaExtractorMark {
    ISchema<string, string>? Extract(Type declaredType, object? instance = null);
}
public interface ISchemaExtractorMark : ISelfMark<ISchemaExtractorMark> {
}
