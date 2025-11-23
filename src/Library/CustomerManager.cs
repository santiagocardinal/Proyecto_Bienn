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

//using System.Linq;

/// <summary>
/// Administra la colección global de clientes del sistema.
/// Provee métodos para CRUD, búsquedas, registro de interacciones
/// y generación de reportes.
/// </summary>
public class CustomerManager
{
    private List<Customer> customers;

    public List<Customer> Customers
    {
        get { return customers; }
        private set { customers = value; }
    }

    public CustomerManager()
    {
        customers = new List<Customer>();
    }

    // ---------------- BÚSQUEDAS ----------------

    public Customer SearchByName(string name)
    {
        Customer c = customers.FirstOrDefault(cu =>
            cu.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (c == null)
            throw new Exceptions.NotExistingCustomerException();

        return c;
    }

    public Customer SearchByMail(string mail)
    {
        Customer c = customers.FirstOrDefault(cu =>
            cu.Mail.Equals(mail, StringComparison.OrdinalIgnoreCase));

        if (c == null)
            throw new Exceptions.NotExistingCustomerException();

        return c;
    }

    /*public Customer SearchByFamilyName(string familyname)
    {
        Customer c = customers.FirstOrDefault(cu =>
            cu.FamilyName.Equals(familyname, StringComparison.OrdinalIgnoreCase));

        if (c == null)
            throw new Exceptions.NotExistingCustomerException();

        return c;
    }*/
    
    public List<Customer> SearchByFamilyName(string familyname)
    {
        if (string.IsNullOrWhiteSpace(familyname))
            throw new Exceptions.InvalidFieldException("El apellido no puede estar vacío.");

        // normalizamos
        string normalized = familyname.Trim().ToLower();

        var list = customers
            .Where(c => c.FamilyName.Trim().ToLower() == normalized)
            .ToList();

        if (list.Count == 0)
            throw new Exceptions.NotExistingCustomerException();

        return list;
    }


    public Customer SearchByPhone(string phone)
    {
        Customer c = customers.FirstOrDefault(cu =>
            cu.Phone.Equals(phone));

        if (c == null)
            throw new Exceptions.NotExistingCustomerException();

        return c;
    }
    
    public Customer SearchById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("El ID no puede estar vacío.");

        Customer? c = customers.FirstOrDefault(cu =>
            cu.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (c == null)
            throw new Exceptions.NotExistingCustomerException();

        return c;
    }

    // ---------------- CRUD DE CUSTOMER ----------------

    public void AddCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        if (customers.Any(c => c.Id == customer.Id))
            throw new Exceptions.DuplicatedCustomerException(customer);

        customers.Add(customer);
    }

    public void Delete(Customer customer)
    {
        if (customer == null || !customers.Contains(customer))
            throw new Exceptions.NotExistingCustomerException();

        customers.Remove(customer);
    }

    /// <summary>
    /// Modifica un campo específico de un cliente según su ID.
    /// Soporta cambios en nombre, apellido, mail, teléfono, género o fecha de nacimiento.
    /// </summary>
    /// <param name="id">Identificador del cliente a modificar.</param>
    /// <param name="field">Nombre del campo a cambiar (en minúsculas).</param>
    /// <param name="newValue">Nuevo valor para el campo.</param>

    public void Modify(string id, string field, string newValue)
    {
        Customer existing = SearchById(id);

        if (existing == null)
            throw new Exceptions.NotExistingCustomerException();

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
                throw new Exceptions.InvalidFieldException(field);
        }
    }

    // ---------------- INTERACTIONS ----------------

    /// <summary>
    /// Registra una interacción entre un cliente y un vendedor.
    /// Actualiza el estado del cliente según el tipo de intercambio
    /// (Recibido o Enviado) y asocia la interacción a ambos.
    /// </summary>
    public void RegisterInteraction(Customer customer, Seller seller, Interaction interaction)
    {
        if (customer == null)
            throw new Exceptions.NotExistingCustomerException();

        if (seller == null)
            throw new Exceptions.SellerNotFoundException("null");

        if (interaction == null)
            throw new ArgumentNullException(nameof(interaction));

        if (interaction.Type == ExchangeType.Received)
        {
            customer.MarkAsPending();
        }
        else if (interaction.Type == ExchangeType.Sent)
        {
            customer.MarkAsActive();
            customer.MarkLastReceivedAsResponded();
        }

        seller.AddInteraction(interaction);
        customer.AddInteraction(interaction);
    }

    // ---------------- DASHBOARD ----------------

    private int GetTotalCustomer() => customers.Count;

    private List<Customer> GetRecentInteraction(TimeSpan lapso)
    {
        DateTime now = DateTime.Now;

        return customers
            .Where(c => c.GetLastInteraction() < now - lapso)
            .ToList();
    }

    private List<Meeting> GetUpcomingMeetings(Customer customer)
    {
        DateTime now = DateTime.Now;

        return customer.Interactions
            .OfType<Meeting>()
            .Where(m => m.Date > now)
            .ToList();
    }

    public DashboardSummary GetDashboard()
    {
        TimeSpan lapse = TimeSpan.FromDays(7);

        var total = GetTotalCustomer();
        var recent = GetRecentInteraction(lapse);

        List<Meeting> upcoming = new List<Meeting>();
        foreach (var c in customers)
            upcoming.AddRange(GetUpcomingMeetings(c));

        return new DashboardSummary(recent, upcoming, total);
    }

    // ---------------- CLIENTES INACTIVOS ----------------

    public List<Customer> GetInactiveCustomers(int days)
    {
        List<Customer> inactive = new List<Customer>();
        TimeSpan lapso = TimeSpan.FromDays(days);
        DateTime now = DateTime.Now;

        foreach (var customer in customers)
        {
            if (!customer.CheckIsActive())
            {
                inactive.Add(customer);
            }
            else if (customer.GetLastInteraction() < now - lapso)
            {
                customer.Deactivate();
                inactive.Add(customer);
            }
        }

        return inactive;
    }

    // ---------------- SIN RESPUESTA ----------------

    public List<Customer> GetUnansweredCustomers(int days)
    {
        DateTime threshold = DateTime.Now.AddDays(-days);

        return customers
            .Where(customer =>
                customer.Interactions.Any(interaction =>
                    interaction.Type == ExchangeType.Received &&
                    !interaction.HasResponse &&
                    interaction.Date >= threshold))
            .ToList();
    }

    // ---------------- ASIGNACIÓN ----------------

    public void AssignCustomerToSeller(Customer customer, Seller seller)
    {
        if (customer == null)
            throw new Exceptions.NotExistingCustomerException();

        if (seller == null)
            throw new Exceptions.SellerNotFoundException("null");

        seller.Customer.Add(customer);
    }

    public List<Interaction> GetCustomerInteractions(Customer customer)
    {
        if (customer == null)
            throw new Exceptions.NotExistingCustomerException();

        return customer.GetInteraction();
    }
}