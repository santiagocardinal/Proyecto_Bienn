namespace Library.Tests;

public class MailTests
{
    private Customer customer;
    private DateTime testDate;

    [SetUp]
    public void Setup()
    {
        testDate = new DateTime(2025, 10, 19, 10, 0, 0);
        customer = new Customer("12345678", "Juan", "Pérez", "juan@email.com", "099123456", "Masculino", new DateTime(1990, 5, 15));
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesMailSuccessfully()
    {
        // Arrange
        string expectedTopic = "Propuesta comercial";
        ExchangeType expectedType = ExchangeType.Sent;

        // Act
        Mail mail = new Mail(testDate, expectedTopic, expectedType, customer);

        // Assert
        Assert.That(mail.Date, Is.EqualTo(testDate));
        Assert.That(mail.Topic, Is.EqualTo(expectedTopic));
        Assert.That(mail.Type, Is.EqualTo(expectedType));
        Assert.That(mail.Customer, Is.EqualTo(customer));
        Assert.That(mail.Note, Is.Not.Null);
        Assert.That(mail.Note.Count, Is.EqualTo(0));
    }

    [Test]
    public void Constructor_InitializesEmptyNoteList()
    {
        // Act
        Mail mail = new Mail(testDate, "Correo de bienvenida", ExchangeType.Received, customer);

        // Assert
        Assert.That(mail.Note, Is.Not.Null);
        Assert.That(mail.Note, Is.Empty);
    }
    

    [Test]
    public void Mail_InheritsFromInteraction()
    {
        // Act
        Mail mail = new Mail(testDate, "Correo", ExchangeType.Received, customer);

        // Assert
        Assert.That(mail, Is.InstanceOf<Interaction>());
    }

    [Test]
    public void Mail_CanBeStoredAsInteraction()
    {
        // Arrange
        Mail mail = new Mail(testDate, "Correo", ExchangeType.Received, customer);

        // Act
        Interaction interaction = mail;

        // Assert
        Assert.That(interaction, Is.Not.Null);
        Assert.That(interaction, Is.InstanceOf<Mail>());
    }
    

    [Test]
    public void Constructor_WithEmailType_CreatesSuccessfully()
    {
        // Act
        Mail mail = new Mail(testDate, "Correo electrónico", ExchangeType.Sent, customer);

        // Assert
        Assert.That(mail.Type, Is.EqualTo(ExchangeType.Sent));
    }
    

    [Test]
    public void Note_AddNote_AddsSuccessfully()
    {
        
        Mail mail = new Mail(testDate, "Correo", ExchangeType.Sent, customer);
        Note note = new Note("Compra",new DateTime(2025, 12, 25),ExchangeType.Received);
        note.Topic = "Mail enviado al cliente";
        note.Date = new DateTime(2025,10,20);

        mail.Note.Add(note);

        Assert.That(mail.Note.Count, Is.EqualTo(1));
        Assert.That(mail.Note[0], Is.EqualTo(note));
    }

    [Test]
    public void Note_AddMultipleNotes_AddsAllSuccessfully()
    {
        // Arrange
        Mail mail = new Mail(testDate, "Correo", ExchangeType.Sent, customer);
        Note note1 = new Note("Compra",new DateTime(2025, 12, 25),ExchangeType.Received);
        note1.Topic = "Nota 1";
        Note note2 = new Note("Venta",new DateTime(2025, 12, 25),ExchangeType.Received);
        note2.Topic = "Nota 2";

        // Act
        mail.Note.Add(note1);
        mail.Note.Add(note2);

        // Assert
        Assert.That(mail.Note.Count, Is.EqualTo(2));
        Assert.That(mail.Note, Contains.Item(note1));
        Assert.That(mail.Note, Contains.Item(note2));
    }
    

    [Test]
    public void Constructor_WithNullTopic_CreatesMail()
    {
        // Act
        Mail mail = new Mail(testDate, null, ExchangeType.Received, customer);

        // Assert
        Assert.That(mail.Topic, Is.Null);
        Assert.That(mail.Note, Is.Not.Null);
    }

    [Test]
    public void Constructor_WithNullCustomer_CreatesMail()
    {
        // Act
        Mail mail = new Mail(testDate, "Correo", ExchangeType.Received, null);

        // Assert
        Assert.That(mail.Customer, Is.Null);
        Assert.That(mail.Note, Is.Not.Null);
    }
    

    [Test]
    public void Mail_IsDifferentFromCallAndMessage()
    {
        // Arrange
        Mail mail = new Mail(testDate, "Correo", ExchangeType.Sent, customer);
        Call call = new Call(testDate, "Llamada", ExchangeType.Received, customer);
        Message message = new Message(testDate, "Mensaje", ExchangeType.Sent, customer);

        // Assert
        Assert.That(mail, Is.Not.InstanceOf<Call>());
        Assert.That(mail, Is.Not.InstanceOf<Message>());
        Assert.That(mail, Is.InstanceOf<Interaction>());
        Assert.That(call, Is.InstanceOf<Interaction>());
        Assert.That(message, Is.InstanceOf<Interaction>());
    }
}