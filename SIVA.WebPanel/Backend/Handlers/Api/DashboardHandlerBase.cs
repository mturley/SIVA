using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SimpleServer.Handlers;
using SimpleServer.Internals;
using SimpleServer.Managers;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    public abstract class DashboardHandlerBase : IHandler
    {
        public abstract string Url { get; }
        public abstract string File { get; }
        public abstract string RunReplacements(string file);
        public abstract Type ChildType { get; }

        public bool CanHandle(SimpleServerRequest request)
        {
            return request.RawUrl == Url;
        }

        public bool BeforeHandle(SivaDashboardContext ctx)
        {
            return true;
        }

        public void Handle(SimpleServerContext context)
        {
            try
            {
                var cookie = context.Request.Headers.ContainsKey("Cookie")
                    ? context.Request.Headers["Cookie"].Split(';')
                        .ToDictionary(x => x.Split('=')[0].Trim(), x => x.Split('=')[1])
                    : new Dictionary<string, string>();
                if (!cookie.ContainsKey("siva.session") || !LoginHandler.Sessions.ContainsKey(cookie["siva.session"]) ||
                    LoginHandler.Sessions[cookie["siva.session"]].New ||
                    LoginHandler.Sessions[cookie["siva.session"]].Expires <= DateTime.Now)
                {
                    context.Response.StatusCode = 302;
                    context.Response.ReasonPhrase = "Found";
                    context.Response.Headers["Location"] = "https://panel.greem.xyz/login.action";
                    context.Response.Close();
                    return;
                }

                var ctx = new SivaDashboardContext
                {
                    IsGuildSelected =
                        SivaGuild.IsNullOrEmpty(LoginHandler.Sessions[cookie["siva.session"]].SelectedGuild),
                    SelectedGuild = LoginHandler.Sessions[cookie["siva.session"]].SelectedGuild,
                    Request = context.Request,
                    Response = context.Response,
                    Cookie = cookie
                };

                if (!BeforeHandle(ctx))
                    return;
                context.Request.Headers["Content-Type"] =
                    Helper.GetMimeType(context.Request.RawUrl.TrimEnd('/').Split('/').Last().Split('.').Last());
                var sr = new StreamReader(Assembly.GetAssembly(ChildType).GetManifestResourceStream(File));
                var sw = new StreamWriter(context.Response.OutputStream);
                sw.Write(RunReplacements(sr.ReadToEnd()));
                sw.Flush();
                sw.Close();
                context.Response.Close();
            }
            catch (Exception ex)
            {
                context.Request.Headers["Content-Type"] = "text/html";
                context.Response.StatusCode = 500;
                context.Response.ReasonPhrase = "Internal Server Error";
                var stream = Assembly.GetAssembly(ErrorManager.ErrorPage.TypeInAssembly)
                    .GetManifestResourceStream(ErrorManager.ErrorPage.NamespaceUrlOfType);
                var sr = new StreamReader(stream);
                var sw = new StreamWriter(context.Response.OutputStream);
                sw.WriteLine(sr.ReadToEnd().Replace("[Header]", "500 Internal Server Error").Replace("[ErrorDetail]",
                    ex + "<br><br>Your Request: " +
                    context.Request.Method + " " + context.Request.RawUrl));
                sw.Flush();
                context.Response.Close();
            }
        }
    }
}