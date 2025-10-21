using System;
using Library;

// Enumeración para representar si una nota fue enviada o recibida
// SRP: Define únicamente los dos estados posibles de una nota
// Este enum ayuda a evitar el uso de strings o booleanos que podrían
// ser menos expresivos y propensos a errores

public class Note
{
    private string topic;
    private DateTime date;
    private ExchangeType sentReceived;
    
    public string Topic
    {
        get { return topic; }
        set { topic = value; }
    }

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public ExchangeType SentReceivedStatus
    {
        get { return sentReceived; }
        set { sentReceived = value; }
    }
    
    public Note(string topic, DateTime date,ExchangeType type)
    {
        this.Topic = topic;
        this.Date = date;
        this.SentReceivedStatus = type;
    }
    
    public override string ToString()
    {
        return $"Topic: {topic}, Date: {date}, Sent/Received: {sentReceived}";
    }
}