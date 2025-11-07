namespace Library;

public class FacadeDos
{
    public static CustomerManager cm = Singleton<CustomerManager>.GetInstance();
    public static SellerManager sm = Singleton<SellerManager>.GetInstance();


    //Como usuario quiero crear un nuevo cliente con su información básica: nombre, apellido,
    //teléfono y correo electrónico, para poder contactarme con ellos cuando lo necesite.
    public static void CreateCustomer(string id, string name, string familyName, string mail, string phone,
        string gender, DateTime birthDate)
    {
        Customer c1 = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        cm.AddCustomer(c1);
    }

    //Como usuario quiero modificar la información de un cliente existente, para mantenerla actualizada.
    public static void ModifyCustomer(string id, string field, string newValue)
    {
        Customer customer = cm.Customers.FirstOrDefault(c => c.Id == id);

        if (customer != null)
        {
            cm.Modify(id, field, newValue);
        }
    }

    
    //Como usuario quiero buscar clientes por nombre, apellido,
    //teléfono o correo electrónico, para identificarlos rápidamente.

    public static string SearchCostumer_ByName(string name)
    {
        Customer customer = cm.SearchByName(name);

        if (customer != null)
        {
            return customer.Name;
        }
        else
        {
            return $"El cliente '{name}' no se ha encontrado.";
        }
    }

    public static string SearchCostumer_ByFamilyName(string familyname)
    {
        Customer customer = cm.SearchByFamilyName(familyname);

        if (customer != null)
        {
            return customer.FamilyName;
        }
        else
        {
            return $"El cliente '{familyname}' no se ha encontrado.";
        }
    }

    public static string SearchCostumer_ByPhone(string phone)
    {
        Customer customer = cm.SearchByPhone(phone);

        if (customer != null)
        {
            return customer.Phone;
        }
        else
        {
            return $"El cliente cuyo numero es: '{phone}' ,no se ha encontrado.";
        }
    }

    public static string SearchCostumer_ByMail(string mail)
    {
        Customer customer = cm.SearchByMail(mail);

        if (customer != null)
        {
            return customer.Mail;
        }
        else
        {
            return $"El cliente cuyo correo es: '{mail}' ,no se ha encontrado.";
        }
    }
//Como usuario quiero ver una lista de todos mis clientes, para tener una vista general de mi cartera.

    public static string ShowCustomers_BySellerId(string sellerId)
    {
        Seller seller = sm.Sellers.FirstOrDefault(s => s.Id == sellerId);
        string result = "";

        if (seller != null)
        {
            foreach (Customer customer in seller.Customer)
            {
                result += $"- {customer.Name}\n";
            }
            return result; 
        }

        return "Vendedor no encontrado o sin clientes.";
    }

    public static void AddCustomer(Customer customer)
    {
        if (customer != null)
        {
            cm.AddCustomer(customer);
        }

    }

    // Como usuario quiero agregar una etiqueta a un cliente.
        public static string AddTag_Customer(string customerId, string tagId, string tagName, string tagDescription)
        {
            Customer customer = cm.SearchById(customerId);

            if (customer != null)
            {
                // Verificar si el cliente ya tiene una etiqueta con el mismo nombre
                bool exists = false;
                foreach (Tag existingTag in customer.Tags)
                {
                    if (existingTag.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase))
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    return $"El cliente '{customerId}' ya tiene una etiqueta llamada '{tagName}'.";
                }

                Tag tag = new Tag(tagId, tagName, tagDescription);
                customer.AddTag(tag);

                return $"Etiqueta '{tagName}' agregada al cliente '{customer.Id}'.";
            }
            else
            {
                return $"El cliente '{customerId}' no se ha encontrado.";
            }
        }
    
    // Ver panel con: clientes totales, interacciones recientes, reuniones próximas 
    
    
    private static List<User> users = new List<User>();

    // Como administrador quiero crear un nuevo usuario (esta mal, se crea en seller manager)
    public static string CreateUser(string id, string name, string mail, string phone)
    {
        foreach (User existingUser in users)
        {
            if (existingUser.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                return $"Ya existe un usuario con el ID '{id}'.";
            }
        }

        User user = new User(name, mail, phone, id);
        users.Add(user);
        return $"Usuario '{name}' creado correctamente.";
    }

    // Como administrador quiero suspender un usuario
    public static string SuspendUser(string id)
    {
        User user = null;

        foreach (User u in users)
        {
            if (u.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                user = u;
                break;
            }
        }

        if (user == null)
        {
            return $"No se encontró el usuario con ID '{id}'.";
        }

        return $"Usuario '{user.Name}' suspendido correctamente (simulado).";
    }

    // Como administrador quiero eliminar un usuario
    public static string DeleteUser(string id)
    {
        User userToRemove = null;

        foreach (User u in users)
        {
            if (u.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                userToRemove = u;
                break;
            }
        }

        if (userToRemove != null)
        {
            users.Remove(userToRemove);
            return $"Usuario '{userToRemove.Name}' eliminado correctamente.";
        }

        return $"No se encontró el usuario con ID '{id}'.";

    }
    
    
    //Como usuario quiero agregar notas o comentarios a las llamadas, reuniones, mensajes y correos enviados o recibidos
    //de los clientes, para tener información adicional de mis interacciones con los clientes.
    public void AddNoteToInteraction(string customerId, string interactionTopic, DateTime interactionDate, Note note)
    {
        // Recorrer los clientes del CustomerManager
        for (int i = 0; i < cm.Customers.Count; i++)
        {
            if (cm.Customers[i].Id.Equals(customerId, StringComparison.OrdinalIgnoreCase))
            {
                // Encontré el cliente, ahora busco su interacción
                for (int j = 0; j < cm.Customers[i].Interactions.Count; j++)
                {
                    if (cm.Customers[i].Interactions[j].Topic.Equals(interactionTopic, StringComparison.OrdinalIgnoreCase) &&
                        cm.Customers[i].Interactions[j].Date == interactionDate)
                    {
                        // Se encontró la interacción, agrego la nota
                        cm.Customers[i].Interactions[j].AddNote(note);
                        Console.WriteLine($"Nota agregada a la interacción del cliente {cm.Customers[i].Name}");
                        return;
                    }
                }

                Console.WriteLine("Interacción no encontrada.");
                return;
            }
        }

        Console.WriteLine("Cliente no encontrado.");
    }
    
    //Como usuario quiero ver todas las interacciones de un cliente, con o sin filtro por tipo de interacción y por fecha,
    //para entender el historial de la relación comercial.
    
    public List<Interaction> GetCustomerInteractions(string customerId,string interactionType = null, DateTime? date = null)
    {
        // Busca cliente por ID
        Customer customer = cm.SearchById(customerId);

        if (customer == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return new List<Interaction>();
        }

        //Toma todas las interacciones del cliente
        List<Interaction> interactions = customer.Interactions;

        //Filtra por tipo si se especifica
        if (!string.IsNullOrEmpty(interactionType))
        {
            interactions = interactions
                .Where(i => i.GetType().Name.Equals(interactionType, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        //Filtrar por fecha si se especifica
        if (date != null)
        {
            interactions = interactions
                .Where(i => i.Date.Date == date.Value.Date)
                .ToList();
        }

        //Retorna la lista resultante
        return interactions;
    }

    
    //Cómo usuario quiero saber los clientes que hace cierto tiempo que no tengo ninguna interacción con ellos, para no
    //peder contacto con ellos.

    public static Interaction LastInteraction(string customerId)
    {
        Customer customer = cm.SearchById(customerId);
        if (customer.Interactions == null || customer.Interactions.Count == 0)
        {
            return null; 
        }
    
        Interaction lastInteraction = customer.Interactions[0];
    
        foreach (var interaction in customer.Interactions)
        {
            if (interaction.Date > lastInteraction.Date)
            {
                lastInteraction = interaction;
            }
        }
        return lastInteraction;
    }
    
    //Como usuario quiero saber los clientes que se pusieron en contacto conmigo y no les contesté hace cierto tiempo,
    //para no dejar de responder mensajes o llamadas
    
    public static string UnansweredInteractions(string customerId)
    {
        Customer customer = cm.SearchById(customerId);
        List<Interaction> unanswered = customer.GetUnansweredInteractions();
    
        if (unanswered.Count == 0)
        {
            return "No hay interacciones sin responder.";
        }
    
        string report = $"Interacciones sin responder ({unanswered.Count}):\n";
        foreach (var interaction in unanswered)
        {
            report += $"- {interaction.ToString()}\n"; // Concatenar al report
        }

        return report;
    }

    public static void AssignCustomer(string customerId, string sellerId)
    {
        Customer customer = cm.SearchById(customerId);
        Seller seller = sm.SearchById(sellerId);
        cm.AssignCustomerToSeller(customer, seller);
    }
    
    public static List<Sale> SalesWithin_A_Period(string sellerId, DateTime startDate, DateTime endDate)
    {
        Seller seller = sm.SearchById(sellerId);
        if (!sm.Sellers.Contains(seller))
        {
            throw new Exception("Seller does not exist in SellerManager");
        }

        if (startDate > endDate)
        {
            throw new ArgumentException("startDate must be before endDate");
        }

        List<Sale> result = new List<Sale>();

        foreach (Interaction interaction in seller.Interactions)
        {
            // Filtra solo las ventas
            if (interaction is Sale sale)
            {
                // Filtra dentro del período
                if (sale.Date >= startDate && sale.Date <= endDate)
                {
                    result.Add(sale);
                }
            }
        }

        return result;
    }
    
    
    private static string RegisterInteraction(Interaction interaction, Customer customer, Seller seller)
    {
        if (interaction == null)
            throw new ArgumentNullException(nameof(interaction));
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        if (seller == null)
            throw new ArgumentNullException(nameof(seller));

        cm.RegisterInteraction(customer, seller, interaction  );
        return $" Interacción registrada: {interaction.GetType().Name} -> Cliente {customer.Name}";
    }
    

    // ----------------------------------------------------------
    //  REGISTRO DE INTERACCIONES
    // ----------------------------------------------------------

    public static void CallRegister(DateTime date, string topic, ExchangeType type, string customerId, string sellerId)
    {
        Customer customer = cm.SearchById(customerId);
        Seller seller = sm.SearchById(sellerId);
        Call call = new Call(date, topic, type, customer);
        RegisterInteraction(call, customer, seller);
    }

    public static void MeetingRegister(string place, DateTime date, string topic, ExchangeType type, string customerId, string sellerId)
    {
        Customer customer = cm.SearchById(customerId);
        Seller seller = sm.SearchById(sellerId);
        Meeting meeting = new Meeting(place, date, topic, type, customer);
        RegisterInteraction(meeting, customer, seller);
    }

    public static void MessageRegister(DateTime date, string topic, ExchangeType type, string customerId, string sellerId)
    {
        Customer customer = cm.SearchById(customerId);
        Seller seller = sm.SearchById(sellerId);
        Message message = new Message(date, topic, type, customer);
        RegisterInteraction(message, customer, seller);
    }

    public static void MailRegister(DateTime date, string topic, ExchangeType type, string customerId, string sellerId)
    {
        Customer customer = cm.SearchById(customerId);
        Seller seller = sm.SearchById(sellerId);
        Mail mail = new Mail(date, topic, type, customer);
        RegisterInteraction(mail, customer, seller);
    }

    public static void QuoteRegister(DateTime date, string topic, ExchangeType type, double amount, string description, string customerId, string sellerId)
    {
        Customer customer = cm.SearchById(customerId);
        Seller seller = sm.SearchById(sellerId);
        Quote quote = new Quote(date, topic, type, customer, amount, description);
        RegisterInteraction(quote, customer, seller);
    }

    // ----------------------------------------------------------
    //  SALE DESDE UNA QUOTE
    // ----------------------------------------------------------
    public static void SaleFromQuote(
        string sellerId,
        string customerId,
        DateTime date,
        string topic,
        ExchangeType type,
        double amount,
        string product)
    {
        Customer foundCustomer = cm.Customers.FirstOrDefault(c => c.Id == customerId);
        Seller foundSeller = sm.Sellers.FirstOrDefault(s => s.Id == sellerId);

        if (foundCustomer == null)
            throw new Exception("Customer no encontrado.");
        if (foundSeller == null)
            throw new Exception("Seller no encontrado.");

        Quote foundQuote = null;

        foreach (var interaction in foundCustomer.Interactions)
        {
            if (interaction is Quote qt)
            {
                if (qt.Date == date &&
                    qt.Topic == topic &&
                    qt.Type == type &&
                    Math.Abs(qt.Amount - amount) < 0.0001)
                {
                    foundQuote = qt;
                    break;
                }
            }
        }

        if (foundQuote == null)
            throw new Exception("No se encontró la Quote correspondiente.");

        Sale sale = new Sale(product, foundQuote, date, topic, type, foundCustomer);
        RegisterInteraction(sale, foundCustomer, foundSeller);
    }
}

