using Feudal.Interfaces;
using System.Reflection;

namespace Feudal.Workings;

public partial class WorkingManager
{
    private Dictionary<object, IWorking> dict = new Dictionary<object, IWorking>();

    private Dictionary<IWorkHood, List<IWorking>> workhood2Workings = new Dictionary<IWorkHood, List<IWorking>>();

    internal IEnumerable<IWorking> FindWorking(ITerrain terrain)
    {
        if (!workhood2Workings.TryGetValue(terrain, out List<IWorking> workings))
        {
            workings = new List<IWorking>();
            workhood2Workings.Add(terrain, workings);
        }

        var refreshWorkingTypes = terrain.IsDiscoverd ? terrain.Resources.SelectMany(x => x.GetWorkingTypes()) : new[] { typeof(DiscoverTerrain) };

        var needRemoveWorkings = workings.Where(x => !refreshWorkingTypes.Contains(x.GetType())).ToArray();
        var needAddWorkings = refreshWorkingTypes.Where(type => workings.All(x => x.GetType() != type))
            .Select(type => Activator.CreateInstance(type, new object[] { terrain }) as IWorking)
            .ToArray();

        foreach (var remove in needRemoveWorkings)
        {
            dict.Remove(remove.Id);
            workings.Remove(remove);
        }

        foreach (var add in needAddWorkings)
        {
            dict.Add(add!.Id, add);
            workings.Add(add);
        }

        return workings;
    }
}
