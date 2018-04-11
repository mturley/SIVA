using System;
using System.Collections.Generic;
using System.IO;
using SimpleServer;
using SimpleServer.Logging;
using System.Net;
using SimpleServer.Handlers;
using SIVA.Backend.Handlers;

namespace SIVA.WebPanel.Backend
{
    public class SivaPanel
    {
        public static SivaPanel Instance { get; set; } = new SivaPanel();
        public static SimpleServer.SimpleServer Server { get; set; } 
        public static List<IHandler> Handlers { get; set; } = new List<IHandler>();
        public List<TextWriter> Loggers { get; set; }
        

        public static void StartPanel()
        {
            SimpleServer.SimpleServer.Initialize();
            Log.AddWriter(Console.Out);
            Server = ServerBuilder.NewServer()
                .NewHost(8443)
                .At(IPAddress.Any)
                .With(Handlers.ToArray())
                .With(new Handler())
                .With(new FileHandler())
                .AddToServer()
                .BuildAndStart();
        }

        public void StopPanel()
        {
            Server.Stop();
        }
        public void InitialiseServer()
        {

        }
    }
}
