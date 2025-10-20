namespace Library.Tests;

public class MessageTests
{
    private Customer customer;
    private DateTime testDate;

    [SetUp]
    public void Setup()
    {
        testDate = new DateTime(2025, 10, 19, 14, 30, 0);
        customer = new Customer("12345678", "Juan", "Pérez", "juan@email.com", "099123456", "Masculino", new DateTime(1990, 5, 15));
    }

    // ============================================
    // PRUEBAS DEL CONSTRUCTOR
    // ============================================

    [Test]
    public void Constructor_WithValidParameters_CreatesMessageSuccessfully()
    {
        // Arrange
        string expectedTopic = "Consulta sobre facturación";
        ExchangeType expectedType = ExchangeType.Sent;

        // Act
        Message message = new Message(testDate, expectedTopic, expectedType, customer);

        // Assert
        Assert.That(message.Date, Is.EqualTo(testDate));
        Assert.That(message.Topic, Is.EqualTo(expectedTopic));
        Assert.That(message.Type, Is.EqualTo(expectedType));
        Assert.That(message.Customer, Is.EqualTo(customer));
        Assert.That(message.Note, Is.Not.Null);
        Assert.That(message.Note.Count, Is.EqualTo(0));
    }

    [Test]
    public void Constructor_InitializesEmptyNoteList()
    {
        // Act
        Message message = new Message(testDate, "Mensaje de seguimiento", ExchangeType.Received, customer);

        // Assert
        Assert.That(message.Note, Is.Not.Null);
        Assert.That(message.Note, Is.Empty);
    }
    
    [Test]
    public void Message_InheritsFromInteraction()
    {
        // Act
        Message message = new Message(testDate, "Mensaje", ExchangeType.Sent, customer);

        // Assert
        Assert.That(message, Is.InstanceOf<Interaction>());
    }

    [Test]
    public void Message_CanBeStoredAsInteraction()
    {
        // Arrange
        Message message = new Message(testDate, "Mensaje", ExchangeType.Sent, customer);

        // Act
        Interaction interaction = message;

        // Assert
        Assert.That(interaction, Is.Not.Null);
        Assert.That(interaction, Is.InstanceOf<Message>());
    }

    // ============================================
    // PRUEBAS CON DIFERENTES EXCHANGETYPE
    // ============================================

    [Test]
    public void Constructor_WithEmailType_CreatesSuccessfully()
    {
        // Act
        Message message = new Message(testDate, "Email corporativo", ExchangeType.Received, customer);

        // Assert
        Assert.That(message.Type, Is.EqualTo(ExchangeType.Received));
    }

    [Test]
    public void Constructor_WithWhatsAppType_CreatesSuccessfully()
    {
        // Act
        Message message = new Message(testDate, "Mensaje WhatsApp", ExchangeType.Sent, customer);

        // Assert
        Assert.That(message.Type, Is.EqualTo(ExchangeType.Sent));
    }

    [Test]
    public void Constructor_WithChatType_CreatesSuccessfully()
    {
        // Act
        Message message = new Message(testDate, "Chat en vivo", ExchangeType.Sent, customer);

        // Assert
        Assert.That(message.Type, Is.EqualTo(ExchangeType.Sent));
    }
    
    [Test]
    public void Note_AddNote_AddsSuccessfully()
    {
        // Arrange
        Message message = new Message(testDate, "Mensaje", ExchangeType.Received, customer);
        Note note = new Note();
        note.Topic = "Respuesta enviada";
        note.Date = new DateTime(2025,10,20);

        // Act
        message.Note.Add(note);

        // Assert
        Assert.That(message.Note.Count, Is.EqualTo(1));
        Assert.That(message.Note[0], Is.EqualTo(note));
    }

    [Test]
    public void Note_AddMultipleNotes_AddsAllSuccessfully()
    {
        // Arrange
        Message message = new Message(testDate, "Mensaje", ExchangeType.Received, customer);
        Note note1 = new Note();
        note1.Topic = "Nota 1";
        Note note2 = new Note();
        note2.Topic = "Nota 2";
        Note note3 = new Note();
        note3.Topic = "Nota 3";

        // Act
        message.Note.Add(note1);
        message.Note.Add(note2);
        message.Note.Add(note3);

        // Assert
        Assert.That(message.Note.Count, Is.EqualTo(3));
        Assert.That(message.Note, Contains.Item(note1));
        Assert.That(message.Note, Contains.Item(note2));
        Assert.That(message.Note, Contains.Item(note3));
    }

    [Test]
    public void Note_RemoveNote_RemovesSuccessfully()
    {
        // Arrange
        Message message = new Message(testDate, "Mensaje", ExchangeType.Received, customer);
        Note note = new Note();
        note.Topic = "Nota a eliminar";
        message.Note.Add(note);

        // Act
        message.Note.Remove(note);

        // Assert
        Assert.That(message.Note.Count, Is.EqualTo(0));
    }

  

    [Test]
    public void Constructor_WithDifferentCustomers_CreatesIndependentMessages()
    {
        // Arrange
        Customer customer2 = new Customer("87654321", "María", "García", "maria@email.com", "099888999", "Femenino", new DateTime(1985, 3, 20));

        // Act
        Message message1 = new Message(testDate, "Mensaje Cliente 1", ExchangeType.Received, customer);
        Message message2 = new Message(testDate, "Mensaje Cliente 2", ExchangeType.Sent, customer2);

        // Assert
        Assert.That(message1.Customer, Is.Not.EqualTo(message2.Customer));
        Assert.That(message1.Customer.Name, Is.EqualTo("Juan"));
        Assert.That(message2.Customer.Name, Is.EqualTo("María"));
    }
    

    [Test]
    public void Constructor_WithNullTopic_CreatesMessage()
    {
        // Act
        Message message = new Message(testDate, null, ExchangeType.Sent, customer);

        // Assert
        Assert.That(message.Topic, Is.Null);
        Assert.That(message.Note, Is.Not.Null);
    }

    [Test]
    public void Constructor_WithNullCustomer_CreatesMessage()
    {
        // Act
        Message message = new Message(testDate, "Mensaje", ExchangeType.Sent, null);

        // Assert
        Assert.That(message.Customer, Is.Null);
        Assert.That(message.Note, Is.Not.Null);
    }


    [Test]
    public void Message_InheritedProperties_CanBeModified()
    {
        // Arrange
        Message message = new Message(testDate, "Mensaje inicial", ExchangeType.Sent, customer);

        // Act
        message.Topic = "Mensaje modificado";
        message.Type = ExchangeType.Received;

        // Assert
        Assert.That(message.Topic, Is.EqualTo("Mensaje modificado"));
        Assert.That(message.Type, Is.EqualTo(ExchangeType.Received));
    }

}