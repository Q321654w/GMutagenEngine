using System.Diagnostics.CodeAnalysis;

namespace GMutagenEngine.Infrastructure.SystemEqualityExtensions
{
    public interface IEqualityComparer<in T1, in T2>
    {
        bool Equals(T1? x, T2? y);
        int GetHashCode([DisallowNull] T1 obj);
        int GetHashCode([DisallowNull] T2 obj);
    }
}