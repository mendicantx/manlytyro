using System;
using System.Linq;
using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

public class JsonCommandsModule {

    private IJsonCommands jsonCommands;
    public JsonCommandsModule(IJsonCommands jsonCommands) {
        this.jsonCommands = jsonCommands;
    }

    public async Task HandleMessage(DiscordClient discordClient, MessageCreateEventArgs eventArgs) {
        if (eventArgs.Message.Author == discordClient.CurrentUser)
            return;
            
        if (!FirstWordIsCommand(eventArgs))
            return;
        
        var commandResult = jsonCommands.GetCommandValue(GetCommandName(eventArgs));
        if (commandResult == string.Empty)
            return;

        await discordClient.SendMessageAsync(eventArgs.Channel, commandResult);

    }

    private bool FirstWordIsCommand(MessageCreateEventArgs eventArgs) {
        var firstWord = eventArgs.Message.Content.Split(" ").FirstOrDefault();
        if (firstWord == null) return false;
        return firstWord.StartsWith("!");
    }

    private string GetCommandName(MessageCreateEventArgs eventArgs) {
        return eventArgs.Message.Content.Split(" ").FirstOrDefault().Replace("!", "");
    }

}