using Feudal.Commands;
using Feudal.Interfaces;

namespace Feudal.Workings;

public class DiscoverTerrain : Working, IProgressWorking
{
    public DiscoverTerrain(IWorkHood workHood) : base(workHood)
    {
    }

    public float Percent { get; set; }

    public void OnFinish(Action<ICommand> SendCommand)
    {
        if (WorkHood is not ITerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        var command = new Command_DiscoverTerrain()
        {
            Position_X = terrainWorkHood.Position.x.ToString(),
            Position_Y = terrainWorkHood.Position.y.ToString()
        };

        SendCommand(command);
    }

    public override IEffectValue GetEffectValue()
    {
        if (WorkHood is not ITerrain terrainWorkHood)
        {
            throw new Exception();
        }

        var effects = new[] { new Effect() { Desc = terrainWorkHood.TerrainType.ToString(), Percent = GetEffect(terrainWorkHood.TerrainType) } };

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
