using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para registrar una reunión entre un cliente y un vendedor.
    /// Uso: !meetingRegister Oficina 2025-03-15 ReunionPresencial C1 V1
    /// </summary>
    public class MeetingRegisterCommand : ModuleBase<SocketCommandContext>
    {
        [Command("meetingRegister")]
        [Summary("Registra una reunión usando lugar, fecha, tema, tipo, ID del cliente y ID del vendedor.")]
        public async Task ExecuteAsync(
            string place,
            string date,
            string topic,
            string type,
            string customerId,
            string sellerId)
        {
            // Conversión de fecha
            /*if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                await ReplyAsync("La fecha ingresada no es válida. Usa el formato YYYY-MM-DD.");
                return;
            }*/

            string result = Facade.MeetingRegister(place, date, topic, type, customerId, sellerId);
            await ReplyAsync(result);
        }
    }
}