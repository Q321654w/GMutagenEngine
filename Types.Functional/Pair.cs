namespace Types;

public class Pair<T1, T2>(T1? first, T2? second) : IEquatable<Pair<T1, T2>>
{
    public T1? First { get; set; } = first;
    public T2? Second { get; set; } = second;

    public bool Equals(Pair<T1, T2>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T1?>.Default.Equals(First, other.First)
               && EqualityComparer<T2?>.Default.Equals(Second, other.Second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Pair<T1, T2>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(First, Second);
    }
}

public class Pair<T>(T? first, T? second) : IEquatable<Pair<T>>
{
    public T? First { get; set; } = first;
    public T? Second { get; set; } = second;

    public bool Equals(Pair<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T?>.Default.Equals(First, other.First)
               && EqualityComparer<T?>.Default.Equals(Second, other.Second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Pair<T>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(First, Second);
    }
}