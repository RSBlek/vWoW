using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Constants.Enums;
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
            this.managedTCP = new ManagedTCP(packetHandler, PacketType.Logon);
            worldClient.SetManagedTcp(managedTCP);
            logonClient.SetManagedTcp(managedTCP);
        }

        public void ConnectLogon()
        {
            managedTCP.Connect("login.zombiewow.com", 3724);
            logonClient.AuthLogonChallengeRequest("scorniii");
        }

    }
}
