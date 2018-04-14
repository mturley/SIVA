namespace SIVA.WebPanel.Backend
{
    public interface ISivaDataProvider
    {
        bool IsLevelsEnabled();
        void LevelsChange(string id);
        SivaGuild GetGuilds(string username,int descriminator);
    }
}