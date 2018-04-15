using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleServer;
using SimpleServer.Logging;
using System.Net;
using System.Reflection;
using SimpleServer.Handlers;
using SIVA.Backend.Handlers;
using SIVA.WebPanel.Backend.Handlers.Api;

namespace SIVA.WebPanel.Backend
{
    public class SivaPanel
    {
        public static SivaPanel Instance { get; set; } = new SivaPanel();
        public static SimpleServer.SimpleServer Server { get; set; } 
        public static List<IHandler> Handlers { get; set; } = new List<IHandler>();
        public List<TextWriter> Loggers { get; set; }
        public ISivaDataProvider DataProvider { get; set; }
        

        public static void StartPanel()
        {
            SimpleServer.SimpleServer.Initialize();
            Log.AddWriter(Console.Out);
            // This code will add all of the handlers defined in this assembly, that doesn't have a [SivaIgnore] attribute
            foreach (var handlerType in Assembly.GetAssembly(typeof(SivaPanel)).GetTypes())
            {
                if (handlerType.IsAbstract || handlerType.IsInterface)
                    continue;
                if (!handlerType.GetInterfaces().Contains(typeof(IHandler))) continue;
                if (!handlerType.IsDefined(typeof(SivaIgnoreAttribute), false))
                {
                    Handlers.Add((IHandler)Activator.CreateInstance(handlerType));
                }
            }
            Server = ServerBuilder.NewServer()
                .NewHost(8443)
                .At(IPAddress.Any)
                .With(Handlers.ToArray())
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
