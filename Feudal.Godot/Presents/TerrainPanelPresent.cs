using System;
using System.Linq;

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

        view.WorkHoodPanel.Visible = terrain.WorkHood.OptionWorkings.Any() || terrain.WorkHood.CurrentWorking != null;
        if (view.WorkHoodPanel.Visible)
        {
            view.WorkHoodPanel.Id = terrain.WorkHood.Id;
        }
    }
}