using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Feudal.Tasks;

public class TaskManager : IReadOnlyDictionary<object, ITask>
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

    public void CreateTask(string clanId, string workHooId)
    {
        var task = dict.Values.SingleOrDefault(x => x.WorkHoodId == workHooId);
        if (task != null)
        {
            throw new Exception($"WorkHood{workHooId}已经关联Labor{task.ClanId}");
        }

        task = new Task(clanId, workHooId);
        dict.Add(task.Id, task);
    }

    public void RelaseTask(string clanId, string workHooId)
    {
        var task = dict.Values.SingleOrDefault(x => x.WorkHoodId == workHooId);
        if (task == null)
        {
            throw new Exception($"WorkHood{workHooId}未关联Labor");
        }
        if (task.ClanId != clanId)
        {
            throw new Exception($"Task{task.Id}中, WorkHood{workHooId}关联Labor是{task.ClanId}, 不是{clanId}");
        }

        dict.Remove(task.Id);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }
}
