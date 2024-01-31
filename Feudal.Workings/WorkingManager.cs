using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Feudal.Workings;

public partial class WorkingManager
{
    private Dictionary<object, IWorking> dict = new Dictionary<object, IWorking>();

    public IEnumerable<IWorking> GetTerrainWorking(ITerrain terrain)
    {
        return !terrain.IsDiscoverd ? new[] { dict[(typeof(DiscoverWorking).Name)] } : terrain.Resources.Select(x => x.GetWorkings()).SelectMany(x => x);
    }

    public WorkingManager(ISession session)
    {
        IWorking working = new DiscoverWorking(session);

        dict.Add(working.Id, working);

        working = new BuildingFarm(session);

        dict.Add(working.Id, working);
    }
}

class EffectValue : IEffectValue
{
    public float BaseValue { get; init; }

    public IEnumerable<IEffect> Effects { get; init; }
}

class Effect : IEffect
{
    public string Desc { get; init; }

    public float Percent { get; init; }
}