using System;
using System.Collections.Generic;
using System.Text;

namespace vWoW.Data.Enums
{
    public enum RealmType : byte //Ember has it as an uint32 but Trinity uses a byte for sure
    {
        /// PvE or Normal server.
        PvE = 0,

        /// PvP server.
        PvP = 1,

        /// RP server.
        RP = 6,

        /// A PvP RP server.
        RPPvP = PvP | RP
    }
}
