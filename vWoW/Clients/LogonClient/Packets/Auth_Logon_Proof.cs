using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Data.Enums;
using vWoW.Network;
using vWoW.Network.PacketHandling;

namespace vWoW.Clients
{
    public partial class LogonClient
    {
        [PacketHandlingMethod(LogonOpCode.AUTH_LOGON_CHALLENGE)]
        public void AuthLogonProofResponse(InPacket inPacket)
        {
            
        }


        public void AuthLogonProofRequest(byte[] clienthmachash)
        {
            OutPacket outPacket = new OutPacket(LogonOpCode.AUTH_LOGON_PROOF);
            outPacket.Write(srp6.A);
            outPacket.Write(srp6.M);
            outPacket.Write(clienthmachash);
            outPacket.WriteB(0); //Number of tokens
            outPacket.WriteB(0); //No security flags
            Send(outPacket);
        }
    }
}
