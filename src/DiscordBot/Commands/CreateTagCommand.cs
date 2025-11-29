using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para crear una etiqueta (Tag).
    /// Uso: !createTag T1 VIP "Cliente muy importante"
    /// </summary>
    public class CreateTagCommand : ModuleBase<SocketCommandContext>
    {
        [Command("createTag")]
        [Summary("Crea una etiqueta con ID, nombre y descripción.")]
        public async Task ExecuteAsync(
            string tagId,
            string tagName,
            [Remainder] string tagDescription)
        {
            Tag tag = new Tag(tagId, tagName, tagDescription);
            
            string result = Facade.CreateTag(tagId, tagName, tagDescription);
            
            await ReplyAsync(result);
            
            /*string message =
                $"Etiqueta creada:\n" +
                $"- ID: {tag.Id}\n" +
                $"- Nombre: {tag.Name}\n" +
                $"- Descripción: {tag.Description}";

            await ReplyAsync(message);*/
        }
    }
}
