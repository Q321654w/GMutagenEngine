using System.Runtime.CompilerServices;
using System.Text;

namespace GMutagenEngine.Utils.Clr
{
    public static class StringExtensions
    {
        #region Базовые операции
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NullToEmpty(this string str) => str ?? string.Empty;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IfNullOrEmpty(this string str, string defaultValue) => 
            string.IsNullOrEmpty(str) ? defaultValue : str;
    
        public static string Truncate(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= maxLength) 
                return str;

            return str.Substring(0, maxLength);
        }
    
        public static string TruncateWithEllipsis(this string str, int maxLength, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(str) || str.Length <= maxLength) 
                return str;

            return str.Substring(0, maxLength - ellipsis.Length) + ellipsis;
        }

        #endregion

        #region Поиск и сравнение
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this string str, string substring, StringComparison comparisonType) =>
            str?.IndexOf(substring, comparisonType) >= 0;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StartsWith(this string str, string substring, StringComparison comparisonType) =>
            str?.StartsWith(substring, comparisonType) == true;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EndsWith(this string str, string substring, StringComparison comparisonType) =>
            str?.EndsWith(substring, comparisonType) == true;
    
        public static int[] FindAllOccurrencesKMP(this string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
                return Array.Empty<int>();

            var lps = ComputeLPSArray(pattern);
            var indices = new List<int>();

            int i = 0, j = 0;
            while (i < text.Length)
            {
                if (pattern[j] == text[i])
                {
                    i++;
                    j++;
                }

                if (j == pattern.Length)
                {
                    indices.Add(i - j);
                    j = lps[j - 1];
                }
                else if (i < text.Length && pattern[j] != text[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i++;
                }
            }

            return indices.ToArray();
        }
    
        private static int[] ComputeLPSArray(string pattern)
        {
            var lps = new int[pattern.Length];
            int length = 0;
            int i = 1;

            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[length])
                {
                    length++;
                    lps[i] = length;
                    i++;
                }
                else
                {
                    if (length != 0)
                        length = lps[length - 1];
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }

            return lps;
        }
    
        public static int FastIndexOf(this string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
                return -1;

            var charTable = MakeCharTable(pattern);
            int i = pattern.Length - 1;

            while (i < text.Length)
            {
                int j = pattern.Length - 1;
                while (pattern[j] == text[i])
                {
                    if (j == 0) return i;
                    i--;
                    j--;
                }

                i += Math.Max(charTable[text[i]], pattern.Length - j);
            }

            return -1;
        }

        private static int[] MakeCharTable(string pattern)
        {
            const int alphabetSize = 256;
            var table = new int[alphabetSize];
        
            for (int i = 0; i < table.Length; i++)
                table[i] = pattern.Length;
        
            for (int i = 0; i < pattern.Length - 1; i++)
                table[pattern[i]] = pattern.Length - 1 - i;
        
            return table;
        }

        #endregion

        #region Трансформации
    
        public static IEnumerable<string> SplitByLength(this string str, int length)
        {
            if (string.IsNullOrEmpty(str) || length <= 0)
                yield break;

            for (int i = 0; i < str.Length; i += length)
                yield return str.Substring(i, Math.Min(length, str.Length - i));
        }
    
        public static string Reverse(this string str)
        {
            if (string.IsNullOrEmpty(str)) 
                return str;

            var charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    
        public static string RemoveChars(this string str, params char[] charsToRemove)
        {
            if (string.IsNullOrEmpty(str)) return str;

            var result = new StringBuilder(str.Length);
            foreach (char c in str)
            {
                if (Array.IndexOf(charsToRemove, c) == -1)
                    result.Append(c);
            }
            return result.ToString();
        }
    
        public static string SubstringBetween(this string str, string startDelimiter, string endDelimiter, 
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null) return null;

            int startIndex = str.IndexOf(startDelimiter, comparison);
            if (startIndex == -1) return null;

            startIndex += startDelimiter.Length;
            int endIndex = str.IndexOf(endDelimiter, startIndex, comparison);
            if (endIndex == -1) return null;

            return str.Substring(startIndex, endIndex - startIndex);
        }

        #endregion

        #region Валидация
    
        public static bool IsValidEmail(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            var atIndex = str.IndexOf('@');
            if (atIndex <= 0 || atIndex == str.Length - 1)
                return false;

            var dotIndex = str.LastIndexOf('.');
            if (dotIndex <= atIndex + 1 || dotIndex == str.Length - 1)
                return false;

            return true;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNumeric(this string str) => 
            double.TryParse(str, out _);
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInteger(this string str) => 
            int.TryParse(str, out _);
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGuid(this string str) => 
            Guid.TryParse(str, out _);

        #endregion

        #region Конвертации
    
        public static int? ToInt(this string str) => 
            int.TryParse(str, out int result) ? result : (int?)null;
    
        public static double? ToDouble(this string str) => 
            double.TryParse(str, out double result) ? result : (double?)null;
    
        public static decimal? ToDecimal(this string str) => 
            decimal.TryParse(str, out decimal result) ? result : (decimal?)null;
    
        public static bool? ToBoolean(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            if (bool.TryParse(str, out bool result))
                return result;
        
            return str.ToLower() switch
            {
                "1" or "yes" or "y" or "true" or "on" => true,
                "0" or "no" or "n" or "false" or "off" => false,
                _ => null
            };
        }

        #endregion
    }
}