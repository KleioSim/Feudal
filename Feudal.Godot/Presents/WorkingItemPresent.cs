﻿using Feudal.Interfaces;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingItemPresent : PresentControl<WorkingItemView, ISessionModel>
{
    protected override void Initialize(WorkingItemView view, ISessionModel model)
    {
        view.TooltipTrigger.funcGetToolTipString = () => GetWorkingEffectDescString(view, model);
    }

    protected override void Update(WorkingItemView view, ISessionModel model)
    {
        var working = view.Id as IWorking;
        view.Button.Text = working.Name;

        switch (working)
        {
            case IProgressWorking progressWorking:
                {
                    view.ProgressPanel.Visible = true;
                    view.ProductPanel.Visible = false;

                    var task = model.Session.Tasks.Values.SingleOrDefault(x => x.WorkHood == working.WorkHood);
                    if (task != null && progressWorking == working.WorkHood.CurrentWorking)
                    {
                        view.ProgressBar.Value = task.Percent;
                    }
                    else
                    {
                        view.ProgressBar.Value = 0;
                    }

                    var step = progressWorking.GetEffectValue().Value;
                    view.Step.Text = (step >= 0 ? "+" : "") + step.ToString("0.0");
                }
                break;
            case IProductWorking productWorking:
                {
                    view.ProductPanel.Visible = true;
                    view.ProgressPanel.Visible = false;


                    view.ProductType.Text = productWorking.ProductType.ToString();

                    var productValue = productWorking.GetEffectValue(working.WorkHood.Id).Value;
                    view.ProductCount.Text = (productValue >= 0 ? "+" : "") + productValue.ToString("0.0");
                }
                break;
            default:
                throw new Exception();
        }
    }

    private string GetWorkingEffectDescString(WorkingItemView view, ISessionModel model)
    {
        var working = view.Id as IProgressWorking;
        var effectValue = working.GetEffectValue();

        return $"BaseValue : {effectValue.BaseValue}\n"
            + string.Join("\n", effectValue.Effects.Select(x => "    " + x.Desc + " " + (x.Percent >= 0 ? "+" : "") + x.Percent.ToString("0.0") + "%"));
    }
}
