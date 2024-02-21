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

    public IWorking Working => Finder.FindWorking(workingId);

    public ILabor Labor => Finder.FindLabor(laborId);

    private string workingId;

    private string laborId;

    public Task(string laborId, string workingId)
    {
        Id = $"TASK{Count++}";

        Desc = Id;

        this.workingId = workingId;
        this.laborId = laborId;
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