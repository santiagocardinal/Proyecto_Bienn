namespace Library;

public class Interaction
{
    private DateTime date;
    private string topic;
    private ExchangeType type;
    private List<Note> note;

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

    public Interaction(DateTime date, string topic, ExchangeType type)
    {
        this.Date = date;
        this.Topic = topic;
        this.Type = type;
    }

    public void AddNote(Note note)
    {
        this.Note.Add(note);
    }
}