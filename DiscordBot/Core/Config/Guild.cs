﻿namespace DiscordBot.Core.Config
{
    public class Guild
    {
        public ulong ServerId { get; set; }
        public bool CanCloseOwnTicket { get; set; }
        public string ReactionEmoji { get; set; }
        public ulong SupportChannelId { get; set; }
        public string SupportChannelName { get; set; }
        public ulong SupportCategoryId { get; set; }
        public string SupportRole { get; set; }
        public string RoleToApply { get; set; }
        public ulong ChannelId { get; set; }


    }
}