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

    public static Action<Command, string[]> CommandSender
    {
        get => Task.CommandSender;
        set => Task.CommandSender = value;
    }

    public void CreateTask(string clanId, string workingId)
    {
        var task = dict.Values.SingleOrDefault(x => x.Working.Id == workingId);
        if (task != null)
        {
            throw new Exception($"Working{workingId}已经关联Labor{task.Clan.Id}");
        }

        task = new Task(clanId, workingId);
        dict.Add(task.Id, task);
    }

    public void RelaseTask(string clanId, string workingId)
    {
        var task = dict.Values.SingleOrDefault(x => x.Working.Id == workingId);
        if (task == null)
        {
            throw new Exception($"Working{workingId}未关联Labor");
        }
        if (task.Clan.Id != clanId)
        {
            throw new Exception($"Task{task.Id}中, Working{workingId}关联Labor是{task.Clan.Id}, 不是{clanId}");
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
            task.OnNextTurn();

            if (task.IsEnd)
            {
                dict.Remove(task.Id);
            }
        }
    }
}
