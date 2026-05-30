using System.Runtime.CompilerServices;

namespace Utils.Clr;

public static class DecimalExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Round(this decimal value, int decimals) => Math.Round(value, decimals);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Round(this decimal value) => Math.Round(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Floor(this decimal value) => Math.Floor(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Ceiling(this decimal value) => Math.Ceiling(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Abs(this decimal value) => Math.Abs(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Clamp(this decimal value, decimal min, decimal max) =>
        value < min ? min : value > max ? max : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInRange(this decimal value, decimal min, decimal max) => value >= min && value <= max;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this decimal value) => Math.Sign(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInteger(this decimal value) => value == Math.Truncate(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal FractionalPart(this decimal value) => value - Math.Truncate(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ToPercentage(this decimal value) => value * 100m;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal FromPercentage(this decimal value) => value / 100m;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this decimal value, decimal tolerance = 0.0000001m) => Math.Abs(value) < tolerance;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal PercentOf(this decimal percentage, decimal total) => (percentage / 100m) * total;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal PercentageOf(this decimal value, decimal total) => total == 0 ? 0 : (value / total) * 100m;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal IncreaseBy(this decimal value, decimal percentage) => value * (1 + percentage / 100m);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal DecreaseBy(this decimal value, decimal percentage) => value * (1 - percentage / 100m);
}