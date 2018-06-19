using System;
using System.Collections.Generic;
using System.Text;

namespace vWoW.Constants.Enums
{
    public enum LogonOpCode : byte
    {
        AUTH_LOGON_CHALLENGE = 0x00,
        AUTH_LOGON_PROOF = 0x01,
        AUTH_RECONNECT_CHALLENGE = 0x02,
        AUTH_RECONNECT_PROOF = 0x03,
        REALM_LIST = 0x10,
        SURVEY = 48,
    }
}
