using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para obtener todos los clientes asignados a un vendedor.
    /// Uso: !getCustomerBySellerId V1
    /// </summary>
    public class GetCustomerBySellerIdCommand : ModuleBase<SocketCommandContext>
    {
        [Command("getCustomerBySellerId")]
        [Summary("Muestra todos los clientes asociados a un vendedor usando su ID.")]
        public async Task ExecuteAsync(string sellerId)
        {
            string result = Facade.ShowCustomers_BySellerId(sellerId);
            await ReplyAsync(result);
        }
    }
}
