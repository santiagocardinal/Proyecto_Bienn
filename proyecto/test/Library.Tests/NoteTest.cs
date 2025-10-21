namespace Library.Tests;

public class NoteTests
{
    [Test] 
    public void Constructor_ShouldCreateNoteWithDefaultValues() 
    { 
        var note = new Note("Venta",new DateTime(2025,10,20),ExchangeType.Sent);
        
        Assert.That(note.Topic, Is.Not.Null); 
        Assert.That(note.Date, Is.EqualTo(new DateTime(2025,10,20))); 
        Assert.That(note.SentReceivedStatus, Is.EqualTo(ExchangeType.Sent));
    }

    [Test]
    public void Notes_SetTopicAndDate()
    {
        
        string expectedTopic = "Reunión importante";
        DateTime expectedDate = new DateTime(2025, 10, 20);
        
        var note = new Note(expectedTopic,expectedDate,ExchangeType.Sent);
        
        Assert.That(note.Topic, Is.EqualTo(expectedTopic));
        Assert.That(note.Date, Is.EqualTo(expectedDate));
    }

    public void Note_Topic()
    {
        string expectedTopic = "Nueva reunión";

        var note = new Note(expectedTopic,new DateTime(2025,10,20),ExchangeType.Received);
        
        Assert.That(note.Topic, Is.EqualTo(expectedTopic));
    }

    [Test]
    public void Note_Date()
    {
        var note = new Note("Compra",new DateTime(2025, 12, 25),ExchangeType.Received);

        DateTime expectedDate = note.Date;

        Assert.That(note.Date, Is.EqualTo(expectedDate));
    }
    [Test]
    public void Note_ToStringMethod()
    {
        var note = new Note("Reunión de equipo",new DateTime(2025, 10, 20), ExchangeType.Sent);
        note.Topic = "Reunión de equipo";
        note.Date = new DateTime(2025, 10, 20);
        note.SentReceivedStatus = ExchangeType.Sent;

        string result = note.ToString();

        string expected = "Topic: Reunión de equipo, Date: 20/10/2025 00:00:00, Sent/Received: Sent";
        Assert.That(result, Does.Contain("Topic: Reunión de equipo"));
        Assert.That(result, Does.Contain("Sent/Received: Sent"));
    }
}
