using System.Collections.Generic;
using SimpleServer.Internals;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    public class SivaDashboardContext : SimpleServerContext
    {
        public bool IsGuildSelected { get; internal set; }
        public SivaGuild SelectedGuild { get; internal set; }
        public Dictionary<string,string> Cookie { get; internal set; }
    }
}