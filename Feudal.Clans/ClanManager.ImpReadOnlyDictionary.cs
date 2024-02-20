using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Feudal.Clans;

public partial class ClanManager : IReadOnlyDictionary<object, IClan>
{
    private Dictionary<object, IClan> dict = new Dictionary<object, IClan>();

    public IClan this[object key] => ((IReadOnlyDictionary<object, IClan>)dict)[key];

    public IEnumerable<object> Keys => ((IReadOnlyDictionary<object, IClan>)dict).Keys;

    public IEnumerable<IClan> Values => ((IReadOnlyDictionary<object, IClan>)dict).Values;

    public int Count => ((IReadOnlyCollection<KeyValuePair<object, IClan>>)dict).Count;

    public bool ContainsKey(object key)
    {
        return ((IReadOnlyDictionary<object, IClan>)dict).ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<object, IClan>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<object, IClan>>)dict).GetEnumerator();
    }

    public bool TryGetValue(object key, [MaybeNullWhen(false)] out IClan value)
    {
        return ((IReadOnlyDictionary<object, IClan>)dict).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }
}
