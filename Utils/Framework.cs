using Types;
using Utils.Clr;

namespace Utils;

public static class Framework
{
    public static Type None { get; } = typeof(None);
    public static Type Null { get; } = typeof(Null);
        
    public static List<Type> Object { get; } =
        StandardTypes.Object;
    public static List<Type> Primitive { get; } =
        StandardTypes.ValueTypes
            .Concat(StandardTypes.String)
            .Concat(StandardTypes.Character)
            .Distinct()
            .ToList();
    public static List<Type> Dictionaries { get; } =
        StandardTypes.DictionaryOnly;
        
    public static List<Type> Collections { get; } =
        StandardTypes.CollectionInterfaces
            .Concat(StandardTypes.CollectionConcrete)
            .Distinct()
            .ToList();
    
    public static List<Type> CollectionsWithoutDictionaries { get; } =
        StandardTypes.CollectionInterfaces
            .Concat(StandardTypes.CollectionConcrete)
            .Except(StandardTypes.DictionaryOnly)
            .Distinct()
            .ToList();
}