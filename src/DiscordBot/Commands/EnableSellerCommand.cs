using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library
{
    public class EnableSellerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("enableSeller")]
        [Summary("Habilita un vendedor por ID (solo administradores).")]
        public async Task ExecuteAsync(string sellerId)
        {
            var user = Context.User as SocketGuildUser;

            // Verificar permisos de administrador
            if (user == null || !user.GuildPermissions.Administrator)
            {
                await ReplyAsync("No tienes permisos para habilitar vendedores.");
                return;
            }

            // Ejecutar operación en la fachada
            string result = Facade.EnableSeller(sellerId);
            await ReplyAsync(result);
        }
    }
}