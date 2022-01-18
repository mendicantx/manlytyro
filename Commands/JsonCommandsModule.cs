using System.Text.RegularExpressions;
using System;
using System.Linq;
using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

public class JsonCommandsModule {

    private IJsonCommands jsonCommands;
    private string[] skateResponses;
    private string[] ignoredCommands;
    private Random randomNum;
    public JsonCommandsModule(IJsonCommands jsonCommands) {
        this.jsonCommands = jsonCommands;
        this.ignoredCommands = new[] {
            "UPDATE",
            "CUSTOMCOM",
            "REGISTER"
        };
        this.skateResponses = new[] {
            "big simpin style",
            "simpin ain't easy",
            "one day you're going to remember how much I loved you and you're going to hate yourself for letting me go",
            "i am here. watching you in the shadows.",
            "our stupid small conversations mean more to me than you'll ever know",
            "insert simp quotes",
            "i think amouranth is streaming right now..."};
        randomNum = new Random();
    }

    public async Task HandleMessage(DiscordClient discordClient, MessageCreateEventArgs eventArgs) {
        try {
            if (eventArgs.Message.Author == discordClient.CurrentUser)
                return;

            if (eventArgs.MentionedUsers.Contains(discordClient.CurrentUser))
                await discordClient.SendMessageAsync(eventArgs.Channel, "Hey fuck you too buddy!");

            SimpCommand(discordClient, eventArgs);
            //SkateCommand(discordClient, eventArgs);
            ZeroCommand(discordClient, eventArgs);
            

            if (!FirstWordIsCommand(eventArgs))
                return;
            
            var command = GetCommandName(eventArgs);

            if (ignoredCommands.Contains(command))
                return;

            var commandResult = jsonCommands.GetCommandValue(command);
            if (commandResult == string.Empty)
                return;

            await discordClient.SendMessageAsync(eventArgs.Channel, commandResult);
        } catch (Exception e) {
            await Console.Out.WriteLineAsync(e.Message);
            await Console.Out.WriteLineAsync(e.InnerException.ToString());
        }
    }

    private async void SimpCommand(DiscordClient discordClient, MessageCreateEventArgs eventArgs) {
        
        try {
            var simpCount = 0;


            simpCount = new Regex(@"(?:^|\W)SIMP(?:$|\W)").Matches(eventArgs.Message.Content.ToUpper()).Count + simpCount;
            simpCount = new Regex(@"(?:^|\W)SIMPS(?:$|\W)").Matches(eventArgs.Message.Content.ToUpper()).Count + simpCount;
            simpCount = new Regex(@"(?:^|\W)sіmp(?:$|\W)").Matches(eventArgs.Message.Content.ToUpper()).Count + simpCount;
            simpCount = new Regex(@"(?:^|\W)sіmps(?:$|\W)").Matches(eventArgs.Message.Content.ToUpper()).Count + simpCount;
            simpCount = new Regex(@"(?:^|\W)símp(?:$|\W)").Matches(eventArgs.Message.Content.ToUpper()).Count + simpCount;
            simpCount = new Regex(@"(?:^|\W)símps(?:$|\W)").Matches(eventArgs.Message.Content.ToUpper()).Count + simpCount;
            
            if (simpCount > 0)
                await discordClient.SendMessageAsync(eventArgs.Channel, "https://cdn.discordapp.com/attachments/652887929043025933/916462925395415120/unknown.png");
        } catch (Exception e) {
            await Console.Out.WriteLineAsync(e.Message);
        }

    }

    private async void SkateCommand(DiscordClient discordClient, MessageCreateEventArgs eventArgs) {
        
        try {
            if (eventArgs.Author.Id != 170578094564835328)
            // if (eventArgs.Author.Id != 291268297658466305)
                return;
            if (eventArgs.Message.CreationTimestamp.Millisecond % 13 == 0) {
                await eventArgs.Message.RespondAsync(skateResponses[randomNum.Next(0, skateResponses.Length-1)]);
            }
        } catch (Exception e) {
            await Console.Out.WriteLineAsync(e.Message);
        }

    }

        private async void ZeroCommand(DiscordClient discordClient, MessageCreateEventArgs eventArgs) {
        
        try {
            if (eventArgs.Message.Content.Contains("......"))
                await discordClient.SendMessageAsync(eventArgs.Channel, "https://c.tenor.com/TCE4pwPVl-sAAAAC/odin-oh-shit.gif");
        } catch (Exception e) {
            await Console.Out.WriteLineAsync(e.Message);
        }

    }

    private bool FirstWordIsCommand(MessageCreateEventArgs eventArgs) {
        var firstWord = eventArgs.Message.Content.Split(" ").FirstOrDefault();
        if (firstWord == null) return false;
        return firstWord.StartsWith("!");
    }

    private string GetCommandName(MessageCreateEventArgs eventArgs) {
        return eventArgs.Message.Content.Split(" ").FirstOrDefault().Substring(1).ToUpper();
    }

}