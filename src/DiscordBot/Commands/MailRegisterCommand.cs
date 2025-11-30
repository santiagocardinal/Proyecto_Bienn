using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para registrar un mail entre un cliente y un vendedor.
    /// Uso: !mailRegister 2025-03-10 TemaDelMail Incoming C1 V1
    /// </summary>
    public class MailRegisterCommand : ModuleBase<SocketCommandContext>
    {
        [Command("mailRegister")]
        [Summary("Registra un mail usando fecha, tema, tipo, ID del cliente y ID del vendedor.")]
        public async Task ExecuteAsync(
            string date,
            string topic,
            string type,
            string customerId,
            string sellerId)
        {
            // Conversión de fecha desde string a DateTime
            /*if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                await ReplyAsync("La fecha ingresada no es válida. Usa el formato YYYY-MM-DD.");
                return;
            }
            
            if (!Enum.TryParse<ExchangeType>(type, true, out var parsedType))
            {
                await ReplyAsync("El tipo de interacción no es válido. Usa uno de: Sent, Received, ...");
                return;
            }*/

            string result = Facade.MailRegister(date, topic, type, customerId, sellerId);
            await ReplyAsync(result);
        }
    }
}
