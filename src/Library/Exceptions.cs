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
    
    public class DuplicatedMailException : Exception
    {
        public DuplicatedMailException(Customer customer)
            : base($"Ya existe un cliente con el mail '{customer.Mail}'.") {}
    }

    public class DuplicatedPhoneException : Exception
    {
        public DuplicatedPhoneException(Customer customer)
            : base($"Ya existe un cliente con el numero '{customer.Phone}'.") {}
    }
    
    public class InvalidFieldException : Exception
    {
        public InvalidFieldException(string field)
            : base($"El campo '{field}' no es válido para ser modificado.") {}
    }
    
    public class CustomerNotAssignedToSellerException : Exception
    {
        public CustomerNotAssignedToSellerException(string message)
            : base(message)
        {
        }
    }

    // --- VENDEDORES ---

    public class SellerNotFoundException : Exception
    {
        public SellerNotFoundException(string id)
            : base($"No se encontró el vendedor con ID {id}.") {}
    }
    
    public class SellerNullException : Exception
    {
        public SellerNullException()
            : base("No se encontró el vendedor con el ID proporcionado.") {}
    }

    public class DuplicateSellerException : Exception
    {
        public DuplicateSellerException(string id)
            : base($"Ya existe un vendedor con ID '{id}'.") {}
    }
    
    
    public class SuspendedSellerException : Exception
    {
        public SuspendedSellerException(string id)
            : base($"El vendedor '{id}' está suspendido y no puede realizar esta acción.")
        {
        }
    }


    // --- TAGS ---

    /*public class DuplicateTagException : Exception
    {
        public DuplicateTagException(string tagName)
            : base($"El cliente ya tiene una etiqueta llamada '{tagName}'.") {}
    }*/

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

    public class DuplicateQuoteException : Exception
    {
        public DuplicateQuoteException()
            : base("Ya existe una cotización con los mismos datos."){}
    }

    public class DuplicateSaleException : Exception
    {
        public DuplicateSaleException()
            : base("Ya existe una venta con los datos proporcionados."){}
    }

    // --- FECHAS ---

    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException()
            : base("La fecha inicial debe ser anterior a la fecha final.") {}
    }
    
    
    // ---  TAGS ---
    
    public class DuplicatedTagException : Exception
    {
        public DuplicatedTagException(string tagId)
            : base($"Ya existe una Tag con ID '{tagId}'.") {}
    }
    
    public class NotExistingTagException : Exception
    {
        public NotExistingTagException(string tagId)
            : base($"No existe una Tag con ID '{tagId}'.") {}
    }
}
