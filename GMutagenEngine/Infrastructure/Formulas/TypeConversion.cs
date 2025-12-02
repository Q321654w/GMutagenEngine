using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public static class TypeConversion
    {
        public static double ToDouble(object input)
        {
            if (input == null)
                return 0d;

            switch (input)
            {
                case double d:
                    return d;
                case float f:
                    return f;
                case int i:
                    return i;
                case long l:
                    return l;
                case bool b:
                    return b ? 1d : 0d;
                case string s:
                    if (double.TryParse(s, System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture, out var parsed))
                        return parsed;
                    return 0d;
                default:
                    try
                    {
                        return Convert.ToDouble(input, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return 0d;
                    }
            }
        }

        public static object ConvertValue(object input, Type targetType)
        {
            if (input == null)
                return default;

            if (targetType.IsInstanceOfType(input))
                return input;

            if (targetType == typeof(float) && input is int i) return (float)i;
            if (targetType == typeof(float) && input is bool b) return b ? 1f : 0f;
            if (targetType == typeof(int) && input is bool bb) return bb ? 1 : 0;
            if (targetType == typeof(bool) && input is int ii) return ii != 0;
            if (targetType == typeof(bool) && input is float ff) return Math.Abs(ff) > float.Epsilon;

            try
            {
                return Convert.ChangeType(input, targetType);
            }
            catch
            {
                throw new InvalidCastException($"Cannot convert {input?.GetType().Name ?? Literals.NULL} to {targetType.Name}");
            }
        }
    }
}