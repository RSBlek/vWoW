using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Data.Enums;
using vWoW.Network;
using vWoW.Network.PacketHandling;

namespace vWoW.Clients
{
    public class Client
    {
        WorldClient worldClient;
        LogonClient logonClient;
        PacketHandler packetHandler;
        ManagedTCP managedTCP;
        
        public Client()
        {
            this.logonClient = new LogonClient();
            this.worldClient = new WorldClient();
            this.packetHandler = new PacketHandler(this, logonClient, worldClient);
            this.managedTCP = new ManagedTCP(packetHandler);
            worldClient.SetManagedTcp(managedTCP);
            logonClient.SetManagedTcp(managedTCP);
        }

        public void Login(String realmlist,String username, String password)
        {
            managedTCP.Connect(realmlist, 3724, PacketType.Logon);
            logonClient.AuthLogonChallengeRequest(username, password);
        }

        public void EnterRealm(byte realmid)
        {
            this.managedTCP.Reconnect(logonClient.Realms[realmid].RealmEndpoint.realmHost, logonClient.Realms[realmid].RealmEndpoint.realmPort, PacketType.World);
        }

    }
}
