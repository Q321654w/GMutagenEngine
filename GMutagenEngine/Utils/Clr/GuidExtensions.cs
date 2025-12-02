using System.Runtime.CompilerServices;

namespace GMutagenEngine.Utils.Clr
{
    /// <summary>
    /// Расширения для Guid
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Проверяет, является ли Guid пустым
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;

        /// <summary>
        /// Проверяет, не является ли Guid пустым
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotEmpty(this Guid guid) => guid != Guid.Empty;

        /// <summary>
        /// Конвертирует Guid в строку в формате base64
        /// </summary>
        public static string ToBase64(this Guid guid)
        {
            var bytes = guid.ToByteArray();
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Конвертирует Guid в строку без дефисов
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringWithoutDashes(this Guid guid) => 
            guid.ToString("N");

        /// <summary>
        /// Конвертирует Guid в массив байт
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToByteArray(this Guid guid) => guid.ToByteArray();

        /// <summary>
        /// Возвращает shortened версию Guid (первые 8 символов)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToShortString(this Guid guid) => 
            guid.ToString("N").Substring(0, 8);

        /// <summary>
        /// Сравнивает два Guid с учетом возможных форматов
        /// </summary>
        public static bool Equals(this Guid guid, string other, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(other))
                return false;

            return guid.ToString().Equals(other, comparison) ||
                   guid.ToString("N").Equals(other, comparison) ||
                   guid.ToString("D").Equals(other, comparison) ||
                   guid.ToString("B").Equals(other, comparison) ||
                   guid.ToString("P").Equals(other, comparison);
        }
    }
}