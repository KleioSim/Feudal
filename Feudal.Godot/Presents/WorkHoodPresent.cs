namespace Feudal.Godot.Presents;

partial class WorkHoodPresent : PresentControl<WorkHoodView, ISessionModel>
{
    protected override void Initialize(WorkHoodView view, ISessionModel model)
    {

    }

    protected override void Update(WorkHoodView view, ISessionModel model)
    {
        view.WorkingArray.WorkHoodId = view.Id;

        //var task = model.Session.Tasks.Values.SingleOrDefault(x => x.WorkHoodId == view.Id);
        //if (task == null)
        //{
        //    view.CurrentLabor.Visible = false;
        //    return;
        //}

        //view.CurrentLabor.Visible = true;

        //var clan = model.Session.Clans.SingleOrDefault(x => x.Id == task.ClanId);
        //if (clan == null)
        //{
        //    throw new Exception();
        //}

        //view.ClanName.Text = clan.Name;
    }
}