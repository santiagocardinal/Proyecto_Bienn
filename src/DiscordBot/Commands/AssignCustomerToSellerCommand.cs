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
        [Command("exchangeCustomer")]
        [Summary("Asigna un cliente a un vendedor para redistribuir el trabajo.")]
        public async Task ExecuteAsync(string customerId, string oldSellerId, string newSellerId)
        {
            string result = Facade.ExchangeCustomer(customerId, oldSellerId, newSellerId);
            await ReplyAsync(result);
        }
    }
}