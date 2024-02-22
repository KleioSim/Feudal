using Feudal.Interfaces;
using Feudal.WorkHoods.Workings;
using System.Reflection;

namespace Feudal.WorkHoods;

public partial class WorkingManager
{
    private Dictionary<object, IWorking> dict = new Dictionary<object, IWorking>();

    private Dictionary<string, Type> workingTypes { get; } = Assembly.GetExecutingAssembly().GetTypes().ToDictionary(x => x.Name, x => x);

    internal IEnumerable<ItemChange<IWorking>> FindWorkingChanges(ITerrain terrain)
    {
        var refreshWorkingTypes = terrain.IsDiscoverd ? terrain.Resources.SelectMany(x => x.GetWorkings()).Select(x => workingTypes[x]) : new[] { typeof(DiscoverTerrain) };

        var needRemoveWorkings = terrain.OptionWorkings.Where(x => !refreshWorkingTypes.Contains(x.GetType())).ToArray();
        var needAddWorkings = refreshWorkingTypes.Where(type => terrain.OptionWorkings.All(x => x.GetType() != type))
            .Select(type => Activator.CreateInstance(type, new object[] { this }) as IWorking)
            .ToArray();

        var list = new List<ItemChange<IWorking>>();

        foreach (var remove in needRemoveWorkings)
        {
            list.Add(new ItemChange<IWorking>() { ChangeType = ItemChangeType.Remove, Item = remove });
            dict.Remove(remove.Id);
        }

        foreach (var add in needAddWorkings)
        {
            list.Add(new ItemChange<IWorking>() { ChangeType = ItemChangeType.Add, Item = add });
            dict.Add(add!.Id, add);
        }

        return list;

    }
}
