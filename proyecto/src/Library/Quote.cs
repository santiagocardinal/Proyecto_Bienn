namespace Library;

public class Quote : Interaction
{
    private string date;
    private double amount;
    private string description;

    public string Date { get { return date; } set { date = value; } }
    public double Amount { get { return amount; } set { amount = value; } }
    public string Description { get { return description; } set { description = value; } }

    public Quote(string date, double amount, string description)
    {
        this.Date = date;
        this.Amount = amount;
        this.Description = description;
    }
    
    public void Sale(string date, double amount, string description)
    {
        this.Date = date;
        this.Amount = amount;
        this.Description = description;
    }
}