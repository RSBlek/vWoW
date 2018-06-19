using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Logging;
using vWoW.Data.Enums;
using vWoW.Network;

namespace vWoW.Logging
{
    public static class Logger
    {
        public static void Log(LogType logType, String logtext)
        {
            String output = "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] ";
            if (logType == LogType.Warning)
                output += "Warning: ";
            output = output + logtext;
            Console.WriteLine(output);
        }

        public static void Log(InPacket inPacket)
        {
            if (inPacket.PacketOp.Type == PacketType.Logon)
                Log(LogType.IncomingPacket, "Received " + (LogonOpCode)inPacket.PacketOp.RawID);
            if (inPacket.PacketOp.Type == PacketType.World)
                Log(LogType.IncomingPacket, "Received " + (WorldOpCode)inPacket.PacketOp.RawID);
        }

    }
}
