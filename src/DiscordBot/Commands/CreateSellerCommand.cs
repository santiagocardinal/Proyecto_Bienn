/*using System;
using System.Threading.Tasks;
using Discord.Interactions;
using Library;

namespace Library;*/


using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library
{
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

            // DEBUG temporal para ver roles
            /*string roles = string.Join(", ", author.Roles.Select(r => r.Name));
            await ReplyAsync($"Tus roles: {roles}");*/

            // ---------------------------
            // **Verificación real de permisos**
            // ---------------------------
            if (!UserIsAdmin(author))
            {
                await ReplyAsync("No tienes permisos. Solo administradores pueden crear vendedores.");
                return;
            }

            // ---------------------------
            // 2. Llamar a la fachada
            // ---------------------------
            string result = Facade.CreateSeller(id, name, mail, phone); 
            // Pasé "1234" porque tu Facade lo exige.
            // Si querés que no use password, te lo ajusto después.

            // ---------------------------
            // 3. Responder
            // ---------------------------
            await ReplyAsync(result);
        }

        /// <summary>
        /// Verifica que el usuario tenga permisos reales de administrador del servidor.
        /// </summary>
        private bool UserIsAdmin(SocketGuildUser user)
        {
            // Permiso REAL de Discord (la forma correcta)
            return user.GuildPermissions.Administrator;
        }
    }
}





/*public class CreateSellerCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("create_seller", "Crea un nuevo vendedor si la contraseña es correcta.")]
    public async Task CreateSellerAsync(
        string id,
        string name,
        string mail,
        string phone,
        string password)
    {
        string result = Facade.CreateSeller(id, name, mail, phone, password);

        await RespondAsync(result, ephemeral: true);
    }
}*/

