using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    public class CommercialActivityCommand  : ModuleBase<SocketCommandContext>
    {        
        /// <summary>
        /// Comando para registrar una cotizacion para un cliente.
        /// Uso: !quoteRegister 2025-11-29 "Cotizacion producto X" 1500.50 Sent C001 V001
        /// </summary>
        [Command("quoteRegister")]
        [Summary("Registra una venta incluyendo qué se vendió, cuándo y cuánto se cobró.")]
        public async Task RegisterSaleCommand(string date, string topic, string exchangeType, string amount,
            string description, string customerId, string sellerId)
        {
            try
            {
                string result =
                    Facade.QuoteRegister(date, topic, exchangeType, amount, description, customerId, sellerId);
                await ReplyAsync(result);
            }
            catch (Exception ex)
            {
                await ReplyAsync($"Error al registrar la venta: {ex.Message}");
            }
        }

        /// <summary>
        /// Comando para registrar un sale entre un cliente y un vendedor.
        /// Uso: !saleRegister S1 C1 2025-11-29 Compra Sent 1500.5 Casa
        /// </summary>
        [Command("saleRegister")]
        [Summary(
            "Registra un Sale usando fecha, tema, tipo, ID del cliente y ID del vendedor y una cotizacion previa.")]
        public async Task SaleRegisterCommand(
            string sellerId,
            string customerId,
            string date,
            string topic,
            string type,
            string amount,
            string product)
        {
            string result = Facade.SaleFromQuote(sellerId, customerId, date, topic, type, amount, product);
            await ReplyAsync(result);
        }
        
        
        /// <summary>
        /// Comando para mostrar la cantidad de ventas en un periodo dado
        /// </summary>
        [Command("totalSales")]
        [Summary("Muestra el total de ventas entre dos fechas.")]
        public async Task TotalSalesCommand(string startDate, string endDate)
        {
            string result = Facade.GetTotalSales(startDate, endDate);
            await ReplyAsync(result);
        }
    }

}