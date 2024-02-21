using Feudal.Commands;
using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Feudal.Tasks;

public partial class TaskManager
{
    public static IFinder Finder
    {
        get => Task.Finder;
        set => Task.Finder = value;
    }

    public static Action<ICommand> CommandSender
    {
        get => Task.CommandSender;
        set => Task.CommandSender = value;
    }

    public static Action<ITask> OnTaskAdded;
    public static Action<ITask> OnTaskRemoved;

    public void CreateTask(string laborId, string workingId)
    {
        var task = dict.Values.SingleOrDefault(x => x.Working.Id == workingId);
        if (task != null)
        {
            throw new Exception($"Working{workingId}已经关联Labor{task.Labor.Id}");
        }

        task = new Task(laborId, workingId);
        dict.Add(task.Id, task);

        OnTaskAdded?.Invoke(task);
    }

    public void RelaseTask(string laborId, string workingId)
    {
        var task = dict.Values.SingleOrDefault(x => x.Working.Id == workingId);
        if (task == null)
        {
            throw new Exception($"Working{workingId}未关联Labor");
        }
        if (task.Labor.Id != laborId)
        {
            throw new Exception($"Task{task.Id}中, Working{workingId}关联Labor是{task.Labor.Id}, 不是{laborId}");
        }

        dict.Remove(task.Id);
        OnTaskRemoved?.Invoke(task);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }

    public void OnNextTurn()
    {
        foreach (Task task in dict.Values)
        {
            task.OnNextTurn();

            if (task.IsEnd)
            {
                dict.Remove(task.Id);
                OnTaskRemoved?.Invoke(task);
            }
        }
    }
}
