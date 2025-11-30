using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para registrar una llamada entre un cliente y un vendedor.
    /// Uso: !callRegister 2025-01-20 TemaDeLaLlamada Incoming C1 V1
    /// </summary>
    public class CallRegisterCommand : ModuleBase<SocketCommandContext>
    {
        [Command("callRegister")]
        [Summary("Registra una llamada usando fecha, tópico, tipo, ID del cliente y ID del vendedor.")]
        public async Task ExecuteAsync(
            string date,
            string topic,
            string type,
            string customerId,
            string sellerId)
        {
            // Convertir fecha desde string → DateTime
            /*if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                await ReplyAsync("La fecha ingresada no es válida. Usa el formato YYYY-MM-DD.");
                return;
            }*/

            string result = Facade.CallRegister(date, topic, type, customerId, sellerId);
            await ReplyAsync(result);
        }
    }
}