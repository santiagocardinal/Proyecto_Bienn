using System.Data.Common;

namespace Library;

// POLIMORFISMO: Quote hereda de Interaction, lo que permite que sea
// usada polimórficamente donde se espere una Interaction.
// Quote representa un tipo específico de interacción: cotización o presupuesto.
//
// LSP: Quote puede sustituir a Interaction en cualquier contexto
// sin romper la funcionalidad del programa. Extiende la funcionalidad
// agregando información específica de cotizaciones (monto y descripción)
// sin alterar el comportamiento heredado.
//
// SRP: Quote tiene la responsabilidad única de representar
// una interacción de tipo cotización, incluyendo información específica
// como el monto y la descripción detallada de lo cotizado.

public class Quote : Interaction
{
    private double amount;      
    private string description; 
    
    public double Amount { get { return amount; } set { amount = value; } }
    public string Description { get { return description; } set { description = value; } }

   
    public Quote(DateTime date, string topic, ExchangeType type, Customer _customer, 
                 double amount, string description) 
        : base(date, topic, type, _customer)
    {
        // Solo inicializa lo específico de Quote
        this.Amount = amount;
        this.Description = description;
    }
    
    public override string ToString()
    {
        return $"Amount: {this.amount}, Description: {this.description}.";
    }
}