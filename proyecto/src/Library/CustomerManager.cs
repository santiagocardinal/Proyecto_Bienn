namespace Library;

public class CustomerManager
{
    private List<Customer> customers;
    
    public CustomerManager()
    {
        customers = new List<Customer>();
    }

    public Customer SearchByName(string name)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) //NO SEA SENSIBLE A LAS MAYUSCULAS Y MINUSCULAS EL StringComparison
            {
                return customer;
            }
        }
        return null;
    }
    
    public Customer SearchByMail(string mail)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Mail.Equals(mail, StringComparison.OrdinalIgnoreCase))
            {
                return customer;
            }
        }
        return null;
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
        return null;
    }
    public Customer SearchByPhone(string phone)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Phone.Equals(phone, StringComparison.OrdinalIgnoreCase))
            {
                return customer;
            }
        }
        return null;
    }

    public Customer SearchById(string id)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Id.Equals( id, StringComparison.OrdinalIgnoreCase))
            {
                return customer;
            }
        }
        return null;
    }

    public void AddCustomer(Customer customer, Seller seller)
    {
        if (customer != null && SearchById(customer.Id) == null)
        {
            
            // Agregamos el cliente a la lista del vendedor
            seller.Customer.Add(customer);

            // Agregamos el cliente a la lista global
            customers.Add(customer);

            Console.WriteLine($"Cliente {customer.Name} agregado exitosamente con vendedor {seller.Name}.");
        }
        else
        {
            Console.WriteLine("No se pudo agregar el cliente.");
        }
    }

    public bool Delete(Customer customer)
    {
        if (customer != null && customers.Contains(customer))
        {
            customers.Remove(customer);
            Console.WriteLine($"Cliente {customer.Name} eliminado.");
            return true;
        }
        Console.WriteLine("Cliente no encontrado.");
        return false;
    }

    public void Modify(Customer customer)
    {
        Customer existing = SearchById(customer.Id);
        if (existing != null)
        {
            int index = customers.IndexOf(existing);
            customers[index] = customer;
            Console.WriteLine($"Cliente {customer.Name} modificado exitosamente.");
        }
        else
        {
            Console.WriteLine("Cliente no encontrado.");
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

        // Buscar el cliente en la lista local (_customers)
        Customer existingCustomer = customers.FirstOrDefault(c => c.Id == customer.Id);

        // Si no existe, devolvemos una lista vacía
        if (existingCustomer == null)
        {
            return upcomingMeetings;
        }

        // Recorremos las interacciones del cliente encontrado
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

        // Definimos el lapso para "recientes", por ejemplo los últimos 7 días
        TimeSpan lapso = TimeSpan.FromDays(7);
        List<Customer> recentInteractions = GetRecentInteraction(lapso);

        // Obtenemos todas las reuniones próximas de todos los clientes
        List<Meeting> upcomingMeetings = new List<Meeting>();
        foreach (var customer in customers)
        {
            upcomingMeetings.AddRange(GetUpcomingMeetings(customer));
        }

        return new DashboardSummary(recentInteractions, upcomingMeetings, totalCustomers);
    }
/*
    public List<Customer> GetInactiveCustomers(int days)
    {
        List<Customer> inactiveCustomers = new List<Customer>();
        DateTime threshold = DateTime.Now.AddDays(-days);

        foreach (Customer customer in customers)
        {
            if (customer.LastInteractionDate < threshold)
            {
                inactiveCustomers.Add(customer);
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
            if (customer.HasPendingQuery && customer.LastQueryDate < threshold)
            {
                unansweredCustomers.Add(customer);
            }
        }
        return unansweredCustomers;
    }
*/
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

    public void AddInteraction(Interaction interaction,Seller seller)
    {
        if (interaction != null)
        {
            seller.addInteraction(interaction);
            Console.WriteLine("Interacción agregada exitosamente.");
        }
    }

    public List<Interaction> GetCustomerInteractions(Customer customer)
    {
        return customer.GetInteraction();
    }
}