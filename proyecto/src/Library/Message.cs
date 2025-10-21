namespace Library;
// POLIMORFISMO: Message hereda de Interaction, lo que permite que sea
// usada polimórficamente donde se espere una Interaction.
// Message representa un tipo específico de interacción: mensajes instantáneos
// (WhatsApp, SMS, chat en línea, etc.).
//
// LSP: Message puede sustituir a Interaction en cualquier contexto
// sin romper la funcionalidad del programa. Respeta completamente el contrato
// de la clase base y mantiene todas sus invariantes.
//
// SRP: Message tiene la responsabilidad única de representar
// una interacción de tipo mensaje.
// Si en el futuro se necesitan características específicas de mensajería
// (plataforma, multimedia, estado de lectura, etc.), esta clase será el lugar correcto.
public class Message : Interaction
{
    public Message(DateTime date, string topic, ExchangeType type, Customer _customer)
        : base(date, topic, type, _customer)
    {
        
    }
}
