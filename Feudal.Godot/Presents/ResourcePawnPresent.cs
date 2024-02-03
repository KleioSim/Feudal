namespace Feudal.Godot.Presents;

partial class ResourcePawnPresent : PresentControl<ResourcePawnView, ISessionModel>
{
    protected override void Initialize(ResourcePawnView view, ISessionModel model)
    {
        
    }

    protected override void Update(ResourcePawnView view, ISessionModel model)
    {
        view.Label.Text = model.Session.Resources[view.Id].Name;
    }
}