using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using vWoW.Data.Enums;
using vWoW.Logging;
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
        private Mutex readLock = new Mutex();
        private bool readLoop = true;

        public ManagedTCP(PacketHandler packetHandler)
        {
            this.packetHandler = packetHandler;
            this.tcpClient = new TcpClient();
            this.packetReadLoopThread = new Thread(PacketReadLoop);
        }

        public void Connect(string host, int port, PacketType packetType)
        {
            this.packetType = packetType;
            this.tcpClient = new TcpClient();
            tcpClient.Connect(host, port);
            networkStream = tcpClient.GetStream();
            if(packetReadLoopThread.IsAlive == false)
            {
                packetReadLoopThread.IsBackground = true;
                packetReadLoopThread.Start();
            }
            Logger.Log(LogType.Normal, $"Connected to {host}:{port}");
        }

        public void Send(byte[] data)
        {
            networkStream.Write(data, 0, data.Length);
        }

        private void PacketReadLoop()
        {
            while (readLoop)
            {
                while (Read()) ;
                Thread.Sleep(5);
            }
        }

        public void Reconnect(string host, int port, PacketType packetType)
        {
            readLock.WaitOne();
            tcpClient.Close();
            Connect(host, port, packetType);
            readLock.ReleaseMutex();
        }

        public void Dispose()
        {
            readLoop = false;
            tcpClient.Close();
            networkStream.Dispose();
        }

        public bool Read()
        {
            if (tcpClient.Available <= 0)
                return false;
            InPacket inPacket = null;
            readLock.WaitOne();
            if(packetType == PacketType.Logon)
            {
                byte[] data = new byte[tcpClient.Available];
                networkStream.Read(data, 0, data.Length);
                inPacket = new InPacket(data, false);
            }else if(packetType == PacketType.World)
            {
                if (tcpClient.Available >= 2)
                {
                    byte[] lengthbytes = new byte[2];
                    networkStream.Read(lengthbytes, 0, 2);
                    //ToDo decrypt length and header
                }
            }
            readLock.ReleaseMutex();
            return packetHandler.AddInPacket(inPacket);
        }


    }
}
