using Feudal.Interfaces;
using System.Collections;

namespace Feudal.Clans;

internal class LaborManager : IEnumerable<ILabor>
{
    public static Action<Labor>? OnAddLabor;
    public static Action<Labor>? OnRemoveLabor;

    private const int unit = 100;

    private readonly List<Labor> list = new List<Labor>();
    private readonly IClan owner;

    public IEnumerator<ILabor> GetEnumerator()
    {
        return ((IEnumerable<Labor>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }

    public LaborManager(IClan clan)
    {
        owner = clan;

        for (int i = 0; i < owner.PopCount / unit; i++)
        {
            var labor = new Labor(owner);

            list.Add(labor);

            OnAddLabor?.Invoke(labor);
        }
    }
}