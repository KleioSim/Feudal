using System;

namespace Feudal.Godot.Presents;

partial class TerrainPanelPresent : PresentControl<TerrainPanelView, ISessionModel>
{
    protected override void Initialize(TerrainPanelView view, ISessionModel model)
    {

    }

    protected override void Update(TerrainPanelView view, ISessionModel model)
    {
        var terrain = model.Session.Terrains[(view.TerrainPosition.X, view.TerrainPosition.Y)];

        view.Title.Text = terrain.TerrainType.ToString();

        view.ResourceArray.Visible = terrain.IsDiscoverd;

        view.WorkHoodPanel.Visible = (terrain.WorkHoodId != null);
        if (view.WorkHoodPanel.Visible)
        {
            var workHood = model.Session.WorkHoods[terrain.WorkHoodId];
            view.WorkHoodPanel.Id = workHood != null ? workHood.Id : null;
        }
    }
}