using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library
{
    public class SuspendSellerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("suspendSeller")]
        [Summary("Suspende un vendedor por ID (solo administradores).")]
        public async Task ExecuteAsync(string sellerId)
        {
            var user = Context.User as SocketGuildUser;

            if (user == null || !user.GuildPermissions.Administrator)
            {
                await ReplyAsync("❌ No tienes permisos para suspender vendedores.");
                return;
            }

            string result = Facade.SuspendSeller(sellerId);
            await ReplyAsync(result);
        }
    }
}