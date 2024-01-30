using Feudal.Interfaces;

namespace Feudal.WorkHoods;

internal class WorkHood : IWorkHood
{
    public static int Count;

    public string Id { get; }

    public IWorking CurrentWorking { get; private set; }

    public virtual IEnumerable<IWorking> OptionWorkings
    {
        get => optionWorkings.Where(x => x != CurrentWorking);
        set
        {
            optionWorkings = value;

            if (optionWorkings.Contains(CurrentWorking))
            {
                return;
            }

            CurrentWorking = optionWorkings.FirstOrDefault();
        }
    }

    private IEnumerable<IWorking> optionWorkings;

    internal void UpdateWorkings(IEnumerable<IWorking> workings)
    {
        optionWorkings = workings;

        if (optionWorkings.Contains(CurrentWorking))
        {
            return;
        }

        CurrentWorking = optionWorkings.First();
    }

    public WorkHood()
    {
        Id = $"WorkHood_{Count++}";
    }
}