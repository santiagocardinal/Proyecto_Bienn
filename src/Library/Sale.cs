namespace Library;

// POLIMORFISMO: Sale hereda de Interaction, lo que permite que sea
// usada polimórficamente donde se espere una Interaction.
// Sale representa un tipo específico de interacción: una venta concretada.
//
// LSP: Sale puede sustituir a Interaction en cualquier contexto
// sin romper la funcionalidad del programa. Extiende la funcionalidad
// agregando información específica de ventas (producto y cotización asociada)
// sin alterar el comportamiento heredado.
//
// SRP: Sale tiene la responsabilidad única de representar
// una interacción de tipo venta, incluyendo información específica
// sobre el producto vendido y el monto (a través de Quote).

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

   
   public Sale(string product, Quote amount, DateTime date, string topic, 
               ExchangeType type, Customer _customer) 
      : base(date, topic, type, _customer)
   {
      this.product = product;

      this.Amount = amount;
   }
   
   public override string ToString()
   {
      return $"Product: {Product}, Date: {Date}, Amount: {Amount.Amount}";
   }
   
}