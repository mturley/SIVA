using System;
using System.Collections.Generic;
using System.IO;
using SimpleServer;
using SimpleServer.Logging;
using System.Net;
using SimpleServer.Handlers;

namespace SIVA.WebPanel.Backend
{
    public class SivaPanel
    {
        public static SivaPanel Instance { get; set; } = new SivaPanel();
        public SimpleServer.SimpleServer Server { get; } 
        public List<IHandler> Handlers { get; set; } = new List<IHandler>();
        public List<TextWriter> Loggers { get; set; }
        
        public SivaPanel()
        {
            Instance = this;
            SimpleServer.SimpleServer.Initialize();
            Log.AddWriter(Console.Out);
            Server = ServerBuilder.NewServer()
                .NewHost(443)
                .At(IPAddress.Any)
                .With(new Handler())
                .AddToServer()
                .Build();
        }

        public void StartPanel()
        {
            Server.Start();
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
