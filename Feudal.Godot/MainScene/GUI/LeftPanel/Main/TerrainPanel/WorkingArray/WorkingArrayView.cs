using Feudal.Godot.Presents;
using Godot;
using System;

public partial class WorkingArrayView : ViewControl
{
    public string WorkHoodId { get; set; }

    public WorkingItemView CurrentWorking => GetNode<WorkingItemView>("CurrentWorking");

    public Control OptionWorkingsPanel => GetNode<Control>("OptionControl");

    public ItemContainer<WorkingItemView> OptionWorkings { get; set; }

    public override void _Ready()
    {
        base._Ready();

        OptionWorkingsPanel.Visible = false;

        CurrentWorking.Button.Pressed += () =>
        {
            OptionWorkingsPanel.Visible = !OptionWorkingsPanel.Visible;
        };

        OptionWorkings = new ItemContainer<WorkingItemView>(() =>
        {
            return GetNode<InstancePlaceholder>("OptionControl/VBoxContainer/OptionWorkingItem");
        });

        OptionWorkings.OnAddedItem = (item) =>
        {
            item.Button.Pressed += () =>
            {
                OptionWorkingsPanel.Visible = false;
            };
        };
    }
}
