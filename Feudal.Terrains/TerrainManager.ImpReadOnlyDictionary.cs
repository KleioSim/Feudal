using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Feudal.Terrains;

public partial class TerrainManager : IReadOnlyDictionary<(int x, int y), ITerrain>
{
    public ITerrain this[(int x, int y) key] => ((IReadOnlyDictionary<(int x, int y), ITerrain>)dict)[key];

    public IEnumerable<(int x, int y)> Keys => ((IReadOnlyDictionary<(int x, int y), ITerrain>)dict).Keys;

    public IEnumerable<ITerrain> Values => ((IReadOnlyDictionary<(int x, int y), ITerrain>)dict).Values;

    public int Count => ((IReadOnlyCollection<KeyValuePair<(int x, int y), ITerrain>>)dict).Count;

    public bool ContainsKey((int x, int y) key)
    {
        return ((IReadOnlyDictionary<(int x, int y), ITerrain>)dict).ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<(int x, int y), ITerrain>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<(int x, int y), ITerrain>>)dict).GetEnumerator();
    }

    public bool TryGetValue((int x, int y) key, [MaybeNullWhen(false)] out ITerrain value)
    {
        return ((IReadOnlyDictionary<(int x, int y), ITerrain>)dict).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dict).GetEnumerator();
    }
}