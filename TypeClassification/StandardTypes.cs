using System.Collections;

namespace Utils.Clr;

public static class StandardTypes
{
    public static List<Type> Primitive { get; } = new()
    {
        typeof(bool),
        typeof(byte),
        typeof(sbyte),
        typeof(char),
        typeof(decimal),
        typeof(double),
        typeof(float),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(short),
        typeof(ushort)
    };

    public static List<Type> Integral { get; } = new()
    {
        typeof(byte),
        typeof(sbyte),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(short),
        typeof(ushort)
    };

    public static List<Type> BinaryFloatingPoint { get; } = new()
    {
        typeof(float),
        typeof(double)
    };

    public static List<Type> DecimalFloatingPoint { get; } = new()
    {
        typeof(decimal)
    };

    public static List<Type> Boolean { get; } = new() { typeof(bool) };
    public static List<Type> Character { get; } = new() { typeof(char) };
    public static List<Type> String { get; } = new() { typeof(string) };
    
    public static List<Type> DateTime { get; } = new()
    {
        typeof(DateTime),
        typeof(DateTimeOffset),
        typeof(TimeSpan),
        typeof(DateOnly),
        typeof(TimeOnly)
    };
    
    public static List<Type> ReferenceTypes { get; } = new() { typeof(string), typeof(object) };
    public static List<Type> Object { get; } = new() { typeof(object) };
    public static List<Type> Guid { get; } = new() { typeof(Guid) };
    
    public static List<Type> NullablePrimitives { get; } = Primitive
        .Select(t => typeof(Nullable<>).MakeGenericType(t))
        .ToList();
    
    public static List<Type> Numeric { get; } = Integral
        .Concat(BinaryFloatingPoint)
        .Concat(DecimalFloatingPoint)
        .Distinct()
        .ToList();
    public static List<Type> ValueTypes { get; } =
        Primitive
            .Where(t => t != typeof(string))
            .Concat(DateTime)
            .Concat(Guid)
            .Distinct()
            .ToList();
    public static List<Type> Enums { get; } = new() { typeof(Enum) };
    public static List<Type> Structs { get; } = new() { typeof(ValueType) };
    
    public static List<Type> Arrays { get; } = new()
    {
        typeof(Array),
        typeof(bool[]),
        typeof(sbyte[]),
        typeof(byte[]),
        typeof(short[]),
        typeof(ushort[]),
        typeof(int[]),
        typeof(uint[]),
        typeof(long[]),
        typeof(ulong[]),
        typeof(float[]),
        typeof(double[]),
        typeof(decimal[]),
        typeof(char[]),
        typeof(string[]),
        typeof(object[])
    };
    
    public static List<Type> CollectionInterfaces { get; } = new()
    {
        typeof(IEnumerable),
        typeof(IEnumerable<>),
        typeof(IReadOnlyCollection<>),
        typeof(IReadOnlyList<>),
        typeof(ICollection<>),
        typeof(IList<>),
        typeof(IDictionary<,>)
    };

    public static List<Type> ReadOnlyCollections { get; } = new()
    {
        typeof(IEnumerable<>),
        typeof(IReadOnlyCollection<>),
        typeof(IReadOnlyList<>),
        typeof(IReadOnlyDictionary<,>)
    };
    
    public static List<Type> CollectionConcrete { get; } = new()
    {
        typeof(Array),
        typeof(List<>),
        typeof(Dictionary<,>),
        typeof(HashSet<>),
        typeof(Queue<>),
        typeof(Stack<>),
        typeof(LinkedList<>)
    };
        
    public static List<Type> ReadWriteCollections { get; } =
        CollectionConcrete
            .Concat([typeof(ICollection<>)])
            .Concat([typeof(IList<>)]).ToList();

    public static List<Type> Collections { get; } =
        CollectionInterfaces.Concat(CollectionConcrete).Distinct().ToList();

    public static List<Type> DictionaryOnly { get; } = new()
    {
        typeof(IDictionary),
        typeof(Dictionary<,>),
        typeof(IDictionary<,>),
        typeof(IReadOnlyDictionary<,>)
    };
    
    public static List<Type> Tuples { get; } = new()
    {
        typeof(ValueTuple<>),
        typeof(ValueTuple<,>),
        typeof(ValueTuple<,,>),
        typeof(Tuple<>),
        typeof(Tuple<,>),
        typeof(Tuple<,,>)
    };

    public static List<Type> Delegates { get; } = new()
    {
        typeof(Delegate),
        typeof(Action),
        typeof(Func<>)
    };
    
    public static List<Type> Tasks { get; } = new()
    {
        typeof(Task),
        typeof(Task<>),
        typeof(ValueTask),
        typeof(ValueTask<>)
    };

    public static List<Type> Spans { get; } = new()
    {
        typeof(Span<>),
        typeof(ReadOnlySpan<>),
        typeof(Memory<>),
        typeof(ReadOnlyMemory<>)
    };
}