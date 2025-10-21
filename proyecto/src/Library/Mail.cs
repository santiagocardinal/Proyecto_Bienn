namespace Library;

// POLIMORFISMO: Mail hereda de Interaction, lo que permite que sea
// usada polimórficamente donde se espere una Interaction.
// Mail es un tipo específico de interacción.
//
// LSP: Mail puede sustituir a Interaction en cualquier contexto
// sin romper la funcionalidad del programa. Respeta el contrato
// de la clase base y no altera el comportamiento heredado.
//
// SRP: Mail mantiene la responsabilidad única de representar
// una interacción específica de tipo correo electrónico.
// Si en el futuro se necesitan funcionalidades específicas de emails
// (adjuntos, destinatarios CC/BCC, etc.), esta clase será el lugar correcto.

public class Mail : Interaction
{
    public Mail(DateTime date, string topic, ExchangeType type, Customer _customer)
        : base(date, topic, type, _customer)
    {
       
    }
}