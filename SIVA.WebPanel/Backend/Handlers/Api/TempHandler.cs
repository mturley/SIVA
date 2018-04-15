using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SimpleServer.Handlers;
using SimpleServer.Internals;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    [SivaIgnore]
    public class TempHandler : IHandler
    {
        public bool CanHandle(SimpleServerRequest request)
        {
            return request.RawUrl == "/";
        }

        public void Handle(SimpleServerContext context)
        {
            var cookie = context.Request.Headers.ContainsKey("Cookie")
                ? context.Request.Headers["Cookie"].Split(';')
                    .ToDictionary(x => x.Split('=')[0].Trim(), x => x.Split('=')[1])
                : new Dictionary<string, string>();
            Console.WriteLine(cookie.ContainsKey("siva.session"));
            Console.WriteLine(LoginHandler.Sessions.ContainsKey(cookie["siva.session"]));
            var sw = new StreamWriter(context.Response.OutputStream);
            sw.Write(JsonConvert.SerializeObject(LoginHandler.Sessions[cookie["siva.session"]]));
            sw.Flush();
            context.Response.Close();
        }
    }
}