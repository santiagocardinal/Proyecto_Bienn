namespace Library;

public class Call : Interaction
{
    public Call(DateTime date, string topic, ExchangeType type)
        : base(date, topic, type)
    {
        this.Note = new List<Note>();
    }
}
