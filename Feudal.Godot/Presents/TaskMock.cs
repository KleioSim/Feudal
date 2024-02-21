using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

public class TaskMock : ITask
{
    public static int Count;

    public string Id { get; set; }

    public string Desc { get; set; }

    public IClan Clan { get; set; }

    public IWorking Working { get; set; }

    public ILabor Labor { get; set; }

    public TaskMock()
    {
        Id = $"TASK{Count++}";

        Desc = Id;
    }
}