using Godot;
using System;

public partial class ProductTopInfoArrayView : ViewControl
{
    internal ItemContainer<ProductTopInfoItemView> Container { get; }

    public ProductTopInfoArrayView()
    {
        Container = new ItemContainer<ProductTopInfoItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("Containter/ProductTopInfoItem");
        });
    }
}
