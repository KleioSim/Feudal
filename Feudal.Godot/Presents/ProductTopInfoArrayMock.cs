namespace Feudal.Godot.Presents;

public partial class ProductTopInfoArrayMock : MockControl<ProductTopInfoArrayView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            return new ModelMock() { Session = session };
        }
    }
}