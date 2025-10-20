using Library;

namespace Library;

public class Seller : User
{
    private bool isSuspended;
    private List<Customer> customer;
    private List<Interaction> interactions;
    
    public bool IsSuspended
    {
        get { return isSuspended; }
        set { isSuspended = value; }
    }
    
    public List<Customer> Customer
    {
        get { return customer; }
        set { customer = value; }
    }

    public List<Interaction> Interactions
    {
        get { return interactions; }
        set { interactions = value; }
    }

    public Seller( string name, string mail, string phone, string id) : base (name,mail,phone,id)
    {
        this.Customer = new List<Customer> ();
        this.Interactions = new List<Interaction>();
    }
    
    public void addInteraction(Interaction interaction)
    {
        if (interaction != null)
        {
            this.Interactions.Add(interaction);
        }
    }
    
    // Método que devuelve el total de ventas en un rango de fechas
    public List<Sale> getTotalSales()
    {
        List<Sale> sales = new List<Sale>();
        foreach (Interaction inter in interactions)
        {
            if (inter is Sale sale)
            {
                sales.Add(sale);
                
            }
        }
        return sales;
    }

    // Método que devuelve las ventas de un cliente específico
    public List<Sale> getSalesByCustomer(Customer _customer)
    {
        List<Sale> salesByClient = new List<Sale>();

        foreach (Interaction inter in interactions)
        {
            // Verificamos que sea Sale y que pertenezca al cliente
            if (inter is Sale sale && sale.Customer == _customer)
            {
                salesByClient.Add(sale);
            }
        }

        return salesByClient;
    }
    
    public void RespondToInteraction(Interaction interaction)
    {
        if (interaction != null && !interaction.HasResponse)
        {
            interaction.MarkAsResponded();
        }
    }
}
