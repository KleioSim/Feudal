namespace Feudal.Godot.Presents;

partial class ResourceItemPresent : PresentControl<ResourceItemView, ISessionModel>
{
    protected override void Initialize(ResourceItemView view, ISessionModel model)
    {

    }

    protected override void Update(ResourceItemView view, ISessionModel model)
    {
        view.Label.Text = model.Session.Resources[view.Id].Name;
    }
}
