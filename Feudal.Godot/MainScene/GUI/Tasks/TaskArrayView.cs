using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TaskArrayView : ViewControl
{
    internal ItemContainer<TaskItemView> Container;

    public TaskArrayView()
    {
        Container = new ItemContainer<TaskItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("Container/TaskItem");
        });
    }
}