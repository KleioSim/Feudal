using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Feudal.Terrains;

public partial class TerrainManager : IReadOnlyDictionary<(int x, int y), ITerrain>
{
    public ITerrain this[(int x, int y) key] => dict[key];

    public IEnumerable<(int x, int y)> Keys => dict.Keys;

    public IEnumerable<ITerrain> Values => dict.Values;

    public int Count => dict.Count;

    public bool ContainsKey((int x, int y) key)
    {
        return dict.ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<(int x, int y), ITerrain>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue((int x, int y) key, [MaybeNullWhen(false)] out ITerrain value)
    {
        if(dict.TryGetValue(key, out Terrain terrain))
        {
            value = terrain;
            return true;
        }

        value = null;
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }
}