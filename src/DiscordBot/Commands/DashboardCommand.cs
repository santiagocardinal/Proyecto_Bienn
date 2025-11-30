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
    /// </summary>
    public class DashboardCommand : ModuleBase<SocketCommandContext>
    {
        [Command("dashboard")]
        [Summary("Muestra un panel general con totales, interacciones recientes y reuniones próximas.")]
        public async Task ExecuteAsync()
        {
            string result = Facade.GetDashboardFormatted();

            await ReplyAsync(result);
        }
    }
}


/*public class DashboardCommand : ModuleBase<SocketCommandContext>
{
    [Command("dashboard")]
    [Summary("Muestra un resumen con clientes totales, interacciones recientes y próximas reuniones.")]
    public async Task ExecuteAsync()
    {
        var customers = Facade.cm.Customers;

        //  Clientes totales
        int totalCustomers = customers.Count;

        //  Interacciones recientes (últimas 5)
        var recentInteractions = customers
            .SelectMany(c => c.Interactions)
            .OrderByDescending(i => i.Date)
            .Take(5)
            .ToList();

        // Próximas reuniones (solo Meeting en el futuro)
        var upcomingMeetings = customers
            .SelectMany(c => c.Interactions)
            .OfType<Meeting>()
            .Where(m => m.Date > DateTime.Now)
            .OrderBy(m => m.Date)
            .Take(5)
            .ToList();

        // Construcción del mensaje final
        string msg = "**PANEL GENERAL**\n";
        msg += "--------------------------------------\n\n";

        // Clientes totales
        msg += $"**Clientes totales:** {totalCustomers}\n\n";

        // Interacciones recientes
        msg += "🕒 **Interacciones recientes:**\n";
        if (recentInteractions.Count == 0)
        {
            msg += "- No hay interacciones registradas.\n";
        }
        else
        {
            foreach (var i in recentInteractions)
            {
                msg += $"- {i.Date:yyyy-MM-dd} — {i.GetType().Name} — Cliente: {i.Customer.Name}\n";
            }
        }

        msg += "\n";

        // Próximas reuniones
        msg += "**Próximas reuniones:**\n";
        if (upcomingMeetings.Count == 0)
        {
            msg += "- No hay reuniones próximas.\n";
        }
        else
        {
            foreach (var m in upcomingMeetings)
            {
                msg += $"- {m.Date:yyyy-MM-dd} — {m.Place} — Cliente: {m.Customer.Name}\n";
            }
        }

        await ReplyAsync(msg);
    }
}*/
