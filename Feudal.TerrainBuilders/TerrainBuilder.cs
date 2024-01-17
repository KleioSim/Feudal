using Feudal.Interfaces;

namespace Feudal.TerrainBuilders;

public class TerrainBuilder
{
    static Dictionary<TerrainType, (float min, float max)> dictTerrainHeight = new Dictionary<TerrainType, (float, float)>()
    {
        {TerrainType.Lake, (-2f, -0.4f) },
        {TerrainType.Marsh, (-0.4f, 0f) },
        {TerrainType.Plain, (0f, 1f) },
        {TerrainType.Hill, (1f, 1.5f) },
        {TerrainType.Mountion, (1.5f, 3f)  },
    };

    private TerrainType mapType;
    private Random random = new Random();
    private Dictionary<(int x, int y), TerrainType> dict = new Dictionary<(int x, int y), TerrainType>();

    public static TerrainType ConvertToTerrainType(float value)
    {
        if (value > dictTerrainHeight[TerrainType.Lake].min &&
            value <= dictTerrainHeight[TerrainType.Lake].max)
        {
            return TerrainType.Lake;
        }
        else if (value > dictTerrainHeight[TerrainType.Marsh].min &&
            value <= dictTerrainHeight[TerrainType.Marsh].max)
        {
            return TerrainType.Marsh;
        }
        else if (value > dictTerrainHeight[TerrainType.Plain].min &&
            value <= dictTerrainHeight[TerrainType.Plain].max)
        {
            return TerrainType.Plain;
        }
        else if (value > dictTerrainHeight[TerrainType.Hill].min &&
            value <= dictTerrainHeight[TerrainType.Hill].max)
        {
            return TerrainType.Hill;
        }
        else if (value > dictTerrainHeight[TerrainType.Mountion].min &&
            value <= dictTerrainHeight[TerrainType.Mountion].max)
        {
            return TerrainType.Mountion;
        }
        else
        {
            throw new Exception($"Not Support Height {value}");
        }
    }

    public static IEnumerable<(int x, int y)> GetNearby((int x, int y) position)
    {
        return Enumerable.Range(position.x - 1, 3)
            .SelectMany(x => Enumerable.Range(position.y - 1, 3).Select(y => (x, y)))
            .Where(pos => pos != position);
    }


    public TerrainBuilder(TerrainType mapType)
    {
        this.mapType = mapType;
    }

    public TerrainType Build((int x, int y) position)
    {
        if (dict.TryGetValue(position, out var exitTerrain))
        {
            return exitTerrain;
        }

        if (position.x == 0 && position.y == 0)
        {
            var terrainType = TerrainType.Plain;
            dict.Add(position, terrainType);
            return terrainType;
        }

        var randomValue = random.Next(1, 101);

        if (Math.Abs(position.x) <= 1 && Math.Abs(position.y) <= 1)
        {
            if (randomValue > 30)
            {
                var terrainType = TerrainType.Plain;
                dict.Add(position, terrainType);
                return terrainType;
            }
        }

        if (dict.Count > 0 && dict.Values.Count(x => x == mapType) * 100 / dict.Count < 30)
        {
            if (randomValue > 80)
            {
                var terrainType = mapType;
                dict.Add(position, terrainType);
                return terrainType;
            }
        }

        if (randomValue >= 99)
        {
            var terrainTypes = Enum.GetValues<TerrainType>();

            var terrainType = terrainTypes[random.Next(0, terrainTypes.Length)];
            dict.Add(position, terrainType);
            return terrainType;
        }
        else if (randomValue > 96)
        {
            var terrainType = TerrainType.Plain;
            dict.Add(position, terrainType);
            return terrainType;
        }

        var min = dictTerrainHeight.Values.Select(x => x.min).Min();
        var max = dictTerrainHeight.Values.Select(x => x.max).Max();

        var randomFactor = min + (max - min) * randomValue / 100f;

        var nears = GetNearby(position).ToArray();

        var list = new List<TerrainType>();
        foreach (var near in nears)
        {
            if (dict.TryGetValue(near, out TerrainType terrainType))
            {
                list.Add(terrainType);
            }
        }

        var nearbyFactor = list.Count == 0 ? randomFactor : list.Average(x =>
        {
            var hegiht = dictTerrainHeight[x];
            return (hegiht.max + hegiht.min) / 2;
        });

        var mapTypeFactor = (dictTerrainHeight[mapType].max + dictTerrainHeight[mapType].min) / 2;

        var terrain = ConvertToTerrainType(randomFactor * 0.3f + nearbyFactor * 0.45f + mapTypeFactor * 0.25f);

        dict.Add(position, terrain);

        return terrain;
    }


    //public static void Build(ref Dictionary<(int x, int y), TerrainType> dict, TerrainType mapType, (int x, int y) position)
    //{
    //    if (position.x == 0 && position.y == 0)
    //    {
    //        dict.Add(position, TerrainType.Plain);
    //        return;
    //    }

    //    var randomValue = random.Next(1, 101);

    //    if (Math.Abs(position.x) <= 1 && Math.Abs(position.y) <= 1)
    //    {
    //        if (randomValue > 30)
    //        {
    //            dict.Add(position, TerrainType.Plain);
    //            return;
    //        }
    //    }

    //    if (dict.Count > 0 && dict.Values.Count(x => x == mapType) * 100 / dict.Count < 30)
    //    {
    //        if (randomValue > 80)
    //        {
    //            dict.Add(position, mapType);
    //            return;
    //        }
    //    }

    //    if (randomValue >= 99)
    //    {
    //        var terrainTypes = Enum.GetValues<TerrainType>();
    //        dict.Add(position, terrainTypes[random.Next(0, terrainTypes.Length)]);

    //        return;
    //    }
    //    else if (randomValue > 96)
    //    {
    //        dict.Add(position, TerrainType.Plain);
    //        return;
    //    }


    //    var min = dictTerrainHeight.Values.Select(x => x.min).Min();
    //    var max = dictTerrainHeight.Values.Select(x => x.max).Max();

    //    var randomFactor = min + (max - min) * randomValue / 100f;

    //    var nears = GetNearby(position).ToArray();

    //    var list = new List<TerrainType>();
    //    foreach (var near in nears)
    //    {
    //        if (dict.TryGetValue(near, out TerrainType terrainType))
    //        {
    //            list.Add(terrainType);
    //        }
    //    }

    //    var nearbyFactor = list.Count == 0 ? randomFactor : list.Average(x =>
    //    {
    //        var hegiht = dictTerrainHeight[x];
    //        return (hegiht.max + hegiht.min) / 2;
    //    });

    //    var mapTypeFactor = (dictTerrainHeight[mapType].max + dictTerrainHeight[mapType].min) / 2;

    //    var terrain = ConvertToTerrainType(randomFactor * 0.3f + nearbyFactor * 0.45f + mapTypeFactor * 0.25f);

    //    dict.Add(position, terrain);
    //}
}