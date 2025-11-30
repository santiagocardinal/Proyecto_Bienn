using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    
    /// <summary>
    /// Comando para mostrar la cantidad de ventas en un periodo dado
    /// </summary>
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