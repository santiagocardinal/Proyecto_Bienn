namespace Library;

public class Call : Interaction
{
    public Call(DateTime date, string topic, ExchangeType type, Customer _customer)
        : base(date, topic, type, _customer)
    {
        this.Note = new List<Note>();
    }
}
