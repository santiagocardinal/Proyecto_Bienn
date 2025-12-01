using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    public class ViewInteractionsCommand : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Comando para ver todas las interacciones de un cliente sin filtros.
        /// Uso: !viewAllInteractions C001
        /// </summary>
        [Command("viewAllInteractions")]
        [Summary("Muestra todas las interacciones de un cliente.")]
        public async Task ViewAllInteractionsCommand(string customerId)
        {
            Facade facade = new Facade();
            string result = facade.GetAllCustomerInteractions(customerId);
            await ReplyAsync(result);
        }


        /// <summary>
        /// Comando para ver interacciones filtradas por tipo.
        /// Uso: !viewInteractionsByType C001 Quote
        /// </summary>
        [Command("viewInteractionsByType")]
        [Summary("Muestra las interacciones de un cliente filtradas por tipo.")]
        public async Task ViewInteractionsByTypeCommand(string customerId, [Remainder] string interactionType)
        {
            Facade facade = new Facade();
            string result = facade.GetCustomerInteractionsByType(customerId, interactionType);
            await ReplyAsync(result);
        }


        /// <summary>
        /// Comando para ver interacciones filtradas por fecha.
        /// Uso: !viewInteractionsByDate C001 2025-11-29
        /// </summary>
        [Command("viewInteractionsByDate")]
        [Summary("Muestra las interacciones de un cliente filtradas por fecha.")]
        public async Task ViewInteractionsByDateCommand(string customerId, [Remainder] string dateString)
        {
            Facade facade = new Facade();
            string result = facade.GetCustomerInteractionsByDate(customerId, dateString);
            await ReplyAsync(result);
        }


        /// <summary>
        /// Comando para ver interacciones filtradas por tipo y fecha.
        /// Uso: !viewInteractionsByTypeAndDate C001 Quote 2025-11-29
        /// </summary>
        [Command("viewInteractionsByTypeAndDate")]
        [Summary("Muestra las interacciones de un cliente filtradas por tipo y fecha.")]
        public async Task ViewInteractionsByTypeAndDateCommand(string customerId, string interactionType,
            [Remainder] string dateString)
        {
            Facade facade = new Facade();
            string result = facade.GetCustomerInteractionsByTypeAndDate(customerId, interactionType, dateString);
            await ReplyAsync(result);
        }
    }
}