namespace Library;

public class Mail : Interaction
{
    public Mail(DateTime date, string topic, ExchangeType type, Customer _customer)
        : base(date, topic, type, _customer)
    {
        this.Note = new List<Note>();
    }
}
