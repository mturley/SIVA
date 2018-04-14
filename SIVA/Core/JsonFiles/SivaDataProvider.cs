using System.Collections.Generic;
using System.Linq;
using Discord;
using SIVA.Core.Bot;
using SIVA.WebPanel.Backend;

namespace SIVA.Core.JsonFiles
{
    public class SivaDataProvider : ISivaDataProvider
    {
        public bool IsLevelsEnabled(string id)
        {
            return GuildConfig.GetGuildConfig(ulong.Parse(id)).Leveling;
        }

        public void LevelsChange(string id)
        {
            GuildConfig.GetGuildConfig(ulong.Parse(id)).Leveling =
                !GuildConfig.GetGuildConfig(ulong.Parse(id)).Leveling;
            GuildConfig.SaveGuildConfig();
        }

        public IEnumerable<SivaGuild> GetGuilds(string username, int descriminator)
        {
            return Program._client.Guilds.Where(x =>
                x.Users.Count(y => y.Username == username && y.Discriminator == descriminator.ToString()) !=
                0).Select(guild => new SivaGuild() {GuildId = guild.Id, GuildName = guild.Name});
        }
    }
}