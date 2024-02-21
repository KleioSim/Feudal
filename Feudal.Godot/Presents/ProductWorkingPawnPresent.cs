using Feudal.Interfaces;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class ProductWorkingPawnPresent : PresentControl<ProductWorkingPawnView, ISessionModel>
{
    protected override void Initialize(ProductWorkingPawnView view, ISessionModel model)
    {

    }

    protected override void Update(ProductWorkingPawnView view, ISessionModel model)
    {
        var task = model.Session.Tasks.Values.Single(x =>
        {
            if (x.Working.WorkHood is ITerrainWorkHood terrainWorkHood)
            {
                return terrainWorkHood.Position == view.TerrainPosition;
            }

            return false;
        });

        var working = task.Working as IProductWorking;

        view.Label.Text = working.ProductType.ToString();

        var step = working.GetEffectValue().Value;
        view.Value.Text = (step >= 0 ? "+" : "") + step.ToString("0.0");
    }
}
