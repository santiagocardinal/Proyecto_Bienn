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
}