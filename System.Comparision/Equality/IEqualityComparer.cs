using GMutagenEngine.MetaData.Runtime.Marks;

namespace System.Equality;

public interface IEqualityComparer : IEqualityComparer<object>, IEqualityComparerMark {
}

public interface IEqualityComparer<in T> : IEqualityComparer<T, T>, IEqualityComparerMark {
}

public interface IEqualityComparer<in T1, in T2> : IEqualityComparerMark {
    bool Equals(T1? obj1, T2? obj2);
}

public interface IEqualityComparerMark : ISelfMark<IEqualityComparerMark> {
}
