using NUnit.Framework;
using Library;

public class SaleTest
{
    [Test]
    
    public void Constructor_Valid()
    {
        string id = "12345678";
        string product = "Papel";
        Quote amount = new Quote(new DateTime(2025, 10, 20), "Reunion", ExchangeType.Received,
            new Customer("12345678", "Jose Jose", "Vazques", "josejosee@gmail.com", "98234234", "Masculino",
                new DateTime(1999, 03, 04)),1000,"Papel de alta gama" );
        DateTime date = DateTime.Today;
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer customer = new Customer("12345678", "Jose Jose", "Vazquez", "josejosee@gmail.com", "98234234",
            "Masculino", new DateTime(1999, 03, 04));
        
        var sale = new Sale(id, product, amount, date,topic,type,customer);

        Assert.That(sale.Product, Is.EqualTo(product));
        Assert.That(sale.Amount, Is.EqualTo(amount));
        Assert.That(sale.Date, Is.EqualTo(date));
        Assert.That(sale.Topic, Is.EqualTo(topic));
        Assert.That(sale.Type, Is.EqualTo(type));
        Assert.That(sale.Customer, Is.EqualTo(customer));

        
    }
    [Test]
    
    public void Constructor_WithNullProduct()
    {
        string id = "12345678";
        string product = null;
        Quote amount = new Quote(new DateTime(2025, 10, 20), "Reunion", ExchangeType.Received,
            new Customer("12345678", "Jose Jose", "Vazques", "josejosee@gmail.com", "98234234", "Masculino",
                new DateTime(1999, 03, 04)),1000,"Papel de alta gama" );
        DateTime date = DateTime.Today;
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer customer = new Customer("12345678", "Jose Jose", "Vazquez", "josejosee@gmail.com", "98234234",
            "Masculino", new DateTime(1999, 03, 04));
        
        var sale = new Sale(id, product, amount, date,topic,type,customer);

        Assert.That(sale.Product, Is.EqualTo(null));
        Assert.That(sale.Amount, Is.EqualTo(amount));
        Assert.That(sale.Date, Is.EqualTo(date));
        Assert.That(sale.Topic, Is.EqualTo(topic));
        Assert.That(sale.Type, Is.EqualTo(type));
        Assert.That(sale.Customer, Is.EqualTo(customer));

        
    }
    [Test]
    
    public void Constructor_WithNullAmount()
    {
        string id = "12345678";
        string product = "Papel";
        Quote amount = null;
        DateTime date = DateTime.Today;
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer customer = new Customer("12345678", "Jose Jose", "Vazquez", "josejosee@gmail.com", "98234234",
            "Masculino", new DateTime(1999, 03, 04));
        
        var sale = new Sale(id, product, amount, date,topic,type,customer);

        Assert.That(sale.Product, Is.EqualTo(product));
        Assert.That(sale.Amount, Is.EqualTo(null));
        Assert.That(sale.Date, Is.EqualTo(date));
        Assert.That(sale.Topic, Is.EqualTo(topic));
        Assert.That(sale.Type, Is.EqualTo(type));
        Assert.That(sale.Customer, Is.EqualTo(customer));

        
    }
    [Test]
    
    public void Constructor_WithNullTopic()
    {
        string id = "12345678";
        string product = "Papel";
        Quote amount = new Quote(new DateTime(2025, 10, 20), "Reunion", ExchangeType.Received,
            new Customer("12345678", "Jose Jose", "Vazques", "josejosee@gmail.com", "98234234", "Masculino",
                new DateTime(1999, 03, 04)),1000,"Papel de alta gama" );
        DateTime date = DateTime.Today;
        string topic = null;
        ExchangeType type = ExchangeType.Received;
        Customer customer = new Customer("12345678", "Jose Jose", "Vazquez", "josejosee@gmail.com", "98234234",
            "Masculino", new DateTime(1999, 03, 04));
        
        var sale = new Sale(id, product, amount, date,topic,type,customer);

        Assert.That(sale.Product, Is.EqualTo(product));
        Assert.That(sale.Amount, Is.EqualTo(amount));
        Assert.That(sale.Date, Is.EqualTo(date));
        Assert.That(sale.Topic, Is.EqualTo(null));
        Assert.That(sale.Type, Is.EqualTo(type));
        Assert.That(sale.Customer, Is.EqualTo(customer));
        
    }
    [Test]
    
    public void Constructor_WithNullCustomer()
    {
        string id = "12345678";
        string product = "Papel";
        Quote amount = new Quote(new DateTime(2025, 10, 20), "Reunion", ExchangeType.Received,
            new Customer("12345678", "Jose Jose", "Vazques", "josejosee@gmail.com", "98234234", "Masculino",
                new DateTime(1999, 03, 04)),1000,"Papel de alta gama" );
        DateTime date = DateTime.Today;
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer customer = null;
        
        var sale = new Sale(id, product, amount, date,topic,type,customer);

        Assert.That(sale.Product, Is.EqualTo(product));
        Assert.That(sale.Amount, Is.EqualTo(amount));
        Assert.That(sale.Date, Is.EqualTo(date));
        Assert.That(sale.Topic, Is.EqualTo(topic));
        Assert.That(sale.Type, Is.EqualTo(type));
        Assert.That(sale.Customer, Is.EqualTo(null));

        
    }
    [Test]
    
    public void Constructor_CanBeCompared()
    {
        string id = "12345678";
        string product = "Papel";
        Quote amount = new Quote(new DateTime(2025, 10, 20), "Reunion", ExchangeType.Received,
            new Customer("12345678", "Jose Jose", "Vazques", "josejosee@gmail.com", "98234234", "Masculino",
                new DateTime(1999, 03, 04)),1000,"Papel de alta gama" );
        DateTime date = DateTime.Today;
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer customer = new Customer("12345678", "Jose Jose", "Vazquez", "josejosee@gmail.com", "98234234",
            "Masculino", new DateTime(1999, 03, 04));
        
        var sale = new Sale(id, product, amount, date,topic,type,customer);
        
        string id2 = "12345678";
        string product2 = "Papel";
        Quote amount2 = new Quote(new DateTime(2025, 10, 20), "Reunion", ExchangeType.Received,
            new Customer("12345678", "Jose Jose", "Vazques", "josejosee@gmail.com", "98234234", "Masculino",
                new DateTime(1999, 03, 04)),1000,"Papel de alta gama" );
        DateTime date2 = DateTime.Today;
        string topic2 = "Reunion";
        ExchangeType type2 = ExchangeType.Received;
        Customer customer2 = new Customer("12345678", "Jose Jose", "Vazquez", "josejosee@gmail.com", "98234234",
            "Masculino", new DateTime(1999, 03, 04));

        var sale2 = new Sale(id2, product2, amount2, date2,topic2,type2,customer2);

        Assert.That(sale, Is.Not.EqualTo(sale2));
        Assert.That(sale2, Is.Not.EqualTo(sale));
        
    }
    [Test]
    public void ToString_TestingOfMethodToString()
    {
        Customer customer = new Customer("1", "Juan", "Perez", "juan@test.com", 
            "123456", "Masculino", new DateTime(2000, 1, 1));
        Quote amount = new Quote(DateTime.Today, "Venta", ExchangeType.Received, 
            customer, 1500, "Producto premium");
        var sale = new Sale("1", "Laptop", amount, DateTime.Today, 
            "Venta de equipos", ExchangeType.Received, customer);

        string result = sale.ToString();
        
        Assert.That(result, Does.Contain("Laptop"));
    }
}