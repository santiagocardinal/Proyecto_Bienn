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
        _testCustomer = new Customer("1", "Juan", "Pérez", "juan@mail.com", "099123456", "male", new DateTime(2000, 10, 05));
        _testSeller = new Seller("Carlos", "carlos@mail.com", "099111222", "S1");
    }

    
    [Test]
    public void SearchByName_CustomerExists_ReturnsCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        Customer result = _customerManager.SearchByName("Juan");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Juan"));
    }

    [Test]
    public void SearchByName_CustomerNotExists_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.SearchByName("NoExiste"));
    }

    [Test]
    public void SearchByName_CaseInsensitive_ReturnsCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        Customer result = _customerManager.SearchByName("JUAN");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Juan"));
    }

    
    [Test]
    public void SearchByMail_CustomerExists_ReturnsCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        Customer result = _customerManager.SearchByMail("juan@mail.com");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Mail, Is.EqualTo("juan@mail.com"));
    }

    [Test]
    public void SearchByMail_CustomerNotExists_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.SearchByMail("noexiste@mail.com"));
    }

    [Test]
    public void SearchByMail_CaseInsensitive_ReturnsCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        Customer result = _customerManager.SearchByMail("JUAN@MAIL.COM");
        
        // Assert
        Assert.That(result, Is.Not.Null);
    }

    
    [Test]
    public void SearchByFamilyName_CustomerExists_ReturnsCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        Customer result = _customerManager.SearchByFamilyName("Pérez");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FamilyName, Is.EqualTo("Pérez"));
    }

    [Test]
    public void SearchByFamilyName_CustomerNotExists_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.SearchByFamilyName("NoExiste"));
    }

    
    [Test]
    public void SearchByPhone_CustomerExists_ReturnsCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        Customer result = _customerManager.SearchByPhone("099123456");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Phone, Is.EqualTo("099123456"));
    }

    [Test]
    public void SearchByPhone_CustomerNotExists_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.SearchByPhone("099999999"));
    }

    
    [Test]
    public void SearchById_CustomerExists_ReturnsCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        Customer result = _customerManager.SearchById("1");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo("1"));
    }

    [Test]
    public void SearchById_CustomerNotExists_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.SearchById("999"));
    }

    [Test]
    public void SearchById_EmptyId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _customerManager.SearchById(""));
    }

    [Test]
    public void SearchById_NullId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _customerManager.SearchById(null));
    }

    
    [Test]
    public void AddCustomer_ValidCustomer_AddsSuccessfully()
    {
        // Act
        _customerManager.AddCustomer(_testCustomer);
        
        // Assert
        Customer result = _customerManager.SearchById("1");
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(_testCustomer.Id));
    }

    [Test]
    public void AddCustomer_DuplicateId_ThrowsException()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        Customer duplicate = new Customer("1", "Pedro", "García", "pedro@mail.com", "099999999", "male", new DateTime(1995, 10, 05));
        
        // Act & Assert
        Assert.Throws<DuplicatedCustomerException>(() => 
            _customerManager.AddCustomer(duplicate));
    }

    [Test]
    public void AddCustomer_NullCustomer_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            _customerManager.AddCustomer(null));
    }

    
    [Test]
    public void Delete_ExistingCustomer_RemovesCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        _customerManager.Delete(_testCustomer);
        
        // Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.SearchById("1"));
    }

    [Test]
    public void Delete_NonExistingCustomer_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.Delete(_testCustomer));
    }

    [Test]
    public void Delete_NullCustomer_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.Delete(null));
    }

    
    [Test]
    public void Modify_ExistingCustomer_Name_UpdatesSuccessfully()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        _customerManager.Modify("1", "name", "Juan Carlos");
        
        // Assert
        Customer result = _customerManager.SearchById("1");
        Assert.That(result.Name, Is.EqualTo("Juan Carlos"));
    }

    [Test]
    public void Modify_ExistingCustomer_FamilyName_UpdatesSuccessfully()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        _customerManager.Modify("1", "familyname", "González");
        
        // Assert
        Customer result = _customerManager.SearchById("1");
        Assert.That(result.FamilyName, Is.EqualTo("González"));
    }

    [Test]
    public void Modify_ExistingCustomer_Mail_UpdatesSuccessfully()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        _customerManager.Modify("1", "mail", "nuevoemail@mail.com");
        
        // Assert
        Customer result = _customerManager.SearchById("1");
        Assert.That(result.Mail, Is.EqualTo("nuevoemail@mail.com"));
    }

    [Test]
    public void Modify_ExistingCustomer_Phone_UpdatesSuccessfully()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        _customerManager.Modify("1", "phone", "098765432");
        
        // Assert
        Customer result = _customerManager.SearchById("1");
        Assert.That(result.Phone, Is.EqualTo("098765432"));
    }

    [Test]
    public void Modify_ExistingCustomer_Gender_UpdatesSuccessfully()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        _customerManager.Modify("1", "gender", "female");
        
        // Assert
        Customer result = _customerManager.SearchById("1");
        Assert.That(result.Gender, Is.EqualTo("female"));
    }

    [Test]
    public void Modify_ExistingCustomer_BirthDate_UpdatesSuccessfully()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        DateTime newDate = new DateTime(1995, 5, 15);
        
        // Act
        _customerManager.Modify("1", "birthdate", "1995-05-15");
        
        // Assert
        Customer result = _customerManager.SearchById("1");
        Assert.That(result.BirthDate.Date, Is.EqualTo(newDate.Date));
    }

    [Test]
    public void Modify_NonExistingCustomer_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.Modify("999", "name", "Test"));
    }

    [Test]
    public void Modify_InvalidField_ThrowsException()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act & Assert
        Assert.Throws<InvalidFieldException>(() => 
            _customerManager.Modify("1", "campoInvalido", "valor"));
    }

    
    [Test]
    public void GetDashboard_WithCompleteData_ReturnsValidSummary()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        Customer customer2 = new Customer("2", "María", "González", "maria@mail.com", "098765432", "female", new DateTime(1995, 10, 05));
        _customerManager.AddCustomer(customer2);
        Customer customer3 = new Customer("3", "Pedro", "Rodríguez", "pedro@mail.com", "091234567", "male", new DateTime(2000, 10, 05));
        _customerManager.AddCustomer(customer3);
    
        InteractionRegular interaction1 = new InteractionRegular(DateTime.Now.AddDays(-3), "Mensaje reciente", ExchangeType.Sent, _testCustomer);
        _customerManager.RegisterInteraction(_testCustomer, _testSeller, interaction1);
    
        Meeting meeting1 = new Meeting("Oficina Central", DateTime.Now.AddDays(5), "Reunión futura 1", ExchangeType.Sent, customer2);
        Meeting meeting2 = new Meeting("Sala de conferencias", DateTime.Now.AddDays(10), "Reunión futura 2", ExchangeType.Sent, customer3);
        _customerManager.RegisterInteraction(customer2, _testSeller, meeting1);
        _customerManager.RegisterInteraction(customer3, _testSeller, meeting2);
    
        // Act
        DashboardSummary dashboard = _customerManager.GetDashboard();
    
        // Assert
        Assert.That(dashboard, Is.Not.Null);
        Assert.That(dashboard.TotalCustomers, Is.EqualTo(3));
        Assert.That(dashboard.RecentInteractions, Is.Not.Null);
        Assert.That(dashboard.UpcomingMeetings, Is.Not.Null);
        Assert.That(dashboard.UpcomingMeetings.Count, Is.EqualTo(2));
        Assert.That(dashboard.UpcomingMeetings, Does.Contain(meeting1));
        Assert.That(dashboard.UpcomingMeetings, Does.Contain(meeting2));
    }

    [Test]
    public void GetDashboard_NoCustomers_ReturnsEmptyDashboard()
    {
        // Act
        DashboardSummary dashboard = _customerManager.GetDashboard();
        
        // Assert
        Assert.That(dashboard, Is.Not.Null);
        Assert.That(dashboard.TotalCustomers, Is.EqualTo(0));
        Assert.That(dashboard.RecentInteractions, Is.Not.Null);
        Assert.That(dashboard.RecentInteractions.Count, Is.EqualTo(0));
        Assert.That(dashboard.UpcomingMeetings, Is.Not.Null);
        Assert.That(dashboard.UpcomingMeetings.Count, Is.EqualTo(0));
    }

    
    [Test]
    public void GetInactiveCustomers_WithOldInteraction_ReturnsInactiveCustomer()
    {
        // Arrange
        Customer customer = new Customer("10", "Inactivo", "Test", "inactivo@mail.com", "099999999", "M", DateTime.Now.AddYears(-30));
        _customerManager.AddCustomer(customer);
        
        InteractionRegular oldInteraction = new InteractionRegular(DateTime.Now.AddDays(-35), "Interacción antigua", ExchangeType.Sent, customer);
        _customerManager.RegisterInteraction(customer, _testSeller, oldInteraction);
        
        // Act
        List<Customer> inactiveCustomers = _customerManager.GetInactiveCustomers(30);
        
        // Assert
        Assert.That(inactiveCustomers, Is.Not.Null);
        Assert.That(inactiveCustomers, Does.Contain(customer));
        Assert.That(customer.CheckIsActive(), Is.False);
    }

    [Test]
    public void GetInactiveCustomers_WithRecentInteraction_DoesNotReturnCustomer()
    {
        // Arrange
        Customer activeCustomer = new Customer("11", "Activo", "Test", "activo@mail.com", "098888888", "F", DateTime.Now.AddYears(-25));
        _customerManager.AddCustomer(activeCustomer);
        InteractionRegular recentInteraction = new InteractionRegular(DateTime.Now.AddDays(-5), "Interacción reciente", ExchangeType.Sent, activeCustomer);
        _customerManager.RegisterInteraction(activeCustomer, _testSeller, recentInteraction);
        
        // Act
        List<Customer> inactiveCustomers = _customerManager.GetInactiveCustomers(30);
        
        // Assert
        Assert.That(inactiveCustomers, Is.Not.Null);
        Assert.That(inactiveCustomers, Does.Not.Contain(activeCustomer));
        Assert.That(activeCustomer.CheckIsActive(), Is.True);
    }

    [Test]
    public void GetInactiveCustomers_AlreadyDeactivated_ReturnsInList()
    {
        // Arrange
        Customer deactivatedCustomer = new Customer("12", "Desactivado", "Previo", "desact@mail.com", "097777777", "M", DateTime.Now.AddYears(-40));
        _customerManager.AddCustomer(deactivatedCustomer);
        deactivatedCustomer.Deactivate();
        
        // Act
        List<Customer> inactiveCustomers = _customerManager.GetInactiveCustomers(30);
        
        // Assert
        Assert.That(inactiveCustomers, Does.Contain(deactivatedCustomer));
    }

    [Test]
    public void GetInactiveCustomers_NoCustomers_ReturnsEmptyList()
    {
        // Act
        List<Customer> inactiveCustomers = _customerManager.GetInactiveCustomers(30);
        
        // Assert
        Assert.That(inactiveCustomers, Is.Not.Null);
        Assert.That(inactiveCustomers.Count, Is.EqualTo(0));
    }

    
    [Test]
    public void GetUnansweredCustomers_WithUnansweredInteraction_ReturnsCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        InteractionRegular interaction = new InteractionRegular(DateTime.Now, "Compra", ExchangeType.Received, _testCustomer);
        _customerManager.RegisterInteraction(_testCustomer, _testSeller, interaction);
        
        // Act
        List<Customer> unanswered = _customerManager.GetUnansweredCustomers(7);
        
        // Assert
        Assert.That(unanswered, Is.Not.Null);
        Assert.That(unanswered, Does.Contain(_testCustomer));
    }

    [Test]
    public void GetUnansweredCustomers_WithAnsweredInteraction_DoesNotReturnCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        InteractionRegular interaction = new InteractionRegular(DateTime.Now, "Compra", ExchangeType.Received, _testCustomer);
        interaction.MarkAsResponded();
        _customerManager.RegisterInteraction(_testCustomer, _testSeller, interaction);
        
        // Act
        List<Customer> unanswered = _customerManager.GetUnansweredCustomers(7);
        
        // Assert
        Assert.That(unanswered, Is.Not.Null);
        Assert.That(unanswered, Does.Not.Contain(_testCustomer));
    }

    [Test]
    public void GetUnansweredCustomers_NoCustomers_ReturnsEmptyList()
    {
        // Act
        List<Customer> unanswered = _customerManager.GetUnansweredCustomers(7);
        
        // Assert
        Assert.That(unanswered, Is.Not.Null);
        Assert.That(unanswered.Count, Is.EqualTo(0));
    }

    
    [Test]
    public void AssignCustomerToSeller_ValidCustomerAndSeller_AssignsSuccessfully()
    {
        // Arrange
        Customer customer = new Customer("2", "María", "González", "maria@mail.com", "098765432", "female", new DateTime(1995, 12, 31));
        Seller seller = new Seller("Ana", "ana@gmail.com", "036025014", "S2");
        
        // Act
        _customerManager.AssignCustomerToSeller(customer, seller);
        
        // Assert
        Assert.That(seller.Customer, Does.Contain(customer));
    }

    [Test]
    public void AssignCustomerToSeller_NullCustomer_ThrowsException()
    {
        // Arrange
        Seller seller = new Seller("Lucia", "lucia@gmail.com", "036025014", "S3");
        
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.AssignCustomerToSeller(null, seller));
    }

    [Test]
    public void AssignCustomerToSeller_NullSeller_ThrowsException()
    {
        // Arrange
        Customer customer = new Customer("2", "María", "González", "maria@mail.com", "098765432", "female", new DateTime(1995, 12, 31));
        
        // Act & Assert
        Assert.Throws<Exceptions.SellerNotFoundException>(() => 
            _customerManager.AssignCustomerToSeller(customer, null));
    }

    
    [Test]
    public void RegisterInteraction_ValidInteraction_AddsToCustomer()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        InteractionRegular interaction = new InteractionRegular(DateTime.Now, "Compra", ExchangeType.Received, _testCustomer);
        
        // Act
        _customerManager.RegisterInteraction(_testCustomer, _testSeller, interaction);
        
        // Assert
        List<Interaction> interactions = _customerManager.GetCustomerInteractions(_testCustomer);
        Assert.That(interactions, Does.Contain(interaction));
    }

    [Test]
    public void RegisterInteraction_ReceivedType_MarksCustomerAsPending()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        InteractionRegular interaction = new InteractionRegular(DateTime.Now, "Consulta", ExchangeType.Received, _testCustomer);
        
        // Act
        _customerManager.RegisterInteraction(_testCustomer, _testSeller, interaction);
        
        // Assert
        List<Interaction> interactions = _customerManager.GetCustomerInteractions(_testCustomer);
        Assert.That(interactions, Does.Contain(interaction));
    }

    [Test]
    public void RegisterInteraction_SentType_MarksCustomerAsActive()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        InteractionRegular interaction = new InteractionRegular(DateTime.Now, "Respuesta", ExchangeType.Sent, _testCustomer);
        
        // Act
        _customerManager.RegisterInteraction(_testCustomer, _testSeller, interaction);
        
        // Assert
        List<Interaction> interactions = _customerManager.GetCustomerInteractions(_testCustomer);
        Assert.That(interactions, Does.Contain(interaction));
        Assert.That(_testCustomer.CheckIsActive(), Is.True);
    }

    [Test]
    public void RegisterInteraction_NullCustomer_ThrowsException()
    {
        // Arrange
        InteractionRegular interaction = new InteractionRegular(DateTime.Now, "Compra", ExchangeType.Received, _testCustomer);
        
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.RegisterInteraction(null, _testSeller, interaction));
    }

    [Test]
    public void RegisterInteraction_NullSeller_ThrowsException()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        InteractionRegular interaction = new InteractionRegular(DateTime.Now, "Compra", ExchangeType.Received, _testCustomer);
        
        // Act & Assert
        Assert.Throws<Exceptions.SellerNotFoundException>(() => 
            _customerManager.RegisterInteraction(_testCustomer, null, interaction));
    }

    [Test]
    public void RegisterInteraction_NullInteraction_ThrowsArgumentNullException()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            _customerManager.RegisterInteraction(_testCustomer, _testSeller, null));
    }

    [Test]
    public void GetCustomerInteractions_ExistingCustomer_ReturnsInteractionsList()
    {
        // Arrange
        _customerManager.AddCustomer(_testCustomer);
        
        // Act
        List<Interaction> interactions = _customerManager.GetCustomerInteractions(_testCustomer);
        
        // Assert
        Assert.That(interactions, Is.Not.Null);
    }

    [Test]
    public void GetCustomerInteractions_NullCustomer_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NotExistingCustomerException>(() => 
            _customerManager.GetCustomerInteractions(null));
    }
}