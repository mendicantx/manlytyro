using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace ManlyTyro.Commands
{
    public class ReferenceModule : BaseCommandModule
    {
        private string currentAbuLink = string.Empty;
        private string currentWaffleLink = string.Empty;
        private string currentAngerLink = "https://cdn.discordapp.com/attachments/653313996241371137/914734144041394226/db7f3f1.jpg";

        public IJsonCommands jsonCommands { private get; set; }

        [Command("register")]
        [Description("Registers a new custom command")]
        public async Task RegisterCommand(CommandContext ctx, string command) {

            await ctx.Channel.SendMessageAsync(jsonCommands.RegisterCommand(command));
        }
        
        [Command("update")]
        [Description("Updates or Creates a new custom command")]
        public async Task UpdateCommand(CommandContext ctx, string command, [RemainingText] string commandValue) {

            await ctx.Channel.SendMessageAsync(jsonCommands.UpdateCommand(command, commandValue));
        }
        
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

        [Command("waffle")]
        [Description("wafflez")]
        public async Task WaffleCommand(CommandContext ctx)
        {
            if(string.IsNullOrWhiteSpace(currentWaffleLink))
                await ctx.Channel.SendMessageAsync("Waffle diagram not set. Please set with `!update_waffle [image_url}`.");
            else
                await ctx.Channel.SendMessageAsync($"{currentWaffleLink}");    
        }

        [Command("update_waffle")]
        [Description("Updates the current diagram for the waffle command.")]
        [Hidden]
        [RequireRoles(RoleCheckMode.Any, "Admin", "データマイナー")]
        public async Task WaffleCommand(CommandContext ctx, [Description("The new image URL for the waffle command.")]string newUrl)
        {
            if(string.IsNullOrWhiteSpace(newUrl))
                await ctx.Message.RespondAsync("You must provide a URL.");
            else
            {
                currentWaffleLink = newUrl;
                await ctx.Message.RespondAsync("Waffle diagram updated.");
            }
        }

        [Command("anger")]
        [Description("anger")]
        public async Task AngerCommand(CommandContext ctx)
        {
            if(string.IsNullOrWhiteSpace(currentAngerLink))
                await ctx.Channel.SendMessageAsync("Anger diagram not set. Please set with `!update_anger [image_url}`.");
            else
                await ctx.Channel.SendMessageAsync($"{currentAngerLink}");    
        }

        [Command("update_anger")]
        [Description("Updates the current diagram for the anger command.")]
        [Hidden]
        [RequireRoles(RoleCheckMode.Any, "Admin", "データマイナー")]
        public async Task AngerCommand(CommandContext ctx, [Description("The new image URL for the anger command.")]string newUrl)
        {
            if(string.IsNullOrWhiteSpace(newUrl))
                await ctx.Message.RespondAsync("You must provide a URL.");
            else
            {
                currentAngerLink = newUrl;
                await ctx.Message.RespondAsync("Anger diagram updated.");
            }
        }
    }
}