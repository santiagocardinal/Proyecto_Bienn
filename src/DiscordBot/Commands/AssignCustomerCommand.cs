
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para asignar un cliente a un vendedor.
    /// Uso: !assignCustomer C001 V001
    /// </summary>
    public class AssignCustomerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("assignCustomer")]
        [Summary("Asigna un cliente a un vendedor para distribuir el trabajo en el equipo.")]
        public async Task ExecuteAsync(
            string customerId,
            string sellerId)
        {
            string result = Facade.AssignCustomer(customerId, sellerId);
            await ReplyAsync(result);
        }
    }
}