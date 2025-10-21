namespace Library.Tests;

public class SellerTests
{
    private Seller seller;
    private Customer customer1;
    private Customer customer2;

    [SetUp]
    public void CreateCustomer_and_Seller()
    {
        seller = new Seller("Carlos Vendedor", "carlos@email.com", "099111222", "11111111");
        customer1 = new Customer("12345678", "Cliente Uno", "cliente1@email.com", "099333444", "Pérez", "Masculino", new DateTime(1990, 1, 1));
        customer2 = new Customer("87654321", "Cliente Dos", "cliente2@email.com", "099555666", "García", "Femenino", new DateTime(2014, 5, 30));
    }
    
    [Test]
    public void CreatesSeller()
    {
        Seller newSeller = new Seller("Juan", "juan@email.com", "099123456", "12345678");

        Assert.That(newSeller.Name, Is.EqualTo("Juan"));
        Assert.That(newSeller.Mail, Is.EqualTo("juan@email.com"));
        Assert.That(newSeller.Phone, Is.EqualTo("099123456"));
        Assert.That(newSeller.Id, Is.EqualTo("12345678"));
        Assert.That(newSeller.IsSuspended, Is.False);
        Assert.That(newSeller.Customer, Is.Not.Null);
        Assert.That(newSeller.Customer.Count, Is.EqualTo(0));
        Assert.That(newSeller.Interactions, Is.Not.Null);
        Assert.That(newSeller.Interactions.Count, Is.EqualTo(0));
    }

    [Test]
    public void InheritsFromUser()
    {
        Seller newSeller = new Seller("Pedro", "pedro@email.com", "099888999", "99999999");

        Assert.That(newSeller, Is.InstanceOf<User>());
    }

    [Test]
    public void IsSuspended_IsFalse()
    {
        Assert.That(seller.IsSuspended, Is.False);
    }

    [Test]
    public void IsSuspended_SetToTrue()
    {
        seller.IsSuspended = true;

        Assert.That(seller.IsSuspended, Is.True);
    }

    [Test]
    public void IsSuspended_SetToFalse()
    {
        seller.IsSuspended = true;

        seller.IsSuspended = false;

        Assert.That(seller.IsSuspended, Is.False);
    }

    [Test]
    public void AddInteraction()
    {
        Interaction interaction = new Interaction(DateTime.Now, "Reunión",ExchangeType.Received,new Customer("12345678", "Cliente Dos", "Rodriguez", "cliente2@gmail.com", "099444333", "Femenino", new DateTime(2000, 11, 20)));
        
        seller.addInteraction(interaction);

        Assert.That(seller.Interactions.Count, Is.EqualTo(1));
        Assert.That(seller.Interactions[0], Is.EqualTo(interaction));
    }

    [Test]
    public void MultipleInteractions()
    {
        Interaction interaction1 = new Interaction(DateTime.Now, "Reunión 1", ExchangeType.Received,new Customer("12545678", "Cliente Uno", "099533444", "cliente1@email.com", "099533444", "Masculino", new DateTime(1990, 1, 1)));
        Interaction interaction2 = new Interaction(DateTime.Now, "Reunión 2", ExchangeType.Sent,new Customer("12445678", "Cliente Dos", "Rodriguez", "cliente2@gmail.com", "099444333", "Femenino", new DateTime(2000, 11, 20)));
        Interaction interaction3 = new Interaction(DateTime.Now, "Reunión 3", ExchangeType.Sent,new Customer("12345678", "Cliente Tres", "Orue", "cliente3@gmail.com", "099964333", "Femenino", new DateTime(2010, 09, 14)));

        seller.addInteraction(interaction1);
        seller.addInteraction(interaction2);
        seller.addInteraction(interaction3);

        Assert.That(seller.Interactions.Count, Is.EqualTo(3));
    }
    
    [Test]
    public void GetTotalSales_WithNoSale()
    {
        List<Sale> sales = seller.getTotalSales();

        Assert.That(sales.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetTotalSales_RegularInteractions()
    {
        Interaction interaction1 = new Interaction(DateTime.Now, "Reunión", ExchangeType.Received,new Customer("12345678", "ClienteA", "Penino", "clientea@gmail.com", "099444333", "Masculino", new DateTime(2000, 11, 20)));
        Interaction interaction2 = new Interaction(DateTime.Now, "Seguimiento", ExchangeType.Sent,new Customer("12445678", "ClienteB", "Carballido", "clienteb@gmail.com", "099454333", "Masculino", new DateTime(2003, 10, 11)));
        seller.addInteraction(interaction1);
        seller.addInteraction(interaction2);

        List<Sale> sales = seller.getTotalSales();

        Assert.That(sales.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetTotalSales_WithSales()
    {
        Customer pennino = new Customer("12345678", "ClienteA", "Pennino", "clientepremiun@gmail.com", "099444333", "Masculino",new DateTime(2007,05,01));
        Quote pepe = new Quote(new DateTime(1891,09,28),"Venta",ExchangeType.Received,pennino,100.0,"Venta de papel.");
        Quote gallina = new Quote(new DateTime(2024,10,18),"Venta",ExchangeType.Received,pennino,3.0,"Venta de piscinas.");

        Sale sale1 = new Sale( "Papel", pepe, new DateTime(2025,07,29),"Reunion",ExchangeType.Received,pennino);
        Sale sale2 = new Sale( "Piscinas", gallina,new DateTime(2025,10,20),"Reunion",ExchangeType.Received,pennino);
        
        Interaction interaction = new Interaction(DateTime.Now, "Reunión", ExchangeType.Received,pennino);
        
        seller.addInteraction(sale1);
        seller.addInteraction(interaction);
        seller.addInteraction(sale2);
        
        List<Sale> sales = seller.getTotalSales();

        Assert.That(sales.Count, Is.EqualTo(2));
        Assert.That(sales, Contains.Item(sale1));
        Assert.That(sales, Contains.Item(sale2));
    }
}