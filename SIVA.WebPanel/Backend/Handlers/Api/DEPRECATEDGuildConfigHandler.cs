using System;
using System.Collections.Generic;
using System.Text;
using SimpleServer.Handlers;
using SimpleServer.Internals;

namespace SIVA.WebPanel.Backend.Handlers.Api
{
    [SivaIgnore]
    [Obsolete]
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
            // - Embed Colour: <input type="color" id="html5colorpicker" class="" style="background: rgba(0,0,0,0); border: none;" value="#<colour as hex>" style="width:85%;" onChange="updateAllColours();">
            // -               or <a href="">Unavailable</a>

            /* JUST SO YOU KNOW, PERKS:
             *      The namespace for managing JSON files is SIVA.Core.JsonFiles
             *      The class is GuildConfig
             *      The methods are GetOrCreateConfig, GetGuildConfig, SaveGuildConfig, and CreateGuildConfig
             *      To get the config for a guild, simply do `var config = GuildConfig.GetGuildConfig(Server ID)`
             *      That will return a list of all values you can modify. Use config.{ConfigOption} to access/set the values
             *      Use GuildConfig.SaveGuildConfig(); to save the new config. The values you can use are stored in the SIVA.Core.JsonFiles.GuildConfig file, 
             *      at the first class defined.
             *      any questions just ask me
             *                                          -Greem
             */
        }
    }
}
