using System.Linq.Expressions;

namespace Library;

// SRP: CustomerManager tiene la responsabilidad de gestionar la colección
// de clientes del sistema y coordinar operaciones de alto nivel sobre ellos.
// Actúa como un controlador/coordinador central para operaciones con clientes.
//
// EXPERT: CustomerManager es el experto en:
// - Gestionar la colección global de clientes
// - Buscar clientes por diferentes criterios
// - Coordinar operaciones entre clientes y vendedores
// - Generar reportes y análisis sobre clientes
public class CustomerManager
{
    
    private List<Customer> customers;
    public List<Customer> Customers
    {
        get{return customers;}
        set { customers = value; }
    }
   
    public CustomerManager()
    {
        customers = new List<Customer>();
    }
    
    public Customer SearchByName(string name)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return customer;
            }
        }

        throw new NotExistingCustomerException();
    }

    
    public Customer SearchByMail(string mail)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Name.Equals(mail, StringComparison.OrdinalIgnoreCase))
            {
                return customer;
            }
        }
        throw new NotExistingCustomerException();
    }

    public Customer SearchByFamilyName(string familyname)
    {
        foreach (Customer customer in customers)
        {
            if (customer.FamilyName.Equals(familyname, StringComparison.OrdinalIgnoreCase))
            {
                return customer;
            }
        }

        throw new NotExistingCustomerException();
    }
    
    public Customer SearchByPhone(string phone)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Phone.Equals(phone))
            {
                return customer;
            }
        }

        throw new NotExistingCustomerException();
    }

    public Customer SearchById(string id)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                return customer;
            }
        }
        return null;
    }

   
    public void AddCustomer(Customer customer)
    {
        // Verificar si ya existe
        if (this.Customers.Contains(customer))
        {
            throw new DuplicatedCustomerException(customer);
        }
        else
        {
            this.customers.Add(customer);
        }
    }
    
    public void Delete(Customer customer)
    {
        if (customer != null && customers.Contains(customer))
        {
            customers.Remove(customer);
        }
        else
        {
            throw new NotExistingCustomerException();
        }
    }

    
    public void Modify(string id, string field, string newValue)
    {
        Customer existing = SearchById(id);

        if (existing == null)
        {
            throw new NotExistingCustomerException();
        }
        else
        {
            switch (field.ToLower())
            {
                case "name":
                    existing.Name = newValue;
                    break;

                case "familyname":
                    existing.FamilyName = newValue;
                    break;

                case "mail":
                    existing.Mail = newValue;
                    break;

                case "phone":
                    existing.Phone = newValue;
                    break;

                case "gender":
                    existing.Gender = newValue;
                    break;

                case "birthdate":
                    if (DateTime.TryParse(newValue, out DateTime bd))
                        existing.BirthDate = bd;
                    break;

                default:
                    throw new InvalidFieldException(field.ToLower());
                    return;
            }
        }
    }

    
    private int GetTotalCustomer()
    {
        return customers.Count;
    }
    
    private List<Customer> GetRecentInteraction(TimeSpan lapso)
    {
        List<Customer> list = new List<Customer>();
        DateTime ahora = DateTime.Now;

        foreach (Customer customer in this.customers)
        {
            DateTime lastinteraction = customer.GetLastInteraction();

            if (lastinteraction < ahora - lapso)
            {
                list.Add(customer);
            }
        }

        return list;
    }
    
  
    private List<Meeting> GetUpcomingMeetings(Customer customer)
    {
        DateTime now = DateTime.Now;
        List<Meeting> upcomingMeetings = new List<Meeting>();

        Customer existingCustomer = customers.FirstOrDefault(c => c.Id == customer.Id);

        if (existingCustomer == null)
        {
            return upcomingMeetings;
        }


        foreach (var inter in existingCustomer.Interactions)
        {
 
            if (inter is Meeting meeting && meeting.Date > now)
            {
                upcomingMeetings.Add(meeting);
            }
        }

        return upcomingMeetings;
    }
    
 
    public DashboardSummary GetDashboard()
    {
        int totalCustomers = GetTotalCustomer();

        TimeSpan lapso = TimeSpan.FromDays(7);
        List<Customer> recentInteractions = GetRecentInteraction(lapso);

        List<Meeting> upcomingMeetings = new List<Meeting>();
        foreach (var customer in customers)
        {
    
            upcomingMeetings.AddRange(GetUpcomingMeetings(customer));
        }

        return new DashboardSummary(recentInteractions, upcomingMeetings, totalCustomers);
    }

 
    public List<Customer> GetInactiveCustomers(int days)
    {
        TimeSpan lapso = TimeSpan.FromDays(days);
        List<Customer> inactiveCustomers = new List<Customer>();
        DateTime now = DateTime.Now;

        foreach (var customer in this.customers) 
        {
            if (!customer.CheckIsActive()) 
            {
                inactiveCustomers.Add(customer);
            }
            else
            {
                DateTime lastInteraction = customer.GetLastInteraction();
                if (lastInteraction < now - lapso)
                {
                    customer.Deactivate(); 
                    inactiveCustomers.Add(customer);
                }
            }
        }

        return inactiveCustomers;
    }
 
    public List<Customer> GetUnansweredCustomers(int days)
    {
        List<Customer> unansweredCustomers = new List<Customer>();
        DateTime threshold = DateTime.Now.AddDays(-days);

        foreach (Customer customer in customers)
        {
            bool hasUnanswered = false;
        
          
            foreach (Interaction interaction in customer.Interactions)
            {
                
                if (interaction.Type == ExchangeType.Received && 
                    !interaction.HasResponse && 
                    interaction.Date >= threshold)
                {
                    hasUnanswered = true;
                    break;
                }
            }
        
            if (hasUnanswered)
            {
                unansweredCustomers.Add(customer);
            }
        }
    
        return unansweredCustomers;
    }

    
    public void AssignCustomerToSeller(Customer customer, Seller seller)
    {
        if (customer != null && seller != null)
        {
            seller.Customer.Add(customer);
            Console.WriteLine($"Cliente {customer.Name} asignado al vendedor {seller.Name}.");
        }
        else
        {
            Console.WriteLine("No se pudo asignar: cliente o vendedor nulo.");
        }
    }

   
    public void AddInteraction(Interaction interaction, Seller seller, Customer customer)
    {
        if (interaction != null)
        {
            seller.addInteraction(interaction);
            
            customer.AddInteraction(interaction);
            
            customer.Activate();
            
            Console.WriteLine("Interacción agregada exitosamente.");
        }
    }

   
    public List<Interaction> GetCustomerInteractions(Customer customer)
    {
        return customer.GetInteraction();
    }
}