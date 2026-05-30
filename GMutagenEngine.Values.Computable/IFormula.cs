/*using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public interface IFormula<out TValue> : IFormulaMark {
        TValue Evaluate(IContext ctx);
    }
}*/
namespace GMutagenEngine.Values.Computable;

public interface IFormulaMark : GMutagenEngine.MetaData.Runtime.Marks.ISelfMark<IFormulaMark>
{
}