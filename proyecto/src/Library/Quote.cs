using System.Data.Common;

namespace Library;

public class Quote : Interaction
{
    private double amount;
    private string description;

    public double Amount { get { return amount; } set { amount = value; } }
    public string Description { get { return description; } set { description = value; } }

    public Quote(DateTime date, string topic, ExchangeType type, double amount, string description) : base (date, topic, type)
    {
        this.Amount = amount;
        this.Description = description;
    }
    
    public void Sale(DateTime date, string topic, ExchangeType type, double amount, string description)
    {
        this.Date = date;
        this.Topic = topic;
        this.Type = type;
        this.Amount = amount;
        this.Description = description;
    }
}