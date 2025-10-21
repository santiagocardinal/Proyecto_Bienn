namespace Library;

// POLIMORFISMO: Call hereda de Interaction, lo que permite que sea
// usada polimórficamente donde se espere una Interaction.
// Call representa un tipo específico de interacción: llamada telefónica.
//
// LSP: Call puede sustituir a Interaction en cualquier contexto
// sin romper la funcionalidad del programa. Mantiene el contrato
// de la clase base y puede ser usada en cualquier lugar donde se
// espere una Interaction.
//
// SRP: Call tiene la responsabilidad única de representar
// una interacción de tipo llamada telefónica.
// Si en el futuro se necesitan características específicas de llamadas
// (duración, grabación, número marcado, etc.), esta clase será el lugar correcto.
public class Call : Interaction
{
    
    public Call(DateTime date, string topic, ExchangeType type, Customer _customer)
        : base(date, topic, type, _customer)
    {
        
    }
}