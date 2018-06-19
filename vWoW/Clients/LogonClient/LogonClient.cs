using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Cryptography;
using vWoW.Network;

namespace vWoW.Clients
{
    public partial class LogonClient
    {
        private ManagedTCP managedTCP;
        private SRP6 srp6;
        private byte[] clientHMACSeed; // 16 Bytes
        private byte securityFlags;
        private string username;
        private string password;

        public void SetManagedTcp(ManagedTCP managedTCP)
        {
            this.managedTCP = managedTCP;
        }

        public void Send(OutPacket outPacket)
        {
            managedTCP.Send(outPacket.GetBytes());
        }

    }
}
