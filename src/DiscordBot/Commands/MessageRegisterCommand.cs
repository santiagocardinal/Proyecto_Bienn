using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para registrar un mensaje entre un cliente y un vendedor.
    /// Uso: !messageRegister 2025-03-10 TemaDelMensaje Incoming C1 V1
    /// </summary>
    public class MessageRegisterCommand : ModuleBase<SocketCommandContext>
    {
        [Command("messageRegister")]
        [Summary("Registra un mensaje usando fecha, tema, tipo, ID del cliente y ID del vendedor.")]
        public async Task ExecuteAsync(
            string date,
            string topic,
            string type,
            string customerId,
            string sellerId)
        {
            // Conversión de fecha
            if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                await ReplyAsync("La fecha ingresada no es válida. Usa el formato YYYY-MM-DD.");
                return;
            }
            
            if (!Enum.TryParse<ExchangeType>(type, true, out var parsedType))
            {
                await ReplyAsync("El tipo de interacción no es válido. Usa uno de: Sent, Received, ...");
                return;
            }

            string result = Facade.MessageRegister(parsedDate, topic, parsedType, customerId, sellerId);
            await ReplyAsync(result);
        }
    }
}
