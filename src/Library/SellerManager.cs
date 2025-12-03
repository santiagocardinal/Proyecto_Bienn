namespace Library;

// SRP: SellerManager tiene una única responsabilidad: gestionar la colección
// de vendedores del sistema (crear, eliminar, suspender y consultar información).
// No se encarga de la lógica interna de Seller ni de otras entidades.
// EXPERT: SellerManager es el experto en gestionar la colección de vendedores
// y operaciones a nivel de múltiples sellers.
//using Library.Exceptions;
//using System.Linq;

/// <summary>
/// Administra la colección global de vendedores del sistema.
/// Se encarga de crear, eliminar, suspender y consultar vendedores,
/// además de gestionar sus interacciones pendientes.
/// </summary>
public class SellerManager
{
    private List<Seller> sellers = new List<Seller>();

    public List<Seller> Sellers
    {
        get { return sellers; }
        private set { sellers = value; }
    }

    public SellerManager()
    {
        this.Sellers = new List<Seller>();
    }
    
    
    // Crear Vendedor
    public void CreateSeller(Seller seller)
    {
        if (seller == null)
            throw new ArgumentNullException(nameof(seller));

        // Buscar directamente sin lanzar excepción
        if (sellers.Any(s => s.Id.Equals(seller.Id, StringComparison.OrdinalIgnoreCase)))
            throw new Exceptions.DuplicateSellerException(seller.Id);

        this.Sellers.Add(seller);
    }
    //Eliminar Vendedor
    public void DeleteSeller(Seller seller)
    {
        if (seller == null)
            throw new ArgumentNullException(nameof(seller));

        if (!this.Sellers.Contains(seller))
            throw new Exceptions.SellerNotFoundException(seller.Id);

        this.Sellers.Remove(seller);
    }

    // Suspender
    public void SuspendSeller(Seller seller)
    {
        if (seller == null)
            throw new ArgumentNullException(nameof(seller));

        if (!this.Sellers.Contains(seller))
            throw new Exceptions.SellerNotFoundException(seller.Id);

        seller.IsSuspended = true;
    }
    
    public void EnableSeller(Seller seller)
    {
        if (seller == null)
            throw new ArgumentNullException(nameof(seller));

        if (!this.Sellers.Contains(seller))
            throw new Exceptions.SellerNotFoundException(seller.Id);
            

        seller.IsSuspended = false;
    }

    /// <summary>
    /// Devuelve la lista de interacciones recibidas por el vendedor que aún no tienen respuesta.
    /// Lanza una excepción si el vendedor no existe.
    /// </summary>
    /// <param name="seller">Vendedor cuyas interacciones se analizarán.</param>
    /// <returns>Lista de interacciones pendientes de respuesta.</returns>

    public List<Interaction> GetPendingResponses(Seller seller)
    {
        if (seller == null)
            throw new ArgumentNullException(nameof(seller));

        if (!this.Sellers.Contains(seller))
            throw new Exceptions.SellerNotFoundException(seller.Id);

        return seller.Interactions
            .Where(i => i.Type == ExchangeType.Received && !i.HasResponse)
            .ToList();
    }

    
    public Seller SearchById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("El ID no puede estar vacío.");

        return sellers.FirstOrDefault(s => 
            s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }
    
    public void EnsureSellerIsActive(Seller seller)
    {
        if (seller == null)
            throw new Exceptions.SellerNullException();

        if (seller.IsSuspended)
            throw new Exceptions.SuspendedSellerException(seller.Id);
    }

    public Seller GetActiveSeller(string id)
    {
        Seller seller = SearchById(id);
        EnsureSellerIsActive(seller);
        return seller;
    }
    
    public List<Sale> GetAllSales()
    {
        return sellers
            .SelectMany(s => s.Interactions)
            .OfType<Sale>()
            .ToList();
    }
    
    public List<Sale> GetSalesBetween(DateTime start, DateTime end)
    {
        return GetAllSales()
            .Where(s => s.Date >= start && s.Date <= end)
            .ToList();
    }
    //---------------------------DEFENSA---------------------------------
    
    //Si o si debe de estar este metodo aqui debido a que SellerManager posee una lista de todos los Seller que hay.
    public string GetBestSellers()
    {
        if (sellers.Count == 0)
            return
                "No hay vendedores cargados, por lo tanto no se puede determinar quién vendió más."; //En caso de que no haya ningun vendedor registrado salta esto

        Seller vendedorConMasVentas = null;
        int mayorCantidad = 0;

        foreach (Seller s in sellers) //recorre la lista de los vendedores 
        {

            int ventasDelVendedor =
                0; //en cuanto vaya encontrando un Sale dentro de cada vendedor, lo que va a hacer es ir sumando 1.
            foreach (Interaction i in s.Interactions) //ventas del vendedor
            {
                if (i is Sale)
                {
                    ventasDelVendedor++;
                }
            }

            if (ventasDelVendedor >
                mayorCantidad) //aca pregundo si la cantidad de ventas del vendedor es mayor que la mayor cantidad, si ese es el caso lo que hace es traspasar el valor de ventas del vendedor a la de mayor cantidad
            {
                mayorCantidad = ventasDelVendedor;
                vendedorConMasVentas =
                    s; //aca dependiendo de quien sea el que tenga la mayor cantidad de ventas, se le va a asignar su nombre a vendedor con mas ventas.
            }
        }

        return vendedorConMasVentas.Name;
    }

}
