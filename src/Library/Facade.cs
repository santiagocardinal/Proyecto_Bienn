namespace Library;

//MI GENTE LATINO
//MISTER WORDWIDE, FIESTA
//DALE

public class Facade

{
    public static CustomerManager cm = new CustomerManager();
    public static SellerManager sm = new SellerManager();

    //Como usuario quiero crear un nuevo cliente con su información básica: nombre, apellido,
    //teléfono y correo electrónico, para poder contactarme con ellos cuando lo necesite.
    public static void CreateCustomer(string id, string name, string familyName, string mail, string phone,
        string gender, DateTime birthDate)
    {
        Customer c1 = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        cm.AddCustomer(c1);
    }

    //Como usuario quiero modificar la información de un cliente existente, para mantenerla actualizada.
    public static void ModifyCustomer(string name)
    {
        Customer customer = cm.Customers.FirstOrDefault(c => c.Name == name);

        if (customer != null)
        {
            cm.Modify(customer);
        }
    }

    // Como usuario quiero eliminar un cliente,
    // para mantener limpia la base de datos.
    public static void DelateCustomer(string name)
    {
        Customer customer = cm.SearchByName(name);

        if (customer != null)
        {
            bool deleted = cm.Delete(customer);
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
    
    /*//Como usuario quiero registrar llamadas enviadas o recibidas de clientes, incluyendo
    //cuándo fueron y de qué tema trataron, para poder saber mis interacciones con los clientes.

    public static void CallRegister(DateTime date, string topic, ExchangeType type, Customer _customer)
    {
        Call call = new Call(date, topic, type, _customer);
    }
    //Como usuario quiero registrar reuniones con los clientes, incluyendo cuándo y dónde fueron,
    //y de qué tema trataron, para poder saber mis interacciones con los clientes.
    public static void MeetingRegister(string place, DateTime date, string topic, ExchangeType type, Customer _customer)
    {
        Meeting meet = new Meeting(place, date, topic, type,_customer);
    }
    //Como usuario quiero registrar mensajes enviados a o recibidos de los clientes, incluendo cuándo y de qué tema fueron,
    //para poder saber mis interacciones con los clientes.
    
    public static void MessageRegister(DateTime date, string topic, ExchangeType type, Customer _customer)
    {
        Message meet = new Message(date, topic, type, _customer);
        
    }
    
    //Como usuario quiero registrar correos electrónicos enviados a o recibidos de los clientes, incluendo cuándo y de qué
    //tema fueron, para poder saber mis interacciones con los clientes.

    public static void MailRegister(DateTime date, string topic, ExchangeType type, Customer _customer)
    {
        Mail mail = new Mail(date, topic, type, _customer);
    }*/
    
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

    //Como usuario quiero poder definir etiquetas para poder organizar y segmentar a mis clientes.
    // Como usuario quiero poder definir etiquetas para poder organizar y segmentar a mis clientes.
    public static string AddTagToCustomer(string customerId, string tagId, string name, string description)
    {
        for (int i = 0; i < cm.Customers.Count; i++)
        {
            if (cm.Customers[i].Id.Equals(customerId, StringComparison.OrdinalIgnoreCase))
            {
                Tag tag = new Tag(tagId, name, description);

                cm.Customers[i].AddTag(tag);

                return $"Etiqueta '{name}' agregada al cliente {cm.Customers[i].Name}.";
            }
        }
        return $"Cliente con ID {customerId} no encontrado.";
    }
    
    //Cómo usuario quiero saber los clientes que hace cierto tiempo que no tengo ninguna interacción con ellos, para no
    //peder contacto con ellos.

    public static Interaction LastInteraction(Customer customer) 
    {
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

    public static string UnansweredInteractions(Customer customer)  
    {
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


    public static void AssignCustomer(Customer customer, Seller seller)
    {
        cm.AssignCustomerToSeller(customer, seller);
    }
    
    public static List<Sale> SalesWithin_A_Period(Seller seller, DateTime startDate, DateTime endDate)
    {
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

    /*public static void QuoteRegister(Customer customer, Seller seller, DateTime date, string topic, ExchangeType type, Customer _customer, double amount, string description)
    {
        // Buscar customer en cm.Customers (por Id o por referencia)
        Customer foundCustomer = null;
        if (customer != null)
        {
            // Intentar por Id (ajusta "Id" al nombre real si es otro)
            foundCustomer = cm.Customers
                                .FirstOrDefault(c => c.Id != null && customer.Id != null && c.Id == customer.Id)
                            ?? cm.Customers.FirstOrDefault(c => ReferenceEquals(c, customer));
        }

        // Buscar seller en sm.Sellers (por Id o por referencia)
        Seller foundSeller = null;
        if (seller != null)
        {
            foundSeller = sm.Sellers
                              .FirstOrDefault(s => s.Id != null && seller.Id != null && s.Id == seller.Id)
                          ?? sm.Sellers.FirstOrDefault(s => ReferenceEquals(s, seller));
        }

        // Validaciones: decidir si lanzar excepción o simplemente salir
        if (foundCustomer == null)
            throw new InvalidOperationException("Customer no está registrado en cm.");
        if (foundSeller == null)
            throw new InvalidOperationException("Seller no está registrado en sm.");

        // Crear la Quote — uso _customer si así lo requiere el constructor
        Quote qt = new Quote(date, topic, type, _customer, amount, description);

        // Añadir la quote a las interacciones de los objetos encontrados en cm y sm
        if (foundCustomer.Interactions == null)
            foundCustomer.Interactions = new List<Interaction>();
        if (foundSeller.Interactions == null)
            foundSeller.Interactions = new List<Interaction>();
        
        cm.AddInteraction(qt, foundSeller, foundCustomer);
    }

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

        if (foundCustomer == null)
            throw new Exception("Customer no encontrado en cm.");

        Seller foundSeller = sm.Sellers.FirstOrDefault(s => s.Id == sellerId);

        if (foundSeller == null)
            throw new Exception("Seller no encontrado en sm.");
        
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

        Sale newSale = new Sale(
            product,
            foundQuote,
            date,
            topic,
            type,
            foundCustomer
        );
        
        cm.AddInteraction(newSale, foundSeller, foundCustomer);
    }*/
    
    // ----------------------------------------------------------
    //  MÉTODO CENTRAL PARA REGISTRAR CUALQUIER INTERACCIÓN
    // ----------------------------------------------------------
    private static void RegisterInteraction(Interaction interaction, Customer customer, Seller seller)
    {
        if (interaction == null)
            throw new ArgumentNullException(nameof(interaction));
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        if (seller == null)
            throw new ArgumentNullException(nameof(seller));

        cm.AddInteraction(interaction, seller, customer);
        Console.WriteLine($"✅ Interacción registrada: {interaction.GetType().Name} -> Cliente {customer.Name}");
    }
    
    

    // ----------------------------------------------------------
    //  REGISTRO DE INTERACCIONES
    // ----------------------------------------------------------

    public static void CallRegister(DateTime date, string topic, ExchangeType type, Customer customer, Seller seller)
    {
        Call call = new Call(date, topic, type, customer);
        RegisterInteraction(call, customer, seller);
    }

    public static void MeetingRegister(string place, DateTime date, string topic, ExchangeType type, Customer customer, Seller seller)
    {
        Meeting meeting = new Meeting(place, date, topic, type, customer);
        RegisterInteraction(meeting, customer, seller);
    }

    public static void MessageRegister(DateTime date, string topic, ExchangeType type, Customer customer, Seller seller)
    {
        Message message = new Message(date, topic, type, customer);
        RegisterInteraction(message, customer, seller);
    }

    public static void MailRegister(DateTime date, string topic, ExchangeType type, Customer customer, Seller seller)
    {
        Mail mail = new Mail(date, topic, type, customer);
        RegisterInteraction(mail, customer, seller);
    }

    public static void QuoteRegister(DateTime date, string topic, ExchangeType type, Customer customer, Seller seller, double amount, string description)
    {
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
            throw new Exception("❌ Customer no encontrado.");
        if (foundSeller == null)
            throw new Exception("❌ Seller no encontrado.");

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
            throw new Exception("❌ No se encontró la Quote correspondiente.");

        Sale sale = new Sale(product, foundQuote, date, topic, type, foundCustomer);
        RegisterInteraction(sale, foundCustomer, foundSeller);
    }


}

