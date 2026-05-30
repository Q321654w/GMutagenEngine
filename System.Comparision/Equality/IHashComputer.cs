using System.Diagnostics.CodeAnalysis;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace System.Equality;

public interface IHashComputer : IHashComputerMark {
    int Compute([DisallowNull] object obj);
}

public interface IHashComputer<in T> : IHashComputerMark {
    int Compute([DisallowNull] T composite);
}

public interface IHashComputerMark : ISelfMark<IHashComputerMark> {
}
