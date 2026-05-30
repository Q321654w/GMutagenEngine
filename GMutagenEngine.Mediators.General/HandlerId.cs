using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Realizations.Ordered;
using GMutagenEngine.Identification.Realizations.Single;

namespace GMutagenEngine.Mediators.General;

public class HandlerId : OrderedMultipleId
{
    public HandlerId(string path, IId handlerId) : base(new SingleId<string>(path), handlerId)
    {
    }
}