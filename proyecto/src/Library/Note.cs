using System;

public enum SentReceived
{
    Sent,
    Received
}

public class Note
{
    private string topic;
    private string date;
    private SentReceived sentReceived;
    
    public string Topic
    {
        get { return topic; }
        set { topic = value; }
    }

    public string Date
    {
        get { return date; }
        set { date = value; }
    }

    public SentReceived SentReceivedStatus
    {
        get { return sentReceived; }
        set { sentReceived = value; }
    }
    
    public void Notes(string topic, string date)
    {
        this.Topic = topic;
        this.Date = date;
        
    }
    
    public override string ToString()
    {
        return $"Topic: {topic}, Date: {date}, Sent/Received: {sentReceived}";
    }
}
