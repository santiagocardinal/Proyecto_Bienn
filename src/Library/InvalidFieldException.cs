namespace Library;

public class InvalidFieldException:  Exception
{
    public InvalidFieldException(string field)
        : base($"No existe el campo '{field}'.")
    {
    }
}