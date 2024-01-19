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

    public WorkingArrayView WorkingArray => GetNode<WorkingArrayView>("VBoxContainer/WorkingArray");

    [Signal]
    public delegate void OccupyLaborEventHandler(string laborId);
}