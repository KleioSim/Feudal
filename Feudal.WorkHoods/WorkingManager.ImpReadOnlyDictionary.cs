using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Feudal.WorkHoods;

public partial class WorkingManager : IReadOnlyDictionary<object, IWorking>
{
    public IWorking this[object key] => ((IReadOnlyDictionary<object, IWorking>)dict)[key];

    public IEnumerable<object> Keys => ((IReadOnlyDictionary<object, IWorking>)dict).Keys;

    public IEnumerable<IWorking> Values => ((IReadOnlyDictionary<object, IWorking>)dict).Values;

    public int Count => ((IReadOnlyCollection<KeyValuePair<object, IWorking>>)dict).Count;

    public bool ContainsKey(object key)
    {
        return ((IReadOnlyDictionary<object, IWorking>)dict).ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<object, IWorking>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<object, IWorking>>)dict).GetEnumerator();
    }

    public bool TryGetValue(object key, [MaybeNullWhen(false)] out IWorking value)
    {
        return ((IReadOnlyDictionary<object, IWorking>)dict).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }
}