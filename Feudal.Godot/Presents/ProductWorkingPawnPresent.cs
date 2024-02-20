using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

public partial class ProductWorkingPawnPresent : PresentControl<ProductWorkingPawnView, ISessionModel>
{
    protected override void Initialize(ProductWorkingPawnView view, ISessionModel model)
    {

    }

    protected override void Update(ProductWorkingPawnView view, ISessionModel model)
    {
        var terrain = model.Session.Terrains[view.TerrainPosition];

        var working = terrain.WorkHood.CurrentWorking as IProductWorking;

        view.Label.Text = working.ProductType.ToString();

        var step = working.GetEffectValue().Value;
        view.Value.Text = (step >= 0 ? "+" : "") + step.ToString("0.0");
    }
}
