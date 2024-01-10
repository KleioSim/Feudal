using Godot;
using System;
using System.Collections.Generic;

public abstract partial class PresentControl<TView, TModel> : PresentBase
    where TView : ViewControl
    where TModel : class, IModel
{
    public TModel Model
    {
        get => PresentManager.Model as TModel;
        set
        {
            PresentManager.Model = value;
        }
    }

    private TView View => GetParent<TView>();
    private MockControl<TView, TModel> MockControl => GetNode<MockControl<TView, TModel>>("Mock");

    public PresentControl()
    {
        this.Ready += () =>
        {
            GD.Print($"[P] {this.GetType().Name} Ready");

            IsDirty = true;

            if (IsMock)
            {
                mockManager.OnPresentReady(this);
            }
        };
    }

    public override void _Process(double delta)
    {
        if (!IsVisibleInTree())
        {
            IsDirty = true;

            return;
        }

        if (!IsDirty)
        {
            return;
        }

        IsDirty = false;

        if (Model == null)
        {
            Model = MockControl.Mock;

            GD.Print($"[P]Use {this.GetType().Name} mock model");

            Initialize(View, Model);
        }

        Update(View, Model);
    }


    internal void SendCommand(UICommand command)
    {
        Model.OnCommand(command);

        foreach (PresentBase item in list)
        {
            item.IsDirty = true;
        }
    }

    protected abstract void Initialize(TView view, TModel model);
    protected abstract void Update(TView view, TModel model);
}