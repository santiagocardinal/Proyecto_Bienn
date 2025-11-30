using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para registrar una cotizacion para un cliente.
    /// Uso: !quoteRegister 2025-11-29 "Cotizacion producto X" 1500.50 Sent C001 V001
    /// </summary>
    public class RegisterSaleCommand : ModuleBase<SocketCommandContext>
    {
        [Command("quoteRegister")]
        [Summary("Registra una venta incluyendo qué se vendió, cuándo y cuánto se cobró.")]
        public async Task ExecuteAsync(string date, string topic, string exchangeType, string amount, string description, string customerId, string sellerId)
        {
            try
            {
                /*if (!DateTime.TryParse(dateString, out DateTime saleDate))
                {
                    await ReplyAsync("Formato de fecha inválido. Usa el formato: YYYY-MM-DD");
                    return;
                }
                
                if (!double.TryParse(amount, out double parsedAmount))
                {
                    await ReplyAsync("Formato de fecha inválido. Usa el formato: YYYY-MM-DD");
                    return;
                }
                
                if (!Enum.TryParse<ExchangeType>(exchangeType, true, out ExchangeType type))
                {
                    await ReplyAsync($"Tipo de intercambio inválido. Usa: {string.Join(", ", Enum.GetNames(typeof(ExchangeType)))}");
                    return;
                }*/
                string result = Facade.QuoteRegister(date, topic, exchangeType, amount, description, customerId, sellerId);
                await ReplyAsync(result);
            }
            catch (Exception ex)
            {
                await ReplyAsync($"Error al registrar la venta: {ex.Message}");
            }
        }
    }
}