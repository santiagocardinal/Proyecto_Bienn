using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Library
{
    /// <summary>
    /// Comando para crear un nuevo cliente desde Discord.
    /// Uso: !createCustomer ID Nombre Apellido Mail Teléfono Genero yyyy-MM-dd
    /// </summary>
    public class CustomerManagementCommand : ModuleBase<SocketCommandContext>
    {
        [Command("createCustomer")]
        [Summary("Crea un cliente en el sistema usando la fachada.")]
        public async Task CreateCustomerCommand(
            string id,
            string name,
            string familyName,
            string mail,
            string phone,
            string gender,
            string birthDate) // lo recibimos como string y lo convertimos
        {
            string result = Facade.CreateCustomer(
                id,
                name,
                familyName,
                mail,
                phone,
                gender,
                birthDate
            );

            await ReplyAsync(result);
        }


        /// <summary>
        /// Comando para modificar los datos de un cliente por Id.
        /// Uso: !modifyCustomer id field newValue
        /// </summary>
        [Command("modifyCustomer")]
        [Summary("Modifica las credenciales d eun cliente.")]
        public async Task ModifyCustomerById(string id, string field, string newValue)
        {
            string result = Facade.ModifyCustomer(id, field, newValue);
            await ReplyAsync(result);
        }

        /// <summary>
        /// Elimina un cliente del CRM por ID.
        /// Uso: !deleteCustomer C1
        /// </summary>
        [Command("deleteCustomer")]
        [Summary("Elimina un cliente por ID. Uso: !deletecustomer <id>")]
        public async Task DeleteCustomerCommand(string id = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                await ReplyAsync("Debes indicar el ID del cliente. Ejemplo: `!deletecustomer C1`");
                return;
            }

            string result = Facade.DeleteCustomer(id);
            await ReplyAsync(result);
        }

        /// <summary>
        /// Comando para buscar un cliente por correo electrónico.
        /// Uso: !searchCustomerByMail correo@ejemplo.com
        /// </summary>
        [Command("searchCustomerByMail")]
        [Summary("Busca un cliente por su dirección de correo electrónico.")]
        public async Task SearchCustomerByMailCommand(string mail)
        {
            string result = Facade.SearchCostumer_ByMail(mail);
            await ReplyAsync(result);
        }


        /// <summary>
        /// Comando para buscar un cliente por apellido.
        /// Uso: !searchCustomerByFamilyName Apellido
        /// </summary>
        [Command("searchCustomerByFamilyName")]
        [Summary("Busca un cliente por su apellido.")]
        public async Task SearchCustomerByFamilyNameCommand(string familyName)
        {
            string result = Facade.SearchCostumer_ByFamilyName(familyName);
            await ReplyAsync(result);
        }


        /// <summary>
        /// Comando para buscar un cliente por ID.
        /// Uso: !searchCustomerById C1
        /// </summary>
        [Command("searchCustomerById")]
        [Summary("Busca un cliente por su ID y muestra toda su información.")]
        public async Task SearchCustomerByIdCommand(string id)
        {
            string result = Facade.SearchCustomer_ById(id);
            await ReplyAsync(result);
        }


        /// <summary>
        /// Comando para buscar un cliente por su nombre.
        /// Uso: !searchCustomerByName Juan
        /// </summary>
        [Command("searchCustomerByName")]
        [Summary("Busca un cliente por su nombre.")]
        public async Task SearchCustomerByNameCommand(string name)
        {
            string result = Facade.SearchCostumer_ByName(name);
            await ReplyAsync(result);
        }


        /// <summary>
        /// Comando para buscar un cliente por teléfono.
        /// Uso: !searchCustomerByPhone 099123456
        /// </summary>
        [Command("searchCustomerByPhone")]
        [Summary("Busca un cliente por su número de teléfono.")]
        public async Task SearchCustomerByPhoneCommand(string phone)
        {
            string result = Facade.SearchCostumer_ByPhone(phone);
            await ReplyAsync(result);
        }

        /// <summary>
        /// Comando para obtener todos los clientes asignados a un vendedor.
        /// Uso: !getCustomerBySellerId V1
        /// </summary>
        [Command("getCustomerBySellerId")]
        [Summary("Muestra todos los clientes asociados a un vendedor usando su ID.")]
        public async Task ExecuteAsyncGetCustomerBySellerIdCommand(string sellerId)
        {
            string result = Facade.ShowCustomers_BySellerId(sellerId);
            await ReplyAsync(result);
        }

        /// <summary>
        /// Comando para asignar un cliente a un vendedor.
        /// Uso: !assignCustomer C001 V001
        /// </summary>
        [Command("assignCustomer")]
        [Summary("Asigna un cliente a un vendedor para distribuir el trabajo en el equipo.")]
        public async Task AssignCustomerCommand(
            string customerId,
            string sellerId)
        {
            string result = Facade.AssignCustomer(customerId, sellerId);
            await ReplyAsync(result);
        }

        /// <summary>
        /// Comando para asignar un cliente a otro vendedor dentro del equipo.
        /// Uso: !exchangeCustomer C1 V3
        /// </summary>
        [Command("exchangeCustomer")]
        [Summary("Asigna un cliente a un vendedor para redistribuir el trabajo.")]
        public async Task AssignCustomerToSellerCommand(string customerId, string oldSellerId, string newSellerId)
        {
            string result = Facade.ExchangeCustomer(customerId, oldSellerId, newSellerId);
            await ReplyAsync(result);
        }
    }
}