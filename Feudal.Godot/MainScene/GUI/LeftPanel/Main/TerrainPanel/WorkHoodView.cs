using Godot;

public partial class WorkHoodView : ViewControl
{
    public string Id { get; set; }

    public Control LaborPanel => GetNode<Control>("VBoxContainer/LaborPanel");
    public Control SelectLabor => LaborPanel.GetNode<Control>("SelectLabor");
    public Button SelectLaborButton => SelectLabor.GetNode<Button>("Button");

    public Control CurrentLabor => LaborPanel.GetNode<Control>("CurrentLabor");
    public Button CancelLaborButton => CurrentLabor.GetNode<Button>("CancelLabor");
    public Label ClanName => CurrentLabor.GetNode<Label>("Label");

    //public WorkingArrayView WorkingArray => GetNode<WorkingArrayView>("VBoxContainer/WorkingArray");

    [Signal]
    public delegate void OccupyLaborEventHandler(string laborId);

    public WorkingItemView CurrentWorking => GetNode<WorkingItemView>("VBoxContainer/WorkingArray/CurrentWorking");

    public Control OptionWorkingsPanel => GetNode<Control>("VBoxContainer/WorkingArray/OptionControl");

    public ItemContainer<WorkingItemView> OptionWorkings { get; set; }

    public override void _Ready()
    {
        base._Ready();

        OptionWorkingsPanel.Visible = false;

        CurrentWorking.WorkHoodId = Id;
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
            item.WorkHoodId = Id;

            item.Button.Pressed += () =>
            {
                OptionWorkingsPanel.Visible = false;
            };
        };
    }
}