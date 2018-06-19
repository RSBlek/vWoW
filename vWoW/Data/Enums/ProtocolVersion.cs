using System;
using System.Collections.Generic;
using System.Text;

namespace vWoW.Data.Enums
{
    public enum ProtocolVersion : byte
    {
        /// Protocol used by clients up until 1.12.3 or build 6141
        ProtocolVersionOld = 3,

        /// Protocol used for authentication for clients >= 2.4.3 or build 8606
        ProtocolVersionNew = 8
    }
}
