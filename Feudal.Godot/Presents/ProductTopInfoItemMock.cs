﻿using Feudal.Godot.UICommands;
using Feudal.Interfaces;
using Godot;
using System;

namespace Feudal.Godot.Presents;

[Tool]
public partial class ProductTopInfoItemMock : MockControl<ProductTopInfoItemView, ISessionModel>
{
    private const ProductType defaultId = Feudal.Interfaces.ProductType.Food;

    private string productType = Enum.GetName(defaultId.GetType(), defaultId);

    [Export]
    public string ProductType
    {
        get => productType;
        set
        {
            if (productType == value)
            {
                return;
            }

            productType = value;

            View.Id = Enum.Parse<ProductType>(productType);

            Present.SendCommand(new UICommand_Refresh());
        }
    }


    [Export]
    public float Current
    {
        get
        {
            return Product.Current;
        }
        set
        {
            Product.Current = value;

            Present.SendCommand(new UICommand_Refresh());
        }
    }

    [Export]
    public float Surplus
    {
        get
        {
            return Product.Surplus;
        }
        set
        {
            Product.Surplus = value;

            Present.SendCommand(new UICommand_Refresh());
        }
    }

    private ProductMock Product => Present.Model.Session.PlayerClan.Products[Enum.Parse<ProductType>(productType)] as ProductMock;

    public override ISessionModel Mock
    {
        get
        {
            var clan = new ClanMock();

            var product = clan.MockProducts[defaultId] as ProductMock;
            product.Current = 10f;
            product.Surplus = 2.1f;

            View.Id = defaultId;

            var session = new SessionMock();
            session.PlayerClan = clan;

            return new SessionModel() { Session = session };
        }
    }
}
