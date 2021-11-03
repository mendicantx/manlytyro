using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace ManlyTyro.Commands
{
    public class ReferenceModule : BaseCommandModule
    {
        private string currentAbuLink = string.Empty;

        [Command("enlir")]
        [Description("Provides links to helpful FFRK information.")]
        public async Task EnlirCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("FFRK Community Database - <https://goo.gl/eirgjN>");
        }        

        [Command("abu")]
        [Description("Provides diagrams with helpful Labyrinth information.")]
        public async Task AbuCommand(CommandContext ctx)
        {
            if(string.IsNullOrWhiteSpace(currentAbuLink))
                await ctx.Channel.SendMessageAsync("Abu diagram not set. Please set with `!update_abu [image_url}`.");
            else
                await ctx.Channel.SendMessageAsync($"{currentAbuLink}");    
        }

        [Command("update_abu")]
        [Description("Updates the current diagram for the Abu command.")]
        [Hidden]
        [RequireRoles(RoleCheckMode.Any, "Admin", "データマイナー")]
        public async Task AbuCommand(CommandContext ctx, [Description("The new image URL for the abu command.")]string newUrl)
        {
            if(string.IsNullOrWhiteSpace(newUrl))
                await ctx.Message.RespondAsync("You must provide a URL.");
            else
            {
                currentAbuLink = newUrl;
                await ctx.Message.RespondAsync("Abu diagram updated.");
            }
        }
    }
}