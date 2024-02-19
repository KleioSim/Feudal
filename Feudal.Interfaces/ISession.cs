namespace Feudal.Interfaces;

using System.Collections.Generic;

public interface ISession
{
    IClan PlayerClan { get; }
    IDate Date { get; }

    IReadOnlyDictionary<object, ITask> Tasks { get; }
    IReadOnlyDictionary<object, IResource> Resources { get; }
    IReadOnlyDictionary<(int x, int y), ITerrain> Terrains { get; }
    IReadOnlyDictionary<object, IWorkHood> WorkHoods { get; }
    IReadOnlyDictionary<object, IClan> Clans { get; }

    void OnCommand(Command command, string[] parameters);
}
