using System.Data.Common;

namespace Library;

public class Quote : Interaction
{
    private double amount;
    private string description;

    public double Amount { get { return amount; } set { amount = value; } }
    public string Description { get { return description; } set { description = value; } }

    public Quote(DateTime date, string topic, ExchangeType type,Customer _customer, double amount, string description) : base (date, topic, type, _customer)
    {
        this.Amount = amount;
        this.Description = description;
    }
    
    public override string ToString()
    {
        return $"Amount: {this.amount}, Description: {this.description}.";
    }
}