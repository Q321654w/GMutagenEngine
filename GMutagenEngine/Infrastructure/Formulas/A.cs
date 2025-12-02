/*using System.Globalization;
using System.Linq.Expressions;
using GMutagenEngine.Formulas.FormulaEngine;
using GMutagenEngine.Identification;
using GMutagenEngine.RuntimeApi.ValueStorages;
using GMutagenEngine.Values;

namespace GMutagenEngine.Formulas;


public class Formula<T> : IFormula<T>
{
    private readonly Formula _formula;

    public Formula(string expression)
    {
        _formula = new Formula(expression);
    }

    public T Evaluate(IValueStorage ctx)
    {
        var expressionContext = new ExpressionContext();

        expressionContext.OnParameterEvaluating += async (name, args) =>
        {
            var value = await ctx.Get(new Id<string>(name));
            args.Value = TypeConversion.ToDouble(value.Value);
            args.HasValue = true;
        };

        var result = _formula.Evaluate(expressionContext);
        return (T)TypeConversion.ConvertValue(result, typeof(T));
    }
}

public class ValueFormula<T> : IValueFormula<T>
{
    private readonly Formula _formula;
    private readonly IValueStorage _context;

    public ValueFormula(string expression, IValueStorage context)
    {
        _formula = new Formula(expression);
        _context = context;
    }

    public T Evaluate(IValueStorage ctx)
    {
        var expressionContext = new ExpressionContext();

        expressionContext.OnParameterEvaluating += async (name, args) =>
        {
            var value = await ctx.Get(new Id<string>(name));
            args.Value = Convert.ToDouble(value.Value);
            args.HasValue = true;
        };

        var result = _formula.Evaluate(expressionContext);
        return (T)TypeConversion.ConvertValue(result, typeof(T));
    }

    public T Value
    {
        get => Evaluate(_context);
        set { }
    }

    object IValue.Value
    {
        get => Value;
        set { }
    }
}

namespace FormulaEngine
{
    #region Базовые типы и исключения

    public enum TokenType
    {
        Number,
        Identifier,
        Operator,
        Function,
        LeftParenthesis,
        RightParenthesis,
        Comma,
        EOF
    }

    public struct Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }

    public class FormulaException : Exception
    {
        public FormulaException(string message) : base(message)
        {
        }
    }

    #endregion

    #region Контракты

    public interface IFunction
    {
        string Name { get; }
        int MinParameters { get; }
        int MaxParameters { get; }
        double Execute(double[] parameters);
    }

    public interface IFunctionRegistry
    {
        void RegisterFunction(IFunction function);
        void RegisterFunction(string name, Func<double[], double> function, int minParams = 1, int maxParams = 1);
        bool FunctionExists(string name);
        IFunction GetFunction(string name);
        IEnumerable<string> GetFunctionNames();
    }

    public interface IOperator
    {
        string Symbol { get; }
        int Precedence { get; }
        bool IsUnary { get; }
        double Evaluate(double[] operands);
    }

    public interface IOperatorRegistry
    {
        void RegisterOperator(IOperator op);
        bool OperatorExists(string symbol);
        IOperator GetOperator(string symbol);
        IEnumerable<IOperator> GetOperators();
    }

    public interface ITypeConverter
    {
        bool CanConvert(string value);
        double Convert(string value);
    }

    public interface ITypeConverterRegistry
    {
        void RegisterConverter(ITypeConverter converter);
        bool TryConvert(string value, out double result);
    }

    public interface IExpressionContext
    {
        IFunctionRegistry FunctionRegistry { get; }
        IOperatorRegistry OperatorRegistry { get; }
        ITypeConverterRegistry TypeConverterRegistry { get; }

        double GetVariableValue(string name);
        bool VariableExists(string name);
        double InvokeFunction(string name, double[] args);

        event Action<string, ParameterEvaluationEventArgs> OnParameterEvaluating;
        event Action<string, FunctionEvaluationEventArgs> OnFunctionEvaluating;
        event Action<string, ExpressionEvaluationEventArgs> OnEvaluating;
    }

    #endregion

    #region Реализации реестров

    public class FunctionRegistry : IFunctionRegistry
    {
        private readonly Dictionary<string, IFunction> _functions =
            new Dictionary<string, IFunction>(StringComparer.OrdinalIgnoreCase);

        public void RegisterFunction(IFunction function)
        {
            _functions[function.Name] = function;
        }

        public void RegisterFunction(string name, Func<double[], double> function, int minParams = 1, int maxParams = 1)
        {
            _functions[name] = new DelegateFunction(name, function, minParams, maxParams);
        }

        public bool FunctionExists(string name)
        {
            return _functions.ContainsKey(name);
        }

        public IFunction GetFunction(string name)
        {
            return _functions.TryGetValue(name, out var function) ? function : null;
        }

        public IEnumerable<string> GetFunctionNames()
        {
            return _functions.Keys;
        }
    }

    public class OperatorRegistry : IOperatorRegistry
    {
        private readonly Dictionary<string, IOperator> _operators = new Dictionary<string, IOperator>();

        public void RegisterOperator(IOperator op)
        {
            _operators[op.Symbol] = op;
        }

        public bool OperatorExists(string symbol) => _operators.ContainsKey(symbol);
        public IOperator GetOperator(string symbol) => _operators.TryGetValue(symbol, out var op) ? op : null;
        public IEnumerable<IOperator> GetOperators() => _operators.Values;
    }

    public class TypeConverterRegistry : ITypeConverterRegistry
    {
        private readonly List<ITypeConverter> _converters = new List<ITypeConverter>();

        public void RegisterConverter(ITypeConverter converter)
        {
            _converters.Add(converter);
        }

        public bool TryConvert(string value, out double result)
        {
            foreach (var converter in _converters)
            {
                if (converter.CanConvert(value))
                {
                    result = converter.Convert(value);
                    return true;
                }
            }

            result = 0;
            return false;
        }
    }

    #endregion

    #region Базовые операторы

    public abstract class BinaryOperator : IOperator
    {
        public abstract string Symbol { get; }
        public abstract int Precedence { get; }
        public bool IsUnary => false;
        public abstract double Evaluate(double[] operands);
    }

    public abstract class UnaryOperator : IOperator
    {
        public abstract string Symbol { get; }
        public abstract int Precedence { get; }
        public bool IsUnary => true;
        public abstract double Evaluate(double[] operands);
    }

    // Стандартные операторы
    public class AddOperator : BinaryOperator
    {
        public override string Symbol => "+";
        public override int Precedence => 1;
        public override double Evaluate(double[] operands) => operands[0] + operands[1];
    }

    public class SubtractOperator : BinaryOperator
    {
        public override string Symbol => "-";
        public override int Precedence => 1;
        public override double Evaluate(double[] operands) => operands[0] - operands[1];
    }

    public class MultiplyOperator : BinaryOperator
    {
        public override string Symbol => "*";
        public override int Precedence => 2;
        public override double Evaluate(double[] operands) => operands[0] * operands[1];
    }

    public class DivideOperator : BinaryOperator
    {
        public override string Symbol => "/";
        public override int Precedence => 2;
        public override double Evaluate(double[] operands) => operands[0] / operands[1];
    }

    public class PowerOperator : BinaryOperator
    {
        public override string Symbol => "^";
        public override int Precedence => 3;
        public override double Evaluate(double[] operands) => Math.Pow(operands[0], operands[1]);
    }

    public class ModuloOperator : BinaryOperator
    {
        public override string Symbol => "%";
        public override int Precedence => 2;
        public override double Evaluate(double[] operands) => operands[0] % operands[1];
    }

    public class UnaryMinusOperator : UnaryOperator
    {
        public override string Symbol => "-";
        public override int Precedence => 4;
        public override double Evaluate(double[] operands) => -operands[0];
    }

    // Кастомные операторы
    public class AtOperator : BinaryOperator
    {
        public override string Symbol => "@";
        public override int Precedence => 2;
        public override double Evaluate(double[] operands) => (operands[0] + operands[1]) * 2;
    }

    public class DoubleColonOperator : BinaryOperator
    {
        public override string Symbol => "::";
        public override int Precedence => 1;
        public override double Evaluate(double[] operands) => Math.Sqrt(operands[0] * operands[1]);
    }

    #endregion

    #region Базовые функции

    public abstract class MathFunctionBase : IFunction
    {
        public abstract string Name { get; }
        public virtual int MinParameters => 1;
        public virtual int MaxParameters => 1;
        public abstract double Execute(double[] parameters);
    }

    public class SinFunction : MathFunctionBase
    {
        public override string Name => "sin";
        public override double Execute(double[] parameters) => Math.Sin(parameters[0]);
    }

    public class CosFunction : MathFunctionBase
    {
        public override string Name => "cos";
        public override double Execute(double[] parameters) => Math.Cos(parameters[0]);
    }

    public class TanFunction : MathFunctionBase
    {
        public override string Name => "tan";
        public override double Execute(double[] parameters) => Math.Tan(parameters[0]);
    }

    public class LogFunction : MathFunctionBase
    {
        public override string Name => "log";
        public override int MinParameters => 1;
        public override int MaxParameters => 2;

        public override double Execute(double[] parameters)
        {
            return parameters.Length == 1
                ? Math.Log(parameters[0])
                : Math.Log(parameters[0], parameters[1]);
        }
    }

    public class ExpFunction : MathFunctionBase
    {
        public override string Name => "exp";
        public override double Execute(double[] parameters) => Math.Exp(parameters[0]);
    }

    public class SqrtFunction : MathFunctionBase
    {
        public override string Name => "sqrt";
        public override double Execute(double[] parameters) => Math.Sqrt(parameters[0]);
    }

    public class AbsFunction : MathFunctionBase
    {
        public override string Name => "abs";
        public override double Execute(double[] parameters) => Math.Abs(parameters[0]);
    }

    public class MaxFunction : IFunction
    {
        public string Name => "max";
        public int MinParameters => 2;
        public int MaxParameters => int.MaxValue;
        public double Execute(double[] parameters) => parameters.Max();
    }

    public class MinFunction : IFunction
    {
        public string Name => "min";
        public int MinParameters => 2;
        public int MaxParameters => int.MaxValue;
        public double Execute(double[] parameters) => parameters.Min();
    }

    public class PowFunction : IFunction
    {
        public string Name => "pow";
        public int MinParameters => 2;
        public int MaxParameters => 2;
        public double Execute(double[] parameters) => Math.Pow(parameters[0], parameters[1]);
    }

    public class DelegateFunction : IFunction
    {
        private readonly Func<double[], double> _function;

        public string Name { get; }
        public int MinParameters { get; }
        public int MaxParameters { get; }

        public DelegateFunction(string name, Func<double[], double> function, int minParams, int maxParams)
        {
            Name = name;
            _function = function;
            MinParameters = minParams;
            MaxParameters = maxParams;
        }

        public double Execute(double[] parameters)
        {
            if (parameters.Length < MinParameters || parameters.Length > MaxParameters)
                throw new FormulaException(
                    $"Function {Name} expects {MinParameters}-{MaxParameters} parameters, got {parameters.Length}");

            return _function(parameters);
        }
    }

    #endregion

    #region Конвертеры типов

    public class NumberConverter : ITypeConverter
    {
        public bool CanConvert(string value)
        {
            return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        }

        public double Convert(string value)
        {
            return double.Parse(value, CultureInfo.InvariantCulture);
        }
    }

    public class BooleanConverter : ITypeConverter
    {
        public bool CanConvert(string value)
        {
            var lower = value.ToLower();
            return lower == "true" || lower == "false";
        }

        public double Convert(string value)
        {
            return value.ToLower() == "true" ? 1 : 0;
        }
    }

    public class ScientificNotationConverter : ITypeConverter
    {
        public bool CanConvert(string value)
        {
            return value.Contains("e") || value.Contains("E");
        }

        public double Convert(string value)
        {
            return double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
        }
    }

    public class HexConverter : ITypeConverter
    {
        public bool CanConvert(string value)
        {
            return value.StartsWith("0x", StringComparison.OrdinalIgnoreCase);
        }

        public double Convert(string value)
        {
            return System.Convert.ToInt32(value, 16);
        }
    }

    public class BinaryConverter : ITypeConverter
    {
        public bool CanConvert(string value)
        {
            return value.StartsWith("0b");
        }

        public double Convert(string value)
        {
            return System.Convert.ToInt32(value);
        }
    }

    #endregion

    public class ParameterEvaluationEventArgs : EventArgs
    {
        public string Name { get; }
        public double Value { get; set; }
        public bool HasValue { get; set; }

        public ParameterEvaluationEventArgs(string name)
        {
            Name = name;
            HasValue = false;
        }
    }

    public class FunctionEvaluationEventArgs : EventArgs
    {
        public string Name { get; }
        public double[] Arguments { get; }
        public double Value { get; set; }
        public bool HasValue { get; set; }

        public FunctionEvaluationEventArgs(string name, double[] args)
        {
            Name = name;
            Arguments = args;
            HasValue = false;
        }
    }

    public class ExpressionEvaluationEventArgs : EventArgs
    {
        public string ExpressionText { get; }
        public double Value { get; set; }
        public bool HasValue { get; set; }

        public ExpressionEvaluationEventArgs(string expr)
        {
            ExpressionText = expr;
            HasValue = false;
        }
    }

    #region AST узлы

    public abstract class Node
    {
        public abstract double Evaluate(IExpressionContext context);
    }

    public class NumberNode : Node
    {
        private readonly string _rawValue;

        public NumberNode(string rawValue)
        {
            _rawValue = rawValue;
        }

        public override double Evaluate(IExpressionContext context)
        {
            if (context.TypeConverterRegistry.TryConvert(_rawValue, out var result))
                return result;

            throw new FormulaException($"Cannot convert '{_rawValue}' to number");
        }
    }

    public class VariableNode : Node
    {
        private readonly string _name;

        public VariableNode(string name)
        {
            _name = name;
        }

        public override double Evaluate(IExpressionContext context)
        {
            return context.GetVariableValue(_name);
        }
    }

    public class BinaryOperationNode : Node
    {
        private readonly Node _left;
        private readonly Node _right;
        private readonly IOperator _operator;

        public BinaryOperationNode(Node left, IOperator op, Node right)
        {
            _left = left;
            _operator = op;
            _right = right;
        }

        public override double Evaluate(IExpressionContext context)
        {
            var leftVal = _left.Evaluate(context);
            var rightVal = _right.Evaluate(context);
            return _operator.Evaluate(new[] { leftVal, rightVal });
        }
    }

    public class UnaryOperationNode : Node
    {
        private readonly Node _operand;
        private readonly IOperator _operator;

        public UnaryOperationNode(IOperator op, Node operand)
        {
            _operator = op;
            _operand = operand;
        }

        public override double Evaluate(IExpressionContext context)
        {
            var operandVal = _operand.Evaluate(context);
            return _operator.Evaluate(new[] { operandVal });
        }
    }

    public class FunctionNode : Node
    {
        private readonly string _functionName;
        private readonly List<Node> _arguments;

        public FunctionNode(string functionName, List<Node> arguments)
        {
            _functionName = functionName;
            _arguments = arguments;
        }

        public override double Evaluate(IExpressionContext context)
        {
            var function = context.FunctionRegistry.GetFunction(_functionName);
            if (function == null)
                throw new FormulaException($"Unknown function: {_functionName}");

            var argValues = _arguments.Select(a => a.Evaluate(context)).ToArray();

            if (argValues.Length < function.MinParameters || argValues.Length > function.MaxParameters)
                throw new FormulaException(
                    $"Function {_functionName} expects {function.MinParameters}-{function.MaxParameters} parameters, got {argValues.Length}");

            return context.InvokeFunction(_functionName, argValues);
        }
    }

    #endregion

    #region Лексер и парсер

    public class Lexer
    {
        private readonly string _input;
        private int _position;
        private readonly IOperatorRegistry _operatorRegistry;

        public Lexer(string input, IOperatorRegistry operatorRegistry)
        {
            _input = input;
            _operatorRegistry = operatorRegistry;
            _position = 0;
        }

        public Token NextToken()
        {
            SkipWhitespace();

            if (_position >= _input.Length)
                return new Token(TokenType.EOF, "");

            var operatorToken = TryReadOperator();
            if (operatorToken.HasValue)
                return operatorToken.Value;

            char current = _input[_position];

            if (char.IsDigit(current) || current == '.' || current == '-' || current == '+')
                return ReadNumber();

            if (char.IsLetter(current))
                return ReadIdentifier();

            if (current == '(')
            {
                _position++;
                return new Token(TokenType.LeftParenthesis, "(");
            }

            if (current == ')')
            {
                _position++;
                return new Token(TokenType.RightParenthesis, ")");
            }

            if (current == ',')
            {
                _position++;
                return new Token(TokenType.Comma, ",");
            }

            throw new FormulaException($"Unexpected character: '{current}' at position {_position}");
        }

        private Token? TryReadOperator()
        {
            for (int length = 3; length >= 1; length--)
            {
                if (_position + length <= _input.Length)
                {
                    var potentialOp = _input.Substring(_position, length);
                    if (_operatorRegistry.OperatorExists(potentialOp))
                    {
                        _position += length;
                        return new Token(TokenType.Operator, potentialOp);
                    }
                }
            }

            return null;
        }

        private Token ReadNumber()
        {
            var start = _position;

            if (_position < _input.Length && (_input[_position] == '+' || _input[_position] == '-'))
                _position++;

            bool hasDecimal = false;
            bool hasExponent = false;

            while (_position < _input.Length)
            {
                char current = _input[_position];

                if (char.IsDigit(current))
                {
                    _position++;
                }
                else if (current == '.' && !hasDecimal)
                {
                    _position++;
                    hasDecimal = true;
                }
                else if ((current == 'e' || current == 'E') && !hasExponent)
                {
                    _position++;
                    hasExponent = true;

                    if (_position < _input.Length && (_input[_position] == '+' || _input[_position] == '-'))
                        _position++;
                }
                else
                {
                    break;
                }
            }

            return new Token(TokenType.Number, _input.Substring(start, _position - start));
        }

        private Token ReadIdentifier()
        {
            var start = _position;
            while (_position < _input.Length && (char.IsLetterOrDigit(_input[_position]) || _input[_position] == '_'))
                _position++;

            return new Token(TokenType.Identifier, _input.Substring(start, _position - start));
        }

        private void SkipWhitespace()
        {
            while (_position < _input.Length && char.IsWhiteSpace(_input[_position]))
                _position++;
        }
    }

    public class Parser
    {
        private readonly Lexer _lexer;
        private Token _currentToken;
        private readonly IOperatorRegistry _operatorRegistry;

        public Parser(Lexer lexer, IOperatorRegistry operatorRegistry)
        {
            _lexer = lexer;
            _operatorRegistry = operatorRegistry;
            _currentToken = lexer.NextToken();
        }

        public Node Parse()
        {
            return ParseExpression();
        }

        private Node ParseExpression(int minPrecedence = 0)
        {
            Node left = ParsePrimary();

            while (true)
            {
                if (_currentToken.Type != TokenType.Operator)
                    break;

                var op = _operatorRegistry.GetOperator(_currentToken.Value);
                if (op == null || op.Precedence < minPrecedence)
                    break;

                if (op.IsUnary)
                {
                    Eat(TokenType.Operator);
                    left = new UnaryOperationNode(op, left);
                }
                else
                {
                    Eat(TokenType.Operator);
                    var right = ParseExpression(op.Precedence + 1);
                    left = new BinaryOperationNode(left, op, right);
                }
            }

            return left;
        }

        private Node ParsePrimary()
        {
            switch (_currentToken.Type)
            {
                case TokenType.Number:
                    var numberNode = new NumberNode(_currentToken.Value);
                    Eat(TokenType.Number);
                    return numberNode;

                case TokenType.Identifier:
                    var identifier = _currentToken.Value;
                    Eat(TokenType.Identifier);

                    if (_currentToken.Type == TokenType.LeftParenthesis)
                        return ParseFunctionCall(identifier);

                    return new VariableNode(identifier);

                case TokenType.LeftParenthesis:
                    Eat(TokenType.LeftParenthesis);
                    var node = ParseExpression();
                    Eat(TokenType.RightParenthesis);
                    return node;

                case TokenType.Operator:
                    var op = _operatorRegistry.GetOperator(_currentToken.Value);
                    if (op != null && op.IsUnary)
                    {
                        Eat(TokenType.Operator);
                        return new UnaryOperationNode(op, ParsePrimary());
                    }

                    throw new FormulaException($"Unexpected operator: {_currentToken.Value}");

                default:
                    throw new FormulaException($"Unexpected token: {_currentToken.Value}");
            }
        }

        private Node ParseFunctionCall(string functionName)
        {
            Eat(TokenType.LeftParenthesis);
            var arguments = new List<Node>();

            if (_currentToken.Type != TokenType.RightParenthesis)
            {
                arguments.Add(ParseExpression());

                while (_currentToken.Type == TokenType.Comma)
                {
                    Eat(TokenType.Comma);
                    arguments.Add(ParseExpression());
                }
            }

            Eat(TokenType.RightParenthesis);
            return new FunctionNode(functionName, arguments);
        }

        private void Eat(TokenType expectedType)
        {
            if (_currentToken.Type == expectedType)
            {
                _currentToken = _lexer.NextToken();
            }
            else
            {
                throw new FormulaException($"Expected {expectedType}, got {_currentToken.Type}");
            }
        }
    }

    #endregion

    #region Контекст выполнения

    public class ExpressionContext : IExpressionContext
    {
        private readonly Dictionary<string, double> _variables;
        private readonly FunctionRegistry _functionRegistry;
        private readonly OperatorRegistry _operatorRegistry;
        private readonly TypeConverterRegistry _typeConverterRegistry;

        public event Action<string, ParameterEvaluationEventArgs> OnParameterEvaluating;
        public event Action<string, FunctionEvaluationEventArgs> OnFunctionEvaluating;
        public event Action<string, ExpressionEvaluationEventArgs> OnEvaluating;

        public ExpressionContext(
            Dictionary<string, double> variables = null,
            FunctionRegistry functionRegistry = null,
            OperatorRegistry operatorRegistry = null,
            TypeConverterRegistry typeConverterRegistry = null)
        {
            _variables = variables ?? new Dictionary<string, double>();
            _functionRegistry = functionRegistry ?? CreateDefaultFunctionRegistry();
            _operatorRegistry = operatorRegistry ?? CreateDefaultOperatorRegistry();
            _typeConverterRegistry = typeConverterRegistry ?? CreateDefaultTypeConverterRegistry();
        }

        public IFunctionRegistry FunctionRegistry => _functionRegistry;
        public IOperatorRegistry OperatorRegistry => _operatorRegistry;
        public ITypeConverterRegistry TypeConverterRegistry => _typeConverterRegistry;

        public double GetVariableValue(string name)
        {
            var args = new ParameterEvaluationEventArgs(name);
            OnParameterEvaluating?.Invoke(name, args);

            if (args.HasValue)
                return args.Value;

            return _variables.TryGetValue(name, out var value)
                ? value
                : throw new FormulaException($"Undefined variable: {name}");
        }

        internal double InvokeFunction(string name, double[] args)
        {
            var evt = new FunctionEvaluationEventArgs(name, args);
            OnFunctionEvaluating?.Invoke(name, evt);

            if (evt.HasValue)
                return evt.Value;

            var func = FunctionRegistry.GetFunction(name)
                       ?? throw new FormulaException($"Unknown function: {name}");

            if (args.Length < func.MinParameters || args.Length > func.MaxParameters)
                throw new FormulaException(
                    $"Function {name} expects {func.MinParameters}-{func.MaxParameters} params, got {args.Length}");

            return func.Execute(args);
        }

        public bool VariableExists(string name) => _variables.ContainsKey(name);

        private static FunctionRegistry CreateDefaultFunctionRegistry()
        {
            var registry = new FunctionRegistry();

            // Базовые математические функции
            registry.RegisterFunction(new SinFunction());
            registry.RegisterFunction(new CosFunction());
            registry.RegisterFunction(new TanFunction());
            registry.RegisterFunction(new LogFunction());
            registry.RegisterFunction(new ExpFunction());
            registry.RegisterFunction(new SqrtFunction());
            registry.RegisterFunction(new AbsFunction());
            registry.RegisterFunction(new MaxFunction());
            registry.RegisterFunction(new MinFunction());
            registry.RegisterFunction(new PowFunction());

            // Дополнительные функции через делегаты
            registry.RegisterFunction("ceil", p => Math.Ceiling(p[0]));
            registry.RegisterFunction("floor", p => Math.Floor(p[0]));
            registry.RegisterFunction("round", p => Math.Round(p[0]));
            registry.RegisterFunction("pi", _ => Math.PI);
            registry.RegisterFunction("e", _ => Math.E);

            return registry;
        }

        private static OperatorRegistry CreateDefaultOperatorRegistry()
        {
            var registry = new OperatorRegistry();

            registry.RegisterOperator(new AddOperator());
            registry.RegisterOperator(new SubtractOperator());
            registry.RegisterOperator(new MultiplyOperator());
            registry.RegisterOperator(new DivideOperator());
            registry.RegisterOperator(new PowerOperator());
            registry.RegisterOperator(new ModuloOperator());
            registry.RegisterOperator(new UnaryMinusOperator());
            registry.RegisterOperator(new AtOperator());
            registry.RegisterOperator(new DoubleColonOperator());

            return registry;
        }

        private static TypeConverterRegistry CreateDefaultTypeConverterRegistry()
        {
            var registry = new TypeConverterRegistry();
            registry.RegisterConverter(new NumberConverter());
            registry.RegisterConverter(new BooleanConverter());
            registry.RegisterConverter(new ScientificNotationConverter());
            registry.RegisterConverter(new HexConverter());
            registry.RegisterConverter(new BinaryConverter());
            return registry;
        }
    }

    #endregion

    #region Основные классы движка

    public class Formula
    {
        private readonly string _expression;
        private readonly Node _ast;
        public event Action<string, ExpressionEvaluationEventArgs> OnEvaluating;

        public Formula(string expression,
            IFunctionRegistry functionRegistry = null,
            IOperatorRegistry operatorRegistry = null,
            ITypeConverterRegistry typeConverterRegistry = null)
        {
            _expression = expression;
            functionRegistry ??= new FunctionRegistry();
            operatorRegistry ??= new OperatorRegistry();

            var lexer = new Lexer(expression, operatorRegistry);
            var parser = new Parser(lexer, operatorRegistry);
            _ast = parser.Parse();
        }

        public double Evaluate(IExpressionContext context = null)
        {
            context ??= new ExpressionContext();

            var args = new ExpressionEvaluationEventArgs(_expression);
            OnEvaluating?.Invoke(_expression, args);

            if (args.HasValue)
                return args.Value;

            return _ast.Evaluate(context);
        }

        public double Evaluate(Dictionary<string, double> variables)
        {
            var context = new ExpressionContext(variables);
            return _ast.Evaluate(context);
        }
    }

    public class FormulaBuilder
    {
        private readonly FunctionRegistry _functionRegistry = new FunctionRegistry();
        private readonly OperatorRegistry _operatorRegistry = new OperatorRegistry();
        private readonly TypeConverterRegistry _typeConverterRegistry = new TypeConverterRegistry();
        private readonly Dictionary<string, double> _constants = new Dictionary<string, double>();

        public FormulaBuilder AddFunction(IFunction function)
        {
            _functionRegistry.RegisterFunction(function);
            return this;
        }

        public FormulaBuilder AddFunction(string name, Func<double, double> function)
        {
            _functionRegistry.RegisterFunction(name, p => function(p[0]));
            return this;
        }

        public FormulaBuilder AddFunction(string name, Func<double, double, double> function)
        {
            _functionRegistry.RegisterFunction(name, p => function(p[0], p[1]), 2, 2);
            return this;
        }

        public FormulaBuilder AddFunction(string name, Func<double[], double> function, int minParams = 1,
            int maxParams = 1)
        {
            _functionRegistry.RegisterFunction(name, function, minParams, maxParams);
            return this;
        }

        public FormulaBuilder AddOperator(IOperator op)
        {
            _operatorRegistry.RegisterOperator(op);
            return this;
        }

        public FormulaBuilder AddTypeConverter(ITypeConverter converter)
        {
            _typeConverterRegistry.RegisterConverter(converter);
            return this;
        }

        public FormulaBuilder AddConstant(string name, double value)
        {
            _constants[name] = value;
            return this;
        }

        public Formula Build(string expression)
        {
            return new Formula(expression, _functionRegistry, _operatorRegistry, _typeConverterRegistry);
        }

        public IExpressionContext CreateContext(Dictionary<string, double> variables = null)
        {
            var allVariables = new Dictionary<string, double>();

            foreach (var constant in _constants)
                allVariables[constant.Key] = constant.Value;

            if (variables != null)
            {
                foreach (var variable in variables)
                    allVariables[variable.Key] = variable.Value;
            }

            return new ExpressionContext(allVariables, _functionRegistry, _operatorRegistry, _typeConverterRegistry);
        }
    }

    #endregion

    #region Примеры использования и дополнительные компоненты

    // Дополнительные кастомные функции
    public class CustomStatisticsFunction : IFunction
    {
        public string Name => "stdev";
        public int MinParameters => 2;
        public int MaxParameters => int.MaxValue;

        public double Execute(double[] parameters)
        {
            var avg = parameters.Average();
            var sum = parameters.Sum(x => Math.Pow(x - avg, 2));
            return Math.Sqrt(sum / parameters.Length);
        }
    }

    public class MatrixOperator : BinaryOperator
    {
        public override string Symbol => "#";
        public override int Precedence => 3;

        public override double Evaluate(double[] operands)
        {
            return operands[0] * 10 + operands[1];
        }
    }

    /*class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("=== Formula Engine Demo ===");

                // 1. Базовое использование
                Console.WriteLine("\n1. Базовые вычисления:");
                var formula1 = new Formula("2 + 3 * 4");
                Console.WriteLine($"2 + 3 * 4 = {formula1.Evaluate()}");

                // 2. С функциями
                Console.WriteLine("\n2. Математические функции:");
                var formula2 = new Formula("sin(0) + cos(0) + sqrt(16)");
                Console.WriteLine($"sin(0) + cos(0) + sqrt(16) = {formula2.Evaluate()}");

                // 3. С переменными
                Console.WriteLine("\n3. Переменные:");
                var formula3 = new Formula("x * y + 5");
                var variables = new Dictionary<string, double> { ["x"] = 3, ["y"] = 4 };
                Console.WriteLine($"x * y + 5 = {formula3.Evaluate(variables)}");

                // 4. Кастомные операторы
                Console.WriteLine("\n4. Кастомные операторы:");
                var builder = new FormulaBuilder()
                    .AddOperator(new AtOperator())
                    .AddOperator(new DoubleColonOperator());

                var formula4 = builder.Build("5 @ 3");
                Console.WriteLine($"5 @ 3 = {formula4.Evaluate()}");

                var formula5 = builder.Build("4 :: 9");
                Console.WriteLine($"4 :: 9 = {formula5.Evaluate()}");

                // 5. Различные системы счисления
                Console.WriteLine("\n5. Системы счисления:");
                var advancedBuilder = new FormulaBuilder()
                    .AddTypeConverter(new HexConverter())
                    .AddTypeConverter(new BinaryConverter());

                var formula6 = advancedBuilder.Build("0xFF + 0b1010");
                Console.WriteLine($"0xFF + 0b1010 = {formula6.Evaluate()}");

                // 6. Сложные выражения
                Console.WriteLine("\n6. Сложные выражения:");
                var complexBuilder = new FormulaBuilder()
                    .AddFunction("square", x => x * x)
                    .AddFunction("cube", x => x * x * x)
                    .AddConstant("GRAVITY", 9.81);

                var formula7 = complexBuilder.Build("square(5) + cube(3)");
                var context = complexBuilder.CreateContext(new Dictionary<string, double> { ["mass"] = 10 });
                Console.WriteLine($"square(5) + cube(3) = {formula7.Evaluate(context)}");

                // 7. Статистические функции
                Console.WriteLine("\n7. Статистические функции:");
                var statsBuilder = new FormulaBuilder()
                    .AddFunction(new CustomStatisticsFunction())
                    .AddFunction("avg", p => p.Average());

                var formula8 = statsBuilder.Build("avg(1, 2, 3, 4, 5)");
                Console.WriteLine($"avg(1, 2, 3, 4, 5) = {formula8.Evaluate()}");

                Console.WriteLine("\n=== Demo Completed ===");
            }
            catch (FormulaException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }#1#

    #endregion
}*/