using Feudal.Commands;
using Feudal.Interfaces;

namespace Feudal.Tasks;

class Task : ITask
{
    public static IFinder Finder { get; set; }
    public static Action<ICommand> CommandSender { get; set; }

    private static int Count;

    public string Id { get; }

    public string Desc { get; }

    public float Percent { get; set; }

    public bool IsEnd { get; set; }

    public IClan Clan => Finder.FindClan(clanId);

    public IWorking Working => Finder.FindWorking(workingId);

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
            if (progressWorking.Percent >= 100)
            {
                progressWorking.OnFinish(CommandSender);

                IsEnd = true;
            }
        }
    }
}