using Library;

namespace Library;

// LSP: Seller es un User y puede sustituirlo en cualquier contexto.
// SRP: Seller tiene la responsabilidad de gestionar las interacciones
// y relaciones comerciales de un vendedor.

/// <summary>
/// Representa a un vendedor del sistema.
/// Gestiona sus clientes, interacciones y ventas, además de su estado de suspensión.
/// </summary>
public class Seller : User
{
    private bool isSuspended;
    private List<Customer> customer;
    private List<Interaction> interactions;
    private int bonus;


    public int Bonus
    {
        get { return bonus; }
        set { bonus = value; }
    }
    
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

    // Patrón EXPERT: User es experto en inicializar datos de usuario,
    public Seller( string name, string mail, string phone, string id) : base (name,mail,phone,id)
    {
        this.Customer = new List<Customer> ();
        this.Interactions = new List<Interaction>();
    }
    
    public void AddInteraction(Interaction interaction)
    {
        if (interaction != null)
        {
            this.Interactions.Add(interaction);
        }
    }
    
    // Patrón EXPERT: Seller es el experto en conocer y filtrar sus propias ventas

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

    // Patrón EXPERT: Seller es el experto en consultar sus ventas por cliente

    public List<Sale> getSalesByCustomer(Customer _customer)
    {
        List<Sale> salesByClient = new List<Sale>();

        foreach (Interaction inter in interactions)
        {
            if (inter is Sale sale && sale.Customer == _customer)
            {
                salesByClient.Add(sale);
            }
        }

        return salesByClient;
    }
    
    // Patrón EXPERT: Seller es el experto en responder sus propias interacciones

    public void RespondToInteraction(Interaction interaction)
    {
        if (interaction != null && !interaction.HasResponse)
        {
            interaction.MarkAsResponded();
        }
    }

    //Metodo para obtener el numero total de ventas
    public int GetSalesAmount()
    {
        int cantSales = 0;
        
        foreach (var inter in interactions)
        {
            if (inter is Sale sale)
                cantSales++;
        }

        if (cantSales == 0)
            throw new Exception("No hay ventas registradas");
            
        return cantSales;
    }

    //Metodo para setear el valor total del bonus
    public void GetBonus(int bonus)
    {
        this.Bonus = bonus;
    }
}