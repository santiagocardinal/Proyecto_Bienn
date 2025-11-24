using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para mostrar todos los clientes asignados a un vendedor.
    /// Uso: !showCustomersBySellerId V1
    /// </summary>
    public class ShowCustomersBySellerIdCommand : ModuleBase<SocketCommandContext>
    {
        [Command("showCustomersBySellerId")]
        [Summary("Muestra todos los clientes asociados a un vendedor por su ID.")]
        public async Task ExecuteAsync(string sellerId)
        {
            string result = Facade.ShowCustomers_BySellerId(sellerId);
            await ReplyAsync(result);
        }
    }
}
