using System;
using System.Collections.Generic;
using System.Text;

namespace vWoW.Data.Enums
{
    public enum AccountAuthorizationFlags : uint //uint32 AccountFlags; (kind of large for no reason)
    {
        None = 0,

        /// GM?
        GM = 0x01,

        /// Limited trial access
        Trial = 0x08,

        /// Arena Tournament granted access.
        ProPass = 0x00800000
    }
}
