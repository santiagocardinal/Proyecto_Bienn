using System.Threading.Tasks;
using Discord.Commands;
namespace Library;

public class ModifyCustomerCommand
{
    /// <summary>
    /// Comando para modificar los datos de un cliente por Id.
    /// Uso: !modifyCustomer id field newValue
    /// </summary>
    public class ModifyCustomerById : ModuleBase<SocketCommandContext>
    {
        [Command("modifyCustomer")]
        [Summary("Modifica las credenciales d eun cliente.")]
        public async Task ExecuteAsync(string id, string field, string newValue)
        {
            string result = Facade.ModifyCustomer(id, field, newValue);
            await ReplyAsync(result);
        }
    }
}