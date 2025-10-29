namespace Library;

//MI GENTE LATINO
//MISTER WORlDWIDE, FIESTA
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
        try
        {
            cm.AddCustomer(c1);
        }
        catch (DuplicatedCustomerException)
        {
            
        }
        
    }

    //Como usuario quiero modificar la información de un cliente existente, para mantenerla actualizada.
    public static void ModifyCustomer(string id, string field, string newValue)
    {
        try
        {
            cm.Modify(id, field, newValue);
        }
        catch (NotExistingCustomerException)
        {
        }
        catch (InvalidFieldException)
        {
        }
    }
    

    // Como usuario quiero eliminar un cliente,
    // para mantener limpia la base de datos.
    public static void DeleteCustomer(string name)
    {
        try
        {
            Customer customer = cm.SearchByName(name);
            cm.Delete(customer);
        }
        catch (NotExistingCustomerException)
        {
            // No usamos Console.WriteLine porque la fachada no debe mostrar UI
            // Puede quedar vacío o rethrow
            throw;
        }
    }


    //Como usuario quiero buscar clientes por nombre, apellido,
    //teléfono o correo electrónico, para identificarlos rápidamente.

    public static Customer SearchCustomer_ByName(string name)
    {
        try
        {
            return cm.SearchByName(name);
        }
        catch (NotExistingCustomerException)
        {
            return null;
        }
    }


    public static Customer SearchCostumer_ByFamilyName(string familyname)
    {
        try
        {
            return cm.SearchByFamilyName(familyname);
        }
        catch (NotExistingCustomerException)
        {
            return null;
        }
    }

    public static Customer SearchCostumer_ByPhone(string phone)
    {
        try
        {
            return cm.SearchByFamilyName(phone);
        }
        catch (NotExistingCustomerException)
        {
            return null;
        }
    }

    public static Customer SearchCostumer_ByMail(string mail)
    {
        try
        {
            return cm.SearchByFamilyName(mail);
        }
        catch (NotExistingCustomerException)
        {
            return null;
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
    public static string AddTag_Customer(string customerName, string tagId, string tagName, string tagDescription)
    {
        Customer customer = cm.SearchByName(customerName);

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
                return $"El cliente '{customerName}' ya tiene una etiqueta llamada '{tagName}'.";
            }

            Tag tag = new Tag(tagId, tagName, tagDescription);
            customer.AddTag(tag);

            return $"Etiqueta '{tagName}' agregada al cliente '{customer.Name}'.";
        }
        else
        {
            return $"El cliente '{customerName}' no se ha encontrado.";
        }
    }
    
    // Ver panel con: clientes totales, interacciones recientes, reuniones próximas 
    public static string ShowDashboard()
    {
        DashboardSummary dashboard = cm.GetDashboard();
        string result = "";

        result += "----- PANEL DE CLIENTES -----\n";
        result += $"Clientes totales: {dashboard.TotalCustomers}\n\n";

        result += "----- INTERACCIONES RECIENTES -----\n";
        if (dashboard.RecentInteractions != null && dashboard.RecentInteractions.Count > 0)
        {
            foreach (Customer customer in dashboard.RecentInteractions)
            {
                result += $"- {customer.Name} ({customer.FamilyName})\n";
            }
        }
        else
        {
            result += "No hay interacciones recientes.\n";
        }

        result += "\n----- REUNIONES PRÓXIMAS -----\n";
        if (dashboard.UpcomingMeetings != null && dashboard.UpcomingMeetings.Count > 0)
        {
            foreach (Meeting meeting in dashboard.UpcomingMeetings)
            {
                string customerName = meeting.Customer != null ? meeting.Customer.Name : "Cliente desconocido";
                result += $"- Reunión con {customerName} el {meeting.Date:dd/MM/yyyy} en {meeting.Place}\n";
            }
        }
        else
        {
            result += "No hay reuniones próximas.\n";
        }

        return result;
    }
    
public class Admin_functions
{
    public static CustomerManager cm = new CustomerManager();
    public static SellerManager sm = new SellerManager();
    private static List<User> users = new List<User>();

    // Como administrador quiero crear un nuevo usuario
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

}

}
=======
    //Como usuario quiero registrar llamadas enviadas o recibidas de clientes, incluyendo
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
    }
    
    //Como usuario quiero agregar notas o comentarios a las llamadas, reuniones, mensajes y correos enviados o recibidos
    //de los clientes, para tener información adicional de mis interacciones con los clientes.
    public static void Notes(string topic, DateTime date,ExchangeType type)
    {
        
        Note note = new Note(topic, date, type); //REVISARRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
        
        
    }
    
    //Como usuario quiero ver todas las interacciones de un cliente, con o sin filtro por tipo de interacción y por fecha,
    //para entender el historial de la relación comercial.
    
                ///VERLO JUNTOSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS

    //Como usuario quiero poder definir etiquetas para poder organizar y segmentar a mis clientes.
    public static void CustomerTag(string id, string name, string description)
    {
        Tag tag = new Tag(id, name, description);
    }
    
    //Cómo usuario quiero saber los clientes que hace cierto tiempo que no tengo ninguna interacción con ellos, para no
    //peder contacto con ellos.

    public static Interaction LastInteraction(Customer customer) //CLAUDIO
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

    public static string UnansweredInteractions(Customer customer)   //CLAUDIO
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
}


