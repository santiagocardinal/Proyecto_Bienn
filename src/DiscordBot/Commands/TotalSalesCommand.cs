using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    public class TotalSalesCommand : ModuleBase<SocketCommandContext>
    {
        [Command("totalSales")]
        [Summary("Muestra el total de ventas entre dos fechas.")]
        public async Task ExecuteAsync(string startDate, string endDate)
        {
            // Convertir fechas
            if (!DateTime.TryParse(startDate, out DateTime start))
            {
                await ReplyAsync("La fecha inicial no es válida. Usa formato YYYY-MM-DD.");
                return;
            }

            if (!DateTime.TryParse(endDate, out DateTime end))
            {
                await ReplyAsync("La fecha final no es válida. Usa formato YYYY-MM-DD.");
                return;
            }

            // Obtener SOLO ventas
            var sales = Facade.cm.Customers
                .SelectMany(c => c.Interactions)
                .Where(i => i is Sale sale &&
                            sale.Date >= start &&
                            sale.Date <= end)
                .Cast<Sale>()       // ahora seguro
                .ToList();

            // Si no hay ventas
            if (sales.Count == 0)
            {
                await ReplyAsync($"No hubo ventas entre {start:yyyy-MM-dd} y {end:yyyy-MM-dd}.");
                return;
            }

            // SUMAR monto real de cada venta (Sale.Amount.Amount)
            double total = sales.Sum(s => s.Amount.Amount);

            await ReplyAsync(
                $"Total de ventas entre {start:yyyy-MM-dd} y {end:yyyy-MM-dd}: **USD {total:F2}**"
            );
        }
    }
}