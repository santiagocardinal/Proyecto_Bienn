using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para listar todos los clientes que no tienen interacciones
    /// en un rango de días especificado.
    /// Uso: !inactiveCustomers 30
    /// </summary>
    public class InactiveCustomersCommand : ModuleBase<SocketCommandContext>
    {
        [Command("inactiveCustomers")]
        [Summary("Muestra los clientes que no tienen interacciones recientes.")]
        public async Task ExecuteAsync(int days)
        {
            string result = Facade.GetInactiveCustomersFormatted(days);
            await ReplyAsync(result);
        }

        
        /*[Command("inactiveCustomers")]
        [Summary("Muestra los clientes que no tienen interacciones recientes en los últimos X días.")]
        public async Task ExecuteAsync(int days)
        {
            if (days <= 0)
            {
                await ReplyAsync("Debes indicar un número de días mayor a cero.");
                return;
            }

            DateTime limitDate = DateTime.Now.AddDays(-days);

            var customers = Facade.cm.Customers;  // Acceso directo al CustomerManager

            var inactive = customers
                .Where(c =>
                    c.Interactions.Count == 0 ||               // Nunca tuvo interacciones
                    c.GetLastInteraction().Date < limitDate)   // Última interacción vieja
                .Select(c => $"{c.Name} (ID: {c.Id})")
                .ToList();

            if (inactive.Count == 0)
            {
                await ReplyAsync($"Todos los clientes han tenido interacción en los últimos {days} días.");
                return;
            }

            string result = $"Clientes sin interacción en más de {days} días:\n";
            result += string.Join("\n", inactive);

            await ReplyAsync(result);
        }*/
    }
}