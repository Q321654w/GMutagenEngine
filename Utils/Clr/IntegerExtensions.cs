using System.Runtime.CompilerServices;

namespace Utils.Clr;

public static class IntegerExtensions
{
    #region Int32 Extensions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this int value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this int value) => (value & 1) == 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInRange(this int value, int min, int max) => value >= min && value <= max;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPowerOfTwo(this int value) => value > 0 && (value & (value - 1)) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Abs(this int value) => Math.Abs(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Inverse(this int value) => ~value + 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clamp(this int value, int min, int max) => value < min ? min : value > max ? max : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this int value) => Math.Sign(value);

    public static bool IsPrime(this int value)
    {
        if (value <= 1) return false;
        if (value == 2) return true;
        if (value % 2 == 0) return false;

        var boundary = (int)Math.Sqrt(value);

        for (int i = 3; i <= boundary; i += 2)
            if (value % i == 0)
                return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Squared(this int value) => value * value;

    public static void Times(this int count, Action action)
    {
        for (int i = 0; i < count; i++)
            action();
    }

    public static void Times(this int count, Action<int> action)
    {
        for (int i = 0; i < count; i++)
            action(i);
    }

    public static IEnumerable<int> To(this int start, int end)
    {
        var step = start <= end ? 1 : -1;
        for (int i = start; i != end + step; i += step)
            yield return i;
    }

    public static string ToString(this int value, int radix, int minLength = 1)
    {
        if (radix < 2 || radix > 36)
            throw new ArgumentException("Radix must be between 2 and 36");

        const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        if (value == 0) return new string('0', minLength);

        var result = new char[32];
        var index = 31;
        var isNegative = value < 0;
        var unsignedValue = isNegative ? -value : value;

        while (unsignedValue > 0)
        {
            result[index--] = chars[unsignedValue % radix];
            unsignedValue /= radix;
        }

        if (isNegative)
            result[index--] = '-';

        var length = 31 - index;
        if (length < minLength)
        {
            var padding = minLength - length;
            for (int i = 0; i < padding; i++)
                result[index--] = '0';
            length = minLength;
        }

        return new string(result, index + 1, length);
    }

    public static string ToBinaryString(this int value, bool includeLeadingZeros = false)
    {
        var binary = Convert.ToString(value, 2);
        return includeLeadingZeros ? binary.PadLeft(32, '0') : binary;
    }

    public static string ToHexString(this int value, bool uppercase = true)
    {
        return value.ToString(uppercase ? "X" : "x");
    }

    #endregion

    #region Long Extensions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this long value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this long value) => (value & 1) == 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInRange(this long value, long min, long max) => value >= min && value <= max;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Abs(this long value) => Math.Abs(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Inverse(this long value) => ~value + 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Clamp(this long value, long min, long max) => value < min ? min : value > max ? max : value;

    #endregion

    #region Short Extensions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this short value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this short value) => (value & 1) == 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInRange(this short value, short min, short max) => value >= min && value <= max;

    #endregion

    #region Byte Extensions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this byte value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this byte value) => (value & 1) == 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInRange(this byte value, byte min, byte max) => value >= min && value <= max;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte SetBit(this byte value, int bitPosition, bool set)
    {
        if (bitPosition < 0 || bitPosition > 7)
            throw new ArgumentOutOfRangeException(nameof(bitPosition));

        return set ? (byte)(value | (1 << bitPosition)) : (byte)(value & ~(1 << bitPosition));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(this byte value, int bitPosition)
    {
        if (bitPosition < 0 || bitPosition > 7)
            throw new ArgumentOutOfRangeException(nameof(bitPosition));

        return (value & (1 << bitPosition)) != 0;
    }

    #endregion
}