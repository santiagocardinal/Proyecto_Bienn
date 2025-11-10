namespace Library;

public class InteractionRegular: Interaction
{
    public InteractionRegular(DateTime date, string topic, ExchangeType type, Customer customer)
        : base(date, topic, type, customer)
    {
    }
}
