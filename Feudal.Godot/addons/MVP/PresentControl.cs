using Feudal.Godot.Presents;
using Feudal.Godot.UICommands;
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

    public bool IsInitialized { get; private set; } = false;

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
            if (!IsMock)
            {
                throw new Exception();
            }

            Model = MockControl.Mock;

            GD.Print($"[P]Use {this.GetType().Name} mock model");

        }

        if (!IsInitialized)
        {
            IsInitialized = true;

            Initialize(View, Model);
        }

        Update(View, Model);
    }


    internal void SendCommand(UICommand command)
    {
        if (command is not UICommand_Refresh)
        {
            GD.Print($"OnCommand:{command.type}, parameters:[{(command.parameters == null ? "" : string.Join(", ", command.parameters))}]");

            Model.OnCommand(command);
        }

        foreach (PresentBase item in list)
        {
            item.IsDirty = true;
        }
    }

    protected abstract void Initialize(TView view, TModel model);
    protected abstract void Update(TView view, TModel model);
}