using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library
{
    /*/// <summary>
    /// Comando para eliminar sellers, lo puede ejecutar unicamente un seller administrador del servidor
    /// Uso: !deleteSeller S1
    /// </summary>
    public class DeleteSellerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("deleteSeller")]
        [Summary("Elimina un vendedor por ID (solo administradores).")]
        public async Task ExecuteAsync(string sellerId)
        {
            var user = Context.User as SocketGuildUser;

            if (user == null || !user.GuildPermissions.Administrator)
            {
                await ReplyAsync(" No tienes permisos para eliminar vendedores.");
                return;
            }

            string result = Facade.DeleteSeller(sellerId);
            await ReplyAsync(result);
        }
    }*/
}