/*using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using NCalc;
using NCalc.Handlers;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public class NCalcValueFormulaSync<TId, TValue> : IValueFormula<TValue>, IDisposable where TId : notnull
    {
        private readonly string _expressionString;
        private readonly Expression _expression;
        private readonly IContext _context;
        private readonly ISyncFunctionIndexedSyncRegistry<TId> _functionIndexedAsyncRegistry;
        private readonly EvaluateParameterHandler _selfContextFunc;

        public NCalcValueFormulaSync(string expressionString, IContext context, ISyncFunctionIndexedSyncRegistry<TId> functionIndexedAsyncRegistry = null)
        {
            _expressionString = expressionString;
            _context = context;
            _expression = new Expression(_expressionString);
            _functionIndexedAsyncRegistry = functionIndexedAsyncRegistry;
        
            _selfContextFunc = (name, args) =>
            {
                var val = _context.GetValue(name);
                args.Result = val.Value;
            };
            _expression.EvaluateParameter += _selfContextFunc;
        
            ConfigureExpression();
        }
    
        private void ConfigureExpression()
        {
            foreach (var pair in _functionIndexedAsyncRegistry)
                _expression.Functions[pair.Key.ToString()] = pair.Value;
        }
    
        private TValue EvaluateSelf()
        {
            var raw = _expression.Evaluate(); 
            return (TValue)TypeConversion.ConvertValue(raw, typeof(TValue));
        }
    
        public TValue Value
        {
            get => EvaluateSelf();
            set { }
        }

        public Type ValueType => typeof(TValue);

        object IValue.Value
        {
            get => Value;
            set { }
        }

        public void Dispose()
        {
            _expression.EvaluateParameter -= _selfContextFunc;
        }
    }
}*/