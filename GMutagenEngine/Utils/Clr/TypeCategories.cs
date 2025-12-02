namespace GMutagenEngine.Utils.Clr
{
    [Flags]
    public enum TypeCategories
    {
        None = 0,
        
        Primitive = 1 << 0,
        Integral = 1 << 1,
        BinaryFloatingPoint = 1 << 2,
        DecimalFloatingPoint = 1 << 3,
        Boolean = 1 << 4,
        Character = 1 << 5,
        String = 1 << 6,
        DateTime = 1 << 7,
        NullablePrimitives = 1 << 8,
        Numeric = 1 << 9,
        ValueTypes = 1 << 10,
        ReferenceTypes = 1 << 11,
        Object = 1 << 12,
        Guid = 1 << 13,
        
        Enums = 1 << 14,
        Structs = 1 << 15,
        
        Arrays = 1 << 16,
        
        CollectionInterfaces = 1 << 17,
        CollectionConcrete = 1 << 18,
        Collections = 1 << 19,
        ReadOnlyCollections = 1 << 20,
        ReadWriteCollections = 1 << 21,
        DictionaryOnly = 1 << 22,
        
        Tuples = 1 << 23,
        
        Delegates = 1 << 24,
        
        Tasks = 1 << 25,
        
        Spans = 1 << 26,
        
        AllNumeric = Integral | BinaryFloatingPoint | DecimalFloatingPoint,
        AllValueTypes = Primitive | DecimalFloatingPoint | DateTime | Guid,
        AllBasicTypes = Primitive | String | DateTime | Guid | Object,
        AllCollections = CollectionInterfaces | CollectionConcrete | ReadOnlyCollections | ReadWriteCollections,
        All = (1 << 27) - 1
    }

}