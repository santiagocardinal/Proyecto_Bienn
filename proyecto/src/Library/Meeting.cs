namespace Library;

public class Meeting : Interaction
{
    private string place;

    public string Place { get { return place; } set { place = value; } }

    public Meeting(string place, DateTime date, string topic, ExchangeType type, Customer _customer) : base(date, topic, type,_customer)
    {
        this.Place = place;
    }
}