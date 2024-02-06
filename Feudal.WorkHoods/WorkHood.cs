using Feudal.Interfaces;

namespace Feudal.WorkHoods;

internal class WorkHood : IWorkHood
{
    public static int Count;

    public string Id { get; }

    public IWorking CurrentWorking { get; private set; }

    public virtual IEnumerable<IWorking> OptionWorkings { get; private set; }

    internal void UpdateWorkings(IEnumerable<IWorking> workings)
    {
        foreach (var working in workings)
        {
            working.WorkHood = this;
        }

        if (!workings.Contains(CurrentWorking))
        {
            CurrentWorking = workings.FirstOrDefault();
        }

        OptionWorkings = workings.Where(x => x != CurrentWorking);
    }

    public WorkHood()
    {
        Id = $"WorkHood_{Count++}";
    }
}