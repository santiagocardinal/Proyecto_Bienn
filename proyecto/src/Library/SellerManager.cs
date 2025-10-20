namespace Library;

public class SellerManager
{
    private List<Seller> sellers = new List<Seller>();

    public List<Seller> Sellers
    {
        get { return sellers; }
        set { sellers = value; }
    }

    public SellerManager()
    {
        this.Sellers = new List<Seller>();
    }

    public void CreateSeller(Seller seller)
    {
        this.Sellers.Add(seller);
    }

    public void DeleteSeller(Seller seller)
    {
        this.Sellers.Remove(seller);
    }

    public void SuspendSeller(Seller seller)
    {
        seller.IsSuspended = true;
    }
    public List<Interaction> GetPendingResponses(Seller seller)
    {
        List<Interaction> pending = new List<Interaction>();
    
        foreach (Interaction interaction in seller.Interactions)
        {
            if (interaction.Type == ExchangeType.Received && !interaction.HasResponse)
            {
                pending.Add(interaction);
            }
        }
    
        return pending;
    }
}