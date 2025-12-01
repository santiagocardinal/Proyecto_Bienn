namespace Library;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Clase fachada que centraliza las operaciones de clientes, vendedores e interacciones.
/// Permite acceder a las funcionalidades del sistema sin exponer su estructura interna.
/// </summary>
public class Facade
{
    public static CustomerManager cm = Singleton<CustomerManager>.GetInstance();
    public static SellerManager sm = Singleton<SellerManager>.GetInstance();


    public static DateTime ParseDate(string dateString)
    {
        if (!DateTime.TryParse(dateString, out DateTime date))
            throw new Exception("Formato de fecha inválido. Usa el formato: YYYY-MM-DD.");

        return date;
    }

    public static double ParseDouble(string value)
    {
        if (!double.TryParse(value, out double number))
            throw new Exception("Formato de número inválido. Usa solo valores numéricos (ej: 1500.75).");

        return number;
    }

    public static ExchangeType ParseExchangeType(string typeString)
    {
        if (!Enum.TryParse<ExchangeType>(typeString, true, out ExchangeType type))
            throw new Exception(
                "Tipo de intercambio inválido. Usa uno de: " +
                string.Join(", ", Enum.GetNames(typeof(ExchangeType)))
            );

        return type;
    }


    /// <summary>
    /// Crea un nuevo cliente a partir de los datos ingresados y lo agrega al sistema.
    /// Devuelve un mensaje de éxito o el mensaje de la excepción lanzada.
    /// </summary>
    public static string CreateCustomer(
        string id, string name, string familyName,
        string mail, string phone, string gender, string birthDate)
    {
        try
        {
            DateTime date = ParseDate(birthDate);
            Customer c1 = new Customer(id, name, familyName, mail, phone, gender, date);
            cm.AddCustomer(c1);
            return $"***Cliente creado correctamente***\n" +
                   "```\n" +
                   $"ID:           " + id + "\n" +
                   $"Nombre:       " + name + " " + familyName + "\n" +
                   $"Email:        " + mail + "\n" +
                   $"Teléfono:     " + phone + "\n" +
                   $"Género:       " + gender + "\n" +
                   $"Fecha Nac.:   " + date.ToString("dd/MM/yyyy") + "\n" +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// Elimina un cliente del sistema según su ID.
    /// Verifica primero si el ID es válido y si el cliente existe.
    /// </summary>
    public static string DeleteCustomer(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"***El ID no puede estar vacío.***");

            Customer customerToRemove = cm.SearchById(id);
            // Si no existe, SearchById ya lanza NotExistingCustomerException

            cm.Customers.Remove(customerToRemove);

            return $"```\n" +
                   "Cliente eliminado correctamente." +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // ---------------------------------------------------------
    //   MODIFICACIÓN DE CLIENTE
    // ---------------------------------------------------------
    public static string ModifyCustomer(string id, string field, string newValue)
    {
        try
        {
            cm.Modify(id, field, newValue);
            return $"```" +
                   "Cliente modificado correctamente." +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // ---------------------------------------------------------
    //   BÚSQUEDAS
    // ---------------------------------------------------------

    public static string SearchCustomer_ById(string id)
    {
        try
        {
            Customer c = cm.SearchById(id);

            return $"***Cliente buscado por ID:***" +
                   $"   ID: {c.Id}\n" +
                   $"    Nombre: {c.Name} {c.FamilyName}\n" +
                   $"    Mail: {c.Mail}\n" +
                   $"    Teléfono: {c.Phone}";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string SearchCostumer_ByName(string name)
    {
        try
        {
            List<Customer> customers = cm.SearchByName(name);

            string result = $"Clientes con nombre '{name}':\n";

            foreach (Customer c in customers)
            {
                result += $"- {c.Name} {c.FamilyName} (ID: {c.Id})\n";
            }

            return $"***Clinete buscado por su NOMBRE:***" +
                   "```" +
                   result +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static string SearchCostumer_ByFamilyName(string familyname)
    {
        try
        {
            List<Customer> customers = cm.SearchByFamilyName(familyname);

            string result = $"Clientes con apellido '{familyname}':\n";

            foreach (Customer c in customers)
            {
                result += $"- {c.Name} {c.FamilyName} (ID: {c.Id})\n";
            }

            return $"***Cliente buscado por su APELLIDO:***" +
                   "```" +
                   result +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static string SearchCostumer_ByPhone(string phone)
    {
        try
        {
            Customer c = cm.SearchByPhone(phone);

            return $"***Cliente encontrado por su NUMERO DE TELEFONO:***\n" +
                   "```\n" +
                   "ID:       " + c.Id + "\n" +
                   "Nombre:   " + c.Name + " " + c.FamilyName + "\n" +
                   "Mail:     " + c.Mail + "\n" +
                   "Teléfono: " + c.Phone + "\n" +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string SearchCostumer_ByMail(string mail)
    {
        try
        {
            Customer c = cm.SearchByMail(mail);

            return "***Cliente encontrado por su MAIL:***\n" +
                   "```\n" +
                   "ID:       " + c.Id + "\n" +
                   "Nombre:   " + c.Name + " " + c.FamilyName + "\n" +
                   "Mail:     " + c.Mail + "\n" +
                   "Teléfono: " + c.Phone + "\n" +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// Muestra todos los clientes asociados a un vendedor específico.
    /// Devuelve un listado con los nombres o un mensaje si no hay clientes asignados.
    /// </summary>
    public static string ShowCustomers_BySellerId(string sellerId)
    {
        try
        {
            //Seller seller = sm.SearchById(sellerId);
            Seller seller = sm.GetActiveSeller(sellerId);


            if (seller == null)
                throw new Exceptions.SellerNullException();

            if (seller.Customer.Count == 0)
                return $"```" +
                       "El vendedor no tiene clientes asignados." +
                       "```";

            string result = "";
            foreach (Customer customer in seller.Customer)
            {
                result += $"- {customer.Name}\n";
            }

            return $"***Clientes por vendedor:***" +
                   "```" +
                   result +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // ---------------------------------------------------------
    //   AGREGAR CUSTOMER
    // ---------------------------------------------------------
    public static string AddCustomer(Customer customer)
    {
        try
        {
            cm.AddCustomer(customer);
            return $"```" +
                   "Cliente agregado correctamente." +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static string CreateTag(string tagId, string tagName, string tagDescription)
    {
        try
        {
            // Verificaciones básicas
            if (tagId == null || tagId.Trim() == "")
                return $"```" +
                       "El ID de la Tag no puede estar vacío." +
                       "```";

            if (tagName == null || tagName.Trim() == "")
                return $"```" +
                       "***El nombre de la Tag no puede estar vacío.***" +
                       "```";

            // Crear la Tag en CustomerManager
            cm.CreateTag(tagId, tagName, tagDescription);

            return $"__La Tag__:\n ***'{tagId}'*** \n Fue creada correctamente.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    // ---------------------------------------------------------
    //   AGREGAR TAG A CLIENTE
    // ---------------------------------------------------------
    public static string AddTag_Customer(string customerId, string tagId)
    {
        try
        {
            // Verificar que el cliente exista
            Customer customer = cm.SearchById(customerId);

            //  Verificar que la tag exista en el sistema global
            if (!cm.TagExists(tagId))
            {
                return $"__La Tag__:\n ** '{tagId}'**\n No existe en el sistema. Créala primero con **CreateTag.**";
            }

            // Añadir la tag al cliente
            cm.AddTagToCustomer(customerId, tagId);

            return $"```" +
                   $"__La Tag__: \n **'{tagId}'** \n Fue añadida correctamente al cliente **'{customerId}'**+" +
                   $"```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    // ---------------------------------------------------------
    //   NOTAS A INTERACCIONES
    // ---------------------------------------------------------
    public static string AddNoteToInteraction(string customerId, string interactionTopic, DateTime interactionDate,
        Note note)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);

            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            Interaction interaction = customer.Interactions
                .FirstOrDefault(i => i.Topic == interactionTopic && i.Date == interactionDate);

            if (interaction == null)
                throw new Exceptions.InteractionNotFoundException();

            interaction.AddNote(note);

            return "```" +
                   "Nota agregada correctamente." +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // ---------------------------------------------------------
    //   OBTENER INTERACCIONES
    // ---------------------------------------------------------
    /// <summary>
    /// Obtiene todas las interacciones de un cliente sin filtros.
    /// </summary>
    public string GetAllCustomerInteractions(string customerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            if (customer == null)
                return $"```" +
                       "No existe un cliente con el ID: " + customerId + "\n" +
                       "```";

            System.Collections.Generic.List<Interaction> interactions = customer.Interactions;
            return FormatInteractionsMessage(customerId, interactions);
        }
        catch (Exceptions.NotExistingCustomerException)
        {
            return $"```" +
                   "No existe un cliente con el ID: " + customerId + "\n" +
                   "```";
        }
        catch (System.Exception ex)
        {
            return $"```" +
                   "**Error al obtener las interacciones:** " + ex.Message +
                   "```";
        }
    }


    public string GetCustomerInteractionsByType(string customerId, string interactionType)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            if (customer == null)
                return "\n" +
                       "No existe un cliente con el ID: " + customerId + "\n" +
                       "\n";

            List<Interaction> interactions = string.IsNullOrEmpty(interactionType)
                ? customer.Interactions
                : customer.GetInteractionsByTypeName(interactionType);

            return FormatInteractionsMessage(customerId, interactions);
        }
        catch (System.Exception ex)
        {
            return "\n" +
                   "Error al obtener las interacciones: " + ex.Message + "\n" +
                   "\n";
        }
    }

    public string GetCustomerInteractionsByDate(string customerId, string dateStr)
    {
        try
        {
            DateTime date = ParseDate(dateStr);
            Customer customer = cm.SearchById(customerId);
            if (customer == null)
                return "```\n" +
                       "No existe un cliente con el ID: " + customerId + "\n" +
                       "```";

            List<Interaction> interactions = customer.GetInteractionsByDate(date);

            return FormatInteractionsMessage(customerId, interactions);
        }
        catch (System.Exception ex)
        {
            return "```\n" +
                   "Error al obtener las interacciones: " + ex.Message + "\n" +
                   "```";
        }
    }

    public string GetCustomerInteractionsByTypeAndDate(string customerId, string interactionType, string dateStr)
    {
        try
        {
            DateTime date = ParseDate(dateStr);
            Customer customer = cm.SearchById(customerId);
            if (customer == null)
                return "```\n" +
                       "No existe un cliente con el ID: " + customerId + "\n" +
                       "```";

            List<Interaction> interactions = customer.GetInteractionsByTypeAndDate(interactionType, date);

            return FormatInteractionsMessage(customerId, interactions);
        }
        catch (System.Exception ex)
        {
            return "```\n" +
                   "Error al obtener las interacciones: " + ex.Message + "\n" +
                   "```";
        }
    }

    private string FormatInteractionsMessage(string customerId, List<Interaction> interactions)
    {
        string mensaje = "Interacciones del cliente " + customerId + "\n";
        mensaje += "Total: " + interactions.Count + "\n\n";

        int numero = 1;
        foreach (var interaction in interactions)
        {
            mensaje += numero + ". " + interaction.GetType().Name + "\n";
            mensaje += "   Fecha: " + interaction.Date.ToString("dd/MM/yyyy") + "\n";
            mensaje += "   Tema: " + interaction.Topic + "\n\n";
            numero++;
        }

        return mensaje;
    }


    
    

    // ---------------------------------------------------------
    //   ÚLTIMA INTERACCIÓN
    // ---------------------------------------------------------
    public static string LastInteraction(string customerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);

            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            if (customer.Interactions.Count == 0)
                return $"```" +
                       "No hay interacciones registradas." +
                       "```";

            return customer.GetLastInteraction().ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // ---------------------------------------------------------
    //   INTERACCIONES SIN RESPUESTA
    // ---------------------------------------------------------
    public static string UnansweredInteractions(string customerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);

            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            var unanswered = customer.GetUnansweredInteractions();

            if (unanswered.Count == 0)
                return $"```" + "No hay interacciones sin responder." +
                       "```";

            string report = $"Interacciones sin responder **({unanswered.Count})**:\n";
            foreach (var inter in unanswered)
                report += $"- {inter}\n";

            return $"```" +
                   report +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string CreateSeller(
        string id, string name, string mail,
        string phone)
    {
        try
        {
            /*const string adminPassword = "1234"; // la cambiás por la real

            if (password != adminPassword)
                throw new Exception("Contraseña incorrecta. No tienes permisos para crear vendedores.");*/

            Seller seller = new Seller(name, mail, phone, id);

            sm.CreateSeller(seller);

            return $"```" +
                   "Vendedor creado correctamente." +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // ---------------------------------------------------------
//   BÚSQUEDAS DE SELLER
// ---------------------------------------------------------

    public static string SearchSeller_ById(string id)
    {
        try
        {
            Seller seller = sm.GetActiveSeller(id);
            //Seller seller = sm.SearchById(id);

            if (seller == null)
                throw new Exceptions.SellerNullException();

            return $"***Vendedor encontrado por su ID:***\n" +
                   "```" +
                   $"   ID: {seller.Id}\n" +
                   $"   Nombre: {seller.Name}\n" +
                   $"   Mail: {seller.Mail}\n" +
                   $"   Teléfono: {seller.Phone}\n" +
                   $"   Suspendido: {(seller.IsSuspended ? "Sí" : "No")}" +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    

    /// <summary>
    /// Asigna un cliente a un vendedor para distribuir el trabajo en el equipo.
    /// Historia de usuario: Como vendedor, quiero poder asignar un cliente a otro 
    /// vendedor para distribuir el trabajo en el equipo.
    /// </summary>
    public static string AssignCustomer(string customerId, string sellerId)
    {
        try
        {
            // Buscar el cliente
            Customer customer = cm.SearchById(customerId);
            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            // Buscar el vendedor
            Seller seller = sm.SearchById(sellerId);
            if (seller == null)
                throw new Exceptions.SellerNotFoundException(sellerId);

            // Realizar la asignación
            cm.AssignCustomerToSeller(customer, seller);

            // Mensaje de confirmación con formato
            return "✅ ***Cliente asignado correctamente***\n\n" +
                   "**Cliente:**\n" +
                   "  • ID: " + customer.Id + "\n" +
                   "  • Nombre: " + customer.Name + " " + customer.FamilyName + "\n" +
                   "  • Email: " + customer.Mail + "\n\n" +
                   "**Asignado a vendedor:**\n" +
                   "  • ID: " + seller.Id + "\n" +
                   "  • Nombre: " + seller.Name + "\n\n" +
                   "_Asignación realizada el " + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "_";
        }
        catch (Exceptions.NotExistingCustomerException)
        {
            return "No existe un cliente con el ID: **" + customerId + "**";
        }
        catch (Exceptions.SellerNullException)
        {
            return "No existe un vendedor con el ID proporcionado";
        }
        catch (System.Exception ex)
        {
            return "Error al asignar cliente: " + ex.Message;
        }
    }

    public static string ExchangeCustomer(string customerId, string oldSellerId, string newSellerId)
    {
        try
        {
            // Buscar el cliente
            Customer customer = cm.SearchById(customerId);
            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            // Buscar vendedor anterior
            Seller oldSeller = sm.SearchById(oldSellerId);
            if (oldSeller == null)
                throw new Exceptions.SellerNotFoundException(oldSellerId);

            // Buscar vendedor nuevo
            Seller newSeller = sm.SearchById(newSellerId);
            if (newSeller == null)
                throw new Exceptions.SellerNotFoundException(newSellerId);

            // Remover el cliente del vendedor anterior
            if (!oldSeller.Customer.Contains(customer))
                throw new Exception($"El cliente {customerId} no pertenece al vendedor {oldSellerId}.");

            oldSeller.Customer.Remove(customer);

            // Reutilizar tu método AssignCustomer
            string assignResult = AssignCustomer(customerId, newSellerId);

            // Respuesta final formateada
            return assignResult;
        }
        catch (Exception ex)
        {
            return "Error en la reasignación: " + ex.Message;
        }
    }


    /// <summary>
    /// Registra de forma centralizada una nueva interacción entre cliente y vendedor.
    /// Este método es reutilizado por los distintos tipos de interacciones (llamadas, reuniones, etc.).  
    /// </summary>
    private static string RegisterInteraction(Interaction interaction, Customer customer, Seller seller)
    {
        try
        {
            cm.RegisterInteraction(customer, seller, interaction);
            return $"```" +
                   "Interacción registrada correctamente." +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    // --------------------------- REGISTROS ---------------------------

    public static string CallRegister(string dateStr, string topic, string typeStr, string customerId,
        string sellerId)
    {
        try
        {
            DateTime date = ParseDate(dateStr);
            ExchangeType type = ParseExchangeType(typeStr);

            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.GetActiveSeller(sellerId);

            //Seller seller = sm.SearchById(sellerId);

            Call call = new Call(date, topic, type, customer);

            return RegisterInteraction(call, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string MeetingRegister(string place, string dateStr, string topic, string typeStr,
        string customerId, string sellerId)
    {
        try
        {
            DateTime date = ParseDate(dateStr);
            ExchangeType type = ParseExchangeType(typeStr);

            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.GetActiveSeller(sellerId);

            //Seller seller = sm.SearchById(sellerId);

            Meeting meeting = new Meeting(place, date, topic, type, customer);

            return RegisterInteraction(meeting, customer, seller);
        }
        catch (Exception ex)
        {
            return $"***Registro de la Interaccion:***" +
                   "```" +
                   ex.Message +
                   "```";
        }
    }

    public static string MessageRegister(string dateStr, string topic, string typeStr, string customerId,
        string sellerId)
    {
        try
        {
            DateTime date = ParseDate(dateStr);
            ExchangeType type = ParseExchangeType(typeStr);

            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.GetActiveSeller(sellerId);

            //Seller seller = sm.SearchById(sellerId);

            Message message = new Message(date, topic, type, customer);

            return RegisterInteraction(message, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string MailRegister(string dateStr, string topic, string typeStr, string customerId, string sellerId)
    {
        try
        {
            DateTime date = ParseDate(dateStr);
            ExchangeType type = ParseExchangeType(typeStr);

            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.GetActiveSeller(sellerId);

            //Seller seller = sm.SearchById(sellerId);

            Mail mailObj = new Mail(date, topic, type, customer);

            return RegisterInteraction(mailObj, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    
    
    public static string QuoteRegister(
        string dateStr, string topic, string typeStr, string amountStr,
        string description, string customerId, string sellerId)
    {
        try
        {
            DateTime date = ParseDate(dateStr);
            ExchangeType type = ParseExchangeType(typeStr);
            double amount = ParseDouble(amountStr);

            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.GetActiveSeller(sellerId);

            // Validar duplicado
            if (customer.FindQuote(date, topic, type, amount, description) != null)
                throw new Exceptions.DuplicateQuoteException();

            // Crear cotización
            Quote quote = new Quote(date, topic, type, customer, amount, description);

            return RegisterInteraction(quote, customer, seller);
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
    
   
    /// <summary>
    /// Genera una venta a partir de una cotización previa (Quote).
    /// Verifica que la cotización coincida con los datos provistos antes de crear la venta.
    /// </summary>
    
    
    public static string SaleFromQuote(
        string dateStr, string topic, string typeStr, string amountStr,
        string product, string customerId, string sellerId)
    {
        try
        {
            DateTime date = ParseDate(dateStr);
            double amount = ParseDouble(amountStr);
            ExchangeType type = ParseExchangeType(typeStr);
        
            Customer customer = cm.SearchById(customerId);
            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            Seller seller = sm.GetActiveSeller(sellerId);
            if (seller == null)
                throw new Exceptions.SellerNullException();

            // Buscar la cotización
            Quote foundQuote = customer.FindQuote(date, topic, type, amount);
            if (foundQuote == null)
                throw new Exceptions.QuoteNotFoundException();

            // Verificar si ya existe la venta
            if (customer.HasSale(date, topic, type, product, amount))
                throw new Exceptions.DuplicateSaleException();

            // Crear la venta
            Sale sale = new Sale(product, foundQuote, date, topic, type, customer);

            return RegisterInteraction(sale, customer, seller);
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

    public static string SuspendSeller(string sellerId)
    {
        try
        {
            Seller seller = sm.SearchById(sellerId);

            if (seller == null)
                throw new Exceptions.SellerNotFoundException(sellerId);

            sm.SuspendSeller(seller);

            return $"El vendedor '{sellerId}' ha sido suspendido correctamente.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static string EnableSeller(string sellerId)
    {
        try
        {
            Seller seller = sm.SearchById(sellerId);

            if (seller == null)
                throw new Exceptions.SellerNullException();

            sm.EnableSeller(seller);

            return $"El vendedor '{sellerId}' ha sido habilitado correctamente.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static string DeleteSeller(string sellerId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(sellerId))
                throw new ArgumentException("El ID no puede estar vacío.");

            Seller seller = sm.SearchById(sellerId);

            if (seller == null)
                throw new Exceptions.SellerNotFoundException(sellerId);

            sm.DeleteSeller(seller);

            return
                "```" +
                $"***Vendedor '{sellerId}' eliminado correctamente.***" +
                "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static string GetTotalSales(string startStr, string endStr)
    {
        try
        {
            DateTime start = ParseDate(startStr);
            DateTime end = ParseDate(endStr);

            var sales = sm.GetSalesBetween(start, end);

            if (sales.Count == 0)
                return $"No hubo ventas entre {start:yyyy-MM-dd} y {end:yyyy-MM-dd}.";

            double total = sales.Sum(s => s.Amount.Amount);

            return $"Total de ventas entre {start:yyyy-MM-dd} y {end:yyyy-MM-dd}: **USD {total:F2}**";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    

    public static string GetInactiveCustomersFormatted(int days)
    {
        try
        {
            if (days <= 0)
                throw new Exception("El número de días debe ser mayor a cero.");

            var inactiveList = cm.GetInactiveCustomers(days);

            if (inactiveList.Count == 0)
                return $"Todos los clientes han tenido interacción en los últimos {days} días.";

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"**Clientes sin interacción en más de {days} días:**");
            sb.AppendLine("```");

            foreach (var c in inactiveList)
            {
                sb.AppendLine($"{c.Name} {c.FamilyName} — ID: {c.Id}");
            }

            sb.AppendLine("```");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    
    public static string GetDashboardFormatted()
    {
        try
        {
            DashboardSummary summary = cm.GetDashboard();

            string mensaje = "**PANEL GENERAL**\n";
            mensaje += "--------------------------------------\n\n";
            mensaje += "**Clientes totales:** " + summary.TotalCustomers + "\n\n";

            mensaje += "**Interacciones recientes:**\n";
            if (summary.RecentInteractions.Count == 0)
            {
                mensaje += "- No hay interacciones registradas.\n\n";
            }
            else
            {
                foreach (var i in summary.RecentInteractions)
                {
                    mensaje += "- " + i.Date.ToString("yyyy-MM-dd") + " — " + i.GetType().Name + "\n";
                }
                mensaje += "\n";
            }

            mensaje += "**Próximas reuniones:**\n";
            if (summary.UpcomingMeetings.Count == 0)
            {
                mensaje += "- No hay reuniones próximas.\n";
            }
            else
            {
                foreach (var m in summary.UpcomingMeetings)
                {
                    mensaje += "- " + m.Date.ToString("yyyy-MM-dd") + " — " + m.Place + "\n";
                }
            }

            return mensaje;
        }
        catch (Exception ex)
        {
            return "Error al obtener el dashboard: " + ex.Message;
        }
    }
}