using System;
using System.Threading.Tasks;

using DiscordBot.Services;
using Library;

namespace Program
{
    /// <summary>
    /// Un programa que implementa un bot de Discord.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada al programa.
        /// </summary>
        
        
        public static async Task Main(string[] args)
        {
            Facade.cm.AddCustomer(new Customer("C1", "Juan", "Perez", "mail@mail.com", "099", "M", DateTime.Now));
            Facade.sm.CreateSeller(new Seller("Carlos", "carlos@mail.com", "099111222", "S1"));

            if (args.Length != 0)
            {
                DemoFacade(args);
            }
            else
            {
                await DemoBot(); // <--- IMPORTANTE
            }
        }

        private static void DemoFacade(string [] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine(Facade.SearchCostumer_ByFamilyName("Algo"));
            }
        }

        private static async Task DemoBot()  // <--- async Task
        {
            await BotLoader.LoadAsync();     // <--- await
        }
        
    }
}

