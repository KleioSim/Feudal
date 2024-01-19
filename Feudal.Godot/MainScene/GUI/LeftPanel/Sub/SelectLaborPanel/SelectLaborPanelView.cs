using Godot;

public partial class SelectLaborPanelView : SubPanelView
{
    [Signal]
    public delegate void SelectedLaborEventHandler(string laborId);

    internal ItemContainer<LaborItemView> Container;

    public SelectLaborPanelView()
    {
        Container = new ItemContainer<LaborItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("VBoxContainer/VBoxContainer/DefaultItem");
        });

        Container.OnAddedItem = (item) =>
        {
            item.Button.Pressed += () =>
            {
                EmitSignal(SignalName.SelectedLabor, item.Id as string);
            };
        };
    }
}
