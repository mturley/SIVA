using System.Linq;
using SimpleServer.Handlers;
using SimpleServer.Internals;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    public class LoginHandler : IHandler
    {
        public bool CanHandle(SimpleServerRequest request)
        {
            return request.Method == "GET" && request.RawUrl == "/login.action";
        }

        public void Handle(SimpleServerContext context)
        {
            var cookie = context.Request.Headers["Cookie"].Split(';').ToDictionary(x => x.Split('=')[0].Trim(),x => x.Split('=')[1]);
        }
    }
}