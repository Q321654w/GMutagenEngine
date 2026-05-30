using System.Collections;
using System.Reflection;
using Constants.Literals;
using GMutagenEngine.Handlers.Funcs.Interfaces;

namespace GMutagenEngine.Logging.Formatters;

public sealed class ObjectFormatter : ISyncFuncHandler<object?, string>
{
    private const string CYCLIC_REFERENCE_LITERAL = "<cyclic-reference>";

    public string Handle(object? data)
    {
        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
        return FormatValue(data, visited);
    }

    private static string FormatValue(object? value, HashSet<object> visited)
    {
        if (value is null)
            return Literals.Keywords.NULL_STRING;

        var type = value.GetType();
        if (IsSimple(type))
            return value.ToString() ?? Literals.Keywords.NULL_STRING;

        var isReferenceType = !type.IsValueType;
        if (isReferenceType && !visited.Add(value))
            return CYCLIC_REFERENCE_LITERAL;

        try
        {
            if (value is IDictionary dictionary)
                return FormatDictionary(dictionary, visited);

            if (value is IEnumerable enumerable && value is not string)
                return FormatEnumerable(enumerable, visited);

            return FormatObjectMembers(value, type, visited);
        }
        finally
        {
            if (isReferenceType)
                visited.Remove(value);
        }
    }

    private static string FormatDictionary(IDictionary dictionary, HashSet<object> visited)
    {
        if (dictionary.Count == 0)
            return Literals.Symbols.OPEN_BRACE_STRING + Literals.Symbols.CLOSE_BRACE_STRING;

        var pairs = new List<string>(dictionary.Count);
        foreach (DictionaryEntry entry in dictionary)
        {
            pairs.Add(
                FormatValue(entry.Key, visited)
                + Literals.Symbols.EQUALS_STRING
                + FormatValue(entry.Value, visited));
        }

        return JoinPairs(pairs);
    }

    private static string FormatEnumerable(IEnumerable enumerable, HashSet<object> visited)
    {
        var pairs = new List<string>();
        var index = 0;

        foreach (var item in enumerable)
        {
            pairs.Add(index + Literals.Symbols.EQUALS_STRING + FormatValue(item, visited));
            index++;
        }

        return JoinPairs(pairs);
    }

    private static string FormatObjectMembers(object value, Type type, HashSet<object> visited)
    {
        var memberAccessors = BuildMemberAccessors(type);
        if (memberAccessors.Length == 0)
            return value.ToString() ?? Literals.Keywords.NULL_STRING;

        var pairs = new List<string>(memberAccessors.Length);
        foreach (var memberAccessor in memberAccessors)
        {
            object? memberValue;
            try
            {
                memberValue = memberAccessor.Getter(value);
            }
            catch
            {
                memberValue = CYCLIC_REFERENCE_LITERAL;
            }

            pairs.Add(
                memberAccessor.Name
                + Literals.Symbols.EQUALS_STRING
                + FormatValue(memberValue, visited));
        }

        return JoinPairs(pairs);
    }

    private static string JoinPairs(IReadOnlyCollection<string> pairs)
    {
        if (pairs.Count == 0)
            return Literals.Symbols.OPEN_BRACE_STRING + Literals.Symbols.CLOSE_BRACE_STRING;

        return Literals.Symbols.OPEN_BRACE_STRING
               + Literals.Whitespace.SPACE_STRING
               + string.Join(Literals.Punctuation.COMMA_SPACE_STRING, pairs)
               + Literals.Whitespace.SPACE_STRING
               + Literals.Symbols.CLOSE_BRACE_STRING;
    }

    private static MemberAccessor[] BuildMemberAccessors(Type type)
    {
        var propertyAccessors = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(property => property.CanRead && property.GetIndexParameters().Length == 0)
            .Select(property => new MemberAccessor(property.Name, instance => property.GetValue(instance)));

        var fieldAccessors = type.GetFields(BindingFlags.Instance | BindingFlags.Public)
            .Select(field => new MemberAccessor(field.Name, instance => field.GetValue(instance)));

        return propertyAccessors
            .Concat(fieldAccessors)
            .DistinctBy(accessor => accessor.Name, StringComparer.Ordinal)
            .OrderBy(accessor => accessor.Name, StringComparer.Ordinal)
            .ToArray();
    }

    private static bool IsSimple(Type type)
    {
        return type.IsPrimitive
               || type.IsEnum
               || type == typeof(string)
               || type == typeof(decimal)
               || type == typeof(DateTime)
               || type == typeof(DateTimeOffset)
               || type == typeof(TimeSpan)
               || type == typeof(Guid);
    }

    private sealed record MemberAccessor(string Name, Func<object, object?> Getter);
}
