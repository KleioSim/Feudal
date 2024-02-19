using Feudal.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Feudal.Tasks;

public partial class TaskManager : IReadOnlyDictionary<object, ITask>
{
    private Dictionary<object, ITask> dict = new Dictionary<object, ITask>();

    public ITask this[object key] => ((IReadOnlyDictionary<object, ITask>)dict)[key];

    public IEnumerable<object> Keys => ((IReadOnlyDictionary<object, ITask>)dict).Keys;

    public IEnumerable<ITask> Values => ((IReadOnlyDictionary<object, ITask>)dict).Values;

    public int Count => ((IReadOnlyCollection<KeyValuePair<object, ITask>>)dict).Count;

    public bool ContainsKey(object key)
    {
        return ((IReadOnlyDictionary<object, ITask>)dict).ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<object, ITask>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<object, ITask>>)dict).GetEnumerator();
    }

    public bool TryGetValue(object key, [MaybeNullWhen(false)] out ITask value)
    {
        return ((IReadOnlyDictionary<object, ITask>)dict).TryGetValue(key, out value);
    }
}
