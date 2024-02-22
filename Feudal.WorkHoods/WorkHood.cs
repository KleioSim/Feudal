//using Feudal.Interfaces;

//namespace Feudal.WorkHoods;

//internal class WorkHood : IWorkHood
//{
//    public static Action<IWorking> OnAddWorking;
//    public static Action<IWorking> OnRemoveWorking;

//    public static int Count;

//    public string Id { get; }

//    public IWorking CurrentWorking { get; internal set; }

//    public virtual IEnumerable<IWorking> OptionWorkings => optionWorkings.Where(x => x != CurrentWorking);

//    private List<IWorking> optionWorkings = new List<IWorking>();

//    internal void UpdateWorkings(IEnumerable<Type> workingTypes)
//    {
//        var expiredWorkings = optionWorkings.Where(x => !workingTypes.Contains(x.GetType())).ToArray();
//        foreach (var expiredWorking in expiredWorkings)
//        {
//            optionWorkings.Remove(expiredWorking);
//            OnRemoveWorking?.Invoke(expiredWorking);
//        }

//        foreach (var workingType in workingTypes)
//        {
//            if (optionWorkings.Any(x => x.GetType() == workingType))
//            {
//                continue;
//            }

//            var newWorking = Activator.CreateInstance(workingType, new object[] { this }) as IWorking;
//            optionWorkings.Add(newWorking);
//            OnAddWorking?.Invoke(newWorking);
//        }

//        if (!optionWorkings.Contains(CurrentWorking))
//        {
//            CurrentWorking = optionWorkings.FirstOrDefault();
//        }
//    }

//    public WorkHood()
//    {
//        Id = $"WorkHood_{Count++}";
//    }
//}