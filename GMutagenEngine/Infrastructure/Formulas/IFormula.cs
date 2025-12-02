using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public interface IFormula<out TValue>
    {
        TValue Evaluate(IContext ctx);
    }
}