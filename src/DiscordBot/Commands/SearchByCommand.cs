using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para buscar un cliente por correo electrónico.
    /// Uso: !searchCustomerByMail correo@ejemplo.com
    /// </summary>
    public class SearchCustomerByMailCommand : ModuleBase<SocketCommandContext>
    {
        [Command("searchCustomerByMail")]
        [Summary("Busca un cliente por su dirección de correo electrónico.")]
        public async Task ExecuteAsync(string mail)
        {
            string result = Facade.SearchCostumer_ByMail(mail);
            await ReplyAsync(result);
        }
    }

    /// <summary>
    /// Comando para buscar un cliente por apellido.
    /// Uso: !searchCustomerByFamilyName Apellido
    /// </summary>
    public class SearchCustomerByFamilyNameCommand : ModuleBase<SocketCommandContext>
    {
        [Command("searchCustomerByFamilyName")]
        [Summary("Busca un cliente por su apellido.")]
        public async Task ExecuteAsync(string familyName)
        {
            string result = Facade.SearchCostumer_ByFamilyName(familyName);
            await ReplyAsync(result);
        }
    }

    /// <summary>
    /// Comando para buscar un cliente por ID.
    /// Uso: !searchCustomerById C1
    /// </summary>
    public class SearchCustomerByIdCommand : ModuleBase<SocketCommandContext>
    {
        [Command("searchCustomerById")]
        [Summary("Busca un cliente por su ID y muestra toda su información.")]
        public async Task ExecuteAsync(string id)
        {
            string result = Facade.SearchCustomer_ById(id);
            await ReplyAsync(result);
        }
    }
}