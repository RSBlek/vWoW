using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using vWoW.Data.Enums;
using vWoW.Network.PacketHandling;

namespace vWoW.Network
{
    public class ManagedTCP
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private PacketType packetType;
        private PacketHandler packetHandler;
        private Thread packetReadLoopThread;

        public ManagedTCP(PacketHandler packetHandler, PacketType packetType)
        {
            this.packetType = packetType;
            this.tcpClient = new TcpClient();
            this.packetHandler = packetHandler;
            this.packetReadLoopThread = new Thread(PacketReadLoop);
        }

        public void Connect(string host, int port)
        {
            tcpClient.Connect(host, port);
            networkStream = tcpClient.GetStream();
            packetReadLoopThread.IsBackground = true;
            packetReadLoopThread.Start();
        }

        public void Send(byte[] data)
        {
            networkStream.Write(data, 0, data.Length);
        }

        private void PacketReadLoop()
        {
            while (true)
            {
                while (Read()) ;
                Thread.Sleep(5);
            }
        }

        public bool Read()
        {
            if (tcpClient.Available <= 0)
                return false;

            InPacket inPacket = null;

            if(packetType == PacketType.Logon)
            {
                byte[] data = new byte[tcpClient.Available];
                networkStream.Read(data, 0, data.Length);
                inPacket = new InPacket(data, false);
            }else if(packetType == PacketType.World)
            {
                if (tcpClient.Available < 2)
                    return false;
                byte[] lengthbytes = new byte[2];
                networkStream.Read(lengthbytes, 0, 2);
                //ToDo decrypt length and header
            }

            return packetHandler.AddInPacket(inPacket);

        }


    }
}
