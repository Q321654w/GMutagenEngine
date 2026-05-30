/*using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using NCalc;
using NCalc.Handlers;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public class NCalcFormulaSync<TId, TValue> : IFormula<TValue> where TId : notnull
    {
        private readonly string _expressionString;
        private readonly Expression _expression;
        private readonly ISyncFunctionIndexedSyncRegistry<TId> _functionIndexedAsyncRegistry;

        public NCalcFormulaSync(string expressionString, ISyncFunctionIndexedSyncRegistry<TId> functionIndexedAsyncRegistry = null)
        {
            _expressionString = expressionString;
            _functionIndexedAsyncRegistry = functionIndexedAsyncRegistry;
            _expression = new Expression(_expressionString);

            ConfigureExpression();
        }

        private void ConfigureExpression()
        {
            foreach (var pair in _functionIndexedAsyncRegistry)
                _expression.Functions[pair.Key.ToString()] = pair.Value;
        }

        public TValue Evaluate(IContext ctx)
        {
            var func = new EvaluateParameterHandler((name, args) =>
            {
                var val = ctx.GetValue(name);
                args.Result = val.Value;
            });
        
            _expression.EvaluateParameter += func;
            var raw = _expression.Evaluate();
            _expression.EvaluateParameter -= func;
        
            return (TValue)TypeConversion.ConvertValue(raw, typeof(TValue));
        }
    }
}*/