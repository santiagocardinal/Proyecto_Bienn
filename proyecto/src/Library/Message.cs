namespace Library;

public class Message : Interaction
{
    public Message(DateTime date, string topic, ExchangeType type)
        : base(date, topic, type)
    {
        this.Note = new List<Note>();
    }
}
