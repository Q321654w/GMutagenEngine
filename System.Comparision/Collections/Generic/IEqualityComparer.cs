using System.Diagnostics.CodeAnalysis;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace System.Collections.Generic;

public interface IEqualityComparer<in T1, in T2> : IEqualityComparerMark {
    bool Equals(T1? x, T2? y);
    int GetHashCode([DisallowNull] T1 obj);
    int GetHashCode([DisallowNull] T2 obj);
}
public interface IEqualityComparerMark : ISelfMark<IEqualityComparerMark> {
}
