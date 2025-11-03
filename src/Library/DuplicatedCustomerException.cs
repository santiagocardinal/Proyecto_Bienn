using System;
namespace Library;
public class DuplicatedCustomerException : Exception
{
    //public Customer Customer { get; }

    public DuplicatedCustomerException(Customer customer)
        : base($"Ya existe un cliente {customer.Id}.")
    {
        //Customer = customer;
    }
}