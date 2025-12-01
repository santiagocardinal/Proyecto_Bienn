using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    
    public class TagCommand : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Comando para crear una etiqueta (Tag).
        /// Uso: !createTag T1 VIP "Cliente muy importante"
        /// </summary>
        [Command("createTag")]
        [Summary("Crea una etiqueta con ID, nombre y descripción.")]
        public async Task CreateTagCommand(
            string tagId,
            string tagName,
            [Remainder] string tagDescription)
        {
            Tag tag = new Tag(tagId, tagName, tagDescription);

            string result = Facade.CreateTag(tagId, tagName, tagDescription);

            await ReplyAsync(result);
        }

        /// <summary>
        /// Comando para agregar una etiqueta (tag) a un cliente.
        /// Uso: !addTag C1 T1 Interesado "Cliente mostró interés en el producto"
        /// </summary>
        [Command("addTag")]
        [Summary("Agrega una etiqueta a un cliente usando su ID.")]
        public async Task AddTagCommand(
            string customerId,
            string tagId)
        {
            string result = Facade.AddTag_Customer(customerId, tagId);
            await ReplyAsync(result);
        }
    }
}