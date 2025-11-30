using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para crear un nuevo cliente desde Discord.
    /// Uso:
    ///    !createCustomer ID Nombre Apellido Mail Teléfono Genero yyyy-MM-dd
    /// </summary>
    public class CreateCustomerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("createCustomer")]
        [Summary("Crea un cliente en el sistema usando la fachada.")]
        public async Task ExecuteAsync(
            string id,
            string name,
            string familyName,
            string mail,
            string phone,
            string gender,
            string birthDate) // lo recibimos como string y lo convertimos
        {
            // Validación y parseo de fecha
            if (!DateTime.TryParse(birthDate, out DateTime parsedDate))
            {
                await ReplyAsync("La fecha debe tener formato válido (por ejemplo: 2000-05-21).");
                return;
            }

            string result = Facade.CreateCustomer(
                id,
                name,
                familyName,
                mail,
                phone,
                gender,
                parsedDate
            );

            await ReplyAsync(result);
        }
    }
}
