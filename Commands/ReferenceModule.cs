using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Linq;

namespace ManlyTyro.Commands
{
    public class ReferenceModule : BaseCommandModule
    {
        private string currentAbuLink = string.Empty;

        public IJsonCommands jsonCommands { private get; set; }

        [Command("register")]
        [Description("Registers a new custom command")]
        public async Task RegisterCommand(CommandContext ctx, string command) {

            command = command.ToUpper();
            await ctx.Channel.SendMessageAsync(jsonCommands.RegisterCommand(command));
        }
        
        [Command("update")]
        [Description("Updates or Creates a new custom command")]
        public async Task UpdateCommand(CommandContext ctx, string command, [RemainingText] string commandValue) {

            command = command.ToUpper();
            await ctx.Channel.SendMessageAsync(jsonCommands.UpdateCommand(command, commandValue));
        }

        [Command("customcom")]
        [Description("Lists all custom commands")]
        public async Task CustomComCommand(CommandContext ctx) {

            var allCommands = jsonCommands.GetCommands().OrderBy(x => x);
            int pageSize = 20;
            int totalPages = (allCommands.Count() + pageSize - 1)/pageSize;
            for (int page = 0; page < totalPages; page++) {
                var currentCommands = allCommands.Skip(page*pageSize).Take(pageSize);
                var returnString = string.Join(Environment.NewLine, currentCommands);
                returnString = $"Custom Commands page {page+1} of {totalPages}{Environment.NewLine}{returnString}{Environment.NewLine}";
                await ctx.Channel.SendMessageAsync(returnString);

            }
            
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

    }
}