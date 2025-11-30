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
            string result = Facade.GetTotalSales(startDate, endDate);
            await ReplyAsync(result);
        }
    }

}