﻿using Feudal.Interfaces;
using Feudal.WorkHoods;
//using Feudal.Terrains;

namespace Feudal.WorkHoods.Workings;

internal class DiscoverWorking : Working, IProgressWorking
{
    public DiscoverWorking(IWorkHood workHood) : base(workHood)
    {
    }

    public float Percent { get; set; }

    public void OnFinish(Action<Command, string[]> SendCommand)
    {
        if (WorkHood is not ITerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        SendCommand(Command.DiscoverTerrain, new string[] { terrainWorkHood.Position.x.ToString(), terrainWorkHood.Position.y.ToString() });

        //((TerrainManager)session.Terrains).SetDiscoverd(terrainWorkHood.Position);
    }

    public IEffectValue GetEffectValue()
    {
        if (WorkHood is not ITerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        var terrain = WorkHoodManager.Finder.FindTerrain(terrainWorkHood.Position);

        var effects = new[] { new Effect() { Desc = terrain.TerrainType.ToString(), Percent = GetEffect(terrain.TerrainType) } };

        var effectValue = new EffectValue() { BaseValue = 20, Effects = effects };

        return effectValue;
    }

    private float GetEffect(TerrainType terrainType)
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
}
