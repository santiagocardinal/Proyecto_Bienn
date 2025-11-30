namespace Library;

// POLIMORFISMO: Meeting hereda de Interaction, lo que permite que sea
// usada polimórficamente donde se espere una Interaction.
// Meeting representa un tipo específico de interacción: reunión presencial o virtual.
//
// LSP: Meeting puede sustituir a Interaction en cualquier contexto
// sin romper la funcionalidad del programa. Extiende la funcionalidad
// de Interaction agregando información específica (lugar) sin alterar
// el comportamiento heredado.
//
// SRP: Meeting tiene la responsabilidad única de representar
// una interacción de tipo reunión, incluyendo información específica
// como el lugar donde ocurre.

public class Meeting : Interaction
{
    private string place;

    public string Place { get { return place; } set { place = value; } }
    
    public Meeting(string place, DateTime date, string topic, ExchangeType type, Customer _customer) 
        : base(date, topic, type, _customer)
    {
        // Solo inicializamos lo específico de Meeting
        this.Place = place;
    }
}