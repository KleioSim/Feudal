using Feudal.Interfaces;

namespace Feudal.Tasks;

class Task : ITask
{
    public static int Count;

    public string Id { get; }

    public string Desc { get; }

    public float Percent { get; set; }

    public string WorkHoodId { get; }

    public string ClanId { get; private set; }

    public Task(string clanId, string workHoodId)
    {
        Id = $"TASK{Count++}";

        Desc = Id;
        WorkHoodId = workHoodId;
        ClanId = clanId;
    }
}