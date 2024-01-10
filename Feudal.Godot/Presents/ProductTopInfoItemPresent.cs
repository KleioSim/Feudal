using System;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class ProductTopInfoItemPresent : PresentControl<ProductTopInfoItemView, ISessionModel>
{
    protected override void Initialize(ProductTopInfoItemView view, ISessionModel model)
    {

    }

    protected override void Update(ProductTopInfoItemView view, ISessionModel model)
    {
        var product = model.Session.PlayerClan.Products[(ProductType)view.Id];
        view.Type.Text = product.Type.ToString();
        view.Value.Text = product.Current.ToString();
        view.Surplus.Text = (product.Surplus >= 0 ? "+" : "") + product.Surplus.ToString("0.0");
    }
}

internal class ClanMock : IClan
{
    public ClanMock()
    {
        Id = $"Clan_{Count++}";

        Name = Id;
        PopCount = Count * 1000;
    }

    private static int Count;

    public string Id { get; set; }

    public IReadOnlyDictionary<ProductType, IProduct> Products => MockProducts;

    public string Name { get; set; }

    public int PopCount { get; set; }

    public Dictionary<ProductType, IProduct> MockProducts = Enum.GetValues<ProductType>().ToDictionary(k => k, v => new ProductMock() { Type = v } as IProduct);
}

internal class ProductMock : IProduct
{
    public ProductType Type { get; set; }

    public float Current { get; set; }

    public float Surplus { get; set; }
}