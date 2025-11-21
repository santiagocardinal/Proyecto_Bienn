using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Elimina un cliente del CRM por ID.
    /// </summary>
    public class DeleteCustomerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("deletecustomer")]
        [Summary("Elimina un cliente por ID. Uso: !deletecustomer <id>")]
        public async Task ExecuteAsync(string id = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                await ReplyAsync("Debes indicar el ID del cliente. Ejemplo: `!deletecustomer C1`");
                return;
            }

            string result = Facade.DeleteCustomer(id);
            await ReplyAsync(result);
        }
    }
}