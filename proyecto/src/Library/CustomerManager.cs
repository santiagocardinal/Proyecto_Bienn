namespace Library;

public class CustomerManager
{
    private List<Customer> customers;
    private List<Interaction> interactions;
    private List<Meeting> meetings;

    public CustomerManager()
    {
        customers = new List<Customer>();
        interactions = new List<Interaction>();
        meetings = new List<Meeting>();
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
        return null;
    }

    public Customer SearchByDate(string date)
    {
        foreach (Customer customer in customers)
        {
            if (customer.RegistrationDate == date)
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
            if (customer.Id == id)
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

    public Customer SearchByPhone(string phone)
    {
        foreach (Customer customer in customers)
        {
            if (customer.Phone == phone)
            {
                return customer;
            }
        }
        return null;
    }

    public void AddCustomer(Customer customer)
    {
        if (customer != null && SearchById(customer.Id) == null)
        {
            customers.Add(customer);
            Console.WriteLine($"Cliente {customer.Name} agregado exitosamente.");
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

    public bool Register(Customer customer)
    {
        if (customer != null && SearchById(customer.Id) == null)
        {
            customers.Add(customer);
            Console.WriteLine($"Cliente {customer.Name} registrado exitosamente.");
            return true;
        }
        Console.WriteLine("No se pudo registrar el cliente (ya existe o es nulo).");
        return false;
    }

    public int GetTotalCustomer()
    {
        return customers.Count;
    }

    public List<Interaction> GetRecentInteractions(int limit)
    {
        if (limit <= 0 || limit > interactions.Count)
        {
            return new List<Interaction>(interactions);
        }
        return interactions.GetRange(interactions.Count - limit, limit);
    }

    public List<Meeting> GetUpcomingMeeting()
    {
        List<Meeting> upcomingMeetings = new List<Meeting>();
        DateTime now = DateTime.Now;

        foreach (Meeting meeting in meetings)
        {
            if (meeting.MeetingDate > now)
            {
                upcomingMeetings.Add(meeting);
            }
        }
        return upcomingMeetings;
    }

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

    public void AssignCustomerToSeller(Customer customer, Seller seller)
    {
        if (customer != null && seller != null)
        {
            customer.AssignedSeller = seller;
            Console.WriteLine($"Cliente {customer.Name} asignado al vendedor {seller.Name}.");
        }
        else
        {
            Console.WriteLine("No se pudo asignar: cliente o vendedor nulo.");
        }
    }

    public void AddInteraction(Interaction interaction)
    {
        if (interaction != null)
        {
            interactions.Add(interaction);
            Console.WriteLine("Interacción agregada exitosamente.");
        }
    }
}