using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;


namespace Library{

    public class SaleRegisterCommand  : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Comando para registrar un sale entre un cliente y un vendedor.
        /// Uso: !saleRegister S1 C1 2025-11-29 Compra Sent 1500.5 Casa
        /// </summary>
        [Command("saleRegister")]
        [Summary(
            "Registra un sale usando fecha, tema, tipo, ID del cliente y ID del vendedor y una cotizacion previa.")]
        public async Task ExecuteAsync(
            string sellerId,
            string customerId,
            string date,
            string topic,
            string type,
            string amount,
            string product)
        {
            // Conversión de fecha desde string a DateTime
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

            if (!double.TryParse(amount, out double parsedAmount))
            {
                await ReplyAsync("Se debe ingresar un monto en pesos en el formato 100.0");
                return;
            }

            string result = Facade.SaleFromQuote(sellerId, customerId, parsedDate, topic, parsedType, parsedAmount, product);
            await ReplyAsync(result);
        }
    }
}