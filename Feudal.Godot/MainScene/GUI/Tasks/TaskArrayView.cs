using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TaskArrayView : ViewControl
{

    [Signal]
    public delegate void ShowTerrainEventHandler(Vector2I position);

    internal ItemContainer<TaskItemView> Container;

    public TaskArrayView()
    {
        Container = new ItemContainer<TaskItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("Container/TaskItem");
        });
    }
}