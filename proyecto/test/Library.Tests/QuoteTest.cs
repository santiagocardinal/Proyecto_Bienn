using NUnit.Framework;
using Library;
using System;

namespace Library.Tests;


public class QuoteTest
{
    [Test]
    public void Constructor_ShouldCreateQuoteWithValidParameters()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer customer = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", 
            "98234234", "Masculino", new DateTime(1999, 03, 21));
        double amount = 1000;
        string description = "Reunion informativa de indumentaria";

        var quote = new Quote(date, topic, type, customer, amount, description);
        
        Assert.That(quote.Date, Is.EqualTo(date));
        Assert.That(quote.Topic, Is.EqualTo(topic));
        Assert.That(quote.Type, Is.EqualTo(type));
        Assert.That(quote.Customer, Is.EqualTo(customer));
        Assert.That(quote.Amount, Is.EqualTo(amount));
        Assert.That(quote.Description, Is.EqualTo(description));
    }

    [Test]
    public void Quote_AmountMutation()
    {
        
        Customer customer = new Customer("1", "Test", "User", "test@test.com", 
            "123456", "Masculino", new DateTime(2000, 1, 1));
        var quote = new Quote(DateTime.Today, "Test", ExchangeType.Received, 
            customer, 500, "Description");

        quote.Amount = 1500;

        Assert.That(quote.Amount, Is.EqualTo(1500));
    }

    [Test]
    public void Quote_DesctriptionMutation()
    {
        Customer customer = new Customer("1", "Test", "User", "test@test.com", 
            "123456", "Masculino", new DateTime(2000, 1, 1));
        var quote = new Quote(DateTime.Today, "Test", ExchangeType.Received, 
            customer, 500, "Original description");

        quote.Description = "Updated description";

        Assert.That(quote.Description, Is.EqualTo("Updated description"));
    }
    
    [Test]
    public void Quote_ToStringMethod()
    {
        Customer customer = new Customer("1", "Test", "User", "test@test.com", 
            "123456", "Masculino", new DateTime(2000, 1, 1));
        var quote = new Quote(DateTime.Today, "Test", ExchangeType.Received, 
            customer, 1000, "Papel de alta gama");

        string result = quote.ToString();

        Assert.That(result, Is.EqualTo("Amount: 1000, Description: Papel de alta gama."));
    }
    
    [Test]
    public void ToString_WithNullDescription()
    {
        Customer customer = new Customer("1", "Maria", "User", "test@test.com", 
            "123456", "Masculino", new DateTime(2000, 1, 1));
        var quote = new Quote(DateTime.Today, "Test", ExchangeType.Received, 
            customer, 500, null);

        Assert.DoesNotThrow(() => quote.ToString());
    }

    [Test]
    public void Constructor_WithNullCustomer()
    {
        var quote = new Quote(DateTime.Today, "Javier", ExchangeType.Received, 
            null, 500, "Description");

        Assert.That(quote.Customer, Is.Null);
    }
    
    [Test]
    public void Quote_CanBeCompared()
    {
        Customer customer1 = new Customer("1", "Test1", "User1", "test1@test.com", 
            "111", "Masculino", new DateTime(2000, 1, 1));
        Customer customer2 = new Customer("2", "Test2", "User2", "test2@test.com", 
            "222", "Femenino", new DateTime(2000, 2, 2));

        var quote1 = new Quote(DateTime.Today, "Quote1", ExchangeType.Received, 
            customer1, 1000, "Description1");
        var quote2 = new Quote(DateTime.Today, "Quote2", ExchangeType.Sent, 
            customer2, 2000, "Description2");

        Assert.That(quote1, Is.Not.SameAs(quote2));
    }
    
}