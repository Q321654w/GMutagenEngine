using System.Numerics;
using GMutagenEngine.Values.Arithmetic.Interfaces;
using GMutagenEngine.Values.BaseClasses;

namespace GMutagenEngine.Values.Default.Realizations;

public class FloatValue(float initial = 0f) : BaseValue<float>(initial),
    IAdd<float>, IAdd<FloatValue>,
    ISubtract<float>, ISubtract<FloatValue>,
    IMultiply<float>, IMultiply<FloatValue>,
    IDivide<float>, IDivide<FloatValue>,
#if NET7_0_OR_GREATER
    IAdditionOperators<FloatValue, float, FloatValue>,
    IAdditionOperators<FloatValue, FloatValue, FloatValue>,
    ISubtractionOperators<FloatValue, float, FloatValue>,
    ISubtractionOperators<FloatValue, FloatValue, FloatValue>,
    IMultiplyOperators<FloatValue, float, FloatValue>,
    IMultiplyOperators<FloatValue, FloatValue, FloatValue>,
    IDivisionOperators<FloatValue, float, FloatValue>,
    IDivisionOperators<FloatValue, FloatValue, FloatValue>,
#endif
    IComparable<float>, IComparable<FloatValue>
{
    public void Add(float delta) => Value += delta;
    public void Add(FloatValue delta) => Value += delta.Value;
    public void Subtract(float delta) => Value -= delta;
    public void Subtract(FloatValue delta) => Value -= delta.Value;
    public void Multiply(float factor) => Value *= factor;
    public void Multiply(FloatValue factor) => Value *= factor.Value;
    public void Divide(float divisor) => Value /= divisor;
    public void Divide(FloatValue divisor) => Value /= divisor.Value;

    public static FloatValue operator +(FloatValue left, float right)
    {
        left.Value += right;
        return left;
    }

    public static FloatValue operator +(FloatValue left, FloatValue right)
    {
        left.Value += right.Value;
        return left;
    }

    public static FloatValue operator -(FloatValue left, float right)
    {
        left.Value -= right;
        return left;
    }

    public static FloatValue operator -(FloatValue left, FloatValue right)
    {
        left.Value -= right.Value;
        return left;
    }

    public static FloatValue operator *(FloatValue left, float right)
    {
        left.Value *= right;
        return left;
    }

    public static FloatValue operator *(FloatValue left, FloatValue right)
    {
        left.Value *= right.Value;
        return left;
    }

    public static FloatValue operator /(FloatValue left, float right)
    {
        left.Value /= right;
        return left;
    }

    public static FloatValue operator /(FloatValue left, FloatValue right)
    {
        left.Value /= right.Value;
        return left;
    }

    public int CompareTo(float other) => Value.CompareTo(other);
    public int CompareTo(FloatValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
}