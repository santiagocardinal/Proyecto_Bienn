using NUnit.Framework;
using Library;
using System;
using System.Collections.Generic;

namespace Library.Tests;

public class InteractionTest
{
    [Test]
    public void Interaction_ValidParameters()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interaction = new Interaction(date, topic, type, pepe);
        
        Assert.That(interaction.Date, Is.EqualTo(date));
        Assert.That(interaction.Topic, Is.EqualTo(topic));
        Assert.That(interaction.Type, Is.EqualTo(type));
        Assert.That(interaction.Customer, Is.EqualTo(pepe));
        Assert.That(interaction.HasResponse, Is.False);
        Assert.That(interaction.Note, Is.Not.Null);
        Assert.That(interaction.Note.Count, Is.EqualTo(0));
    }

    [Test]
    public void Interaction_NullTopic()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = null;
        ExchangeType type = ExchangeType.Sent;
        Customer pablo = new Customer("12345678", "Pablo", "Josemaria", "pablojosemaria7@gmail.com", 
            "98123123", "Masculino", new DateTime(1993, 11, 07));

        var interaction = new Interaction(date, topic, type, pablo);
        
        Assert.That(interaction.Date, Is.EqualTo(date));
        Assert.That(interaction.Topic, Is.Null);
        Assert.That(interaction.Type, Is.EqualTo(type));
        Assert.That(interaction.Customer, Is.EqualTo(pablo));
    }

    [Test]
    public void Interaction_NullCustomer()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer customer = null;

        var interaction = new Interaction(date, topic, type, customer);
        
        Assert.That(interaction.Date, Is.EqualTo(date));
        Assert.That(interaction.Topic, Is.EqualTo(topic));
        Assert.That(interaction.Type, Is.EqualTo(type));
        Assert.That(interaction.Customer, Is.Null);
    }

    [Test]
    public void Interaction_DateMutable()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interaction = new Interaction(date, topic, type, pepe);
        interaction.Date = new DateTime(2025, 12, 25);
        
        Assert.That(interaction.Date, Is.EqualTo(new DateTime(2025, 12, 25)));
        Assert.That(interaction.Topic, Is.EqualTo(topic));
        Assert.That(interaction.Type, Is.EqualTo(type));
        Assert.That(interaction.Customer, Is.EqualTo(pepe));
    }

    [Test]
    public void Interaction_TopicMutable()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interaction = new Interaction(date, topic, type, pepe);
        interaction.Topic = "Encuentro";
        
        Assert.That(interaction.Date, Is.EqualTo(date));
        Assert.That(interaction.Topic, Is.EqualTo("Encuentro"));
        Assert.That(interaction.Type, Is.EqualTo(type));
        Assert.That(interaction.Customer, Is.EqualTo(pepe));
    }

    [Test]
    public void Interaction_TypeMutable()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interaction = new Interaction(date, topic, type, pepe);
        interaction.Type = ExchangeType.Received;
        
        Assert.That(interaction.Date, Is.EqualTo(date));
        Assert.That(interaction.Topic, Is.EqualTo(topic));
        Assert.That(interaction.Type, Is.EqualTo(ExchangeType.Received));
        Assert.That(interaction.Customer, Is.EqualTo(pepe));
    }

    [Test]
    public void Interaction_CustomerMutable()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));
        Customer javier = new Customer("87654321", "Javier", "Gonzalez", "javiergonzalez@gmail.com", 
            "98321321", "Masculino", new DateTime(1995, 01, 10));

        var interaction = new Interaction(date, topic, type, pepe);
        interaction.Customer = javier;
        
        Assert.That(interaction.Date, Is.EqualTo(date));
        Assert.That(interaction.Topic, Is.EqualTo(topic));
        Assert.That(interaction.Type, Is.EqualTo(type));
        Assert.That(interaction.Customer, Is.EqualTo(javier));
        Assert.That(interaction.Customer.Name, Is.EqualTo("Javier"));
    }

    [Test]
    public void Interaction_HasResponseInitiallyFalse()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interaction = new Interaction(date, topic, type, pepe);
        
        Assert.That(interaction.HasResponse, Is.False);
    }

    [Test]
    public void Interaction_MarkAsResponded()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interaction = new Interaction(date, topic, type, pepe);
        interaction.MarkAsResponded();
        
        Assert.That(interaction.HasResponse, Is.True);
    }

    [Test]
    public void Interaction_HasResponseMutable()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Sent;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interaction = new Interaction(date, topic, type, pepe);
        interaction.HasResponse = true;
        
        Assert.That(interaction.HasResponse, Is.True);
        
        interaction.HasResponse = false;
        Assert.That(interaction.HasResponse, Is.False);
    }
    
    [Test]
    public void Interaction_AddNote()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));
        Note note1 = new Note();
        note1.Topic = "Primera nota";
        note1.Date = DateTime.Today;

        var interaction = new Interaction(date, topic, type, pepe);
        interaction.AddNote(note1);
        
        Assert.That(interaction.Note.Count, Is.EqualTo(1));
        Assert.That(interaction.Note[0], Is.EqualTo(note1));
    }

    [Test]
    public void Interaction_AddMultipleNotes()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        ExchangeType type = ExchangeType.Received;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));
        Note note1 = new Note();
        note1.Topic = "Primera nota";
        Note note2 = new Note();
        note2.Topic = "Segunda nota";
        Note note3 = new Note();
        note3.Topic = "Tercera nota";

        var interaction = new Interaction(date, topic, type, pepe);
        interaction.AddNote(note1);
        interaction.AddNote(note2);
        interaction.AddNote(note3);
        
        Assert.That(interaction.Note.Count, Is.EqualTo(3));
        Assert.That(interaction.Note[0], Is.EqualTo(note1));
        Assert.That(interaction.Note[1], Is.EqualTo(note2));
        Assert.That(interaction.Note[2], Is.EqualTo(note3));
    }
    
    [Test]
    public void Interaction_ToStringWithNullTopic()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = null;
        ExchangeType type = ExchangeType.Sent;
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interaction = new Interaction(date, topic, type, pepe);
        
        Assert.DoesNotThrow(() => interaction.ToString());
    }

    [Test]
    public void Interaction_DifferentExchangeTypes()
    {
        DateTime date = new DateTime(2025, 10, 20);
        string topic = "Reunion";
        Customer pepe = new Customer("12345678", "Pepe", "Castro", "pepecastro@gmail.com", "98234234", 
            "Masculino", new DateTime(1999, 03, 21));

        var interactionSent = new Interaction(date, topic, ExchangeType.Sent, pepe);
        var interactionReceived = new Interaction(date, topic, ExchangeType.Received, pepe);
        
        Assert.That(interactionSent.Type, Is.EqualTo(ExchangeType.Sent));
        Assert.That(interactionReceived.Type, Is.EqualTo(ExchangeType.Received));
    }
}