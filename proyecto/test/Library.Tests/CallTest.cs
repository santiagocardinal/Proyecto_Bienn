namespace Library.Tests;

public class CallTests
{
    private Customer customer;
    private DateTime testDate;

    [SetUp]
    public void Setup()
    {
        testDate = new DateTime(2025, 10, 19, 14, 30, 0);
        customer = new Customer("12345678", "Juan", "Pérez", "juan@email.com", "099123456", "Masculino", new DateTime(1990, 5, 15));
    }
    

    [Test]
    public void Constructor_WithValidParameters_CreatesCallSuccessfully()
    {
        // Arrange
        string expectedTopic = "Consulta sobre producto";
        ExchangeType expectedType = ExchangeType.Sent;

        // Act
        Call call = new Call(testDate, expectedTopic, expectedType, customer);

        // Assert
        Assert.That(call.Date, Is.EqualTo(testDate));
        Assert.That(call.Topic, Is.EqualTo(expectedTopic));
        Assert.That(call.Type, Is.EqualTo(expectedType));
        Assert.That(call.Customer, Is.EqualTo(customer));
        Assert.That(call.Note, Is.Not.Null);
        Assert.That(call.Note.Count, Is.EqualTo(0));
    }

    [Test]
    public void Constructor_InitializesEmptyNoteList()
    {
        // Act
        Call call = new Call(testDate, "Llamada de seguimiento", ExchangeType.Received, customer);

        // Assert
        Assert.That(call.Note, Is.Not.Null);
        Assert.That(call.Note, Is.Empty);
    }
    

    [Test]
    public void Call_InheritsFromInteraction()
    {
        // Act
        Call call = new Call(testDate, "Llamada", ExchangeType.Received, customer);

        // Assert
        Assert.That(call, Is.InstanceOf<Interaction>());
    }

    [Test]
    public void Call_CanBeStoredAsInteraction()
    {
        // Arrange
        Call call = new Call(testDate, "Llamada", ExchangeType.Sent, customer);

        // Act
        Interaction interaction = call;

        // Assert
        Assert.That(interaction, Is.Not.Null);
        Assert.That(interaction, Is.InstanceOf<Call>());
    }


    [Test]
    public void Constructor_WithPhoneType_CreatesSuccessfully()
    {
        // Act
        Call call = new Call(testDate, "Llamada telefónica", ExchangeType.Received, customer);

        // Assert
        Assert.That(call.Type, Is.EqualTo(ExchangeType.Received));
    }

    [Test]
    public void Constructor_WithVideoCallType_CreatesSuccessfully()
    {
        // Act
        Call call = new Call(testDate, "Videollamada", ExchangeType.Received, customer);

        // Assert
        Assert.That(call.Type, Is.EqualTo(ExchangeType.Received));
    }
    

    [Test]
    public void Note_AddNote_AddsSuccessfully()
    {
        // Arrange
        Call call = new Call(testDate, "Llamada", ExchangeType.Sent, customer);
        Note note = new Note();
        note.Topic = "Cliente interesado";
        note.Date = "2025-10-19";

        // Act
        call.Note.Add(note);

        // Assert
        Assert.That(call.Note.Count, Is.EqualTo(1));
        Assert.That(call.Note[0], Is.EqualTo(note));
    }

    [Test]
    public void Note_AddMultipleNotes_AddsAllSuccessfully()
    {
        // Arrange
        Call call = new Call(testDate, "Llamada", ExchangeType.Received, customer);
        Note note1 = new Note();
        note1.Topic = "Nota 1";
        Note note2 = new Note();
        note2.Topic = "Nota 2";
        Note note3 = new Note();
        note3.Topic = "Nota 3";

        // Act
        call.Note.Add(note1);
        call.Note.Add(note2);
        call.Note.Add(note3);

        // Assert
        Assert.That(call.Note.Count, Is.EqualTo(3));
        Assert.That(call.Note, Contains.Item(note1));
        Assert.That(call.Note, Contains.Item(note2));
        Assert.That(call.Note, Contains.Item(note3));
    }

    [Test]
    public void Note_RemoveNote_RemovesSuccessfully()
    {
        // Arrange
        Call call = new Call(testDate, "Llamada", ExchangeType.Sent, customer);
        Note note = new Note();
        note.Topic = "Nota a eliminar";
        call.Note.Add(note);

        // Act
        call.Note.Remove(note);

        // Assert
        Assert.That(call.Note.Count, Is.EqualTo(0));
    }

    // ============================================
    // PRUEBAS CON DIFERENTES CUSTOMERS
    // ============================================

    [Test]
    public void Constructor_WithDifferentCustomers_CreatesIndependentCalls()
    {
        // Arrange
        Customer customer2 = new Customer("87654321", "María", "García", "maria@email.com", "099888999", "Femenino", new DateTime(1985, 3, 20));

        // Act
        Call call1 = new Call(testDate, "Llamada Cliente 1", ExchangeType.Received, customer);
        Call call2 = new Call(testDate, "Llamada Cliente 2", ExchangeType.Sent, customer2);

        // Assert
        Assert.That(call1.Customer, Is.Not.EqualTo(call2.Customer));
        Assert.That(call1.Customer.Name, Is.EqualTo("Juan"));
        Assert.That(call2.Customer.Name, Is.EqualTo("María"));
    }
    
    [Test]
    public void Constructor_WithNullTopic_CreatesCall()
    {
        // Act
        Call call = new Call(testDate, null, ExchangeType.Sent, customer);

        // Assert
        Assert.That(call.Topic, Is.Null);
        Assert.That(call.Note, Is.Not.Null);
    }

    [Test]
    public void Constructor_WithNullCustomer_CreatesCall()
    {
        // Act
        Call call = new Call(testDate, "Llamada", ExchangeType.Received, null);

        // Assert
        Assert.That(call.Customer, Is.Null);
        Assert.That(call.Note, Is.Not.Null);
    }

    [Test]
    public void Call_InheritedProperties_CanBeModified()
    {
        // Arrange
        Call call = new Call(testDate, "Llamada inicial", ExchangeType.Received, customer);

        // Act
        call.Topic = "Llamada modificada";
        call.Type = ExchangeType.Sent;

        // Assert
        Assert.That(call.Topic, Is.EqualTo("Llamada modificada"));
        Assert.That(call.Type, Is.EqualTo(ExchangeType.Sent));
    }
}