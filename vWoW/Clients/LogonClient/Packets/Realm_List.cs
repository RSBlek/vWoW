using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Data;
using vWoW.Data.Enums;
using vWoW.Logging;
using vWoW.Network;
using vWoW.Network.PacketHandling;

namespace vWoW.Clients
{
    public partial class LogonClient
    {
        [PacketHandlingMethod(LogonOpCode.REALM_LIST)]
        public void RealmListResponse(InPacket inPacket)
        {
            ushort PayloadSize = inPacket.ReadUInt16();
            uint unknown01 = inPacket.ReadUInt32(); //Always 0
            ushort realmCount = inPacket.ReadUInt16();
            for (int i = 0; i < realmCount; i++)
            {
                RealmInfo realmInfo;
                RealmBuildInformation realmBuildInformation = null;
                RealmType realmType = (RealmType)inPacket.ReadByte();
                bool isLocked = inPacket.ReadBoolean();
                RealmFlags realmFlags = (RealmFlags)inPacket.ReadByte();
                string realmName = inPacket.ReadString();
                string realmEndpointInformation = inPacket.ReadString();
                float populationLevel = inPacket.ReadSingle();
                byte characterCount = inPacket.ReadByte();
                byte realmTimeZone = inPacket.ReadByte();
                byte realmID = inPacket.ReadByte();
                if (realmFlags.HasFlag(RealmFlags.SpecifyBuild))
                {
                    ExpansionType expansionType = (ExpansionType)inPacket.ReadByte();
                    byte majorVersion = inPacket.ReadByte();
                    byte minorVersion = inPacket.ReadByte();
                    short buildNumber = inPacket.ReadInt16();
                    realmBuildInformation = new RealmBuildInformation(expansionType, majorVersion, minorVersion, buildNumber);
                }
                realmInfo = new RealmInfo(realmType, isLocked, realmFlags, realmName, realmEndpointInformation, populationLevel, characterCount, realmTimeZone, realmID, realmBuildInformation);
                this.realms.Add(realmInfo);
                Logger.Log(LogType.Normal, $"[{realmInfo.RealmID}] {realmInfo.RealmName} {realmInfo.RealmEndpoint.realmHost}:{realmInfo.RealmEndpoint.realmPort}");
            }
            short unknown02 = inPacket.ReadInt16(); // 2.x + 3.x clients sends 0x10 0x00 and 1.12.1 client sends 0x00 0x02 
        }

        public void RealmListRequest()
        {
            OutPacket outPacket = new OutPacket(LogonOpCode.REALM_LIST);
            outPacket.Write(0); //Hardcoded in blizzard client
            Send(outPacket);
        }
    }
}
