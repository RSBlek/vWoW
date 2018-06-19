using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Data.Enums;
using vWoW.Network;

namespace vWoW.Clients
{
    public partial class LogonClient
    {
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
