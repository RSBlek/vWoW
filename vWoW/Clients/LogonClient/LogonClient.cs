using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Network;

namespace vWoW.Clients
{
    public partial class LogonClient
    {
        private ManagedTCP managedTCP;

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
