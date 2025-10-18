namespace Program;

public class Seller
{
    private List<Client> clients;
    private List<Interaction> interactions;

    public List<Client> Clients
    {
        get { return clients; }
        set { clients = value; }
    }

    public List<Interaction> Interactions
    {
        get { return interactions; }
        set { interactions = value; }
    }

    public Seller(List<client> clients, List<interacton> interactions)
    {
        this.clients = clients;
        this.interactions = interactions;
    }
    
    public void addInteraction(Interaction interaction)
    {
        if (interaction != null)
        {
            interactions.Add(interaction);
        }
    }
}
