/*using System;
using System.Threading.Tasks;
using Discord.Interactions;
using Library;

namespace Library;*/


/*using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library
{
    //<summary>
    ///Comando para registrar una seller, solo se genera un nuevo seller si el usuario que lo ejecuta es administrador.
    /// Uso: !createSeller S1 Raul raul@mail.com 099333666
    /// </summary>
    public class CreateSellerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("createSeller")]
        [Summary("Crea un nuevo vendedor (solo administradores).")]
        public async Task ExecuteAsync(
            string id,
            string name,
            string mail,
            string phone)
        {
            var author = Context.User as SocketGuildUser;

            if (author == null)
            {
                await ReplyAsync("Error interno: no se pudo determinar el usuario.");
                return;
            }
            

           ///<summary>
           /// LLama al método para verificar si el usuario es administrador (no verifica roles creados manuales en discord)
           /// </summary>
            if (!UserIsAdmin(author))
            {
                await ReplyAsync("No tienes permisos. Solo administradores pueden crear vendedores.");
                return;
            }

            
            string result = Facade.CreateSeller(id, name, mail, phone); 
            await ReplyAsync(result);
        }

        /// <summary>
        /// Verifica que el usuario tenga permisos reales de administrador del servidor.
        /// </summary>
        private bool UserIsAdmin(SocketGuildUser user)
        {
            return user.GuildPermissions.Administrator;
        }
    }
}
*/
