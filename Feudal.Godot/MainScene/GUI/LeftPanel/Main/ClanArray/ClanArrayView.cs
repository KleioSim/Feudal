using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ClanArrayView : MainPanelView
{
    internal ItemContainer<ClanItemView> Container;

    public ClanArrayView()
    {
        Container = new ItemContainer<ClanItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("VBoxContainer/VBoxContainer/DefaultItem");
        });
    }
}