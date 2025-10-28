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
    
    
    
    
    
    
    
    
    
    
    //MI GENTE LATINO
}