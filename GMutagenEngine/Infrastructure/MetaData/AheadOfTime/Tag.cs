namespace GMutagenEngine.Infrastructure.MetaData.AheadOfTime;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
public class TagsAttribute : Attribute
{
    public TagsAttribute(params object[] tags)
    {
        
    }
}