namespace Library;

public class Sale : Interaction
{
   private string product;
   private string date;
   private Quote amount;

   public string Product
   {
      get { return product; }
      set { product = value; }
   }

   public string Date
   {
      get { return date; }
      set { date = value; }
   }

   public Quote Amount
   {
      get { return amount; }
      set { amount = value; }
   }

   public Sale(string id, Client client, string product, string date, decimal amount)
   {
      this.product = product;
      this.Date = date;
//      this.Amount = new Quote(id, client, date, amount, $"Quote for {product}");
   }
   /*
   // Método que devuelve el total de ventas en un rango de fechas
   public decimal getTotalSales(string startDate, string endDate)
   {
      // Esto es un ejemplo: normalmente usarías una lista de ventas o una base de datos
      DateTime start = DateTime.Parse(startDate);
      DateTime end = DateTime.Parse(endDate);

      if (DateTime.Parse(this.Date) >= start && DateTime.Parse(this.Date) <= end)
      {
         return Amount.Amount;
      }
      return 0;
   }

   // Método que devuelve las ventas de un cliente específico
   public List<Sale> getSalesByClient(string clientId)
   {
      // En una implementación real, este método buscaría en una colección global o base de datos.
      // Por ahora devuelve una lista vacía como estructura base.
      return new List<Sale>();
   }

   // Sobrescribe ToString() de Interaction
   public override string ToString()
   {
      return $"Product: {Product}, Date: {Date}, Amount: {Amount.Amount}";
   }
   */
}
