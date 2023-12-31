﻿using Feudal.Interfaces;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

internal partial class ClanItemPresent : Present<ClanItemView, ISession>
{
    protected override ISession MockModel
    {
        get
        {
            var clan = new ClanMock();
            view.Id = clan.Id;

            var mock = new SessionMock();
            mock.ClanMocks.Add(clan);

            return mock;
        }
    }
}
