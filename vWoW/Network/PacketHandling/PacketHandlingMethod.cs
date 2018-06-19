using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Constants.Enums;

namespace vWoW.Network.PacketHandling
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PacketHandlingMethod : Attribute
    {
        public PacketOp PacketOp { get; set; }
        public PacketHandlingMethod(WorldOpCode worldOpCode)
        {
            this.PacketOp = new PacketOp(worldOpCode);
        }

        public PacketHandlingMethod(LogonOpCode logonOpCode)
        {
            this.PacketOp = new PacketOp(logonOpCode);
        }
    }
}
