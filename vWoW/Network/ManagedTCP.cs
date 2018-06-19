using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using vWoW.Constants.Enums;
using vWoW.Network.PacketHandling;

namespace vWoW.Network
{
    public class ManagedTCP
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private PacketType packetType;
        private PacketHandler packetHandler;

        public ManagedTCP(PacketHandler packetHandler, PacketType packetType)
        {
            this.packetType = packetType;
            tcpClient = new TcpClient();
            this.packetHandler = packetHandler;
        }

        public void Connect(string host, int port)
        {
            tcpClient.Connect(host, port);
            networkStream = tcpClient.GetStream();
        }

        public void Send(byte[] data)
        {
            networkStream.Write(data, 0, data.Length);
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
                return true;
            }else if(packetType == PacketType.World)
            {
                if (tcpClient.Available < 2)
                    return false;
                byte[] lengthbytes = new byte[2];
                networkStream.Read(lengthbytes, 0, 2);
                //ToDo decrypt length and header
            }

            return false;
            
        }


    }
}
