using Feudal.Interfaces;

namespace Feudal.Tasks;

class Task : ITask
{
    private static int Count;

    public string Id { get; }

    public string Desc { get; }

    public float Percent { get; set; }

    public bool IsEnd { get; set; }

    public IClan Clan => TaskManager.Finder.FindClan(clanId);

    //public IWorkHood WorkHood => TaskManager.Finder.FindWorkHood(workHoodId);

    public IWorking Working => TaskManager.Finder.FindWorking(workingId);

    private string workingId;

    private string clanId;

    public Task(string clanId, string workingId)
    {
        Id = $"TASK{Count++}";

        Desc = Id;

        this.workingId = workingId;
        this.clanId = clanId;
    }

    internal void OnNextTurn()
    {
        if (Working is IProgressWorking progressWorking)
        {
            progressWorking.Percent += progressWorking.GetEffectValue().Value;
        }
    }
}