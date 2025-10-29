namespace Library;

public class Facade

{
    public static CustomerManager cm = new CustomerManager();
    public static SellerManager sm = new SellerManager();
    
    //Como usuario quiero crear un nuevo cliente con su información básica: nombre, apellido,
    //teléfono y correo electrónico, para poder contactarme con ellos cuando lo necesite.
    public static void CreateCustomer(string id, string name, string familyName, string mail, string phone, string gender, DateTime birthDate)
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
}