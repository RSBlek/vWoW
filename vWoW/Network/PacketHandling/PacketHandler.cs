using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using vWoW.Clients;
using vWoW.Data.Enums;

namespace vWoW.Network.PacketHandling
{
    public class PacketHandler
    {
        private Dictionary<PacketOp, MethodInfo> Handles { get; } = new Dictionary<PacketOp, MethodInfo>();
        private LinkedList<InPacket> inPackets = new LinkedList<InPacket>();
        private WorldClient worldClient;
        private LogonClient logonClient;
        private Client client;

        public PacketHandler(Client client,LogonClient logonClient, WorldClient worldClient)
        {
            this.client = client;
            this.logonClient = logonClient;
            this.worldClient = worldClient;
            Initialize();
        }

        public void Initialize()
        {
            foreach (Type asmType in Assembly.GetCallingAssembly().GetTypes())
            {
                foreach (MethodInfo methodinfo in asmType.GetMethods())
                {
                    foreach (Attribute attribute in methodinfo.GetCustomAttributes(true))
                    {
                        if (!(attribute is PacketHandlingMethod))
                            continue;
                        PacketHandlingMethod packetHandlingMethodAttribute = attribute as PacketHandlingMethod;
                        Handles.Add(packetHandlingMethodAttribute.PacketOp, methodinfo);
                    }
                }
            }
        }

        public void Handle(InPacket inPacket)
        {
            if (!Handles.ContainsKey(inPacket.PacketOp))
                return;
            MethodInfo method = Handles[inPacket.PacketOp];

            if(inPacket.PacketOp.Type == PacketType.Logon)
                method.Invoke(logonClient, new object[] { inPacket });
            else if(inPacket.PacketOp.Type == PacketType.World)
                method.Invoke(worldClient, new object[] { inPacket });
        }

        public void AddInPacket(InPacket inPacket)
        {
            inPackets.AddLast(inPacket);
        }


    }
}
