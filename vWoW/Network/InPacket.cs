using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using vWoW.Constants.Enums;

namespace vWoW.Network
{
    public class InPacket : BinaryReader
    {
         public PacketOp PacketOp { get; }
        public InPacket(byte[] data, bool isWorld) : base(new MemoryStream(data))
        {
            if (isWorld)
                PacketOp = new PacketOp((WorldOpCode)base.ReadUInt32());
            else
                PacketOp = new PacketOp((LogonOpCode)base.ReadUInt16());
        }


    }
}
