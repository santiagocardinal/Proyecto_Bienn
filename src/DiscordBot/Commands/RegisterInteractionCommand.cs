using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    public class RegisterInteractionCommand : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Comando para registrar una llamada entre un cliente y un vendedor.
        /// Uso: !callRegister 2025-01-20 TemaDeLaLlamada Incoming C1 V1
        /// </summary>
        [Command("callRegister")]
        [Summary("Registra una llamada usando fecha, tópico, tipo, ID del cliente y ID del vendedor.")]
        public async Task CallRegisterCommand(
            string date,
            string topic,
            string type,
            string customerId,
            string sellerId)
        {
            string result = Facade.CallRegister(date, topic, type, customerId, sellerId);
            await ReplyAsync(result);
        }

        /// <summary>
        /// Comando para registrar un mail entre un cliente y un vendedor.
        /// Uso: !mailRegister 2025-03-10 TemaDelMail Incoming C1 V1
        /// </summary>
        [Command("mailRegister")]
        [Summary("Registra un mail usando fecha, tema, tipo, ID del cliente y ID del vendedor.")]
        public async Task MailRegisterCommand(
            string date,
            string topic,
            string type,
            string customerId,
            string sellerId)
        {
            string result = Facade.MailRegister(date, topic, type, customerId, sellerId);
            await ReplyAsync(result);
        }

        /// <summary>
        /// Comando para registrar una reunión entre un cliente y un vendedor.
        /// Uso: !meetingRegister Oficina 2025-03-15 ReunionPresencial C1 V1
        /// </summary>
        [Command("meetingRegister")]
        [Summary("Registra una reunión usando lugar, fecha, tema, tipo, ID del cliente y ID del vendedor.")]
        public async Task MeetingRegisterCommand(
            string place,
            string date,
            string topic,
            string type,
            string customerId,
            string sellerId)
        {
            string result = Facade.MeetingRegister(place, date, topic, type, customerId, sellerId);
            await ReplyAsync(result);
        }

        /// <summary>
        /// Comando para registrar un mensaje entre un cliente y un vendedor.
        /// Uso: !messageRegister 2025-03-10 TemaDelMensaje Incoming C1 V1
        /// </summary>
        [Command("messageRegister")]
        [Summary("Registra un mensaje usando fecha, tema, tipo, ID del cliente y ID del vendedor.")]
        public async Task MessageRegisterCommand(
            string date,
            string topic,
            string type,
            string customerId,
            string sellerId)
        {
            string result = Facade.MessageRegister(date, topic, type, customerId, sellerId);
            await ReplyAsync(result);
        }

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
    }
}