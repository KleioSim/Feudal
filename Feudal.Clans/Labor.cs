using Feudal.Interfaces;
using System.Collections.Generic;

namespace Feudal.Clans;

internal class Labor : ILabor
{
    private static int count;

    public ITask Task => Clan.Finder.FindTaskByLabor(this);

    public string Id { get; }

    public IClan From { get; }

    public Labor(IClan from)
    {
        this.From = from;

        Id = $"{from.Id}_LABOR_{count++}";
    }
}
