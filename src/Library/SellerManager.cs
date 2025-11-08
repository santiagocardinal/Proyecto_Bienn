namespace Library;

// SRP: SellerManager tiene una única responsabilidad: gestionar la colección
// de vendedores del sistema (crear, eliminar, suspender y consultar información).
// No se encarga de la lógica interna de Seller ni de otras entidades.
// EXPERT: SellerManager es el experto en gestionar la colección de vendedores
// y operaciones a nivel de múltiples sellers.
//using Library.Exceptions;
//using System.Linq;

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

            if (SearchById(seller.Id) != null)
                throw new Exceptions.DuplicateSellerException(seller.Id);

            this.Sellers.Add(seller);
        }

        // Eliminar Vendedor
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

        // Interacciones sin responder
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

        // Búsqueda por ID
        public Seller SearchById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID no puede estar vacío.");

            return sellers
                .FirstOrDefault(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }
    }
