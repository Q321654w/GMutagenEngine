namespace GMutagenEngine.Utils.Clr
{
    public static class StandardTypes
    {
        private static readonly Dictionary<TypeCategories, List<Type>> CategoryMap = new()
        {
            [TypeCategories.Primitive] = Primitive,
            [TypeCategories.Integral] = Integral,
            [TypeCategories.BinaryFloatingPoint] = BinaryFloatingPoint,
            [TypeCategories.DecimalFloatingPoint] = DecimalFloatingPoint,
            [TypeCategories.Boolean] = Boolean,
            [TypeCategories.Character] = Character,
            [TypeCategories.String] = String,
            [TypeCategories.DateTime] = DateTime,

            [TypeCategories.NullablePrimitives] = NullablePrimitives,
            [TypeCategories.Numeric] = Numeric,
            [TypeCategories.ValueTypes] = ValueTypes,

            [TypeCategories.ReferenceTypes] = ReferenceTypes,
            [TypeCategories.Object] = Object,
            [TypeCategories.Guid] = Guid,

            [TypeCategories.Enums] = Enums,
            [TypeCategories.Structs] = Structs,

            [TypeCategories.Arrays] = Arrays,

            [TypeCategories.CollectionInterfaces] = CollectionInterfaces,
            [TypeCategories.CollectionConcrete] = CollectionConcrete,
            [TypeCategories.Collections] = Collections,
            [TypeCategories.ReadOnlyCollections] = ReadOnlyCollections,
            [TypeCategories.ReadWriteCollections] = ReadWriteCollections,
            [TypeCategories.DictionaryOnly] = DictionaryOnly,

            [TypeCategories.Tuples] = Tuples,

            [TypeCategories.Delegates] = Delegates,

            [TypeCategories.Tasks] = Tasks,

            [TypeCategories.Spans] = Spans
        };

        // --------------------------------------------------------------------
        // Primitive CLR
        // --------------------------------------------------------------------
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

        // --------------------------------------------------------------------
        // Date & Time
        // --------------------------------------------------------------------
        public static List<Type> DateTime { get; } = new()
        {
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(DateOnly),
            typeof(TimeOnly)
        };

        // --------------------------------------------------------------------
        // Reference
        // --------------------------------------------------------------------
        public static List<Type> ReferenceTypes { get; } = new() { typeof(string), typeof(object) };
        public static List<Type> Object { get; } = new() { typeof(object) };
        public static List<Type> Guid { get; } = new() { typeof(Guid) };

        // --------------------------------------------------------------------
        // Nullable
        // --------------------------------------------------------------------
        public static List<Type> NullablePrimitives { get; } = Primitive
            .Select(t => typeof(Nullable<>).MakeGenericType(t))
            .ToList();

        // --------------------------------------------------------------------
        // Numeric
        // --------------------------------------------------------------------
        public static List<Type> Numeric { get; } = Integral
            .Concat(BinaryFloatingPoint)
            .Concat(DecimalFloatingPoint)
            .Distinct()
            .ToList();

        // --------------------------------------------------------------------
        // Value Types
        // --------------------------------------------------------------------
        public static List<Type> ValueTypes { get; } =
            Primitive
                .Where(t => t != typeof(string))
                .Concat(DateTime)
                .Concat(Guid)
                .Distinct()
                .ToList();

        // --------------------------------------------------------------------
        // Enums & structs
        // --------------------------------------------------------------------
        public static List<Type> Enums { get; } = new() { typeof(Enum) };
        public static List<Type> Structs { get; } = new() { typeof(ValueType) };

        // --------------------------------------------------------------------
        // Arrays
        // --------------------------------------------------------------------
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

        // --------------------------------------------------------------------
        // Collection Abstractions
        // --------------------------------------------------------------------
        public static List<Type> CollectionInterfaces { get; } = new()
        {
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
        
        // --------------------------------------------------------------------
        // Collection Implementations
        // --------------------------------------------------------------------
        public static List<Type> CollectionConcrete { get; } = new()
        {
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
            typeof(Dictionary<,>),
            typeof(IDictionary<,>),
            typeof(IReadOnlyDictionary<,>)
        };

        // --------------------------------------------------------------------
        // Tuples
        // --------------------------------------------------------------------
        public static List<Type> Tuples { get; } = new()
        {
            typeof(ValueTuple<>),
            typeof(ValueTuple<,>),
            typeof(ValueTuple<,,>),
            typeof(Tuple<>),
            typeof(Tuple<,>),
            typeof(Tuple<,,>)
        };

        // --------------------------------------------------------------------
        // Delegates
        // --------------------------------------------------------------------
        public static List<Type> Delegates { get; } = new()
        {
            typeof(Delegate),
            typeof(Action),
            typeof(Func<>)
        };

        // --------------------------------------------------------------------
        // Async types
        // --------------------------------------------------------------------
        public static List<Type> Tasks { get; } = new()
        {
            typeof(Task),
            typeof(Task<>),
            typeof(ValueTask),
            typeof(ValueTask<>)
        };

        // --------------------------------------------------------------------
        // Memory & Span
        // --------------------------------------------------------------------
        public static List<Type> Spans { get; } = new()
        {
            typeof(Span<>),
            typeof(ReadOnlySpan<>),
            typeof(Memory<>),
            typeof(ReadOnlyMemory<>)
        };
    }
}