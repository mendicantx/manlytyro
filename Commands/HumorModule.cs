using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace ManlyTyro.Commands
{
    public class HumorModule : BaseCommandModule
    {
        [Command("mindread"), Description("Tells you your deepest FFRK desires.")]
        public async Task MindreadCommand(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await Task.Delay(TimeSpan.FromSeconds(3));

            var emoji = DiscordEmoji.FromName(ctx.Client, ":mythril:");

            await ctx.Message.RespondAsync(GetMindreadMessage(ctx.Client));
        }

        [Command("elnino"), Description("Does what the French do.")]
        public async Task ElNinoCommand(CommandContext ctx)
        {
            var emoji = DiscordEmoji.FromName(ctx.Client, ":french_bread:");

            await ctx.Channel.SendMessageAsync($"{emoji}");
        }

        private string GetMindreadMessage(DiscordClient client)
        {
            var messageIndex = new Random().Next(1, 8);

            var mythril = DiscordEmoji.FromName(client, ":mythril:");
            var baguette = DiscordEmoji.FromName(client, ":french_bread:");
            var enlir = DiscordEmoji.FromName(client, ":enlir:");
            var praise = DiscordEmoji.FromName(client, ":praise:");
            var lolz = DiscordEmoji.FromName(client, ":lolz:");

            return messageIndex switch
            {
                1 => $"You want a {baguette}.",
                2 => $"You want {enlir} to come back.",
                3 => $"{praise} KamiW!",
                4 => $"You want Cloud DAASB.",
                5 => $"You want a free 100-pull. {lolz}",
                _ => $"You want 50 {mythril}."
            };
        }
    }
}