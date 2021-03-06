﻿using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using SIVA.Core.Bot;
using SIVA.Core.JsonFiles;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Discord.Net;
using System;
using Microsoft.CodeAnalysis.Operations;

namespace SIVA.Core.Modules.Management
{
    public class Owner : ModuleBase<SocketCommandContext>
    {

        [Command("Shutdown")]
        [RequireOwner]
        public async Task Shutdown()
        {
            var client = Program._client;
            var embed = new EmbedBuilder()
                .WithDescription(Bot.Utilities.GetFormattedLocaleMsg("LoggingOutMsg", Context.User.Mention))
                .WithColor(Config.bot.DefaultEmbedColour)
                .WithFooter(Bot.Utilities.GetFormattedLocaleMsg("CommandFooter", Context.User.Username));
            await ReplyAsync("", false, embed);
            await client.LogoutAsync();
            await client.StopAsync();
        }

        [Command("NotifyBotUsers"), Alias("Nbu")]
        [RequireOwner]
        public async Task NotifyPeopleWhoUseBot([Remainder]string message)
        {
            var client = Program._client;
            var embed = new EmbedBuilder()
                .WithDescription(message)
                .WithTitle("Message from Greem (Bot Creator)")
                .WithColor(Config.bot.DefaultEmbedColour);

            foreach (SocketGuild server in client.Guilds)
            {
                var dm = await server.Owner.GetOrCreateDMChannelAsync();
                
                try {
                    await dm.SendMessageAsync("", false, embed);
                } catch (RateLimitedException e)
                {
                    Console.WriteLine($"ratelimited. {e.Message}");
                }
            }

            await ReplyAsync($"Successfully sent `{message}` to all server owners.");
        }

        [Command("VerifyGuild"), Alias("Verify")]
        [RequireOwner]
        public async Task VerifyGuildById(ulong guildId = 0)
        {
            var id = guildId;
            if (id == 0) id = Context.Guild.Id;
            var config = GuildConfig.GetGuildConfig(id);
            config.VerifiedGuild = true;
            var embed = new EmbedBuilder()
                .WithDescription("Successfully verified this server.")
                .WithColor(Config.bot.DefaultEmbedColour)
                .WithFooter(Bot.Utilities.GetFormattedLocaleMsg("CommandFooter", Context.User.Username));
            await ReplyAsync("", false, embed);
        }

        [Command("Eval")]
        [RequireOwner]
        public async Task EvaluateCSharpCode([Remainder]string code)
        {
            var result = await CSharpScript.EvaluateAsync(code, ScriptOptions.Default.AddImports("System", "System.IO", "System.Collections.Generic", "System.Threading.Tasks", "System.Threading"));
            var embed = new EmbedBuilder()
                .WithDescription($"Input: \n```cs\n{code}```\n\nOutput: `{result}`")
                .WithColor(Config.bot.DefaultEmbedColour);
            await ReplyAsync("", false, embed);
        }

        [Command("Stream")]
        [RequireOwner]
        public async Task SetBotStream(string streamer, [Remainder]string streamName)
        {
            await Program._client.SetGameAsync(streamName, $"https://twitch.tv/{streamer}", StreamType.Twitch);
            var embed = Helpers.CreateEmbed(Context, $"Set the stream name to **{streamName}**, and set the streamer to <https://twitch.tv/{streamer}>!");
            await Helpers.SendMessage(Context, embed);
        }


        [Command("Game")]
        [RequireOwner]
        public async Task SetBotGame([Remainder] string game)
        {
            var client = Program._client;

            var embed = new EmbedBuilder();
            embed.WithDescription($"Set the bot's game to {game}");
            embed.WithColor(Bot.Config.bot.DefaultEmbedColour);
            embed.WithFooter(Bot.Utilities.GetFormattedLocaleMsg("CommandFooter", Context.User.Username));
            await client.SetGameAsync(game);
            await ReplyAsync("", false, embed);
        }

        [Command("Status")]
        [RequireOwner]
        public async Task SetBotStatus(string status)
        {
            var embed = new EmbedBuilder();
            embed.WithDescription($"Set the status to {status}.");
            embed.WithFooter(Bot.Utilities.GetFormattedLocaleMsg("CommandFooter", Context.User.Username));
            embed.WithColor(Bot.Config.bot.DefaultEmbedColour);

            var client = Program._client;

            switch (status)
            {
                case "dnd":
                    await client.SetStatusAsync(UserStatus.DoNotDisturb);
                    break;
                case "idle":
                    await client.SetStatusAsync(UserStatus.Idle);
                    break;
                case "online":
                    await client.SetStatusAsync(UserStatus.Online);
                    break;
                case "offline":
                    await client.SetStatusAsync(UserStatus.Invisible);
                    break;
            }

            await ReplyAsync("", false, embed);
        }

        [Command("LeaveServer")]
        [RequireOwner]
        public async Task LeaveServer()
        {
            var embed = new EmbedBuilder();
            embed.WithDescription(Bot.Utilities.GetLocaleMsg("BotLeftServer"));
            embed.WithColor(Bot.Config.bot.DefaultEmbedColour);
            embed.WithFooter(Bot.Utilities.GetFormattedLocaleMsg("CommandFooter", Context.User.Username));
            await ReplyAsync("", false, embed);
            await Context.Guild.LeaveAsync();
        }

        [Command("ServerCount"), Alias("Sc")]
        [RequireOwner]
        public async Task ServerCountStream()
        {
            var client = Program._client;
            var guilds = Context.Client.Guilds.Count;
            var embed = new EmbedBuilder();
            embed.WithDescription("Done.");
            embed.WithColor(Bot.Config.bot.DefaultEmbedColour);
            embed.WithFooter(Bot.Utilities.GetFormattedLocaleMsg("CommandFooter", Context.User.Username));
            await ReplyAsync("", false, embed);
            await client.SetGameAsync($"in {(Context.Client as DiscordSocketClient).Guilds.Count} servers!", $"https://twitch.tv/{Config.bot.TwitchStreamer}", StreamType.Twitch);

        }
    }
}
