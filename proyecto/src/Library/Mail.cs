namespace Library;

public class Mail : Interaction
{
    public Mail(DateTime date, string topic, ExchangeType type)
        : base(date, topic, type)
    {
        this.Note = new List<Note>();
    }
}
