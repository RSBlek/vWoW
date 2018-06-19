using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using vWoW.Clients;
using vWoW.Data.Enums;
using vWoW.Logging;

namespace vWoW.Network.PacketHandling
{
    public class PacketHandler
    {
        private Mutex packetMutex = new Mutex();
        private Thread packetHandlingLoopThread;
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
            LoadHandlers();
            StartHandlingLoop();
        }

        private void StartHandlingLoop()
        {
            packetHandlingLoopThread = new Thread(PacketHandlingLoop);
            packetHandlingLoopThread.Start();
        }


        private void LoadHandlers()
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

        private void PacketHandlingLoop()
        {
            while (true)
            {
                while(inPackets.Count > 0)
                {
                    packetMutex.WaitOne();
                    InPacket inPacket = inPackets.First.Value;
                    inPackets.RemoveFirst();
                    packetMutex.ReleaseMutex();
                    Handle(inPacket);
                }
                Thread.Sleep(10);
            }
        }

        public void Handle(InPacket inPacket)
        {
            if (!Handles.ContainsKey(inPacket.PacketOp))
            {
                Logger.Log(LogType.Warning, $"Received unhandled {inPacket.PacketOp.Type}Packet OpCode={inPacket.PacketOp.RawID}");
                return;
            }
            MethodInfo method = Handles[inPacket.PacketOp];
            Logger.Log(inPacket);
            if (inPacket.PacketOp.Type == PacketType.Logon)
                method.Invoke(logonClient, new object[] { inPacket });
            else if(inPacket.PacketOp.Type == PacketType.World)
                method.Invoke(worldClient, new object[] { inPacket });
        }

        public bool AddInPacket(InPacket inPacket)
        {
            if (inPacket == null)
                return false;
            packetMutex.WaitOne();
            inPackets.AddLast(inPacket);
            packetMutex.ReleaseMutex();
            return true;
        }


    }
}
