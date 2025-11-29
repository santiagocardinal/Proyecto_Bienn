using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para obtener todos los clientes que tienen interacciones sin responder
    /// desde hace más de X días.
    /// Uso: !unansweredCustomers 30
    /// </summary>
    public class UnansweredCustomersCommand : ModuleBase<SocketCommandContext>
    {
        [Command("unansweredCustomers")]
        [Summary("Muestra los clientes con interacciones sin responder más viejas que X días.")]
        public async Task ExecuteAsync(int days)
        {
            if (days <= 0)
            {
                await ReplyAsync("Debes ingresar un número de días mayor a cero.");
                return;
            }

            DateTime limit = DateTime.Now.AddDays(-days);
            var customers = Facade.cm.Customers;

            var resultList = customers
                .Where(c =>
                    c.GetUnansweredInteractions().Any(i => i.Date < limit))
                .Select(c => $"- {c.Name} (ID: {c.Id})")
                .ToList();

            if (resultList.Count == 0)
            {
                await ReplyAsync($"No hay clientes con interacciones sin responder de hace más de {days} días.");
                return;
            }

            string message =
                $"Clientes con interacciones sin responder desde hace más de {days} días:\n" +
                string.Join("\n", resultList);

            await ReplyAsync(message);
        }
    }
}