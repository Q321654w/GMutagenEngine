using GMutagenEngine.MetaData.Runtime.Marks;

namespace System.Collections.Generic;

public interface IComparer<in T1, in T2> : IComparerMark {
    int Compare(T1? obj1, T2? obj2);
}
public interface IComparerMark : ISelfMark<IComparerMark> {
}
