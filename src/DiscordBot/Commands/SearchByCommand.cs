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

    /// <summary>
    /// Comando para buscar un cliente por su nombre.
    /// Uso: !searchCustomerByName Juan
    /// </summary>
    public class SearchCustomerByNameCommand : ModuleBase<SocketCommandContext>
    {
        [Command("searchCustomerByName")]
        [Summary("Busca un cliente por su nombre.")]
        public async Task ExecuteAsync(string name)
        {
            string result = Facade.SearchCostumer_ByName(name);
            await ReplyAsync(result);
        }
    }

    /// <summary>
    /// Comando para buscar un cliente por teléfono.
    /// Uso: !searchCustomerByPhone 099123456
    /// </summary>
    public class SearchCustomerByPhoneCommand : ModuleBase<SocketCommandContext>
    {
        [Command("searchCustomerByPhone")]
        [Summary("Busca un cliente por su número de teléfono.")]
        public async Task ExecuteAsync(string phone)
        {
            string result = Facade.SearchCostumer_ByPhone(phone);
            await ReplyAsync(result);
        }
    }
}
