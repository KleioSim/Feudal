using Godot;

public partial class LeftView : ViewControl
{

    public MainPanelContainer MainPanelContainer => GetNode<MainPanelContainer>("Main/HBoxContainer");
    public SubPanelContainer SubPanelContainer => GetNode<SubPanelContainer>("Sub/HBoxContainer");


    public override void _Ready()
    {
        MainPanelContainer.Close.Pressed += CloseAllPanel;

        MainPanelContainer.Next.Pressed += () => SubPanelContainer.ClosePanel();
        MainPanelContainer.Prev.Pressed += () => SubPanelContainer.ClosePanel();
        SubPanelContainer.Close.Pressed += () => SubPanelContainer.ClosePanel();

        SubPanelContainer.Visible = false;

        ClanItemView.ShowClan = (clanId) => ShowClanPanel(clanId);
        base._Ready();
    }

    internal ClanPanelView ShowClanPanel(string clanId)
    {
        var manPanel = MainPanelContainer.AddOrFindMainPanel<ClanPanelView>(x => x.Id == clanId);
        manPanel.Id = clanId;

        return manPanel;
    }

    internal ClanArrayView ShowClanArrayPanel()
    {
        var manPanel = MainPanelContainer.AddOrFindMainPanel<ClanArrayView>();
        return manPanel;
    }

    internal TerrainPanelView ShowTerrainPanel(Vector2I pos)
    {
        var manPanel = MainPanelContainer.AddOrFindMainPanel<TerrainPanelView>(x => x.TerrainPosition == pos);
        manPanel.TerrainPosition = pos;

        manPanel.WorkHoodPanel.SelectLaborButton.Pressed += () =>
        {
            var subPanel = SubPanelContainer.AddSubPanel<SelectLaborPanelView>();
            subPanel.SelectedLabor += (Id) =>
            {
                SubPanelContainer.ClosePanel();
                manPanel.WorkHoodPanel.EmitSignal(WorkHoodView.SignalName.AssginLabor, Id);
            };
        };

        return manPanel;
    }

    internal void CloseAllPanel()
    {
        MainPanelContainer.ClosePanel();
        SubPanelContainer.ClosePanel();
    }
}
