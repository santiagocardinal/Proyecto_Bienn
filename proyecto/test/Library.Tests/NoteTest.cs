namespace Library.Tests;

public class NoteTests
{
    [Test] 
    public void Constructor_ShouldCreateNoteWithDefaultValues() 
    { 
        var note = new Note();
        
        Assert.That(note.Topic, Is.Null); 
        Assert.That(note.Date, Is.EqualTo(default(DateTime))); 
        Assert.That(note.SentReceivedStatus, Is.EqualTo(SentReceived.Sent));
    }

    [Test]
    public void Notes_SetTopicAndDate()
    {
        var note = new Note();
        string expectedTopic = "Reunión importante";
        DateTime expectedDate = new DateTime(2025, 10, 20);
        
        note.Notes(expectedTopic, expectedDate);

        Assert.That(note.Topic, Is.EqualTo(expectedTopic));
        Assert.That(note.Date, Is.EqualTo(expectedDate));
    }

    public void Note_Topic()
    {
        // Arrange
        var note = new Note();
        string expectedTopic = "Nueva reunión";

        // Act
        note.Topic = expectedTopic;

        // Assert
        Assert.That(note.Topic, Is.EqualTo(expectedTopic));
    }

    [Test]
    public void Note_Date()
    {
        // Arrange
        var note = new Note();
        DateTime expectedDate = new DateTime(2025, 12, 25);

        // Act
        note.Date = expectedDate;

        // Assert
        Assert.That(note.Date, Is.EqualTo(expectedDate));
    }
    [Test]
    public void Note_ToStringMethod()
    {
        // Arrange
        var note = new Note();
        note.Topic = "Reunión de equipo";
        note.Date = new DateTime(2025, 10, 20);
        note.SentReceivedStatus = SentReceived.Sent;

        // Act
        string result = note.ToString();

        // Assert
        string expected = "Topic: Reunión de equipo, Date: 20/10/2025 00:00:00, Sent/Received: Sent";
        Assert.That(result, Does.Contain("Topic: Reunión de equipo"));
        Assert.That(result, Does.Contain("Sent/Received: Sent"));
    }
}
