using System.Diagnostics.CodeAnalysis;

namespace GMutagenEngine.Infrastructure.Equality;

public interface IHashComputer
{
    int Compute([DisallowNull] object obj);
}

public interface IHashComputer<in T>
{
    int Compute([DisallowNull] T composite);
}