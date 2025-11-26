using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para agregar una etiqueta (tag) a un cliente.
    /// Uso: !addTag C1 T1 Interesado "Cliente mostró interés en el producto"
    /// </summary>
    public class AddTagCommand : ModuleBase<SocketCommandContext>
    {
        [Command("addTag")]
        [Summary("Agrega una etiqueta a un cliente usando su ID.")]
        public async Task ExecuteAsync(
            string customerId,
            string tagId,
            string tagName,
            string tagDescription)
        {
            string result = Facade.AddTag_Customer(customerId, tagId, tagName, tagDescription);
            await ReplyAsync(result);
        }
    }
}