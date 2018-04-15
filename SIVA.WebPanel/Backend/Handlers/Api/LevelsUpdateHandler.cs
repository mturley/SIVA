using System.Collections.Generic;
using SimpleServer.Handlers;
using SimpleServer.Internals;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    public class LevelsUpdateHandler : UpdateHandlerBase
    {
        public override string Url => "/levels.action";
        public override string ChangeValue(string guildId, IEnumerable<string> arguments, string requestId)
        {
            return null;
        }
    }
}