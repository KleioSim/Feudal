using Godot;

public partial class WorkHoodView : ViewControl
{
    public object Id { get; set; }

    public Control LaborPanel => GetNode<Control>("VBoxContainer/LaborPanel");
    public Control SelectLabor => LaborPanel.GetNode<Control>("SelectLabor");
    public Button SelectLaborButton => SelectLabor.GetNode<Button>("Button");

    public Control CurrentLabor => LaborPanel.GetNode<Control>("CurrentLabor");
    public Button CancelLaborButton => CurrentLabor.GetNode<Button>("CancelLabor");
    public Label ClanName => CurrentLabor.GetNode<Label>("Label");

    [Signal]
    public delegate void OccupyLaborEventHandler(string laborId);

    [Signal]
    public delegate void SwitchWorkingEventHandler(WorkingItemView workingItem);

    public WorkingItemView CurrentWorking => GetNode<WorkingItemView>("VBoxContainer/WorkingArray/CurrentWorking");

    public Control OptionWorkingsPanel => GetNode<Control>("VBoxContainer/WorkingArray/OptionControl");

    public ItemContainer<WorkingItemView> OptionWorkings { get; set; }

    public override void _Ready()
    {
        base._Ready();

        OptionWorkingsPanel.Visible = false;

        CurrentWorking.Button.Pressed += () =>
        {
            OptionWorkingsPanel.Visible = !OptionWorkingsPanel.Visible;
        };

        OptionWorkings = new ItemContainer<WorkingItemView>(() =>
        {
            return OptionWorkingsPanel.GetNode<InstancePlaceholder>("VBoxContainer/OptionWorkingItem");
        });

        OptionWorkings.OnAddedItem = (item) =>
        {
            item.Button.Pressed += () =>
            {
                OptionWorkingsPanel.Visible = false;

                CurrentWorking.Id = item.Id;

                EmitSignal(SignalName.SwitchWorking, item);
            };
        };
    }
}