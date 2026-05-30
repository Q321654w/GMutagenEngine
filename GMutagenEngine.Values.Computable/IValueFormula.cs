using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Values.Interfaces;

namespace GMutagenEngine.Values.Computable;

public interface IValueFormula<TValue> : IValue<TValue>, IValueFormulaMark {
}
public interface IValueFormulaMark : ISelfMark<IValueFormulaMark> {
}
