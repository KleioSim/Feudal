using Feudal.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.WorkHoods;

public partial class WorkHoodManager : IReadOnlyDictionary<object, IWorkHood>
{
    public IWorkHood this[object key] => ((IReadOnlyDictionary<object, IWorkHood>)dict)[key];

    public IEnumerable<object> Keys => ((IReadOnlyDictionary<object, IWorkHood>)dict).Keys;

    public IEnumerable<IWorkHood> Values => ((IReadOnlyDictionary<object, IWorkHood>)dict).Values;

    public int Count => ((IReadOnlyCollection<KeyValuePair<object, IWorkHood>>)dict).Count;

    public bool ContainsKey(object key)
    {
        return ((IReadOnlyDictionary<object, IWorkHood>)dict).ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<object, IWorkHood>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<object, IWorkHood>>)dict).GetEnumerator();
    }

    public bool TryGetValue(object key, [MaybeNullWhen(false)] out IWorkHood value)
    {
        return ((IReadOnlyDictionary<object, IWorkHood>)dict).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }
}
