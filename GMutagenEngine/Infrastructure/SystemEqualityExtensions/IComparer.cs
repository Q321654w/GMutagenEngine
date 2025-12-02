namespace GMutagenEngine.Infrastructure.SystemEqualityExtensions
{
    public interface IComparer<in T1, in T2>
    {
        int Compare(T1? obj1, T2? obj2);
    }
}