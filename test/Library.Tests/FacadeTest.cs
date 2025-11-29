namespace Library.Tests;

public class FacadeTests
{
   private Customer _testCustomer;
   private Seller _testSeller;

    [SetUp]
    public void Setup()
    {
        _testCustomer = new Customer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        _testSeller = new Seller("Carlos", "carlos@mail.com", "099111222", "S1");
    }

    [TearDown]
    public void TearDown()
    {
        // Limpia todos los datos creados en los tests
        Facade.cm.Customers.Clear();
        Facade.sm.Sellers.Clear();
    }

    // ============= CREACIÓN DE CLIENTE =============
    
    [Test]
    public void CreateCustomer_ReturnsSuccessMessage()
    {
        // Act
        string result = Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Assert
        Assert.That(result, Is.EqualTo("Cliente creado correctamente."));
    }

    [Test]
    public void CreateCustomer_DuplicateId_ReturnsErrorMessage()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Act
        string result = Facade.CreateCustomer("C1", "Pedro", "García", "pedro@mail.com", "099999999", "M", DateTime.Now.AddYears(-25));
        
        // Assert
        Assert.That(result, Does.Contain("Ya existe un cliente"));
    }

    // ============= MODIFICACIÓN DE CLIENTE =============
    
    [Test]
    public void ModifyCustomer_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Act
        string result = Facade.ModifyCustomer("C1", "name", "Juan Carlos");
        
        // Assert
        Assert.That(result, Is.EqualTo("Cliente modificado correctamente."));
    }
    
    [Test]
    public void NonExistingCustomer_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.ModifyCustomer("C999", "name", "Test");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    // ============= BÚSQUEDAS =============
    
    [Test]
    public void SearchCustomer_ByName_ReturnsName()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Act
        string result = Facade.SearchCostumer_ByName("Juan");
        
        // Assert
        Assert.That(result, Is.EqualTo("Juan"));
    }

    [Test]
    public void SearchCustomer_ByName_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.SearchCostumer_ByName("No existe");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    [Test]
    public void SearchCustomer_ByFamilyName_ReturnsFamilyName()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Act
        string result = Facade.SearchCostumer_ByFamilyName("Pérez");
        
        // Assert
        Assert.That(result, Is.EqualTo("Pérez"));
    }

    [Test]
    public void SearchCustomer_ByPhone_ReturnsPhone()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Act
        string result = Facade.SearchCostumer_ByPhone("099123456");
        
        // Assert
        Assert.That(result, Is.EqualTo("099123456"));
    }

    [Test]
    public void SearchCustomer_ByMail_ReturnsMail()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Act
        string result = Facade.SearchCostumer_ByMail("juan@mail.com");
        
        // Assert
        Assert.That(result, Is.EqualTo("juan@mail.com"));
    }

    // ============= MOSTRAR CLIENTES POR VENDEDOR =============
    
    [Test]
    public void ShowCustomers_BySellerId_ReturnsCustomerList()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.AssignCustomer("C1", "S1");
        
        // Act
        string result = Facade.ShowCustomers_BySellerId("S1");
        
        // Assert
        Assert.That(result, Does.Contain("Juan"));
    }

    [Test]
    public void ShowCustomers_BySellerId_ReturnsMessage()
    {
        // Arrange
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.ShowCustomers_BySellerId("S1");
        
        // Assert
        Assert.That(result, Is.EqualTo("El vendedor no tiene clientes asignados."));
    }

    [Test]
    public void ShowCustomers_BySellerId_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.ShowCustomers_BySellerId("S999");
        
        // Assert
        Assert.That(result, Does.Contain("No se encontró el vendedor"));
    }

    // ============= AGREGAR CUSTOMER =============
    
    [Test]
    public void AddCustomer_ValidCustomer_ReturnsSuccessMessage()
    {
        // Act
        string result = Facade.AddCustomer(_testCustomer);
        
        // Assert
        Assert.That(result, Is.EqualTo("Cliente agregado correctamente."));
    }

    [Test]
    public void AddCustomer_DuplicateCustomer_ReturnsErrorMessage()
    {
        // Arrange
        Facade.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.AddCustomer(_testCustomer);
        
        // Assert
        Assert.That(result, Does.Contain("Ya existe un cliente"));
    }

    // ============= AGREGAR TAG A CLIENTE =============
    
    [Test]
    public void AddTagCustomer_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.AddTag_Customer("C1", "T1");
        
        // Assert
        Assert.That(result, Does.Contain("agregada correctamente"));
    }

    [Test]
    public void AddTagCustomer_ReturnsErrorMessage1()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.AddTag_Customer("C1", "T1");
        
        // Act
        string result = Facade.AddTag_Customer("C1", "T2");
        
        // Assert
        Assert.That(result, Does.Contain("ya tiene una etiqueta"));
    }

    [Test]
    public void AddTagCustomer_ReturnsErrorMessage2()
    {
        // Act
        string result = Facade.AddTag_Customer("C999", "T1");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    // ============= ÚLTIMA INTERACCIÓN =============
    
    [Test]
    public void LastInteraction_ReturnsLastInteraction()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.CallRegister(DateTime.Now, "Consulta", ExchangeType.Received, "C1", "S1");
        
        // Act
        string result = Facade.LastInteraction("C1");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
    }

    [Test]
    public void LastInteraction_ReturnsMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.LastInteraction("C1");
        
        // Assert
        Assert.That(result, Is.EqualTo("No hay interacciones registradas."));
    }

    [Test]
    public void LastInteraction_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.LastInteraction("C999");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    // ============= INTERACCIONES SIN RESPUESTA =============
    
    [Test]
    public void UnansweredInteractions_ReturnsReport()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.CallRegister(DateTime.Now, "Consulta", ExchangeType.Received, "C1", "S1");
        
        // Act
        string result = Facade.UnansweredInteractions("C1");
        
        // Assert
        Assert.That(result, Does.Contain("Interacciones sin responder"));
    }

    [Test]
    public void UnansweredInteractions_ReturnsMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.UnansweredInteractions("C1");
        
        // Assert
        Assert.That(result, Is.EqualTo("No hay interacciones sin responder."));
    }

    [Test]
    public void UnansweredInteractions_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.UnansweredInteractions("C999");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    // ============= ASIGNAR CLIENTE A VENDEDOR =============
    
    [Test]
    public void AssignCustomer_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.AssignCustomer("C1", "S1");
        
        // Assert
        Assert.That(result, Is.EqualTo("Cliente asignado correctamente."));
    }

    [Test]
    public void AssignCustomer_ReturnsErrorMessage()
    {
        // Arrange
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.AssignCustomer("C999", "S1");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    [Test]
    public void AssignCustomer_SellerNotFound_ReturnsErrorMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.AssignCustomer("C1", "S999");
        
        // Assert
        Assert.That(result, Does.Contain("No se encontró el vendedor"));
    }

    // ============= REGISTRO DE INTERACCIONES =============
    
    [Test]
    public void CallRegister_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.CallRegister(DateTime.Now, "Consulta", ExchangeType.Received, "C1", "S1");
        
        // Assert
        Assert.That(result, Is.EqualTo("Interacción registrada correctamente."));
    }

    [Test]
    public void CallRegister_ReturnsErrorMessage()
    {
        // Arrange
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.CallRegister(DateTime.Now, "Consulta", ExchangeType.Received, "C999", "S1");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    [Test]
    public void MeetingRegister_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.MeetingRegister("Oficina", DateTime.Now.AddDays(5), "Reunión", ExchangeType.Sent, "C1", "S1");
        
        // Assert
        Assert.That(result, Is.EqualTo("Interacción registrada correctamente."));
    }

    [Test]
    public void MessageRegister_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.MessageRegister(DateTime.Now, "Mensaje", ExchangeType.Sent, "C1", "S1");
        
        // Assert
        Assert.That(result, Is.EqualTo("Interacción registrada correctamente."));
    }

    [Test]
    public void MailRegister_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.MailRegister(DateTime.Now, "Email", ExchangeType.Received, "C1", "S1");
        
        // Assert
        Assert.That(result, Is.EqualTo("Interacción registrada correctamente."));
    }

    [Test]
    public void QuoteRegister_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.QuoteRegister(DateTime.Now, "Cotización", ExchangeType.Sent, 1000.0, "Producto X", "C1", "S1");
        
        // Assert
        Assert.That(result, Is.EqualTo("Interacción registrada correctamente."));
    }

    // ============= SALE FROM QUOTE =============
    
    [Test]
    public void SaleFromQuote_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        DateTime quoteDate = DateTime.Now;
        Facade.QuoteRegister(quoteDate, "Cotización", ExchangeType.Sent, 1000.0, "Producto X", "C1", "S1");
        
        // Act
        string result = Facade.SaleFromQuote("S1", "C1", quoteDate, "Cotización", ExchangeType.Sent, 1000.0, "Producto X");
        
        // Assert
        Assert.That(result, Is.EqualTo("Interacción registrada correctamente."));
    }

    [Test]
    public void SaleFromQuote_ReturnsErrorMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.SaleFromQuote("S1", "C1", DateTime.Now, "Cotización", ExchangeType.Sent, 1000.0, "Producto X");
        
        // Assert
        Assert.That(result, Does.Contain("No se encontró una Quote"));
    }
    

    [Test]
    public void SaleFromQuote_SellerNotFound_ReturnsErrorMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.SaleFromQuote("S999", "C1", DateTime.Now, "Cotización", ExchangeType.Sent, 1000.0, "Producto X");
        
        // Assert
        Assert.That(result, Does.Contain("No se encontró el vendedor"));
    }

    // ============= GET CUSTOMER INTERACTIONS =============
    
    [Test]
    public void GetCustomerInteractions_ReturnsInteractionsList()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.CallRegister(DateTime.Now, "Consulta", ExchangeType.Received, "C1", "S1");
        var facadeInstance = new Facade();
        
        // Act
        List<Interaction> result = facadeInstance.GetCustomerInteractions("C1");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.GreaterThan(0));
    }

    [Test]
    public void GetCustomerInteractions_ReturnsEmptyList()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        var facadeInstance = new Facade();
        
        // Act
        List<Interaction> result = facadeInstance.GetCustomerInteractions("C1");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetCustomerInteractions_CustomerNotFound_ReturnsEmptyList()
    {
        // Arrange
        var facadeInstance = new Facade();
        
        // Act
        List<Interaction> result = facadeInstance.GetCustomerInteractions("C999");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetCustomerInteractions_ReturnsFilteredList()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        DateTime today = DateTime.Now;
        Facade.CallRegister(today, "Consulta hoy", ExchangeType.Received, "C1", "S1");
        Facade.CallRegister(today.AddDays(-5), "Consulta hace 5 días", ExchangeType.Received, "C1", "S1");
        var facadeInstance = new Facade();
        
        // Act
        List<Interaction> result = facadeInstance.GetCustomerInteractions("C1", null, today);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Topic, Does.Contain("hoy"));
    }
}