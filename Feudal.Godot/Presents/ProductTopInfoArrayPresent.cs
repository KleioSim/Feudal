using System.Linq;

namespace Feudal.Godot.Presents;

public partial class ProductTopInfoArrayPresent : PresentControl<ProductTopInfoArrayView, ISessionModel>
{
    protected override void Initialize(ProductTopInfoArrayView view, ISessionModel model)
    {

    }

    protected override void Update(ProductTopInfoArrayView view, ISessionModel model)
    {
        view.Container.Refresh(model.Session.PlayerClan.Products.Keys.OfType<object>().ToHashSet());
    }
}
