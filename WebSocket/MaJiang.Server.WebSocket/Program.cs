using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MaJiang.WebSocket.Core.Server;
using MaJiang.WebSocket.Core;
using System.Configuration;

namespace MaJiang.Server.WebSocket
{
    class Program
    {
        public static void Main()
        {
            var httpsv = new MaJiangServer();

            httpsv.Start();
            
            Console.ReadLine();

            httpsv.Stop();
        }


    }
}
