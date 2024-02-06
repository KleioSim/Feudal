using Feudal.Interfaces;

namespace Feudal.Tasks;

class Task : ITask
{
    public static Func<string, float> CalcStep;
    private static int Count;

    public string Id { get; }

    public string Desc { get; }

    public float Percent { get; set; }

    public float Step => CalcStep(WorkHood.Id);

    public IClan Clan => TaskManager.Finder.FindClan(clanId);

    public IWorkHood WorkHood => TaskManager.Finder.FindWorkHood(workHoodId);

    private string workHoodId;

    private string clanId;

    public Task(string clanId, string workHoodId)
    {
        Id = $"TASK{Count++}";

        Desc = Id;

        this.workHoodId = workHoodId;
        this.clanId = clanId;
    }
}