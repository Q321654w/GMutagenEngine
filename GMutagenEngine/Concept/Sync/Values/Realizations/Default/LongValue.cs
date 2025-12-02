using System.Numerics;
using GMutagenEngine.Concept.Sync.Values.Arithmetic;
using GMutagenEngine.Concept.Sync.Values.BaseClases;

namespace GMutagenEngine.Concept.Sync.Values.Realizations.Default
{
    public class LongValue(long initial = 0) : BaseValue<long>(initial),
        IAdd<long>, IAdd<LongValue>,
        ISubtract<long>, ISubtract<LongValue>,
        IMultiply<long>, IMultiply<LongValue>,
        IDivide<long>, IDivide<LongValue>,
        IModulo<long>, IModulo<LongValue>,
#if NET7_0_OR_GREATER
        IAdditionOperators<LongValue, long, LongValue>,
        IAdditionOperators<LongValue, LongValue, LongValue>,
        ISubtractionOperators<LongValue, long, LongValue>,
        ISubtractionOperators<LongValue, LongValue, LongValue>,
        IMultiplyOperators<LongValue, long, LongValue>,
        IMultiplyOperators<LongValue, LongValue, LongValue>,
        IDivisionOperators<LongValue, long, LongValue>,
        IDivisionOperators<LongValue, LongValue, LongValue>,
#endif
        IComparable<long>, IComparable<LongValue>
    {
        public void Add(long delta) => Value += delta;
        public void Add(LongValue delta) => Value += delta.Value;
        public void Subtract(long delta) => Value -= delta;
        public void Subtract(LongValue delta) => Value -= delta.Value;
        public void Multiply(long factor) => Value *= factor;
        public void Multiply(LongValue factor) => Value *= factor.Value;
        public void Divide(long divisor) => Value /= divisor;
        public void Divide(LongValue divisor) => Value /= divisor.Value;
        public void Modulo(long divisor) => Value %= divisor;
        public void Modulo(LongValue divisor) => Value %= divisor.Value;

        public static LongValue operator +(LongValue left, long right)
        {
            left.Value += right;
            return left;
        }

        public static LongValue operator +(LongValue left, LongValue right)
        {
            left.Value += right.Value;
            return left;
        }

        public static LongValue operator -(LongValue left, long right)
        {
            left.Value -= right;
            return left;
        }

        public static LongValue operator -(LongValue left, LongValue right)
        {
            left.Value -= right.Value;
            return left;
        }

        public static LongValue operator *(LongValue left, long right)
        {
            left.Value *= right;
            return left;
        }

        public static LongValue operator *(LongValue left, LongValue right)
        {
            left.Value *= right.Value;
            return left;
        }

        public static LongValue operator /(LongValue left, long right)
        {
            left.Value /= right;
            return left;
        }

        public static LongValue operator /(LongValue left, LongValue right)
        {
            left.Value /= right.Value;
            return left;
        }

        public int CompareTo(long other) => Value.CompareTo(other);
        public int CompareTo(LongValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
    }
}