using Godot;

public partial class ResourceArrayView : ViewControl
{
    internal ItemContainer<ResourceItemView> Container;

    internal (int x, int y) TerrainPos { get; set; }

    public ResourceArrayView()
    {
        Container = new ItemContainer<ResourceItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("MarginContainer/HBoxContainer/Item");
        });
    }
}
