using System;
using System.Diagnostics;
using vWoW.Network;
using vWoW.Network.PacketHandling;

namespace vWoW
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("jaaa");
            Console.ReadLine();
            Clients.Client client = new Clients.Client();
            client.ConnectLogon();
            Console.ReadLine();
        }
    }
}
