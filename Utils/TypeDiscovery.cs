using System.Collections;

namespace Utils;

public class TypeDiscovery(Type rootType, TypeDiscoverySettings settings) : IEnumerable<Type>
{
    private readonly DefaultTypeEnumerator _enumerator = new(rootType, settings);

    public IEnumerator<Type> GetEnumerator()
    {
        var seen = new HashSet<Type>();
        var count = 0;
            
        seen.Add(rootType);

        if (settings.Options.HasFlag(BaseTypeOptions.IncludeSelf))
        {
            if (settings.Filter == null || settings.Filter.Handle(rootType))
            {
                yield return rootType;

                if (++count >= settings.TypeLimit)
                    yield break;
            }
        }

        while (_enumerator.MoveNext())
        {
            var t = _enumerator.Current;

            if (!seen.Add(t))
                continue;

            if (settings.Filter != null && !settings.Filter.Handle(t))
                continue;

            yield return t;

            if (++count >= settings.TypeLimit)
                yield break;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}