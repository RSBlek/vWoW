using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using vWoW.Constants.Enums;

namespace vWoW.Network
{
    public class OutPacket : BinaryWriter
    {
        PacketOp packetOp;
        public OutPacket(WorldOpCode opcode) : base(new MemoryStream())
        {
            packetOp = new PacketOp(opcode);
            this.Write((uint)opcode);
        }

        public OutPacket(LogonOpCode opcode) : base(new MemoryStream())
        {
            packetOp = new PacketOp(opcode);
            this.Write((byte)opcode);
        }

        public byte[] GetBytes()
        {
            return ((MemoryStream)BaseStream).ToArray();
        }

        public void Write(Enum enumerable)
        {
            object value = Convert.ChangeType(enumerable, enumerable.GetTypeCode());
            if (value is int)
                Write((int)value);
            else if (value is byte)
                Write((byte)value);
            else if(value is short)
                Write((short)value);
            else if (value is ushort)
                Write((ushort)value);
            else if (value is uint)
                Write((uint)value);
        }

        public void WriteUH(int value)
        {
            Write((UInt16)value);
        }

        public void WriteB(int value)
        {
            Write((byte)value);
        }

        public void WriteString(String s, bool nullterminated)
        {
            if (s == null)
                return;
            Write(Encoding.ASCII.GetBytes(s));
            if (nullterminated)
                WriteB(0);
        }

    }
}
