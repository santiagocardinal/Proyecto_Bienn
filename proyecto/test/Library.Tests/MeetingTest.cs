namespace Library.Tests;
/*
  public Meeting(string place, DateTime date, string topic, ExchangeType type, Customer _customer) : base(date, topic, type,_customer)
    {
        this.Place = place;
    }
 */
public class MeetingTest
{
    [Test]
    public void Meeting_ValidParameters()
    {
        Customer pablo = new Customer("12345678", "Pablo", "Josemaria", "pablojosemaria7@gmail.com", "98123123",
            "Masculino", new DateTime(1993, 11, 07));
        string place = "Ucu";
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;

        var meet = new Meeting(place, date, topic, type, pablo);
        
        Assert.That(meet.Place, Is.EqualTo(place));
        Assert.That(meet.Date, Is.EqualTo(date));
        Assert.That(meet.Topic, Is.EqualTo(topic));
        Assert.That(meet.Type, Is.EqualTo(type));
        Assert.That(meet.Customer, Is.EqualTo(pablo));
        
    }

    [Test]
    public void Meeting_NullPlace()
    {
        Customer pablo = new Customer("12345678", "Pablo", "Josemaria", "pablojosemaria7@gmail.com", "98123123",
            "Masculino", new DateTime(1993, 11, 07));
        string place = null;
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;

        var meet = new Meeting(place, date, topic, type, pablo);
        
        Assert.That(meet.Place, Is.EqualTo(null));
        Assert.That(meet.Date, Is.EqualTo(date));
        Assert.That(meet.Topic, Is.EqualTo(topic));
        Assert.That(meet.Type, Is.EqualTo(type));
        Assert.That(meet.Customer, Is.EqualTo(pablo));  
    }
    [Test]
    public void Meeting_NullTopic()
    {
        Customer pablo = new Customer("12345678", "Pablo", "Josemaria", "pablojosemaria7@gmail.com", "98123123",
            "Masculino", new DateTime(1993, 11, 07));
        string place = "Ucu";
        DateTime date = new DateTime(2025, 10, 20);
        string topic = null;
        ExchangeType type = ExchangeType.Sent;

        var meet = new Meeting(place, date, topic, type, pablo);
        
        Assert.That(meet.Place, Is.EqualTo(place));
        Assert.That(meet.Date, Is.EqualTo(date));
        Assert.That(meet.Topic, Is.EqualTo(null));
        Assert.That(meet.Type, Is.EqualTo(type));
        Assert.That(meet.Customer, Is.EqualTo(pablo));  
    }
    [Test]
    public void Meeting_NullCustomer()
    {
        Customer pablo = null;
        string place = "Ucu";
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;

        var meet = new Meeting(place, date, topic, type, pablo);
        
        Assert.That(meet.Place, Is.EqualTo(place));
        Assert.That(meet.Date, Is.EqualTo(date));
        Assert.That(meet.Topic, Is.EqualTo(topic));
        Assert.That(meet.Type, Is.EqualTo(type));
        Assert.That(meet.Customer, Is.EqualTo(null));  
    }
    [Test]
    public void Meeting_PlaceMutable()
    {
        // Arrange
        Customer pablo = new Customer("12345678", "Pablo", "Josemaria", "pablojosemaria7@gmail.com", 
            "98123123", "Masculino", new DateTime(1993, 11, 07));
        string place = "Ucu";
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;
        
        var meet = new Meeting(place, date, topic, type, pablo);

        // Act
        meet.Place = "Punta Carretas";
        
        // Assert - Corregido: Verifica el NUEVO valor de Place
        Assert.That(meet.Place, Is.EqualTo("Punta Carretas"));
        Assert.That(meet.Date, Is.EqualTo(date));
        Assert.That(meet.Topic, Is.EqualTo(topic));
        Assert.That(meet.Type, Is.EqualTo(type));
        Assert.That(meet.Customer, Is.EqualTo(pablo));
    }
    [Test]
    public void Meeting_TopicMutable()
    {
        Customer pablo = new Customer("12345678", "Pablo", "Josemaria", "pablojosemaria7@gmail.com", "98123123",
            "Masculino", new DateTime(1993, 11, 07));
        string place = "Ucu";
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;

        var meet = new Meeting(place, date, topic, type, pablo);

        meet.Topic = "Encuentro";
        
        Assert.That(meet.Place, Is.EqualTo(place));
        Assert.That(meet.Date, Is.EqualTo(date));
        Assert.That(meet.Topic, Is.EqualTo("Encuentro"));
        Assert.That(meet.Type, Is.EqualTo(type));
        Assert.That(meet.Customer, Is.EqualTo(pablo));
    }
    [Test]
    public void Meeting_MutableCustomer()
    {
        Customer pablo = new Customer("12345678", "Pablo", "Josemaria", "pablojosemaria7@gmail.com", 
            "98123123", "Masculino", new DateTime(1993, 11, 07));
        Customer javier = new Customer("12345678", "Javier", "Gonzalez", "javiergonzalez@gmail.com", 
            "98321321", "Masculino", new DateTime(1995, 01, 10));
        string place = "Ucu";
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;

        var meet = new Meeting(place, date, topic, type, pablo);
        
        meet.Customer = javier;
        
        Assert.That(meet.Place, Is.EqualTo(place));
        Assert.That(meet.Date, Is.EqualTo(date));
        Assert.That(meet.Topic, Is.EqualTo(topic));
        Assert.That(meet.Type, Is.EqualTo(type));
        Assert.That(meet.Customer, Is.EqualTo(javier));
        Assert.That(meet.Customer.Name, Is.EqualTo("Javier"));
    }
}