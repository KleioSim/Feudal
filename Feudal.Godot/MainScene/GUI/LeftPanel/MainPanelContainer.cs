using Godot;
using System;
using System.Linq;

public partial class MainPanelContainer : Control
{
    public Button Close => GetNode<Button>("ControlContainer/XButton");
    public Button Next => GetNode<Button>("ControlContainer/>Button");
    public Button Prev => GetNode<Button>("ControlContainer/<Button");
    public Control Content => GetNode<Control>("Content");

    public override void _Ready()
    {
        Prev.Pressed += () =>
        {
            var mainPanels = Content.GetChildren().OfType<MainPanelView>().ToArray();
            var index = Array.FindIndex(mainPanels, x => x.Visible);

            mainPanels[index].Visible = false;
            mainPanels[index - 1].Visible = true;

            Prev.Disabled = index == 1;
            Next.Disabled = false;

        };

        Next.Pressed += () =>
        {
            var mainPanels = Content.GetChildren().OfType<MainPanelView>().ToArray();
            var index = Array.FindIndex(mainPanels, x => x.Visible);

            mainPanels[index].Visible = false;
            mainPanels[index + 1].Visible = true;

            Next.Disabled = index == mainPanels.Length - 2;
            Prev.Disabled = false;
        };


        base._Ready();
    }

    internal T AddOrFindMainPanel<T>(Predicate<T> predicate = null) where T : MainPanelView
    {
        this.Visible = true;

        var mainPanels = Content.GetChildren().OfType<MainPanelView>().ToList();
        var index = mainPanels.FindIndex(x => x.Visible);

        if (index != -1)
        {
            var needRemoves = mainPanels.Skip(index + 1).ToArray();
            foreach (var item in needRemoves)
            {
                mainPanels.Remove(item);

                var control = item as Control;
                Content.RemoveChild(control);
                control.QueueFree();
            }

            mainPanels[index].Visible = false;
        }

        var manPanel = mainPanels.OfType<T>().SingleOrDefault(x => predicate != null ? predicate(x) : true);
        if (manPanel == null)
        {
            manPanel = Content.GetNode<InstancePlaceholder>(typeof(T).Name).CreateInstance() as T;
        }

        manPanel.Visible = true;

        Content.MoveChild(manPanel, -1);

        Next.Disabled = true;
        Prev.Disabled = Content.GetChildren().OfType<MainPanelView>().Count() <= 1;

        return manPanel;
    }

    internal void ClosePanel()
    {
        var mainPanels = Content.GetChildren().OfType<MainPanelView>().ToList();
        foreach (var panel in mainPanels)
        {
            panel.QueueFree();
        }

        this.Visible = false;
    }
}