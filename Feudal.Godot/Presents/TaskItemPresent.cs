using Feudal.Interfaces;
using System;

namespace Feudal.Godot.Presents;

partial class TaskItemPresent : PresentControl<TaskItemView, ISessionModel>
{

    protected override void Initialize(TaskItemView view, ISessionModel model)
    {

    }

    protected override void Update(TaskItemView view, ISessionModel model)
    {
        var taskObj = model.Session.Tasks[view.Id];

        view.Label.Text = taskObj.Desc;
        switch (taskObj.Working)
        {
            case IProgressWorking progressWorking:
                {
                    view.ProgressWorking.Visible = true;
                    view.ProductWorking.Visible = false;
                    view.Progress.Value = progressWorking.Percent;
                }
                break;
            case IProductWorking productWorking:
                {
                    view.ProgressWorking.Visible = false;
                    view.ProductWorking.Visible = true;
                    view.ProductType.Text = productWorking.ProductType.ToString();
                    view.ProductValue.Text = productWorking.GetEffectValue().Value.ToString();
                }
                break;
            default:
                throw new Exception();
        }

    }
}

public interface ISessionModel : IModel
{
    ISession Session { get; }
}