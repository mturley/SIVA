using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using SimpleServer.Handlers;
using SimpleServer.Internals;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    public class LoginHandler : IHandler
    {
        public static Dictionary<string, Session> Sessions { get; set; } = new Dictionary<string, Session>();

        public bool CanHandle(SimpleServerRequest request)
        {
            return request.Method == "GET" && request.RawUrl == "/login.action";
        }

        public void Handle(SimpleServerContext context)
        {
            var cookie = context.Request.Headers.ContainsKey("Cookie")
                ? context.Request.Headers["Cookie"].Split(';')
                    .ToDictionary(x => x.Split('=')[0].Trim(), x => x.Split('=')[1])
                : new Dictionary<string, string>();
            if (!cookie.ContainsKey("siva.session"))
            {
                var sid = Guid.NewGuid().ToString();
                context.Response.Headers.Add("Set-Cookie", "siva.session=" + sid);
                Sessions.Add(sid, new Session());
                context.Response.StatusCode = 302;
                context.Response.ReasonPhrase = "Found";
                context.Response.Headers.Add("Location",
                    "https://discordapp.com/api/oauth2/authorize?client_id=434043533444251669&redirect_uri=https%3A%2F%2Fpanel.greem.xyz%2Fauthenticate.action&response_type=code&scope=identify%20guilds%20email&state=" +
                    sid);
                Console.WriteLine(""+context.Response.Headers);
                context.Response.Close();
            }
            else if (cookie.ContainsKey("siva.session"))
            {
                if (!Sessions.ContainsKey(cookie["siva.session"]))
                {
                    var sid = Guid.NewGuid().ToString();
                    context.Response.Headers["Set-Cookie"] = "siva.session=" + sid;
                    Sessions.Add(sid, new Session());
                    context.Response.StatusCode = 302;
                    context.Response.ReasonPhrase = "Found";
                    context.Response.Headers.Add("Location",
                        "https://discordapp.com/api/oauth2/authorize?client_id=434043533444251669&redirect_uri=https%3A%2F%2Fpanel.greem.xyz%2Fauthenticate.action&response_type=code&scope=identify%20guilds%20email&state=" +
                        sid);
                    Console.WriteLine(""+context.Response.Headers);
                    context.Response.Close();
                }
                else if (Sessions[cookie["siva.session"]].New)
                {
                    context.Response.StatusCode = 302;
                    context.Response.ReasonPhrase = "Found";
                    context.Response.Headers.Add("Location",
                        "https://discordapp.com/api/oauth2/authorize?client_id=434043533444251669&redirect_uri=https%3A%2F%2Fpanel.greem.xyz%2Fauthenticate.action&response_type=code&scope=identify%20guilds%20email&state=" +
                        Sessions[cookie["siva.session"]].Key);
                    Console.WriteLine(""+context.Response.Headers);
                    context.Response.Close();
                }
                else if (Sessions[cookie["siva.session"]].Expires <= DateTime.Now)
                {
                    var sid = Guid.NewGuid().ToString();
                    context.Response.Headers["Set-Cookie"] = "siva.session=" + sid;
                    Sessions.Add(sid, new Session());
                    context.Response.StatusCode = 302;
                    context.Response.ReasonPhrase = "Found";
                    context.Response.Headers.Add("Location",
                        "https://discordapp.com/api/oauth2/authorize?client_id=434043533444251669&redirect_uri=https%3A%2F%2Fpanel.greem.xyz%2Fauthenticate.action&response_type=code&scope=identify%20guilds%20email&state=" +
                        sid);
                    Console.WriteLine(""+context.Response.Headers);
                    context.Response.Close();
                }
                else
                {
                    context.Response.RedirectAsync(new Uri("https://panel.greem.xyz/")).GetAwaiter().GetResult();
                }
            }
        }
    }

    public class Session
    {
        public DateTime Expires { get; set; }
        public string Key { get; set; }
        public string DiscordKey { get; set; }
        public bool New { get; set; } = true;
        public SivaGuild SelectedGuild { get; set; } = SivaGuild.Empty;
    }
}