using System.Numerics;
using GMutagenEngine.Values.Arithmetic.Interfaces;
using GMutagenEngine.Values.BaseClasses;

namespace GMutagenEngine.Values.Default.Realizations;

public class DecimalValue(decimal initial = 0m) : BaseValue<decimal>(initial),
    IAdd<decimal>, IAdd<DecimalValue>,
    ISubtract<decimal>, ISubtract<DecimalValue>,
    IMultiply<decimal>, IMultiply<DecimalValue>,
    IDivide<decimal>, IDivide<DecimalValue>,
#if NET7_0_OR_GREATER
    IAdditionOperators<DecimalValue, decimal, DecimalValue>,
    IAdditionOperators<DecimalValue, DecimalValue, DecimalValue>,
    ISubtractionOperators<DecimalValue, decimal, DecimalValue>,
    ISubtractionOperators<DecimalValue, DecimalValue, DecimalValue>,
    IMultiplyOperators<DecimalValue, decimal, DecimalValue>,
    IMultiplyOperators<DecimalValue, DecimalValue, DecimalValue>,
    IDivisionOperators<DecimalValue, decimal, DecimalValue>,
    IDivisionOperators<DecimalValue, DecimalValue, DecimalValue>,
#endif
    IComparable<decimal>, IComparable<DecimalValue>
{
    public void Add(decimal delta) => Value += delta;
    public void Add(DecimalValue delta) => Value += delta.Value;
    public void Subtract(decimal delta) => Value -= delta;
    public void Subtract(DecimalValue delta) => Value -= delta.Value;
    public void Multiply(decimal factor) => Value *= factor;
    public void Multiply(DecimalValue factor) => Value *= factor.Value;
    public void Divide(decimal divisor) => Value /= divisor;
    public void Divide(DecimalValue divisor) => Value /= divisor.Value;

    public static DecimalValue operator +(DecimalValue left, decimal right)
    {
        left.Value += right;
        return left;
    }

    public static DecimalValue operator +(DecimalValue left, DecimalValue right)
    {
        left.Value += right.Value;
        return left;
    }

    public static DecimalValue operator -(DecimalValue left, decimal right)
    {
        left.Value -= right;
        return left;
    }

    public static DecimalValue operator -(DecimalValue left, DecimalValue right)
    {
        left.Value -= right.Value;
        return left;
    }

    public static DecimalValue operator *(DecimalValue left, decimal right)
    {
        left.Value *= right;
        return left;
    }

    public static DecimalValue operator *(DecimalValue left, DecimalValue right)
    {
        left.Value *= right.Value;
        return left;
    }

    public static DecimalValue operator /(DecimalValue left, decimal right)
    {
        left.Value /= right;
        return left;
    }

    public static DecimalValue operator /(DecimalValue left, DecimalValue right)
    {
        left.Value /= right.Value;
        return left;
    }

    public int CompareTo(decimal other) => Value.CompareTo(other);
    public int CompareTo(DecimalValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
}