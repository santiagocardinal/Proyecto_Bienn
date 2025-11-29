using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para registrar una venta a un cliente.
    /// Uso: !registerSale 2025-11-29 "Producto X" efectivo 1500.50 "Venta de productos" C001 V001
    /// </summary>
    public class RegisterSaleCommand : ModuleBase<SocketCommandContext>
    {
        [Command("registerSale")]
        [Summary("Registra una venta incluyendo qué se vendió, cuándo y cuánto se cobró.")]
        public async Task ExecuteAsync(string dateString, string topic, string exchangeType, double amount, string description, string customerId, string sellerId)
        {
            try
            {
                if (!DateTime.TryParse(dateString, out DateTime saleDate))
                {
                    await ReplyAsync("Formato de fecha inválido. Usa el formato: YYYY-MM-DD");
                    return;
                }
                if (!Enum.TryParse<ExchangeType>(exchangeType, true, out ExchangeType type))
                {
                    await ReplyAsync($"Tipo de intercambio inválido. Usa: {string.Join(", ", Enum.GetNames(typeof(ExchangeType)))}");
                    return;
                }
                string result = Facade.QuoteRegister(saleDate, topic, type,amount, description, customerId, sellerId);
                string response = $"**Venta Registrada**\n" +
                                  $"```\n" +
                                  $"Fecha:        {saleDate:dd/MM/yyyy}\n" +
                                  $"Producto:     {topic}\n" +
                                  $"Monto:        ${amount:N2}\n" +
                                  $"Tipo:         {type}\n" +
                                  $"Cliente:      {customerId}\n" +
                                  $"Vendedor:     {sellerId}\n" +
                                  $"Descripción:  {description}\n" +
                                  $"```\n" +
                                  $"{result}";
                await ReplyAsync(response);
            }
            catch (Exception ex)
            {
                await ReplyAsync($"Error al registrar la venta: {ex.Message}");
            }
        }
    }
}