using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Data.Enums;
using vWoW.Logging;
using vWoW.Network;
using vWoW.Network.PacketHandling;

namespace vWoW.Clients
{
    public partial class LogonClient
    {
        [PacketHandlingMethod(LogonOpCode.AUTH_LOGON_PROOF)]
        public void AuthLogonProofResponse(InPacket inPacket)
        {
            LogonProofType logonProofType = (LogonProofType)inPacket.ReadByte();
            if(logonProofType != LogonProofType.Success)
            {
                inPacket.ReadBytes(2); // Always 0x03 0x00 !?!?
                Logger.Log(LogType.Warning, "Wrong username or password");
                return;
            }
            byte[] M2 = inPacket.ReadBytes(20);
            AccountAuthorizationFlags accountAuthorizationFlags = (AccountAuthorizationFlags)inPacket.ReadUInt32();
            uint surveyId = inPacket.ReadUInt32(); //Always 0
            ushort unk01 = inPacket.ReadUInt16(); // Always 0
            RealmListRequest();
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
