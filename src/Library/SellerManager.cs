namespace Library;

// SRP: SellerManager tiene una única responsabilidad: gestionar la colección
// de vendedores del sistema (crear, eliminar, suspender y consultar información).
// No se encarga de la lógica interna de Seller ni de otras entidades.
// EXPERT: SellerManager es el experto en gestionar la colección de vendedores
// y operaciones a nivel de múltiples sellers.
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
    
    public Seller SearchById(string id)
    {
        foreach (Seller seller in sellers)
        {
            if (seller.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                return seller;
            }
        }
        return null;
    }
}