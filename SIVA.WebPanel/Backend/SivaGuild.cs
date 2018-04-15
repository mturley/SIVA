namespace SIVA.WebPanel.Backend
{
    public class SivaGuild
    {
        public static SivaGuild Empty => new SivaGuild(){IsEmpty = true};

        public static bool IsNullOrEmpty(SivaGuild guild)
        {
            return guild == null || guild.IsEmpty;
        }
        internal bool IsEmpty { get; set; } = false;
        public ulong GuildId { get; set; }
        public string GuildName { get; set; }
    }
}