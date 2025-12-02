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
    public class TrackingAndInactivityCommand : ModuleBase<SocketCommandContext>
    {
        [Command("inactiveCustomers")]
        [Summary("Muestra los clientes que no tienen interacciones recientes.")]
        public async Task InactiveCustomersCommnad(int days)
        {
            string result = Facade.GetInactiveCustomersFormatted(days);
            await ReplyAsync(result);
        }
        
        
        [Command("unansweredCustomers")]
        [Summary("Muestra los clientes con interacciones sin responder más viejas que X días.")]
        public async Task UnansweredCustomersAsync(int days)
        {
            string result = Facade.GetUnansweredCustomersFormatted(days);
            await ReplyAsync(result);
        }
    }
    
}