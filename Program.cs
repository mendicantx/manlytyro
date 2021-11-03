using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using ManlyTyro.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManlyTyro
{
    class Program
    {
        private CancellationTokenSource _cts;

        private IConfigurationRoot _config;

        private DiscordClient _discord;

        static async Task Main(string[] args) => await new Program().InitBot(args);

        async Task InitBot(string[] args)
        {
            try
            {
                Console.WriteLine("[info] Starting ManlyTyro...");
                _cts = new CancellationTokenSource();

                Console.WriteLine("[info] Loading configuration...");
                _config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                    .AddUserSecrets<Program>()
                    .Build();

                Console.WriteLine("[info] Loading services...");
                var services = new ServiceCollection()
                    .BuildServiceProvider();

                Console.WriteLine("[info] Initializing Discord client...");
                _discord = new DiscordClient(new DiscordConfiguration
                {
                    Token = _config.GetValue<string>("discord:token"),               
                    TokenType = TokenType.Bot,
                    Intents = DiscordIntents.AllUnprivileged
                });

                Console.WriteLine("[info] Registering custom commands...");
                var commands = _discord.UseCommandsNext(new CommandsNextConfiguration
                {
                    StringPrefixes = new [] { _config.GetValue<string>("discord:commandPrefix") },
                    Services = services
                });

                commands.RegisterCommands<HumorModule>();
                commands.RegisterCommands<ReferenceModule>();
            }
            catch(Exception exInit)
            {
                Console.Error.WriteLine(exInit.ToString());
            }

            RunAsync(args).Wait();
        }

        async Task RunAsync(string[] args)
        {
            Console.WriteLine("[info] Connecting Discord client...");
            await _discord.ConnectAsync();
            Console.WriteLine("[info] Connected!");

            while(!_cts.IsCancellationRequested)
                await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }
}
