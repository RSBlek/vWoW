using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Network;

namespace vWoW.Clients
{
    public partial class WorldClient
    {
        private ManagedTCP managedTCP;

        public void SetManagedTcp(ManagedTCP managedTCP)
        {
            this.managedTCP = managedTCP;
        }
    }
}
