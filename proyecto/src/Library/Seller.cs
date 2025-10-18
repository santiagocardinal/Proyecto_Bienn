using Library;

namespace Library;

public class Seller
{
    private List<Customer> customer;
    private List<Interaction> interactions;

    public List<Customer> Custumer
    {
        get { return customer; }
        set { customer = value; }
    }

    public List<Interaction> Interactions
    {
        get { return interactions; }
        set { interactions = value; }
    }

    public Seller(List<Customer> clients, List<Interaction> interactions)
    {
        this.Custumer = clients;
        this.Interactions = interactions;
    }
    
    public void addInteraction(Interaction interaction)
    {
        if (interaction != null)
        {
            this.Interactions.Add(interaction);
        }
    }
}
