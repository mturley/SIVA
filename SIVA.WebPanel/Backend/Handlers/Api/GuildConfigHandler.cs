using System;
using System.Collections.Generic;
using System.Text;
using SimpleServer.Handlers;
using SimpleServer.Internals;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    public class GuildInfoHandler : IHandler
    {
        public bool CanHandle(SimpleServerRequest request)
        {
            return request.Method == "GET" && request.RawUrl == "/server.dashboard";
        }

        public void Handle(SimpleServerContext context)
        {
            // NOTE: string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
            
            // TODO replacements:
            // - Levels: Enabled/Disabled
            // - LevelsBox: !^ Enable/Disable
            // - Verified: Yes/Not Verified
            // - Antilink: Enabled/Disabled
            // - AntilinkBox: !^ Enable/Disable
            // - TruthOrDare: Enabled/Disabled
            // - TruthOrDareBox: !^ Enable/Disable
            // - JoinMessage: <join message>
            // - LeaveMessage: <leave message>
            // - AdminRole: <admin role>
            // - Roles: <roles>
            // - CommandPrefix: <prefix>
            // - WelcomeColour: <colour as hex>
        }
    }
}
