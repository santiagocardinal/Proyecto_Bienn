namespace Library.Tests;

public class CustomerManagerTests
{
    private CustomerManager _customerManager;
    private Customer _testCustomer;
    private Seller _testSeller;

    [SetUp]
    public void Setup()
    {
        _customerManager = new CustomerManager();
        _testCustomer = new Customer("1", "Juan", "Pérez", "juan@mail.com", "099123456", "male", new DateTime(2000,10,05));
        _testSeller = new Seller("S1", "Carlos", "Vendedor", "AA");
    }

    [Test]
    public void SearchByName_CustomerExists_ReturnsCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        Customer result = _customerManager.SearchByName("Juan");
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Juan"));
    }

    [Test]
    public void SearchByName_CustomerNotExists_ReturnsNull()
    {
        Customer result = _customerManager.SearchByName("NoExiste");
        
        Assert.That(result, Is.Null);
    }

    [Test]
    public void SearchByName_CaseInsensitive_ReturnsCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        Customer result = _customerManager.SearchByName("JUAN");
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Juan"));
    }

    [Test]
    public void SearchByMail_CustomerExists_ReturnsCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        Customer result = _customerManager.SearchByMail("juan@mail.com");
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Mail, Is.EqualTo("juan@mail.com"));
    }

    [Test]
    public void SearchByMail_CaseInsensitive_ReturnsCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        Customer result = _customerManager.SearchByMail("JUAN@MAIL.COM");
        
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void SearchByFamilyName_CustomerExists_ReturnsCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        Customer result = _customerManager.SearchByFamilyName("Pérez");
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FamilyName, Is.EqualTo("Pérez"));
    }

    [Test]
    public void SearchByPhone_CustomerExists_ReturnsCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        Customer result = _customerManager.SearchByPhone("099123456");
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Phone, Is.EqualTo("099123456"));
    }

    [Test]
    public void SearchById_CustomerExists_ReturnsCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        Customer result = _customerManager.SearchById("1");
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo("1"));
    }

    [Test]
    public void SearchById_CustomerNotExists_ReturnsNull()
    {
        Customer result = _customerManager.SearchById("999");
        
        Assert.That(result, Is.Null);
    }

    [Test]
    public void AddCustomer_ValidCustomer_AddsSuccessfully()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        Customer result = _customerManager.SearchById("1");
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(_testCustomer.Id));
    }

    [Test]
    public void AddCustomer_DuplicateId_DoesNotAdd()
    {
        _customerManager.AddCustomer(_testCustomer);
        Customer duplicate = new Customer("1", "Pedro", "García", "pedro@mail.com", "099999999", "male",  new DateTime(1995, 10, 05));
        
        _customerManager.AddCustomer(duplicate);
        
        Customer result = _customerManager.SearchByName("Pedro");
        Assert.That(result, Is.Null);
    }

    [Test]
    public void AddCustomer_NullCustomer_DoesNotAdd()
    {
        _customerManager.AddCustomer(null);
        
        Customer result = _customerManager.SearchById("1");
        
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Delete_ExistingCustomer_RemovesCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        bool result = _customerManager.Delete(_testCustomer);
        
        Assert.That(result, Is.True);
        Assert.That(_customerManager.SearchById("1"), Is.Null);
    }

    [Test]
    public void Delete_NonExistingCustomer_ReturnsFalse()
    {
        bool result = _customerManager.Delete(_testCustomer);
        
        Assert.That(result, Is.False);
    }

    [Test]
    public void Delete_NullCustomer_ReturnsFalse()
    {
        bool result = _customerManager.Delete(null);
        
        Assert.That(result, Is.False);
    }

    [Test]
    public void Modify_ExistingCustomer_UpdatesCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        Customer modified = new Customer("1", "Juan Carlos", "Pérez", "nuevoemail@mail.com", "099123456", "male", new DateTime(1985,05,01)) ;
        
        _customerManager.Modify(modified);
        
        Customer result = _customerManager.SearchById("1");
        Assert.That(result.Name, Is.EqualTo("Juan Carlos"));
        Assert.That(result.Mail, Is.EqualTo("nuevoemail@mail.com"));
    }

    [Test]
    public void Modify_NonExistingCustomer_DoesNotModify()
    {
        Customer nonExisting = new Customer("999", "NoExiste", "Test", "test@mail.com", "099999999",  "-", new DateTime(1995, 10, 05));
        
        _customerManager.Modify(nonExisting);
        
        Assert.That(_customerManager.SearchById("999"), Is.Null);
    }

    [Test]
    public void GetDashboard_WithCompleteData_ReturnsValidSummary()
    {
        // Arrange - Agregar múltiples clientes
        _customerManager.AddCustomer(_testCustomer);
        Customer customer2 = new Customer("2", "María", "González", "maria@mail.com", "098765432", "female",  new DateTime(1995, 10, 05));
        _customerManager.AddCustomer(customer2);
        Customer customer3 = new Customer("3", "Pedro", "Rodríguez", "pedro@mail.com", "091234567", "male",  new DateTime(2000, 10, 05));
        _customerManager.AddCustomer(customer3);
    
        // Agregar interacciones recientes (últimos 7 días) - usando Interaction regular
        Interaction interaction1 = new Interaction(DateTime.Now.AddDays(-3), "Mensaje reciente", ExchangeType.Sent, _testCustomer);
        _customerManager.AddInteraction(interaction1, _testSeller, _testCustomer);
    
        // Agregar reuniones futuras - Meeting hereda de Interaction
        Meeting meeting1 = new Meeting("Oficina Central", DateTime.Now.AddDays(5), "Reunión futura 1", ExchangeType.Sent, customer2);
        Meeting meeting2 = new Meeting("Sala de conferencias", DateTime.Now.AddDays(10), "Reunión futura 2", ExchangeType.Sent, customer3);
        _customerManager.AddInteraction(meeting1, _testSeller, customer2);
        _customerManager.AddInteraction(meeting2, _testSeller, customer3);
    
        // Act
        DashboardSummary dashboard = _customerManager.GetDashboard();
    
        // Assert - Verificar estructura del dashboard
        Assert.That(dashboard, Is.Not.Null);
        Assert.That(dashboard.TotalCustomers, Is.EqualTo(3));
        Assert.That(dashboard.RecentInteractions, Is.Not.Null);
        Assert.That(dashboard.UpcomingMeetings, Is.Not.Null);
    
        // Verificar que las reuniones futuras se detectaron correctamente
        Assert.That(dashboard.UpcomingMeetings.Count, Is.EqualTo(2));
        Assert.That(dashboard.UpcomingMeetings, Does.Contain(meeting1));
        Assert.That(dashboard.UpcomingMeetings, Does.Contain(meeting2));
    }

    [Test]
public void GetInactiveCustomers_WithOldInteraction_ReturnsInactiveCustomer()
{
    // Arrange - Crear cliente con interacción antigua (más de 30 días)
    Customer customer = new Customer("10", "Inactivo", "Test", "inactivo@mail.com", "099999999", "M", DateTime.Now.AddYears(-30));
    _customerManager.AddCustomer(customer);
    
    // Agregar una interacción antigua (hace 35 días)
    Interaction oldInteraction = new Interaction(DateTime.Now.AddDays(-35), "Interacción antigua", ExchangeType.Sent, customer);
    _customerManager.AddInteraction(oldInteraction, _testSeller, customer);
    
    // Act - Buscar clientes inactivos (sin interacción en últimos 30 días)
    List<Customer> inactiveCustomers = _customerManager.GetInactiveCustomers(30);
    
    // Assert
    Assert.That(inactiveCustomers, Is.Not.Null);
    Assert.That(inactiveCustomers, Does.Contain(customer));
    Assert.That(customer.CheckIsActive(), Is.False); // Debe estar desactivado
}

[Test]
public void GetInactiveCustomers_WithRecentInteraction_DoesNotReturnCustomer()
{
    // Arrange - Crear cliente con interacción reciente
    Customer activeCustomer = new Customer("11", "Activo", "Test", "activo@mail.com", "098888888", "F", DateTime.Now.AddYears(-25));
    _customerManager.AddCustomer(activeCustomer);
    
    // Agregar una interacción reciente (hace 5 días)
    Interaction recentInteraction = new Interaction(DateTime.Now.AddDays(-5), "Interacción reciente", ExchangeType.Sent, activeCustomer);
    _customerManager.AddInteraction(recentInteraction, _testSeller, activeCustomer);
    
    // Act - Buscar clientes inactivos (sin interacción en últimos 30 días)
    List<Customer> inactiveCustomers = _customerManager.GetInactiveCustomers(30);
    
    // Assert
    Assert.That(inactiveCustomers, Is.Not.Null);
    Assert.That(inactiveCustomers, Does.Not.Contain(activeCustomer));
    Assert.That(activeCustomer.CheckIsActive(), Is.True); // Debe seguir activo
}

[Test]
public void GetInactiveCustomers_AlreadyDeactivated_ReturnsInList()
{
    // Arrange - Crear cliente ya desactivado
    Customer deactivatedCustomer = new Customer("12", "Desactivado", "Previo", "desact@mail.com", "097777777", "M", DateTime.Now.AddYears(-40));
    _customerManager.AddCustomer(deactivatedCustomer);
    deactivatedCustomer.Deactivate();
    
    // Act
    List<Customer> inactiveCustomers = _customerManager.GetInactiveCustomers(30);
    
    // Assert
    Assert.That(inactiveCustomers, Does.Contain(deactivatedCustomer));
}

    [Test]
    public void GetUnansweredCustomers_ReturnsCustomersWithUnansweredInteractions()
    {
        _customerManager.AddCustomer(_testCustomer);
        Interaction interaction = new Interaction(DateTime.Now,"Compra",ExchangeType.Received, _testCustomer);
        _customerManager.AddInteraction(interaction, _testSeller, _testCustomer);
        
        List<Customer> unanswered = _customerManager.GetUnansweredCustomers(7);
        
        Assert.That(unanswered, Is.Not.Null);
    }

    [Test]
    public void AssignCustomerToSeller_ValidCustomerAndSeller_AssignsSuccessfully()
    {
        Customer customer = new Customer("2", "María", "González", "maria@mail.com", "098765432", "female",  new DateTime(1995, 12, 31));
        Seller seller = new Seller("Ana", "ana@gmail.com", "036025014","S2" );
        
        _customerManager.AssignCustomerToSeller(customer, seller);
        
        Assert.That(seller.Customer, Does.Contain(customer));
    }

    [Test]
    public void AssignCustomerToSeller_NullCustomer_DoesNotAssign()
    {
        Seller seller = new Seller("Lucia", "lucia@gmail.com", "036025014","S3" );
        int initialCount = seller.Customer.Count;
        
        _customerManager.AssignCustomerToSeller(null, seller);
        
        Assert.That(seller.Customer.Count, Is.EqualTo(initialCount));
    }

    [Test]
    public void AddInteraction_ValidInteraction_AddsToCustomer()
    {
        _customerManager.AddCustomer(_testCustomer);
        Interaction interaction = new Interaction(DateTime.Now,"Compra",ExchangeType.Received, _testCustomer);
        
        _customerManager.AddInteraction(interaction, _testSeller, _testCustomer);
        
        List<Interaction> interactions = _customerManager.GetCustomerInteractions(_testCustomer);
        Assert.That(interactions, Does.Contain(interaction));
    }

    [Test]
    public void AddInteraction_NullInteraction_DoesNotAdd()
    {
        _customerManager.AddCustomer(_testCustomer);
        int initialCount = _testCustomer.Interactions.Count;
        
        _customerManager.AddInteraction(null, _testSeller, _testCustomer);
        
        Assert.That(_testCustomer.Interactions.Count, Is.EqualTo(initialCount));
    }

    [Test]
    public void GetCustomerInteractions_ReturnsCustomerInteractionsList()
    {
        _customerManager.AddCustomer(_testCustomer);
        
        List<Interaction> interactions = _customerManager.GetCustomerInteractions(_testCustomer);
        
        Assert.That(interactions, Is.Not.Null);
    }
}