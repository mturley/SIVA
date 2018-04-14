using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;
using SimpleServer.Handlers;
using SimpleServer.Internals;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    public class DiscordCallbackHandler : IHandler
    {
        public bool CanHandle(SimpleServerRequest request)
        {
            return request.RawUrl.Split('?')[0] == "/authenticate.action";
        }

        public void Handle(SimpleServerContext context)
        {
            var qraw = "";
            var cookie = context.Request.Headers.ContainsKey("Cookie")
                ? context.Request.Headers["Cookie"].Split(';')
                    .ToDictionary(x => x.Split('=')[0].Trim(), x => x.Split('=')[1])
                : new Dictionary<string, string>();
            context.Request.RawUrl.Split('?').Where(x => x != context.Request.RawUrl.Split('?')[0]).ToList()
                .ForEach(x => qraw += x);
            Console.WriteLine(qraw);
            Console.WriteLine(cookie.ContainsKey("siva.session"));
            Console.WriteLine(LoginHandler.Sessions.ContainsKey(cookie["siva.session"]));
            var query = qraw.Split('&').ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);
            if (!query.ContainsKey("state"))
            {
                var sw = new StreamWriter(context.Response.OutputStream);
                sw.Write(
                    "The authentication request failed because the OAuth state wasn't provided. Click <a href=\"login.action\">here</a> to retry authentication.");
                sw.Flush();
                sw.Close();
                context.Response.Close();
            }
            else if (!cookie.ContainsKey("siva.session"))
            {
                context.Response.StatusCode = 302;
                context.Response.ReasonPhrase = "Found";
                context.Response.Headers.Add("Location",
                    "https://panel.greem.xyz/login.action");
                context.Response.Close();
            }
            else if (query["state"] != cookie["siva.session"])
            {
                var sw = new StreamWriter(context.Response.OutputStream);
                sw.Write(
                    "The authentication request failed because the OAuth state doesn't match the SIVA session ID. Click <a href=\"login.action\">here</a> to retry authentication.");
                sw.Flush();
                sw.Close();
                context.Response.Close();
            }
            else if (string.IsNullOrWhiteSpace(cookie["siva.session"]) ||
                     !LoginHandler.Sessions.ContainsKey(cookie["siva.session"]))
            {
                var sw = new StreamWriter(context.Response.OutputStream);
                sw.Write("Bad session ID. Click <a href=\"login.action\">here</a> to retry authentication.");
                sw.Flush();
                sw.Close();
                context.Response.Close();
            }
            else if (!query.ContainsKey("code"))
            {
                context.Response.StatusCode = 302;
                context.Response.ReasonPhrase = "Found";
                context.Response.Headers["Location"] =
                    "https://panel.greem.xyz/login.action";
                context.Response.Close();
            }
            else
            {
                try
                {
                    var cl = new HttpClient();
                    var atoken = JObject.Parse(cl.PostAsync("https://discordapp.com/api/oauth2/token",
                            new StringContent(
                                "client_id=434043533444251669&client_secret=a5uA5E5XxPjUFMUjaM8ZZPt3R6b7XTok&grant_type=authorization_code&code=" +
                                query["code"] + "&redirect_uri=https://panel.greem.xyz/authenticate.action",Encoding.UTF8,"application/x-www-form-urlencoded"))
                        .GetAwaiter()
                        .GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    Console.WriteLine(atoken.ToString());
                    LoginHandler.Sessions[cookie["siva.session"]].DiscordKey = atoken["access_token"]?.ToString();
                    LoginHandler.Sessions[cookie["siva.session"]].Expires =
                        DateTime.Now.AddSeconds(long.Parse(atoken["expires_in"]?.ToString()));
                    context.Response.StatusCode = 302;
                    context.Response.ReasonPhrase = "Found";
                    context.Response.Headers["Location"] =
                        "https://panel.greem.xyz/";
                    context.Response.Close();
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 200;
                    context.Response.ReasonPhrase = "OK";
                    var sw = new StreamWriter(context.Response.OutputStream);
                    sw.WriteLine(ex.ToString());
                    sw.Flush();
                    context.Response.Close();
                }
            }
        }
    }
}