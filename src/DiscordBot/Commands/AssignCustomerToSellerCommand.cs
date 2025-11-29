using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para asignar un cliente a otro vendedor dentro del equipo.
    /// Uso: !assignCustomerToSeller C1 V3
    /// </summary>
    public class AssignCustomerToSellerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("assignCustomerToSeller")]
        [Summary("Asigna un cliente a un vendedor para redistribuir el trabajo.")]
        public async Task ExecuteAsync(string customerId, string sellerId)
        {
            string result = Facade.AssignCustomer(customerId, sellerId);
            await ReplyAsync(result);
        }
    }
}