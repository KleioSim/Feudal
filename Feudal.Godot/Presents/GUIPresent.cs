using Feudal.Interfaces;
using Godot;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class GUIPresent : PresentControl<GUIView, ISessionModel>
{
    protected override void Initialize(GUIView view, ISessionModel model)
    {
        view.NextTurn.ButtonDown += () => SendCommand(new UICommand_NextTurn());
    }


    protected override void Update(GUIView view, ISessionModel model)
    {
        view.Year.Text = model.Session.Date.Year.ToString();
        view.Month.Text = model.Session.Date.Month.ToString();

        if (model.Session.Date.Day > 20)
        {
            view.Day.Text = "下";
        }
        else if (model.Session.Date.Day > 10)
        {
            view.Day.Text = "中";
        }
        else
        {
            view.Day.Text = "上";
        }

        view.PlayerClanId = model.Session.PlayerClan.Id;

        view.PlayerClanName.Text = model.Session.PlayerClan.Name;
        view.PlayerClanPopCount.Text = model.Session.PlayerClan.PopCount.ToString();

        view.ClansCount.Text = model.Session.Clans.Count().ToString();
    }
}

internal class UICommand_NextTurn : UICommand
{
    public override object[] parameters { get; }

    public override Command type => Command.NextTurn;
}
