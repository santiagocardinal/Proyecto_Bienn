using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library
{
    /// <summary>
    /// Esta clase implementa el comando 'showcustomers' del bot.
    /// Este comando muestra todos los clientes asignados a un vendedor específico.
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class ShowCustomersCommand : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Implementa el comando 'showcustomers'.
        /// </summary>
        /// <param name="sellerId">El ID del vendedor.</param>
        [Command("showcustomers")]
        [Summary("Muestra todos los clientes asignados a un vendedor. Uso: !showcustomers <sellerId>")]
        // ReSharper disable once UnusedMember.Global
        public async Task ExecuteAsync(
            [Summary("El ID del vendedor")] string sellerId)
        {
            string result = Facade.ShowCustomers_BySellerId(sellerId);
            await ReplyAsync(result);
        }
    }
}
