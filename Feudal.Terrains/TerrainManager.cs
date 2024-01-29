using Feudal.Interfaces;
using Feudal.TerrainBuilders;

namespace Feudal.Terrains;

public partial class TerrainManager
{
    //public static Func<ITerrain, string> GetWorkHoodId
    //{
    //    get => Terrain.GetWorkHoodId;
    //    set
    //    {
    //        Terrain.GetWorkHoodId = value;
    //    }
    //}

    public static IFinder Finder
    {
        get => Terrain.Finder;
        set
        {
            Terrain.Finder = value;
        }
    }

    private Dictionary<(int x, int y), Terrain> dict = new Dictionary<(int x, int y), Terrain>();
    private TerrainBuilder terrainBuilder;

    public void Initialize(TerrainType terrainType)
    {
        terrainBuilder = new TerrainBuilder(terrainType);
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                var pos = (x, y);
                var terrain = new Terrain(pos, terrainBuilder.Build(pos));
                terrain.IsDiscoverd = Math.Abs(pos.x) <= 1 && Math.Abs(pos.y) <= 1;

                dict.Add(terrain.Position, terrain);
            }
        }
    }

    public void SetDiscoverd((int x, int y) position)
    {
        var terrain = dict[position];
        if (terrain.IsDiscoverd)
        {
            return;
        }

        terrain.IsDiscoverd = true;

        var nearPositions = GetRound(position);
        foreach (var nearPos in nearPositions)
        {
            if (dict.ContainsKey(nearPos))
            {
                continue;
            }

            var newTerrain = new Terrain(nearPos, terrainBuilder.Build(nearPos));
            dict.Add(newTerrain.Position, newTerrain);
        }
    }

    private IEnumerable<(int x, int y)> GetRound((int x, int y) position)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                yield return (position.x + i, position.y + j);
            }
        }
    }
}