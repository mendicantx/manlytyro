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

        if (eventArgs.MentionedUsers.Contains(discordClient.CurrentUser))
            await discordClient.SendMessageAsync(eventArgs.Channel, "Hey fuck you too buddy!");

        if (eventArgs.Message.Content.ToUpper().Contains(" SIMP ") || 
            eventArgs.Message.Content.ToUpper().Contains(" SIMP.") ||
            eventArgs.Message.Content.ToUpper() == "SIMP")
            await discordClient.SendMessageAsync(eventArgs.Channel, "https://cdn.discordapp.com/attachments/652887929043025933/916462925395415120/unknown.png");
        

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