/*using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using NCalc;
using NCalc.Handlers;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public class NCalcFormulaAsync<TId, TValue> : IFormula<TValue> 
        where TId : notnull
    {
        private readonly string _expressionString;
        private readonly AsyncExpression _expression;
        private readonly IAsyncFunctionIndexedSyncRegistry<TId> _functionIndexedAsyncRegistry;

        public NCalcFormulaAsync(string expressionString, IAsyncFunctionIndexedSyncRegistry<TId> functionIndexedAsyncRegistry = null)
        {
            _expressionString = expressionString;
            _functionIndexedAsyncRegistry = functionIndexedAsyncRegistry;
            _expression = new AsyncExpression(_expressionString);

            ConfigureExpression();
        }

        private void ConfigureExpression()
        {
            foreach (var expression in _functionIndexedAsyncRegistry)
                _expression.Functions[expression.Key.ToString()] = expression.Value;
        }

        public async Task<TValue> EvaluateAsync(IContext ctx)
        {
            var func = new AsyncEvaluateParameterHandler((name, args) =>
            {
                var val = ctx.GetValue(name);
                args.Result = val.Value;
                return ValueTask.CompletedTask;
            });
        
            _expression.EvaluateParameterAsync += func;
            var raw = await _expression.EvaluateAsync();
            _expression.EvaluateParameterAsync -= func;
        
            return (TValue)TypeConversion.ConvertValue(raw, typeof(TValue));
        }

        public TValue Evaluate(IContext ctx)
        {
            return EvaluateAsync(ctx).GetAwaiter().GetResult();
        }
    }
}*/