using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Interactions;

namespace DiscordBot.Services
{
    /// <summary>
    /// Esta clase implementa el bot de Discord.
    /// </summary>
    public class Bot : IBot
    {
        private ServiceProvider serviceProvider;
        private readonly ILogger<Bot> logger;
        private readonly IConfiguration configuration;
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;

        // 🔥 AGREGADO: InteractionService para slash commands
        private readonly InteractionService interactions;

        public Bot(ILogger<Bot> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;

            DiscordSocketConfig config = new DiscordSocketConfig()
            {
                AlwaysDownloadUsers = true,
                GatewayIntents =
                    GatewayIntents.AllUnprivileged
                    | GatewayIntents.GuildMembers
                    | GatewayIntents.MessageContent
            };

            client = new DiscordSocketClient(config);
            commands = new CommandService();

            // 🔥 AGREGADO
            interactions = new InteractionService(client);
        }

        public async Task StartAsync(ServiceProvider services)
        {
            serviceProvider = services;

            string discordToken = configuration["DiscordToken"];
            if (discordToken == null)
                throw new Exception("Falta el token");

            logger.LogInformation($"Iniciando bot...");

            // Carga módulos de comandos de texto !
            await commands.AddModulesAsync(Assembly.GetExecutingAssembly(), serviceProvider);

            // 🔥 Carga módulos de slash commands /
            await interactions.AddModulesAsync(Assembly.GetExecutingAssembly(), serviceProvider);

            client.Log += Log;

            // Manejar comandos de texto "!"
            client.MessageReceived += HandleCommandAsync;

            // 🔥 Manejar slash commands "/"
            client.InteractionCreated += HandleInteractionAsync;

            // 🔥 Registrar slash commands cuando el bot está listo
            client.Ready += async () =>
            {
                ulong guildId = 1440397599608803459; // <- PEGAR ACÁ EL ID DE TU SERVIDOR

                await interactions.RegisterCommandsToGuildAsync(guildId);

                logger.LogInformation("Slash commands registrados en el servidor.");
            };

            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task StopAsync()
        {
            logger.LogInformation("Finalizando");
            await client.LogoutAsync();
            await client.StopAsync();
        }

        // 🔵 Comandos texto !
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null || message.Author.IsBot)
                return;

            int position = 0;
            if (message.HasCharPrefix('!', ref position))
            {
                await commands.ExecuteAsync(
                    new SocketCommandContext(client, message),
                    position,
                    serviceProvider);
            }
        }

        // 🔵 Slash commands /
        private async Task HandleInteractionAsync(SocketInteraction interaction)
        {
            try
            {
                var context = new SocketInteractionContext(client, interaction);
                await interactions.ExecuteCommandAsync(context, serviceProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                if (interaction.Type == InteractionType.ApplicationCommand)
                    await interaction.GetOriginalResponseAsync()
                        .ContinueWith(async msg => await msg.Result.DeleteAsync());
            }
        }
    }
}
