using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;


public interface IJsonCommands {

    string RegisterCommand(string command);
    string UpdateCommand(string command, string commandValue);
    string GetCommandValue(string command);
}
public class JsonCommands : IJsonCommands {

    private IDictionary<string, string> commands;
    private const string commandsFile = "commands.json";

    public JsonCommands() {
        var text = File.ReadAllText(commandsFile);
        commands = JsonSerializer.Deserialize<IDictionary<string, string>>(text);

    }

    private void WriteCommands() {
        var text = JsonSerializer.Serialize(commands);
        File.WriteAllText(commandsFile, text);
    }

    public string UpdateCommand(string command, string commandValue) {
        var returnMessage = new StringBuilder();
        if (!commands.ContainsKey(command))
            returnMessage.AppendLine($"Command doesn't exist. Adding.");

        commands[command] = commandValue;

        WriteCommands();
        returnMessage.AppendLine($"Updated {command} to {commandValue}");
        return returnMessage.ToString();
    }

    public string GetCommandValue(string command) {
        if (commands.ContainsKey(command))
            return commands[command];
        return string.Empty;
    }

    public string RegisterCommand(string command) {
        if (commands.ContainsKey(command))
            return $"{command} already exists.";
        
        commands[command] = $"Please set a value for !{command}";
        WriteCommands();
        return $"Registered command {command}!";

    }
}