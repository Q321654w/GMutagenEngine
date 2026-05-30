using System.Runtime.CompilerServices;

namespace Utils.Clr;

/// <summary>
/// Расширения для char типа
/// </summary>
public static class CharExtensions
{
    /// <summary>
    /// Проверяет, является ли символ буквой
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLetter(this char c) => char.IsLetter(c);

    /// <summary>
    /// Проверяет, является ли символ цифрой
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDigit(this char c) => char.IsDigit(c);

    /// <summary>
    /// Проверяет, является ли символ буквой или цифрой
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLetterOrDigit(this char c) => char.IsLetterOrDigit(c);

    /// <summary>
    /// Проверяет, является ли символ whitespace
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsWhiteSpace(this char c) => char.IsWhiteSpace(c);

    /// <summary>
    /// Проверяет, является ли символ верхним регистром
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUpper(this char c) => char.IsUpper(c);

    /// <summary>
    /// Проверяет, является ли символ нижним регистром
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLower(this char c) => char.IsLower(c);

    /// <summary>
    /// Преобразует символ в верхний регистр
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static char ToUpper(this char c) => char.ToUpper(c);

    /// <summary>
    /// Преобразует символ в нижний регистр
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static char ToLower(this char c) => char.ToLower(c);

    /// <summary>
    /// Проверяет, является ли символ гласной (для английского алфавита)
    /// </summary>
    public static bool IsVowel(this char c)
    {
        var lower = char.ToLower(c);
        return lower == 'a' || lower == 'e' || lower == 'i' || lower == 'o' || lower == 'u';
    }

    /// <summary>
    /// Проверяет, является ли символ согласной (для английского алфавита)
    /// </summary>
    public static bool IsConsonant(this char c) => 
        char.IsLetter(c) && !c.IsVowel();

    /// <summary>
    /// Повторяет символ указанное количество раз
    /// </summary>
    public static string Repeat(this char c, int count) => 
        new string(c, count);

    /// <summary>
    /// Конвертирует символ в числовое значение
    /// </summary>
    public static int? ToInt(this char c) => 
        char.IsDigit(c) ? c - '0' : null;

    /// <summary>
    /// Возвращает позицию символа в алфавите (1-based)
    /// </summary>
    public static int? AlphabetPosition(this char c)
    {
        if (!char.IsLetter(c)) 
            return null;

        var lower = char.ToLower(c);
        return lower - 'a' + 1;
    }

    /// <summary>
    /// Проверяет, является ли символ ASCII
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAscii(this char c) => c <= 127;

    /// <summary>
    /// Проверяет, является ли символ hex-цифрой
    /// </summary>
    public static bool IsHexDigit(this char c) => 
        (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');

    /// <summary>
    /// Конвертирует hex-символ в числовое значение
    /// </summary>
    public static int? HexToInt(this char c)
    {
        if (c >= '0' && c <= '9') 
            return c - '0';
        if (c >= 'A' && c <= 'F') 
            return c - 'A' + 10;
        if (c >= 'a' && c <= 'f') 
            return c - 'a' + 10;
        return null;
    }
}