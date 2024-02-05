using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Feudal.Tasks;

public class TaskManager : IReadOnlyDictionary<object, ITask>
{
    public static IFinder Finder { get; set; }

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

    public TaskManager()
    {
        Task.CalcStep = (workHoodId) =>
        {
            var workHood = Finder.FindWorkHood(workHoodId);
            if (workHood.CurrentWorking is IProgressWorking working)
            {
                return working.GetEffectValue(workHood.Id).Value;
            }
            else
            {
                throw new Exception();
            }
        };
    }

    public void CreateTask(string clanId, string workHooId)
    {
        var task = dict.Values.SingleOrDefault(x => x.WorkHood.Id == workHooId);
        if (task != null)
        {
            throw new Exception($"WorkHood{workHooId}已经关联Labor{task.Clan.Id}");
        }

        task = new Task(clanId, workHooId);
        dict.Add(task.Id, task);
    }

    public void RelaseTask(string clanId, string workHooId)
    {
        var task = dict.Values.SingleOrDefault(x => x.WorkHood.Id == workHooId);
        if (task == null)
        {
            throw new Exception($"WorkHood{workHooId}未关联Labor");
        }
        if (task.Clan.Id != clanId)
        {
            throw new Exception($"Task{task.Id}中, WorkHood{workHooId}关联Labor是{task.Clan.Id}, 不是{clanId}");
        }

        dict.Remove(task.Id);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }

    public void OnNextTurn()
    {
        foreach (Task task in dict.Values)
        {
            //task.Step = CalcStep(task);

            task.Percent += task.Step;

            if (task.Percent >= 100)
            {

                OnTaskFinsihed(task);

                dict.Remove(task.Id);
            }
        }
    }

    //private float CalcStep(Task task)
    //{
    //    var workHood = FindWorkHood(task.WorkHoodId);
    //    if (workHood.CurrentWorking is ICanFinishedWorking working)
    //    {
    //        working.Finished(workHood);
    //    }
    //    else
    //    {
    //        throw new Exception();
    //    }
    //}

    private void OnTaskFinsihed(Task task)
    {
        if (task.WorkHood.CurrentWorking is IProgressWorking working)
        {
            working.Finished(task.WorkHood);
        }
        else
        {
            throw new Exception();
        }
    }
}
