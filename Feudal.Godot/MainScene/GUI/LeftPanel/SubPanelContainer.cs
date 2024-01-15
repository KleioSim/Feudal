using Godot;
using System;
using System.Linq;

public partial class SubPanelContainer : Control
{
    public Control Content => GetNode<Control>("Content");
    public Button Close => GetNode<Button>("ControlContainer/XButton");

    internal T AddSubPanel<T>() where T : SubPanelView
    {
        this.Visible = true;

        var subPanels = Content.GetChildren().OfType<SubPanelView>().ToList();
        foreach (var panel in subPanels)
        {
            panel.QueueFree();
        }

        var subPanel = Content.GetNode<InstancePlaceholder>(typeof(T).Name).CreateInstance() as T;

        Content.MoveChild(subPanel, -1);

        return subPanel;
    }

    internal void ClosePanel()
    {
        var subPanels = Content.GetChildren().OfType<SubPanelView>().ToList();
        foreach (var panel in subPanels)
        {
            panel.QueueFree();
        }

        this.Visible = false;
    }
}