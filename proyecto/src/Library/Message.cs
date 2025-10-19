namespace Library;

public class Message : Interaction
{
    public Message(DateTime date, string topic, ExchangeType type, Customer _customer)
        : base(date, topic, type, _customer)
    {
        this.Note = new List<Note>();
    }
}
