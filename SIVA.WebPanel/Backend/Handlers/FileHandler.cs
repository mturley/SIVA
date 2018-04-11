using System.IO;
using System.Linq;
using System.Reflection;
using SimpleServer.Handlers;
using SimpleServer.Internals;
using SIVA.WebPanel;

namespace SIVA.Backend.Handlers
{
    public class FileHandler : IHandler
    {
        public const string RootNamespace = "SIVA.WebPanel.Frontend";

        public bool HasFile(string file)
        {
            return Assembly.GetAssembly(typeof(FileHandler)).GetManifestResourceNames()
                .Contains(RootNamespace + file.Replace('/', '.').TrimEnd('.'));
        }

        public bool CanHandle(SimpleServerRequest request)
        {
            return HasFile(request.RawUrl);
        }

        public void Handle(SimpleServerContext context)
        {
            if (context.Request.Method == "GET")
            {
                context.Response.OutputStream.WriteFile(context.Request.RawUrl);
            }
            else
            {
                context.Response.StatusCode = 405;
                context.Response.ReasonPhrase = "Method Not Allowed";
                var sw = new StreamWriter(context.Response.OutputStream);
                sw.Write("<h1>405 Method Not Allowed</h1>");
                sw.Flush();
                context.Response.Close();
            }
        }
    }
}