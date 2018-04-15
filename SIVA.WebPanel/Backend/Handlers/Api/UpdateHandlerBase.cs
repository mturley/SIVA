using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SimpleServer.Handlers;
using SimpleServer.Internals;
using SimpleServer.Managers;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    public abstract class UpdateHandlerBase : IHandler
    {
        public abstract string Url { get; }
        
        public abstract string ChangeValue(string guildId, IEnumerable<string> arguments, string requestId);
        
        public bool CanHandle(SimpleServerRequest request)
        {
            return request.Method == "POST" && request.RawUrl == "/levels.action";
        }

        public void Handle(SimpleServerContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Siva-Token"))
                MissingHeader(context);
            else if (!LoginHandler.Sessions.ContainsKey(context.Request.Headers["X-Siva-Token"]))
                SessionDoesNotExist(context);
        }
        private static void MissingHeader(SimpleServerContext context)
        {
            context.Response.StatusCode = 403;
            context.Response.ReasonPhrase = "Forbidden";
            var stream = Assembly.GetAssembly(ErrorManager.ErrorPage.TypeInAssembly)
                .GetManifestResourceStream(ErrorManager.ErrorPage.NamespaceUrlOfType);
            var sr = new StreamReader(stream);
            var sw = new StreamWriter(context.Response.OutputStream);
            sw.WriteLine(sr.ReadToEnd().Replace("[Header]", "403 Forbidden").Replace("[ErrorDetail]",
                "We were unable to serve your request because the X-Siva-Token header was missing.<br><br>Your Request: " +
                context.Request.Method + " " + context.Request.RawUrl));
            sw.Flush();
            context.Response.Close();
        }
        private static void SessionDoesNotExist(SimpleServerContext context)
        {
            context.Response.StatusCode = 403;
            context.Response.ReasonPhrase = "Forbidden";
            var stream = Assembly.GetAssembly(ErrorManager.ErrorPage.TypeInAssembly)
                .GetManifestResourceStream(ErrorManager.ErrorPage.NamespaceUrlOfType);
            var sr = new StreamReader(stream);
            var sw = new StreamWriter(context.Response.OutputStream);
            sw.WriteLine(sr.ReadToEnd().Replace("[Header]", "403 Forbidden").Replace("[ErrorDetail]",
                "We were unable to serve your request because the X-Siva-Token does not represent a valid session ID.<br><br>Your Request: " +
                context.Request.Method + " " + context.Request.RawUrl));
            sw.Flush();
            context.Response.Close();
        }
    }
}