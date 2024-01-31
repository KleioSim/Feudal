using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Feudal.Resources;

public partial class ResourceManager : IReadOnlyDictionary<object, IResource>
{
    private Dictionary<object, IResource> dict = new Dictionary<object, IResource>();

    public IResource this[object key] => ((IReadOnlyDictionary<object, IResource>)dict)[key];

    public IEnumerable<object> Keys => ((IReadOnlyDictionary<object, IResource>)dict).Keys;

    public IEnumerable<IResource> Values => ((IReadOnlyDictionary<object, IResource>)dict).Values;

    public int Count => ((IReadOnlyCollection<KeyValuePair<object, IResource>>)dict).Count;

    public bool ContainsKey(object key)
    {
        return ((IReadOnlyDictionary<object, IResource>)dict).ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<object, IResource>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<object, IResource>>)dict).GetEnumerator();
    }

    public bool TryGetValue(object key, [MaybeNullWhen(false)] out IResource value)
    {
        return ((IReadOnlyDictionary<object, IResource>)dict).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }
}