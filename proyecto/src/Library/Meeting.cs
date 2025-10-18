namespace Library;

public class Meeting : Interaction
{
    private string place;
    private string date;
    private string topic;

    public string Place { get { return place; } set { place = value; } }
    public string Date { get { return date; } set { date = value; } }
    public string Topic { get { return topic; } set { topic = value; } }

    public Meeting(string place, string date, string topic)
    {
        this.Place = place;
        this.Date = date;
        this.Topic = topic;
    }
    
    public void Sale(string place, string date, string topic)
    {
        this.Place = place;
        this.Date = date;
        this.Topic = topic;
    }
    
}