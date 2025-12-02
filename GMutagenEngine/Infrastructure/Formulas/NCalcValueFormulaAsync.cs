using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using NCalc;
using NCalc.Handlers;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public class NCalcValueFormulaAsync<TId, TValue> : IValueFormula<TValue>, IDisposable where TId : notnull
    {
        private readonly string _expressionString;
        private readonly AsyncExpression _expression;
        private readonly IContext _context;
        private readonly IAsyncFunctionIndexedSyncRegistry<TId> _asyncFunctionIndexedSyncRegistry;
        private readonly AsyncEvaluateParameterHandler _selfContextFunc;

        public NCalcValueFormulaAsync(string expressionString, IContext context, IAsyncFunctionIndexedSyncRegistry<TId> asyncFunctionIndexedSyncRegistry = null)
        {
            _expressionString = expressionString;
            _context = context;
            _expression = new AsyncExpression(_expressionString);
            _asyncFunctionIndexedSyncRegistry = asyncFunctionIndexedSyncRegistry;
        
            _selfContextFunc = (name, args) =>
            {
                var val = _context.GetValue(name);
                args.Result = val.Value;
                return ValueTask.CompletedTask;
            };
        
            _expression.EvaluateParameterAsync += _selfContextFunc;
        
            ConfigureExpression();
        }
    
        private void ConfigureExpression()
        {
            foreach (var expression in _asyncFunctionIndexedSyncRegistry)
                _expression.Functions[expression.Key.ToString()] = expression.Value;
        }

        public TValue EvaluateSelf()
        {
            var raw = _expression.EvaluateAsync().GetAwaiter().GetResult();
            return (TValue)TypeConversion.ConvertValue(raw, typeof(TValue));
        }

        public TValue Value
        {
            get => EvaluateSelf();
            set { }
        }

        object IValue.Value
        {
            get => Value;
            set { }
        }
    
        public Type ValueType => typeof(TValue);
    
        public void Dispose()
        {
            _expression.EvaluateParameterAsync -= _selfContextFunc;
        }
    }
}