using System;

public enum SentReceived
{
    Sent,
    Received
}

public class Note
{
    private string theme;
    private string date;
    private SentReceived sentReceived;
    /*private Call[] calls;
    private Meeting[] meetings;
    private Message[] message;*/
    
    public string Theme
    {
        get { return theme; }
        set { theme = value; }
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

    /*public Call[] Calls
    {
        get { return calls; }
        set { calls = value; }
    }

    public Meeting[] Meetings
    {
        get { return meetings; }
        set { meetings = value; }
    }

    public Message[] Message
    {
        get { return message; }
        set { message = value; }
    }*/
    
    public void Notes(string theme, string date)
    {
        this.theme = theme;
        this.date = date;
        /*this.calls = new Call[0];
        this.meetings = new Meeting[0];
        this.message = new Message[0];*/
    }
    
    public override string ToString()
    {
        return $"Theme: {theme}, Date: {date}, Sent/Received: {sentReceived}";
    }
}
