using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library
{
    /// <summary>
    /// Comando para habiliar sellers, lo puede ejecutar unicamente un seller administrador del servidor
    /// Uso: !enableSeller S1
    /// </summary>
    public class EnableSellerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("enableSeller")]
        [Summary("Habilita un vendedor por ID (solo administradores).")]
        public async Task ExecuteAsync(string sellerId)
        {
            var user = Context.User as SocketGuildUser;
            
            if (user == null || !user.GuildPermissions.Administrator)
            {
                await ReplyAsync("No tienes permisos para habilitar vendedores.");
                return;
            }
            string result = Facade.EnableSeller(sellerId);
            await ReplyAsync(result);
        }
    }
}