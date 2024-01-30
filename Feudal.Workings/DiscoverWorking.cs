using Feudal.Interfaces;
using Feudal.Terrains;

namespace Feudal.Workings;

internal class DiscoverWorking : Working, IProgressWorking
{
    public DiscoverWorking(ISession session) : base(session)
    {
    }

    public void Finished(IWorkHood workHood)
    {
        if (workHood is not ITerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        ((TerrainManager)session.Terrains).SetDiscoverd(terrainWorkHood.Position);
    }

    public float GetStep(IWorkHood workHood)
    {
        if (workHood is not ITerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        var baseValue = 20f;
        var terrain = session.Terrains[terrainWorkHood.Position];

        var effect = GetEffect(terrain.TerrainType);
        return baseValue * (1 + effect);
    }

    public float GetEffect(TerrainType terrainType)
    {
        switch (terrainType)
        {
            case TerrainType.Plain:
                return 0;
            case TerrainType.Hill:
                return -50f;
            case TerrainType.Mountion:
                return -90f;
            case TerrainType.Lake:
                return -30f;
            case TerrainType.Marsh:
                return -60f;
            default:
                throw new Exception();
        }
    }

    public IEffectValue GetEffectValue(string workHoodId)
    {
        var workHood = session.WorkHoods[workHoodId];

        if (workHood is not ITerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        var terrain = session.Terrains[terrainWorkHood.Position];

        var effects = new[] { new Effect() { Desc = terrain.TerrainType.ToString(), Percent = GetEffect(terrain.TerrainType) } };

        var effectValue = new EffectValue() { BaseValue = 20, Effects = effects };

        return effectValue;
    }
}
