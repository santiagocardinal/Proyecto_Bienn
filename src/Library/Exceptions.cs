namespace Library;
//namespace Library.Exceptions;
public static class Exceptions

{
    // --- CLIENTES ---

    public class NotExistingCustomerException : Exception
    {
        public NotExistingCustomerException()
            : base("No existe un cliente con los datos proporcionados.") {}
    }

    public class DuplicatedCustomerException : Exception
    {
        public DuplicatedCustomerException(Customer customer)
            : base($"Ya existe un cliente con ID '{customer.Id}'.") {}
    }

    public class InvalidFieldException : Exception
    {
        public InvalidFieldException(string field)
            : base($"El campo '{field}' no es válido para ser modificado.") {}
    }

    // --- VENDEDORES ---

    public class SellerNotFoundException : Exception
    {
        public SellerNotFoundException(string id)
            : base($"No se encontró el vendedor con ID '{id}'.") {}
    }

    public class DuplicateSellerException : Exception
    {
        public DuplicateSellerException(string id)
            : base($"Ya existe un vendedor con ID '{id}'.") {}
    }

    // --- TAGS ---

    public class DuplicateTagException : Exception
    {
        public DuplicateTagException(string tagName)
            : base($"El cliente ya tiene una etiqueta llamada '{tagName}'.") {}
    }

    // --- INTERACCIONES ---

    public class InteractionNotFoundException : Exception
    {
        public InteractionNotFoundException()
            : base("No se encontró la interacción solicitada.") {}
    }

    public class QuoteNotFoundException : Exception
    {
        public QuoteNotFoundException()
            : base("No se encontró una Quote que coincida con los datos proporcionados.") {}
    }

    // --- FECHAS ---

    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException()
            : base("La fecha inicial debe ser anterior a la fecha final.") {}
    }
}
