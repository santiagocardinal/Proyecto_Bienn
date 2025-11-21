using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library
{
    /// <summary>
    /// Implementa el comando 'deleteuser' para eliminar un cliente del sistema
    /// según su nombre en Discord.
    /// </summary>
    public class DeleteUserCommand : ModuleBase<SocketCommandContext>
    {
        [Command("deleteuser")]
        [Summary("Elimina al usuario indicado por nombre.")]
        public async Task ExecuteAsync(
            [Remainder]
            [Summary("El nombre del usuario a eliminar.")]
            string displayName = null)
        {
            if (displayName == null)
            {
                await ReplyAsync("Debes indicar el nombre del usuario a eliminar.");
                return;
            }

            // 1. Buscar usuario en Discord
            SocketGuildUser user = CommandHelper.GetUser(Context, displayName);

            if (user == null)
            {
                await ReplyAsync($"No encuentro el usuario '{displayName}' en esta aplicación.");
                return;
            }

            // 2. Obtener el DisplayName real
            string resolvedName = CommandHelper.GetDisplayName(Context, displayName);

            // 3. Buscar cliente en tu sistema (Facade)
            Customer customer;
            try
            {
                customer = Facade.cm.SearchByName(resolvedName);
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
                return;
            }

            // 4. Eliminar cliente por ID (la única forma válida en tu Facade)
            string result = Facade.DeleteCustomer(customer.Id);

            await ReplyAsync(result);
        }
    }
}