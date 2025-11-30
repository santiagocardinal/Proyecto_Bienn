
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
        string mail, string phone, string gender, DateTime birthDate)
    {
        try
        {
            Customer c1 = new Customer(id, name, familyName, mail, phone, gender, birthDate);
            cm.AddCustomer(c1);
            return $"***Cliente creado correctamente***\n" +
                   "```\n" +
                   $"ID:           " + id + "\n" + 
                   $"Nombre:       " + name + " " + familyName + "\n" +
                   $"Email:        " + mail + "\n" +
                   $"Teléfono:     " + phone + "\n" +
                   $"Género:       " + gender + "\n" +
                   $"Fecha Nac.:   " + birthDate.ToString("dd/MM/yyyy") + "\n" +
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

            return $"```\n"+
                   "Cliente eliminado correctamente."+
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
            return $"```"+
                   "Cliente modificado correctamente."+
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

            return$"***Cliente buscado por ID:***"+
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


    /*public static string SearchCostumer_ByName(string name)
    {
        try
        {
            Customer c = cm.SearchByName(name);

            return $"Cliente encontrado:\n" +
                   $"- ID: {c.Id}\n" +
                   $"- Nombre: {c.Name} {c.FamilyName}\n" +
                   $"- Mail: {c.Mail}\n" +
                   $"- Teléfono: {c.Phone}";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }*/
    
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
                   "```"+
                   result+
                    "```";

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    
    
    

    /*public static string SearchCostumer_ByFamilyName(string familyname)
    {
        try
        {
            Customer c = cm.SearchByFamilyName(familyname);

            return $"Cliente encontrado:\n" +
                   $"- ID: {c.Id}\n" +
                   $"- Nombre: {c.Name} {c.FamilyName}\n" +
                   $"- Mail: {c.Mail}\n" +
                   $"- Teléfono: {c.Phone}";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }*/
    
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

            return $"***Cliente buscado por su APELLIDO:***"+
                "```"+
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
                throw new Exceptions.SellerNotFoundException(seller.Id);

            if (seller.Customer.Count == 0)
                return $"```" +
                       "El vendedor no tiene clientes asignados." +
                       "```";

            string result = "";
            foreach (Customer customer in seller.Customer)
            {
                result += $"- {customer.Name}\n";
            }

            return $"***Clientes por vendedor:***"+
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
            // 1. Verificar que el cliente exista
            Customer customer = cm.SearchById(customerId);

            // 2. Verificar que la tag exista en el sistema global
            if (!cm.TagExists(tagId))
            {
                return $"__La Tag__:\n ** '{tagId}'**\n No existe en el sistema. Créala primero con **CreateTag.**";
            }

            // 3. Añadir la tag al cliente
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
    public static string AddNoteToInteraction(string customerId, string interactionTopic, DateTime interactionDate, Note note)
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
                   "No existe un cliente con el ID: " + customerId + "\n"+
                   "```";
            
        System.Collections.Generic.List<Interaction> interactions = customer.Interactions;
        return FormatInteractionsMessage(customerId, interactions, null, null);
    }
    catch (Exceptions.NotExistingCustomerException)
    {
        return $"```" +
               "No existe un cliente con el ID: " + customerId+"\n" +
               "```";
    }
    catch (System.Exception ex)
    {
        return $"```" +
               "**Error al obtener las interacciones:** " + ex.Message +
               "```";
    }
}

/// <summary>
/// Obtiene las interacciones de un cliente filtradas por tipo.
/// </summary>
public string GetCustomerInteractionsByType(string customerId, string interactionType)
{
    try
    {
        Customer customer = cm.SearchById(customerId);
        if (customer == null)
            return "```\n" +
                   "No existe un cliente con el ID: " + customerId + "\n" +
                   "```";
            
        System.Collections.Generic.List<Interaction> interactions = customer.Interactions;
        
        // Filtrar por tipo
        if (!string.IsNullOrEmpty(interactionType))
        {
            System.Collections.Generic.List<Interaction> filtered = new System.Collections.Generic.List<Interaction>();
            foreach (var interaction in interactions)
            {
                if (interaction.GetType().Name.Equals(interactionType, System.StringComparison.OrdinalIgnoreCase))
                {
                    filtered.Add(interaction);
                }
            }
            interactions = filtered;
        }
        
        return FormatInteractionsMessage(customerId, interactions, interactionType, null);
    }
    catch (Exceptions.NotExistingCustomerException)
    {
        return $"```" +
               "No existe un cliente con el ID: **" + customerId + "\n" +
               "```";
    }
    catch (System.Exception ex)
    {
        return $"```" +
               "Error al obtener las interacciones: " + ex.Message +"\n"+
               "```";
    }
}

/// <summary>
/// Obtiene las interacciones de un cliente filtradas por fecha.
/// </summary>
public string GetCustomerInteractionsByDate(string customerId, string dateString)
{
    try
    {
        if (!System.DateTime.TryParse(dateString, out System.DateTime parsedDate))
        {
            return $"```" +
                   "Formato de fecha inválido. Usa el formato: YYYY-MM-DD" 
                         +"\n"+ "```";
        }

        Customer customer = cm.SearchById(customerId);
        if (customer == null)
            return $"```" +
                   "No existe un cliente con el ID: " + customerId + "\n" +
                   "```";
            
        System.Collections.Generic.List<Interaction> interactions = customer.Interactions;
        
        // Filtrar por fecha
        System.Collections.Generic.List<Interaction> filtered = new System.Collections.Generic.List<Interaction>();
        foreach (var interaction in interactions)
        {
            if (interaction.Date.Date == parsedDate.Date)
            {
                filtered.Add(interaction);
            }
        }
        interactions = filtered;
        
        return FormatInteractionsMessage(customerId, interactions, null, parsedDate);
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
               "Error al obtener las interacciones: " + ex.Message + "\n"+
               "```";
    }
}

/// <summary>
/// Obtiene las interacciones de un cliente filtradas por tipo y fecha.
/// </summary>
public string GetCustomerInteractionsByTypeAndDate(string customerId, string interactionType, string dateString)
{
    try
    {
        if (!System.DateTime.TryParse(dateString, out System.DateTime parsedDate))
        {
            return $"```" +
                   "Formato de fecha inválido. Usa el formato: YYYY-MM-DD" + "\n"+
                   "```";
        }

        Customer customer = cm.SearchById(customerId);
        if (customer == null)
            return $"```" +
                   "No existe un cliente con el ID: " + customerId + "\n" +
                   "```";
                   
            
        System.Collections.Generic.List<Interaction> interactions = customer.Interactions;
        
        // Filtrar por tipo
        if (!string.IsNullOrEmpty(interactionType))
        {
            System.Collections.Generic.List<Interaction> filteredByType = new System.Collections.Generic.List<Interaction>();
            foreach (var interaction in interactions)
            {
                if (interaction.GetType().Name.Equals(interactionType, System.StringComparison.OrdinalIgnoreCase))
                {
                    filteredByType.Add(interaction);
                }
            }
            interactions = filteredByType;
        }
        
        // Filtrar por fecha
        System.Collections.Generic.List<Interaction> filteredByDate = new System.Collections.Generic.List<Interaction>();
        foreach (var interaction in interactions)
        {
            if (interaction.Date.Date == parsedDate.Date)
            {
                filteredByDate.Add(interaction);
            }
        }
        interactions = filteredByDate;
        
        return FormatInteractionsMessage(customerId, interactions, interactionType, parsedDate);
    }
    catch (Exceptions.NotExistingCustomerException)
    {
        return $"```" +
               "No existe un cliente con el ID: " + customerId + "*\n" +
               "```";
    }
    catch (System.Exception ex)
    {
        return $"```" +
               "Error al obtener las interacciones: " + ex.Message + "\n"+
               "```";
    }
}
private string FormatInteractionsMessage(
    string customerId, 
    System.Collections.Generic.List<Interaction> interactions,
    string interactionType = null,
    System.DateTime? filterDate = null)
{
    // Si no hay interacciones
    if (interactions == null || interactions.Count == 0)
    {
        string noResultsMsg = $"```" +$"No se encontraron interacciones para el cliente " + customerId ;
        if (!string.IsNullOrEmpty(interactionType))
            noResultsMsg += $" del tipo " + interactionType +"```";
        if (filterDate.HasValue)
            noResultsMsg += $" en la fecha " + filterDate.Value.ToString("dd/MM/yyyy")  + "```";
        
        return noResultsMsg;
    }

    // Construir el mensaje de respuesta
    System.Text.StringBuilder response = new System.Text.StringBuilder();
    response.AppendLine($"Historial de Interacciones - Cliente " + customerId );
    
    // Mostrar filtros aplicados si hay
    if (!string.IsNullOrEmpty(interactionType) || filterDate.HasValue)
    {
        response.Append($"Filtros aplicados:");
        if (!string.IsNullOrEmpty(interactionType))
            response.Append($"Tipo=" + interactionType + " ");
        if (filterDate.HasValue)
            response.Append($"Fecha=" + filterDate.Value.ToString("dd/MM/yyyy"));
        response.AppendLine();
    }
    
    response.AppendLine($"Total: " + interactions.Count + " interacción(es)\n");
    response.AppendLine("```");

    // Listar cada interacción ordenada por fecha
    int counter = 1;
    var orderedInteractions = interactions.OrderByDescending(i => i.Date);
    foreach (var interaction in orderedInteractions)
    {
        response.AppendLine("[" + counter + "] " + interaction.GetType().Name);
        response.AppendLine($"    Fecha:  " + interaction.Date.ToString("dd/MM/yyyy HH:mm"));
        response.AppendLine($"    Tema:   " + interaction.Topic);
        response.AppendLine($"    Tipo:   " + interaction.Type);
        
        // Información adicional según el tipo de interacción
        if (interaction is Quote)
        {
            Quote quote = (Quote)interaction;
            response.AppendLine($"    Monto:  $" + quote.Amount.ToString("N2"));
        }
        
        // Acceder a Description de forma segura usando reflexión
        var descProperty = interaction.GetType().GetProperty("Description");
        if (descProperty != null)
        {
            var descValue = descProperty.GetValue(interaction);
            if (descValue != null)
            {
                string desc = descValue.ToString();
                if (!string.IsNullOrEmpty(desc))
                    response.AppendLine($"    Desc:   " + desc);
            }
        }
        
        response.AppendLine();
        counter++;
    }

    response.AppendLine("```");

    // Validar longitud del mensaje (límite de Discord: 2000 caracteres)
    string finalMessage = response.ToString();
    if (finalMessage.Length > 1990)
    {
        return $"Historial de Interacciones - Cliente " + customerId + "\n" +
               "Total: " + interactions.Count + " interacciones\n\n" +
               "Demasiadas interacciones para mostrar en un solo mensaje.\n" +
               "Usa filtros más específicos para ver los detalles.";
    }

    return finalMessage;
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

            return $"```"+ 
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
                throw new Exceptions.SellerNotFoundException(seller.Id);

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
    
    
    // ---------------------------------------------------------
    //   ASIGNAR CLIENTE A VENDEDOR
    // ---------------------------------------------------------
    public static string AssignCustomer(string customerId, string sellerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.GetActiveSeller(sellerId);

            //Seller seller = sm.SearchById(sellerId);

            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            if (seller == null)
                throw new Exceptions.SellerNotFoundException(seller.Id);

            cm.AssignCustomerToSeller(customer, seller);

            return $"```"+ 
                "Cliente asignado correctamente." +
                   "```";
        }
        catch (Exception ex)
        {
            return ex.Message;
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
            return $"```"+
                "Interacción registrada correctamente."+
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
            return $"***Registro de la Interaccion:***"+
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

            //Seller seller = sm.SearchById(sellerId);

            // -----------------------------------------
            // VALIDACIÓN: evitar duplicados de Quote
            // -----------------------------------------
            bool alreadyExists = customer.Interactions
                .OfType<Quote>()
                .Any(q =>
                    q.Date == date &&
                    q.Topic == topic &&
                    q.Type == type &&
                    Math.Abs(q.Amount - amount) < 0.0001 &&
                    q.Description == description);

            if (alreadyExists)
                throw new Exceptions.DuplicateQuoteException();

            // -----------------------------------------
            // Crear Quote NUEVA
            // -----------------------------------------
            Quote quote = new Quote(date, topic, type, customer, amount, description);

            return RegisterInteraction(quote, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
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
            Seller seller = sm.GetActiveSeller(sellerId);

            //Seller seller = sm.SearchById(sellerId);

            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            if (seller == null)
                throw new Exceptions.SellerNotFoundException("null");

            Quote foundQuote = customer.Interactions
                .OfType<Quote>()
                .FirstOrDefault(q =>
                    q.Date == date &&
                    q.Topic == topic &&
                    q.Type == type &&
                    Math.Abs(q.Amount - amount) < 0.0001);

            if (foundQuote == null)
                throw new Exceptions.QuoteNotFoundException();
            
            Sale existingSale = customer.Interactions
                .OfType<Sale>()
                .FirstOrDefault(s =>
                    s.Date == date &&
                    s.Topic == topic &&
                    s.Type == type &&
                    s.Product == product &&
                    s.Amount.Amount == amount);

            if (existingSale != null)
                throw new Exceptions.DuplicateSaleException();

            Sale sale = new Sale(product, foundQuote, date, topic, type, customer);

            return RegisterInteraction(sale, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
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
                throw new Exceptions.SellerNotFoundException(sellerId);

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
        catch(Exception ex)
        {
            return ex.Message;
        }
    }

    public static string GetDashboardFormatted()
    {
        var customers = cm.Customers;

        int totalCustomers = customers.Count;

        var recentInteractions = customers
            .SelectMany(c => c.Interactions)
            .OrderByDescending(i => i.Date)
            .Take(5)
            .ToList();

        var upcomingMeetings = customers
            .SelectMany(c => c.Interactions)
            .OfType<Meeting>()
            .Where(m => m.Date > DateTime.Now)
            .OrderBy(m => m.Date)
            .Take(5)
            .ToList();

        // Construir DTO (si querés usarlo en otros lados)
        DashboardSummary summary = new DashboardSummary(
            recentInteractions,
            upcomingMeetings,
            totalCustomers
        );

        // Ahora formatear el mensaje final
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("**PANEL GENERAL**");
        sb.AppendLine("--------------------------------------\n");

        sb.AppendLine($"**Clientes totales:** {summary.TotalCustomers}\n");

        sb.AppendLine("🕒 **Interacciones recientes:**");
        if (summary.RecentInteractions.Count == 0)
        {
            sb.AppendLine("- No hay interacciones registradas.\n");
        }
        else
        {
            foreach (var i in summary.RecentInteractions)
            {
                sb.AppendLine(
                    $"- {i.Date:yyyy-MM-dd} — {i.GetType().Name} — Cliente: {i.Customer.Name}"
                );
            }
            sb.AppendLine();
        }

        sb.AppendLine("📅 **Próximas reuniones:**");
        if (summary.UpcomingMeetings.Count == 0)
        {
            sb.AppendLine("- No hay reuniones próximas.\n");
        }
        else
        {
            foreach (var m in summary.UpcomingMeetings)
            {
                sb.AppendLine(
                    $"- {m.Date:yyyy-MM-dd} — {m.Place} — Cliente: {m.Customer.Name}"
                );
            }
        }

        return sb.ToString();
    }

}
