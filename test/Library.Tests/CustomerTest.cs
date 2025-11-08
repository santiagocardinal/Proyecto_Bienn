using NUnit.Framework;
using Library;
using System;
using System.Collections.Generic;

namespace Library.Tests;

[TestFixture]
public class CustomerTest
{
    [Test]
    public void Customer_ValidParameters()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);

        Assert.That(customer.Id, Is.EqualTo(id));
        Assert.That(customer.Name, Is.EqualTo(name));
        Assert.That(customer.FamilyName, Is.EqualTo(familyName));
        Assert.That(customer.Mail, Is.EqualTo(mail));
        Assert.That(customer.Phone, Is.EqualTo(phone));
        Assert.That(customer.Gender, Is.EqualTo(gender));
        Assert.That(customer.BirthDate, Is.EqualTo(birthDate));
        Assert.That(customer.IsActive, Is.True);
        Assert.That(customer.Tags, Is.Not.Null);
        Assert.That(customer.Tags.Count, Is.EqualTo(0));
        Assert.That(customer.Interactions, Is.Not.Null);
        Assert.That(customer.Interactions.Count, Is.EqualTo(0));
    }

    [Test]
    public void Customer_NullName()
    {
        string id = "12345678";
        string name = null;
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);

        Assert.That(customer.Name, Is.Null);
        Assert.That(customer.Id, Is.EqualTo(id));
    }

    [Test]
    public void Customer_NullMail()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = null;
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);

        Assert.That(customer.Mail, Is.Null);
        Assert.That(customer.Name, Is.EqualTo(name));
    }
    
    [Test]
    public void Customer_MailMutable()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        customer.Mail = "nuevo@email.com";

        Assert.That(customer.Mail, Is.EqualTo("nuevo@email.com"));
        Assert.That(customer.Name, Is.EqualTo(name));
    }

    [Test]
    public void Customer_PhoneMutable()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        customer.Phone = "99999999";

        Assert.That(customer.Phone, Is.EqualTo("99999999"));
        Assert.That(customer.Name, Is.EqualTo(name));
    }

    [Test]
    public void Customer_GenderMutable()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        customer.Gender = "Femenino";

        Assert.That(customer.Gender, Is.EqualTo("Femenino"));
        Assert.That(customer.Name, Is.EqualTo(name));
    }
    
    [Test]
    public void Customer_Deactivate()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        customer.Deactivate();

        Assert.That(customer.IsActive, Is.False);
        Assert.That(customer.CheckIsActive(), Is.False);
    }

    [Test]
    public void Customer_Activate()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        customer.Deactivate();
        customer.Activate();

        Assert.That(customer.IsActive, Is.True);
        Assert.That(customer.CheckIsActive(), Is.True);
    }
    

    [Test]
    public void Customer_IsValidMail()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        bool isValid = customer.IsValidMail("pablojosemaria7@gmail.com");

        Assert.That(isValid, Is.True);
    }
    
    [Test]
    public void Customer_TagsInitializedEmpty()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);

        Assert.That(customer.Tags, Is.Not.Null);
        Assert.That(customer.Tags, Is.Empty);
        Assert.That(customer.Tags.Count, Is.EqualTo(0));
    }

    [Test]
    public void Customer_AddTag()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);
        Tag tag1 = new Tag("12345678",name,"Amateur cutomer");

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        customer.AddTag(tag1);

        Assert.That(customer.Tags.Count, Is.EqualTo(1));
        Assert.That(customer.Tags[0], Is.EqualTo(tag1));
    }

    [Test]
    public void Customer_AddMultipleTags()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);
        Tag tag1 = new Tag("12345678",name,"VIP");
        Tag tag2 = new Tag("12345678",name,"Premiun");
        Tag tag3 = new Tag("12345678",name,"Gold");

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        customer.AddTag(tag1);
        customer.AddTag(tag2);
        customer.AddTag(tag3);

        Assert.That(customer.Tags.Count, Is.EqualTo(3));
        Assert.That(customer.Tags[0], Is.EqualTo(tag1));
        Assert.That(customer.Tags[1], Is.EqualTo(tag2));
        Assert.That(customer.Tags[2], Is.EqualTo(tag3));
    }

    [Test]
    public void Customer_RemoveTag()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);
        Tag tag1 = new Tag("12345678",name,"VIP");
        Tag tag2 = new Tag("12345678",name,"Premiun");

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        customer.AddTag(tag1);
        customer.AddTag(tag2);
        customer.RemoveTag(tag1);

        Assert.That(customer.Tags.Count, Is.EqualTo(1));
        Assert.That(customer.Tags[0], Is.EqualTo(tag2));
    }

    [Test]
    public void Customer_InteractionsInitializedEmpty()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);

        Assert.That(customer.Interactions, Is.Not.Null);
        Assert.That(customer.Interactions, Is.Empty);
        Assert.That(customer.Interactions.Count, Is.EqualTo(0));
    }

    [Test]
    public void Customer_AddInteraction()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        Interaction interaction1 = new InteractionRegular(new DateTime(2025, 10, 20), "Reunion", 
            ExchangeType.Received, customer);

        customer.AddInteraction(interaction1);

        Assert.That(customer.Interactions.Count, Is.EqualTo(1));
        Assert.That(customer.Interactions[0], Is.EqualTo(interaction1));
    }

    [Test]
    public void Customer_AddMultipleInteractions()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        Interaction interaction1 = new InteractionRegular(new DateTime(2025, 10, 20), "Reunion", 
            ExchangeType.Received, customer);
        Interaction interaction2 = new InteractionRegular(new DateTime(2025, 10, 21), "Llamada", 
            ExchangeType.Sent, customer);

        customer.AddInteraction(interaction1);
        customer.AddInteraction(interaction2);

        Assert.That(customer.Interactions.Count, Is.EqualTo(2));
        Assert.That(customer.Interactions[0], Is.EqualTo(interaction1));
        Assert.That(customer.Interactions[1], Is.EqualTo(interaction2));
    }
    

    [Test]
    public void Customer_GetInteractionsByType()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        Meeting meeting1 = new Meeting("Ucu", new DateTime(2025, 10, 20), "Reunion", 
            ExchangeType.Received, customer);
        Quote quote1 = new Quote(new DateTime(2025, 10, 21), "Cotizacion", 
            ExchangeType.Sent, customer, 1000, "Producto");

        customer.AddInteraction(meeting1);
        customer.AddInteraction(quote1);

        List<Meeting> meetings = customer.GetInteractionsByType<Meeting>();

        Assert.That(meetings.Count, Is.EqualTo(1));
        Assert.That(meetings[0], Is.EqualTo(meeting1));
    }

    [Test]
    public void Customer_GetInteractionsByDate()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        Interaction interaction1 = new InteractionRegular(new DateTime(2025, 10, 20), "Reunion", 
            ExchangeType.Received, customer);
        Interaction interaction2 = new InteractionRegular(new DateTime(2025, 10, 20, 15, 30, 0), "Llamada", 
            ExchangeType.Sent, customer);
        Interaction interaction3 = new InteractionRegular(new DateTime(2025, 10, 21), "Email", 
            ExchangeType.Received, customer);

        customer.AddInteraction(interaction1);
        customer.AddInteraction(interaction2);
        customer.AddInteraction(interaction3);

        List<Interaction> interactions = customer.GetInteractionsByDate(new DateTime(2025, 10, 20));

        Assert.That(interactions.Count, Is.EqualTo(2));
        Assert.That(interactions[0], Is.EqualTo(interaction1));
        Assert.That(interactions[1], Is.EqualTo(interaction2));
    }

    [Test]
    public void Customer_GetLastInteraction()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        Interaction interaction1 = new InteractionRegular(new DateTime(2025, 10, 20), "Reunion", 
            ExchangeType.Received, customer);
        Interaction interaction2 = new InteractionRegular(new DateTime(2025, 10, 25), "Llamada", 
            ExchangeType.Sent, customer);
        Interaction interaction3 = new InteractionRegular(new DateTime(2025, 10, 22), "Email", 
            ExchangeType.Received, customer);

        customer.AddInteraction(interaction1);
        customer.AddInteraction(interaction2);
        customer.AddInteraction(interaction3);

        DateTime lastInteraction = customer.GetLastInteraction();

        Assert.That(lastInteraction, Is.EqualTo(new DateTime(2025, 10, 25)));
    }

    [Test]
    public void Customer_NoInteractions()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);

        Assert.Throws<InvalidOperationException>(() => customer.GetLastInteraction());
    }

    [Test]
    public void Customer_GetUnansweredInteractions()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        Interaction interaction1 = new InteractionRegular(new DateTime(2025, 10, 20), "Reunion", 
            ExchangeType.Received, customer);
        Interaction interaction2 = new InteractionRegular(new DateTime(2025, 10, 21), "Llamada", 
            ExchangeType.Sent, customer);
        Interaction interaction3 = new InteractionRegular(new DateTime(2025, 10, 22), "Email", 
            ExchangeType.Received, customer);
        interaction1.MarkAsResponded();

        customer.AddInteraction(interaction1);
        customer.AddInteraction(interaction2);
        customer.AddInteraction(interaction3);

        List<Interaction> unanswered = customer.GetUnansweredInteractions();

        Assert.That(unanswered.Count, Is.EqualTo(1));
        Assert.That(unanswered[0], Is.EqualTo(interaction3));
    }

    [Test]
    public void Customer_SellerMutable()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        Seller seller = new Seller("Vendedor1", "vendedor@email.com", "99999999", "1");

        customer.Seller = seller;

        Assert.That(customer.Seller, Is.EqualTo(seller));
        Assert.That(customer.Seller.Name, Is.EqualTo("Vendedor1"));
    }

    [Test]
    public void Customer_ToString()
    {
        string id = "12345678";
        string name = "Pablo";
        string familyName = "Josemaria";
        string mail = "pablojosemaria7@gmail.com";
        string phone = "98123123";
        string gender = "Masculino";
        DateTime birthDate = new DateTime(1993, 11, 07);

        var customer = new Customer(id, name, familyName, mail, phone, gender, birthDate);
        Tag tag1 = new Tag("12345678",name,"VIP");
        customer.AddTag(tag1);

        string result = customer.ToString();

        Assert.That(result, Does.Contain("12345678"));
        Assert.That(result, Does.Contain("Pablo"));
        Assert.That(result, Does.Contain("Josemaria"));
        Assert.That(result, Does.Contain("pablojosemaria7@gmail.com"));
        Assert.That(result, Does.Contain("98123123"));
        Assert.That(result, Does.Contain("Masculino"));
        Assert.That(result, Does.Contain("Tags: 1"));
    }
}