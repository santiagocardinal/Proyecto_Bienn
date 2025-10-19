namespace Library.Tests;

public class NoteTests
{
    private Note note;

    [SetUp]
    public void Setup()
    {
        note = new Note();
    }
    
    [Test]
    public void Constructor()
    {
        // Arrange & Act
        Note newNote = new Note();

        // Assert
        Assert.That(newNote.Topic, Is.Null);
        Assert.That(newNote.Date, Is.Null);
        Assert.That(newNote.SentReceivedStatus, Is.EqualTo(SentReceived.Sent)); // Valor por defecto del enum
    }
    
    [Test]
    public void Topic_ValidValue()
    {
        // Arrange
        string expectedTopic = "Reunión con cliente";

        // Act
        note.Topic = expectedTopic;

        // Assert
        Assert.That(note.Topic, Is.EqualTo(expectedTopic));
    }

    [Test]
    public void Date_ValidValue()
    {
        // Arrange
        string expectedDate = "2025-10-19";

        // Act
        note.Date = expectedDate;

        // Assert
        Assert.That(note.Date, Is.EqualTo(expectedDate));
    }
    
    [Test]
    public void SentReceivedStatus_SetToReceived_UpdatesSuccessfully()
    {
        // Act
        note.SentReceivedStatus = SentReceived.Received;

        // Assert
        Assert.That(note.SentReceivedStatus, Is.EqualTo(SentReceived.Received));
    }
    
    [Test]
    public void Notes_WithValidParameters()
    {
        // Arrange
        string expectedTopic = "Seguimiento de venta";
        string expectedDate = "2025-10-20";

        // Act
        note.Notes(expectedTopic, expectedDate);

        // Assert
        Assert.That(note.Topic, Is.EqualTo(expectedTopic));
        Assert.That(note.Date, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Notes_WithNullParameters()
    {
        // Act
        note.Notes(null, null);

        // Assert
        Assert.That(note.Topic, Is.Null);
        Assert.That(note.Date, Is.Null);
    }

    [Test]
    public void Notes_CalledMultipleTimes()
    {
        // Act
        note.Notes("Primer tema", "2025-10-15");
        note.Notes("Segundo tema", "2025-10-16");
        note.Notes("Tercer tema", "2025-10-17");

        // Assert
        Assert.That(note.Topic, Is.EqualTo("Tercer tema"));
        Assert.That(note.Date, Is.EqualTo("2025-10-17"));
    }


    [Test]
    public void ToString_WithAllPropertiesSet_ReturnsFormattedString()
    {
        // Arrange
        note.Topic = "Reunión importante";
        note.Date = "2025-10-19";
        note.SentReceivedStatus = SentReceived.Sent;

        // Act
        string result = note.ToString();

        // Assert
        Assert.That(result, Does.Contain("Reunión importante"));
        Assert.That(result, Does.Contain("2025-10-19"));
        Assert.That(result, Does.Contain("Sent"));
    }

    [Test]
    public void ToString_WithReceivedStatus_IncludesReceived()
    {
        // Arrange
        note.Topic = "Mensaje recibido";
        note.Date = "2025-10-18";
        note.SentReceivedStatus = SentReceived.Received;

        // Act
        string result = note.ToString();

        // Assert
        Assert.That(result, Does.Contain("Received"));
    }

    [Test]
    public void ToString_WithNullValues_HandlesGracefully()
    {
        // Arrange
        note.Topic = null;
        note.Date = null;

        // Act
        string result = note.ToString();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Does.Contain("Topic:"));
        Assert.That(result, Does.Contain("Date:"));
    }
}