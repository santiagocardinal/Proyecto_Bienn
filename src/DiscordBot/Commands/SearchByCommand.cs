using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Implementa el comando 'searchcustomer' del bot.
    /// Busca un cliente según su ID dentro del sistema.
    /// </summary>
    public class SearchCustomerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("searchcustomer")]
        [Summary("Busca un cliente por su ID. Uso: !searchcustomer <id>")]
        public async Task ExecuteAsync(
            [Summary("El ID del cliente")] string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                await ReplyAsync("Debes ingresar un ID. Ejemplo: `!searchcustomer C1`");
                return;
            }

            string result = Facade.SearchCustomer_ById(customerId);

            await ReplyAsync(result);
        }
    }
}