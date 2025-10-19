namespace Library;

public class Interaction 
{
    private DateTime date;
    private string topic;
    private ExchangeType type;
    private List<Note> note;
    private Customer _customer;

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public string Topic
    {
        get { return topic; }
        set { topic = value; }
    }

    public ExchangeType Type
    {
        get { return type; }
        set { type = value; }
    }

    public List<Note> Note
    {
        get { return note; }
        set { note = value; }
    }

    public Customer Customer
    {
        get { return _customer; }
        set { _customer = value; }
    }

    public Interaction(DateTime date, string topic, ExchangeType type, Customer _customer)
    {
        this.Date = date;
        this.Topic = topic;
        this.Type = type;
        this.Note = new List<Note>();
        this.Customer = _customer;
    }

    public void AddNote(Note note)
    {
        this.Note.Add(note);
    }
    
    public override string ToString()
    {
        return $"Date: {this.date}, Topic: {this.Topic}, Type: {this.Type}";
    }
    
}