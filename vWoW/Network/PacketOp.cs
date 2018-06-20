using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Data.Enums;

namespace vWoW.Network
{
    public class PacketOp
    {
        public PacketType Type { get; set; }
        public UInt32 RawID { get; set; }

        public PacketOp(LogonOpCode logonOpCode)
        {
            this.RawID = (UInt32)logonOpCode;
            this.Type = PacketType.Logon;
        }

        public PacketOp(WorldOpCode worldOpCode)
        {
            this.RawID = (UInt32)worldOpCode;
            this.Type = PacketType.World;
        }

        public override string ToString()
        {
            string output = string.Empty;
            if (Type == PacketType.Logon)
                output = "LogonPacket " + (LogonOpCode)RawID;
            else if(Type == PacketType.World)
                output = "WorldPacket " + (WorldOpCode)RawID;
            return output;
        }

        public static bool operator ==(PacketOp packetOp1, PacketOp packetOp2)
        {
            return (packetOp1.RawID == packetOp2.RawID) && (packetOp1.Type == packetOp2.Type);
        }

        public static bool operator !=(PacketOp packetOp1, PacketOp packetOp2)
        {
            return !(packetOp1 == packetOp2);
        }

        public override int GetHashCode()
        {
            if (this.Type == PacketType.World)
                return (int)this.RawID;
            else
                return int.MaxValue - (int)this.RawID;
        }
        public override bool Equals(Object obj)
        {
            if (!(obj is PacketOp))
                return false;
            return
                (PacketOp)obj == this;
        }

    }
}
