using System;
using System.Diagnostics;
using System.Numerics;
using vWoW.Network;
using vWoW.Network.PacketHandling;

namespace vWoW
{
    class Program
    {

        static void Main(string[] args)
        {
            Clients.Client client = new Clients.Client();
            client.Login("login.zombiewow.com", "hackxlol", "asdasdasdd");
            Console.ReadLine();
        }
    }
}
