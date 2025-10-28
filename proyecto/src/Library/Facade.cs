namespace Library;

public class Facade

{
    public static CustomerManager cm = new CustomerManager();

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
}