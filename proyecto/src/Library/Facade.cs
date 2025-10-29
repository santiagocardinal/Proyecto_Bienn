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