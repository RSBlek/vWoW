using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using vWoW.Data.Enums;
using vWoW.Logging;
using vWoW.Network;
using vWoW.Network.PacketHandling;

namespace vWoW.Clients
{
    public partial class LogonClient
    {
        [PacketHandlingMethod(LogonOpCode.AUTH_LOGON_CHALLENGE)]
        public void AuthLogonChallengeResponse(InPacket inPacket)
        {
            AccountStatus accountStatus = (AccountStatus)inPacket.ReadByte();
            if(accountStatus != AccountStatus.Success)
            {
                Logger.Log(LogType.Warning, $"Login failed: {accountStatus}");
                return;
            }

        }


        public void AuthLogonChallengeRequest(string identity)
        {
            OutPacket outPacket = new OutPacket(LogonOpCode.AUTH_LOGON_CHALLENGE);
            outPacket.Write(ProtocolVersion.ProtocolVersionNew);
            outPacket.WriteUH(30 + identity.Length);
            outPacket.WriteString("WoW", true);
            outPacket.Write(ExpansionType.WrathOfTheLichKing);
            outPacket.WriteB(3); //Major patch
            outPacket.WriteB(5); //Minor patch
            outPacket.Write(ClientBuild.Wotlk_3_3_5a);
            outPacket.WriteString("68x", true); 
            outPacket.WriteString("niW", true);
            outPacket.WriteString("EDed", false); //DEde reversed
            outPacket.Write(TimeZoneBias.MEZ);
            outPacket.Write(IPAddress.Parse("192.168.2.105").GetAddressBytes());
            outPacket.WriteB(identity.Length);
            outPacket.WriteString(identity.ToUpper(), false);
            Send(outPacket);
        }

    }
}
