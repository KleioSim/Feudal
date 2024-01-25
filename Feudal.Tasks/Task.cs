using Feudal.Interfaces;

namespace Feudal.Tasks;

class Task : ITask
{
    public static Func<string, float> CalcStep;
    private static int Count;

    public string Id { get; }

    public string Desc { get; }

    public float Percent { get; set; }

    public string WorkHoodId { get; }

    public string ClanId { get; private set; }

    public float Step => CalcStep(WorkHoodId);

    public Task(string clanId, string workHoodId)
    {
        Id = $"TASK{Count++}";

        Desc = Id;
        WorkHoodId = workHoodId;
        ClanId = clanId;
    }
}