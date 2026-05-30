using System.Runtime.CompilerServices;

namespace Utils.Clr;

/// <summary>
/// Расширения для boolean типа
/// </summary>
public static class BooleanExtensions
{
    /// <summary>
    /// Возвращает указанное значение в зависимости от булевого флага
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Then<T>(this bool condition, T trueValue, T falseValue) => 
        condition ? trueValue : falseValue;

    /// <summary>
    /// Выполняет указанное действие, если условие истинно
    /// </summary>
    public static void IfTrue(this bool condition, Action action)
    {
        if (condition)
            action();
    }

    /// <summary>
    /// Выполняет указанное действие, если условие ложно
    /// </summary>
    public static void IfFalse(this bool condition, Action action)
    {
        if (!condition)
            action();
    }

    /// <summary>
    /// Выполняет одно из двух действий в зависимости от условия
    /// </summary>
    public static void IfTrueOrFalse(this bool condition, Action trueAction, Action falseAction)
    {
        if (condition)
            trueAction();
        else
            falseAction();
    }

    /// <summary>
    /// Возвращает противоположное значение
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Not(this bool condition) => !condition;

    /// <summary>
    /// Логическое И (удобно для цепочки условий)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool And(this bool condition, bool other) => condition && other;

    /// <summary>
    /// Логическое ИЛИ (удобно для цепочки условий)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Or(this bool condition, bool other) => condition || other;

    /// <summary>
    /// Логическое исключающее ИЛИ (XOR)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Xor(this bool condition, bool other) => condition ^ other;

    /// <summary>
    /// Конвертирует булево значение в целое число
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt(this bool condition) => condition ? 1 : 0;

    /// <summary>
    /// Конвертирует булево значение в строку с пользовательскими представлениями
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString(this bool condition, string trueString, string falseString) => 
        condition ? trueString : falseString;

    /// <summary>
    /// Конвертирует булево значение в "Yes"/"No"
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToYesNo(this bool condition) => condition ? "Yes" : "No";

    /// <summary>
    /// Конвертирует булево значение в "Enabled"/"Disabled"
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToEnabledDisabled(this bool condition) => condition ? "Enabled" : "Disabled";

    /// <summary>
    /// Конвертирует булево значение в "True"/"False"
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToTrueFalse(this bool condition) => condition ? "True" : "False";
}