using Godot;
using System;

public partial class GUIView : ViewControl
{
    public Label Year => GetNode<Label>("Top/VBoxContainer/TopInfos/Date/HBoxContainer/Year/Value");
    public Label Month => GetNode<Label>("Top/VBoxContainer/TopInfos/Date/HBoxContainer/Month/Value");
    public Label Day => GetNode<Label>("Top/VBoxContainer/TopInfos/Date/HBoxContainer/Day/Value");

    public Button PlayerButton => GetNode<Button>("Top/Player");
    public Label PlayerClanName => GetNode<Label>("Top/Player/VBoxContainer/ClanName");
    public Label PlayerClanPopCount => GetNode<Label>("Top/Player/VBoxContainer/ClanPopCount");

    public Button ClansButton => GetNode<Button>("Top/VBoxContainer/TopInfos/Clan");
    public Label ClansCount => GetNode<Label>("Top/VBoxContainer/TopInfos/Clan/HBoxContainer/Value");

    public Button NextTurn => GetNode<Button>("Right/NextTurn");

    public InstancePlaceholder LeftPlaceHolder => GetNode<InstancePlaceholder>("Left");

    public string PlayerClanId { get; set; }

    public override void _Ready()
    {
        base._Ready();

        PlayerButton.Pressed += () =>
        {
            var leftView = ShowLeftView();

            var clanPanelView = leftView.ShowClanPanel(PlayerClanId);
        };

        ClansButton.Pressed += () =>
        {
            var leftView = ShowLeftView();

            var clanPanelView = leftView.ShowClanArrayPanel();
        };
    }

    public void ShowLeftView_Terrain(Vector2I pos)
    {
        var leftView = ShowLeftView();

        var mainPanelView = leftView.ShowTerrainPanel(pos);
    }

    private LeftView ShowLeftView()
    {
        var leftView = LeftPlaceHolder.GetParent().GetNodeOrNull<LeftView>(nameof(LeftView));
        if (leftView == null)
        {
            leftView = LeftPlaceHolder.CreateInstance().GetNode<LeftView>(".");
            leftView.Name = nameof(LeftView);
        }

        return leftView;
    }
}
