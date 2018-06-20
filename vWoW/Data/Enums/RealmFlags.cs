﻿using System;
using System.Collections.Generic;
using System.Text;

namespace vWoW.Data.Enums
{
    public enum RealmFlags : byte
    {
        None = 0x00,
        Invalid = 0x01,
        Offline = 0x02,
        SpecifyBuild = 0x04,
        Unknown1 = 0x08,
        Unknown2 = 0x10,
        Recommended = 0x20, // can set manually or allow client to do so by setting the population to 600.0f
        NewPlayers = 0x40, // can set manually or allow client to do so by setting the population to 200.0f
        Full = 0x80  // can set manually or allow client to do so by setting the population to 400.0f
    }
}
