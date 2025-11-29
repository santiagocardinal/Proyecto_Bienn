namespace Library;
using System;

/// <summary>
/// Clase fachada que centraliza las operaciones de clientes, vendedores e interacciones.
/// Permite acceder a las funcionalidades del sistema sin exponer su estructura interna.
/// </summary>
public class Facade
{
    public static CustomerManager cm = Singleton<CustomerManager>.GetInstance();
    public static SellerManager sm = Singleton<SellerManager>.GetInstance();

    
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
            return "Cliente creado correctamente.";
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
                throw new ArgumentException("El ID no puede estar vacío.");

            Customer customerToRemove = cm.SearchById(id);
            // Si no existe, SearchById ya lanza NotExistingCustomerException

            cm.Customers.Remove(customerToRemove);

            return "Cliente eliminado correctamente.";
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
            return "Cliente modificado correctamente.";
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

            return result;
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

            return result;
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
    }

    public static string SearchCostumer_ByMail(string mail)
    {
        try
        {
            Customer c = cm.SearchByMail(mail);

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
    }

    /// <summary>
    /// Muestra todos los clientes asociados a un vendedor específico.
    /// Devuelve un listado con los nombres o un mensaje si no hay clientes asignados.
    /// </summary>
    public static string ShowCustomers_BySellerId(string sellerId)
    {
        try
        {
            Seller seller = sm.SearchById(sellerId);

            if (seller == null)
                throw new Exceptions.SellerNotFoundException(seller.Id);

            if (seller.Customer.Count == 0)
                return "El vendedor no tiene clientes asignados.";

            string result = "";
            foreach (Customer customer in seller.Customer)
            {
                result += $"- {customer.Name}\n";
            }

            return result;
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
            return "Cliente agregado correctamente.";
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
                return "El ID de la Tag no puede estar vacío.";

            if (tagName == null || tagName.Trim() == "")
                return "El nombre de la Tag no puede estar vacío.";

            // Crear la Tag en CustomerManager
            cm.CreateTag(tagId, tagName, tagDescription);

            return $"La Tag '{tagId}' fue creada correctamente.";
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
                return $"La Tag '{tagId}' no existe en el sistema. Créala primero con CreateTag.";
            }

            // 3. Añadir la tag al cliente
            cm.AddTagToCustomer(customerId, tagId);

            return $"La Tag '{tagId}' fue añadida correctamente al cliente '{customerId}'.";
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

            return "Nota agregada correctamente.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // ---------------------------------------------------------
    //   OBTENER INTERACCIONES
    // ---------------------------------------------------------
    public List<Interaction> GetCustomerInteractions(
        string customerId, string interactionType = null, DateTime? date = null)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);

            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            List<Interaction> interactions = customer.Interactions;

            if (!string.IsNullOrEmpty(interactionType))
            {
                interactions = interactions
                    .Where(i => i.GetType().Name.Equals(interactionType, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (date != null)
            {
                interactions = interactions
                    .Where(i => i.Date.Date == date.Value.Date)
                    .ToList();
            }

            return interactions;
        }
        catch
        {
            return new List<Interaction>();
        }
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
                return "No hay interacciones registradas.";

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
                return "No hay interacciones sin responder.";

            string report = $"Interacciones sin responder ({unanswered.Count}):\n";
            foreach (var inter in unanswered)
                report += $"- {inter}\n";

            return report;
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

            return "Vendedor creado correctamente.";
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
            Seller seller = sm.SearchById(id);

            if (seller == null)
                throw new Exceptions.SellerNotFoundException(seller.Id);

            return $"Vendedor encontrado:\n" +
                   $"- ID: {seller.Id}\n" +
                   $"- Nombre: {seller.Name}\n" +
                   $"- Mail: {seller.Mail}\n" +
                   $"- Teléfono: {seller.Phone}\n" +
                   $"- Suspendido: {(seller.IsSuspended ? "Sí" : "No")}";
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
            Seller seller = sm.SearchById(sellerId);

            if (customer == null)
                throw new Exceptions.NotExistingCustomerException();

            if (seller == null)
                throw new Exceptions.SellerNotFoundException(seller.Id);

            cm.AssignCustomerToSeller(customer, seller);

            return "Cliente asignado correctamente.";
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
            return "Interacción registrada correctamente.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    
    
    
    // --------------------------- REGISTROS ---------------------------

    public static string CallRegister(DateTime date, string topic, ExchangeType type, string customerId,
        string sellerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.SearchById(sellerId);

            Call call = new Call(date, topic, type, customer);

            return RegisterInteraction(call, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string MeetingRegister(string place, DateTime date, string topic, ExchangeType type,
        string customerId, string sellerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.SearchById(sellerId);

            Meeting meeting = new Meeting(place, date, topic, type, customer);

            return RegisterInteraction(meeting, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string MessageRegister(DateTime date, string topic, ExchangeType type, string customerId,
        string sellerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.SearchById(sellerId);

            Message message = new Message(date, topic, type, customer);

            return RegisterInteraction(message, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string MailRegister(DateTime date, string topic, ExchangeType type, string customerId,
        string sellerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.SearchById(sellerId);

            Mail mailObj = new Mail(date, topic, type, customer);

            return RegisterInteraction(mailObj, customer, seller);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string QuoteRegister(DateTime date, string topic, ExchangeType type, double amount,
        string description, string customerId, string sellerId)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.SearchById(sellerId);

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
        string sellerId,
        string customerId,
        DateTime date,
        string topic,
        ExchangeType type,
        double amount,
        string product)
    {
        try
        {
            Customer customer = cm.SearchById(customerId);
            Seller seller = sm.SearchById(sellerId);

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
}