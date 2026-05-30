using System.Runtime.CompilerServices;

namespace Utils.Clr;

public static class FloatingPointExtensions
{
    #region Float Extensions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximately(this float a, float b, float tolerance = 1e-6f) => Math.Abs(a - b) < tolerance;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN(this float value) => float.IsNaN(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinity(this float value) => float.IsInfinity(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this float value) => !float.IsNaN(value) && !float.IsInfinity(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(this float value, float min, float max) => value < min ? min : value > max ? max : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp01(this float value) => value < 0 ? 0 : value > 1 ? 1 : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(this float t, float a, float b) => a + (b - a) * t.Clamp01();

    public static float InverseLerp(this float value, float a, float b)
    {
        if (a.Approximately(b)) return 0;
        return ((value - a) / (b - a)).Clamp01();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Abs(this float value) => Math.Abs(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RoundToInt(this float value) => (int)Math.Round(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FloorToInt(this float value) => (int)Math.Floor(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CeilingToInt(this float value) => (int)Math.Ceiling(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Squared(this float value) => value * value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Pow(this float value, float power) => (float)Math.Pow(value, power);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float RadiansToDegrees(this float radians) => radians * (180f / (float)Math.PI);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float DegreesToRadians(this float degrees) => degrees * ((float)Math.PI / 180f);

    #endregion

    #region Double Extensions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximately(this double a, double b, double tolerance = 1e-12) => Math.Abs(a - b) < tolerance;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN(this double value) => double.IsNaN(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinity(this double value) => double.IsInfinity(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this double value) => !double.IsNaN(value) && !double.IsInfinity(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp(this double value, double min, double max) =>
        value < min ? min : value > max ? max : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp01(this double value) => value < 0 ? 0 : value > 1 ? 1 : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Lerp(this double t, double a, double b) => a + (b - a) * t.Clamp01();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Abs(this double value) => Math.Abs(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RoundToInt(this double value) => (int)Math.Round(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FloorToInt(this double value) => (int)Math.Floor(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CeilingToInt(this double value) => (int)Math.Ceiling(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Squared(this double value) => value * value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Pow(this double value, double power) => Math.Pow(value, power);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double RadiansToDegrees(this double radians) => radians * (180.0 / Math.PI);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DegreesToRadians(this double degrees) => degrees * (Math.PI / 180.0);

    #endregion
}