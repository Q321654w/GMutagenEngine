using System.Numerics;
using GMutagenEngine.Values.Arithmetic.Interfaces;
using GMutagenEngine.Values.BaseClasses;

namespace GMutagenEngine.Values.Default.Realizations;

public class IntValue(int initial = 0) : BaseValue<int>(initial),
    IAdd<int>, IAdd<IntValue>,
    ISubtract<int>, ISubtract<IntValue>,
    IMultiply<int>, IMultiply<IntValue>,
    IDivide<int>, IDivide<IntValue>,
    IModulo<int>, IModulo<IntValue>,
#if NET7_0_OR_GREATER
    IAdditionOperators<IntValue, int, IntValue>,
    IAdditionOperators<IntValue, IntValue, IntValue>,
    ISubtractionOperators<IntValue, int, IntValue>,
    ISubtractionOperators<IntValue, IntValue, IntValue>,
    IMultiplyOperators<IntValue, int, IntValue>,
    IMultiplyOperators<IntValue, IntValue, IntValue>,
    IDivisionOperators<IntValue, int, IntValue>,
    IDivisionOperators<IntValue, IntValue, IntValue>,
    IModulusOperators<IntValue, int, IntValue>,
    IModulusOperators<IntValue, IntValue, IntValue>,
    IIncrementOperators<IntValue>,
    IDecrementOperators<IntValue>,
#endif
    IComparable<int>, IComparable<IntValue>
{
    public void Add(int delta) => Value += delta;
    public void Add(IntValue delta) => Value += delta.Value;
    public void Subtract(int delta) => Value -= delta;
    public void Subtract(IntValue delta) => Value -= delta.Value;
    public void Multiply(int factor) => Value *= factor;
    public void Multiply(IntValue factor) => Value *= factor.Value;
    public void Divide(int divisor) => Value /= divisor;
    public void Divide(IntValue divisor) => Value /= divisor.Value;
    public void Modulo(int divisor) => Value %= divisor;
    public void Modulo(IntValue divisor) => Value %= divisor.Value;

    public static IntValue operator +(IntValue left, int right)
    {
        left.Value += right;
        return left;
    }

    public static IntValue operator +(IntValue left, IntValue right)
    {
        left.Value += right.Value;
        return left;
    }

    public static IntValue operator -(IntValue left, int right)
    {
        left.Value -= right;
        return left;
    }

    public static IntValue operator -(IntValue left, IntValue right)
    {
        left.Value -= right.Value;
        return left;
    }

    public static IntValue operator *(IntValue left, int right)
    {
        left.Value *= right;
        return left;
    }

    public static IntValue operator *(IntValue left, IntValue right)
    {
        left.Value *= right.Value;
        return left;
    }

    public static IntValue operator /(IntValue left, int right)
    {
        left.Value /= right;
        return left;
    }

    public static IntValue operator /(IntValue left, IntValue right)
    {
        left.Value /= right.Value;
        return left;
    }

    public static IntValue operator %(IntValue left, int right)
    {
        left.Value %= right;
        return left;
    }

    public static IntValue operator %(IntValue left, IntValue right)
    {
        left.Value %= right.Value;
        return left;
    }

    public static IntValue operator ++(IntValue value)
    {
        value.Value++;
        return value;
    }

    public static IntValue operator --(IntValue value)
    {
        value.Value--;
        return value;
    }

    public int CompareTo(int other) => Value.CompareTo(other);
    public int CompareTo(IntValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
}