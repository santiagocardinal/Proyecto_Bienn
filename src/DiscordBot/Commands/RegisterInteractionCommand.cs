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
        /// Comando para agregar una nota a una interacción existente.
        /// Uso: !addNoteToInteraction C1 "Tema" 2025-03-10 "Contenido de la nota"
        /// </summary>
        [Command("addNoteToInteraction")]
        [Summary("Agrega una nota a una interacción identificada por tópico y fecha.")]
        public async Task AddNoteToInteractionCommand(
            string customerId,
            string interactionTopic,
            string interactionDate,
            string interactionType)
        {
            // 1. Convertir la fecha
            if (!DateTime.TryParse(interactionDate, out DateTime parsedDate))
            {
                await ReplyAsync("La fecha ingresada no es válida. Usa formato YYYY-MM-DD.");
                return;
            }

            if (!Enum.TryParse<ExchangeType>(interactionType, true, out var parsedType))
            {
                await ReplyAsync("El tipo de interacción no es válido. Usa uno de: Sent, Received, ...");
                return;
            }

            // 2. Crear la nota
            Note note = new Note(interactionTopic, parsedDate, parsedType);


            // 4. Llamamos al método real de negocio
            string result = Facade.AddNoteToInteraction(
                customerId,
                interactionTopic,
                parsedDate,
                note);

            await ReplyAsync(result);
        }
    }
}