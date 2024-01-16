﻿using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
public partial class LeftMock : MockControl<LeftView, ISessionModel>
{
    private string mainPanelType = "NULL";
    public string MainPanelType
    {
        get => mainPanelType;
        set
        {
            if (mainPanelType == value)
            {
                return;
            }

            mainPanelType = value;

            if (mainPanelType == nameof(ClanArrayView))
            {
                View.ShowClanArrayPanel();
            }
            else if (mainPanelType == nameof(ClanPanelView))
            {
                View.ShowClanPanel(Present.Model.Session.Clans.First().Value.Id);
            }
            else if (mainPanelType == nameof(TerrainPanelView))
            {
                var terrain = Present.Model.Session.Terrains.First().Value;
                View.ShowTerrainPanel(new Vector2I(terrain.Position.x, terrain.Position.y));
            }
            else if (mainPanelType == "NULL")
            {
                View.CloseAllPanel();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        var MainPanelTypes = MainPanelView.DerivedTypes.Select(x => x.Name).ToArray();

        properties.Add(new Dictionary()
        {
            { "name", nameof(MainPanelType) },
            { "type", (int)Variant.Type.String },
            { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
            { "hint", (int)PropertyHint.Enum },
            { "hint_string", string.Join(",", MainPanelTypes.Append("NULL")) }
        });

        return properties;
    }


    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            for (int i = 0; i < 3; i++)
            {
                var clan = new ClanMock();
                clan.LaborMock.TotalCount = i * 10;
                session.MockClans.Add(clan.Id, clan);
            }

            var terrain = new MockTerrain();
            session.MockTerrains.Add(terrain.Position, terrain);

            for (int i = 0; i < 3; i++)
            {
                var resource = new MockResource();
                terrain.MockResources.Add(resource);
                session.MockResources.Add(resource.Id, resource);
            }

            var workHood = new MockWorkHood();
            terrain.WorkHoodId = workHood.Id;

            session.MockWorkHoods.Add(workHood.Id, workHood);

            for (int i = 0; i < 2; i++)
            {
                var working = new MockWorking();

                workHood.MockOptionWorkings.Add(working);
                session.MockWorkings.Add(working.Id, working);
            }

            workHood.CurrentWorking = workHood.OptionWorkings.First();

            return new ModelMock() { Session = session };
        }
    }
}