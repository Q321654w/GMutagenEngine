using System.Numerics;
using GMutagenEngine.Values.Arithmetic.Interfaces;
using GMutagenEngine.Values.BaseClasses;

namespace GMutagenEngine.Values.Default.Realizations;

public class DoubleValue(double initial = 0d) : BaseValue<double>(initial),
    IAdd<double>, IAdd<DoubleValue>,
    ISubtract<double>, ISubtract<DoubleValue>,
    IMultiply<double>, IMultiply<DoubleValue>,
    IDivide<double>, IDivide<DoubleValue>,
#if NET7_0_OR_GREATER
    IAdditionOperators<DoubleValue, double, DoubleValue>,
    IAdditionOperators<DoubleValue, DoubleValue, DoubleValue>,
    ISubtractionOperators<DoubleValue, double, DoubleValue>,
    ISubtractionOperators<DoubleValue, DoubleValue, DoubleValue>,
    IMultiplyOperators<DoubleValue, double, DoubleValue>,
    IMultiplyOperators<DoubleValue, DoubleValue, DoubleValue>,
    IDivisionOperators<DoubleValue, double, DoubleValue>,
    IDivisionOperators<DoubleValue, DoubleValue, DoubleValue>,
#endif
    IComparable<double>, IComparable<DoubleValue>
{
    public void Add(double delta) => Value += delta;
    public void Add(DoubleValue delta) => Value += delta.Value;
    public void Subtract(double delta) => Value -= delta;
    public void Subtract(DoubleValue delta) => Value -= delta.Value;
    public void Multiply(double factor) => Value *= factor;
    public void Multiply(DoubleValue factor) => Value *= factor.Value;
    public void Divide(double divisor) => Value /= divisor;
    public void Divide(DoubleValue divisor) => Value /= divisor.Value;

    public static DoubleValue operator +(DoubleValue left, double right)
    {
        left.Value += right;
        return left;
    }

    public static DoubleValue operator +(DoubleValue left, DoubleValue right)
    {
        left.Value += right.Value;
        return left;
    }

    public static DoubleValue operator -(DoubleValue left, double right)
    {
        left.Value -= right;
        return left;
    }

    public static DoubleValue operator -(DoubleValue left, DoubleValue right)
    {
        left.Value -= right.Value;
        return left;
    }

    public static DoubleValue operator *(DoubleValue left, double right)
    {
        left.Value *= right;
        return left;
    }

    public static DoubleValue operator *(DoubleValue left, DoubleValue right)
    {
        left.Value *= right.Value;
        return left;
    }

    public static DoubleValue operator /(DoubleValue left, double right)
    {
        left.Value /= right;
        return left;
    }

    public static DoubleValue operator /(DoubleValue left, DoubleValue right)
    {
        left.Value /= right.Value;
        return left;
    }

    public int CompareTo(double other) => Value.CompareTo(other);
    public int CompareTo(DoubleValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
}