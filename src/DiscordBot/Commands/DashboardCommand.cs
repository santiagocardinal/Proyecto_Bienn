using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Panel de resumen general del sistema.
    /// Muestra clientes totales, interacciones recientes y reuniones próximas.
    /// Uso: !dashboard
    /// </summary>
    public class DashboardCommand : ModuleBase<SocketCommandContext>
    {
        [Command("dashboard")]
        [Summary("Muestra un panel general con totales, interacciones recientes y reuniones próximas.")]
        public async Task Dashboard()
        {
            string result = Facade.GetDashboardFormatted();

            await ReplyAsync(result);
        }
    }
}

