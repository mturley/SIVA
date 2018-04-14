using System.Collections.Generic;

namespace SIVA.WebPanel.Backend
{
    public interface ISivaDataProvider
    {
        bool IsLevelsEnabled(string id);
        void LevelsChange(string id);
        IEnumerable<SivaGuild> GetGuilds(string username,int descriminator);
    }
}