namespace Library;

public class Sale : Interaction
{
   private string product;
   private Quote amount;

   public string Product
   {
      get { return product; }
      set { product = value; }
   }

   public Quote Amount
   {
      get { return amount; }
      set { amount = value; }
   }

   public Sale(string id, string product, Quote amount, DateTime date, string topic, ExchangeType type, Customer _customer) 
      : base( date, topic, type, _customer)
   {
      this.product = product;
      this.Amount = amount;
   }
   
   // Sobrescribe ToString() de Interaction
   public override string ToString()
   {
      return $"Product: {Product}, Date: {Date}, Amount: {Amount.Amount}";
   }
   
}
