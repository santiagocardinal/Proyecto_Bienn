namespace Library;

public class Meeting : Interaction
{
    private string place;

    public string Place { get { return place; } set { place = value; } }

    public Meeting(string place, DateTime date, string topic, ExchangeType type) : base(date, topic, type)
    {
        this.Place = place;
    }
    
    public void Sale(string place, DateTime date, string topic)
    {
        this.Place = place;
        this.Date = date;
        this.Topic = topic;
    }
}