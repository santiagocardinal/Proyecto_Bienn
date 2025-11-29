using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para ver todas las interacciones de un cliente.
    /// Uso: 
    /// - Sin filtros: !viewInteractions C001
    /// - Con tipo: !viewInteractions C001 Quote
    /// - Con fecha: !viewInteractions C001 null 2025-11-29
    /// - Con ambos filtros: !viewInteractions C001 Quote 2025-11-29
    /// </summary>
    public class ViewCustomerInteractionsCommand : ModuleBase<SocketCommandContext>
    {
        [Command("viewInteractions")]
        [Summary("Muestra todas las interacciones de un cliente, con filtros opcionales por tipo y fecha.")]
        public async Task ExecuteAsync(
            string customerId,
            string interactionType = null,
            string dateString = null)
        {
            try
            {
                // Parsear la fecha si se proporciona
                DateTime? filterDate = null;
                if (!string.IsNullOrEmpty(dateString) && dateString.ToLower() != "null")
                {
                    if (DateTime.TryParse(dateString, out DateTime parsedDate))
                    {
                        filterDate = parsedDate;
                    }
                    else
                    {
                        await ReplyAsync("Formato de fecha inválido. Usa el formato: YYYY-MM-DD");
                        return;
                    }
                }

                // Normalizar interactionType (convertir "null" string a null)
                if (interactionType?.ToLower() == "null")
                {
                    interactionType = null;
                }

                // Obtener las interacciones usando el método de la fachada
                Facade facade = new Facade();
                var interactions = facade.GetCustomerInteractions(customerId, interactionType, filterDate);

                if (interactions == null || interactions.Count == 0)
                {
                    string noResultsMsg = $"No se encontraron interacciones para el cliente **{customerId}**";
                    if (!string.IsNullOrEmpty(interactionType))
                        noResultsMsg += $" del tipo **{interactionType}**";
                    if (filterDate.HasValue)
                        noResultsMsg += $" en la fecha **{filterDate.Value:dd/MM/yyyy}**";
                    
                    await ReplyAsync(noResultsMsg);
                    return;
                }

                // Construir el mensaje de respuesta
                StringBuilder response = new StringBuilder();
                response.AppendLine($"**Historial de Interacciones - Cliente {customerId}**");
                
                if (!string.IsNullOrEmpty(interactionType) || filterDate.HasValue)
                {
                    response.Append("Filtros aplicados: ");
                    if (!string.IsNullOrEmpty(interactionType))
                        response.Append($"Tipo={interactionType} ");
                    if (filterDate.HasValue)
                        response.Append($"Fecha={filterDate.Value:dd/MM/yyyy}");
                    response.AppendLine();
                }
                
                response.AppendLine($"Total: **{interactions.Count}** interacción(es)\n");
                response.AppendLine("```");

                // Listar cada interacción
                int counter = 1;
                foreach (var interaction in interactions.OrderByDescending(i => i.Date))
                {
                    response.AppendLine($"[{counter}] {interaction.GetType().Name}");
                    response.AppendLine($"    Fecha:  {interaction.Date:dd/MM/yyyy HH:mm}");
                    response.AppendLine($"    Tema:   {interaction.Topic}");
                    
                    // Información adicional según el tipo de interacción
                    if (interaction is Quote quote)
                    {
                        response.AppendLine($"    Monto:  ${quote.Amount:N2}");
                        response.AppendLine($"    Tipo:   {quote.Type}");
                        if (!string.IsNullOrEmpty(quote.Description))
                            response.AppendLine($"    Desc:   {quote.Description}");
                    }
                    else
                    {
                        // Para otros tipos de interacciones, intenta acceder a Description si existe
                        var descProperty = interaction.GetType().GetProperty("Description");
                        if (descProperty != null)
                        {
                            var desc = descProperty.GetValue(interaction)?.ToString();
                            if (!string.IsNullOrEmpty(desc))
                                response.AppendLine($"    Desc:   {desc}");
                        }
                    }
                    
                    response.AppendLine();
                    counter++;
                }

                response.AppendLine("```");

                // Discord tiene un límite de 2000 caracteres por mensaje
                string finalMessage = response.ToString();
                if (finalMessage.Length > 1990)
                {
                    // Dividir en múltiples mensajes si es necesario
                    await ReplyAsync($"**Historial de Interacciones - Cliente {customerId}**\n" +
                                   $"Total: **{interactions.Count}** interacciones\n\n" +
                                   $"Demasiadas interacciones para mostrar. " +
                                   $"Usa filtros más específicos (tipo o fecha) para ver los detalles.");
                }
                else
                {
                    await ReplyAsync(finalMessage);
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync($"Error al obtener las interacciones: {ex.Message}");
            }
        }
    }
}