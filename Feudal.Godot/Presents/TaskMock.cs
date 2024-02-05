using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

public class TaskMock : ITask
{
    public static int Count;

    public string Id { get; set; }

    public string Desc { get; set; }

    public float Percent { get; set; }

    public string WorkHoodId { get; set; }

    public string ClanId { get; set; }

    public float Step { get; set; }

    public IClan Clan { get; set; }

    public IWorkHood WorkHood { get; set; }

    public TaskMock()
    {
        Id = $"TASK{Count++}";

        Desc = Id;
        Percent = 33;
    }
}