namespace Library;

public class NotExistingCustomerException : Exception
{
    public NotExistingCustomerException()
        : base($"No existe el cliente.")
    {
    }
    
}