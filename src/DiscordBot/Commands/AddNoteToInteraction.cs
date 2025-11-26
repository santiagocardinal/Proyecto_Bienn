using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para agregar una nota a una interacción existente.
    /// Uso: !addNoteToInteraction C1 "Tema" 2025-03-10 "Contenido de la nota"
    /// </summary>
    public class AddNoteToInteractionCommand : ModuleBase<SocketCommandContext>
    {
        [Command("addNoteToInteraction")]
        [Summary("Agrega una nota a una interacción identificada por tópico y fecha.")]
        public async Task ExecuteAsync(
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
            Note note = new Note(interactionTopic,parsedDate,parsedType);
            

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